using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceManager : MonoBehaviour
{
    public static PlaceManager instance;
    public static Places _places; // Init in init scene



    [HideInInspector] public GlyphLandscape loadedLandscape;
    [HideInInspector] public GlyphBiome loadedBiome;
    [HideInInspector] public GlyphTime loadedTime;

    [Header("Setup glyph axis to load/save.")]
    //public GlyphLandscape landscape;
    public GlyphBiome biome;
    public GlyphTime time;

    [Header("References")]
    public LevelManager levelManager;

    [Header("External References")]
    public GlyphCollection collection;
    public Places places;

    [Header("Debug")]
    public GameObject currentPlace;



    private void Awake()
    {
        instance = this;
    }

    public void LoadPlace()
    {
        if (currentPlace != null)
            throw new System.Exception("ERROR: A place is already loaded. Pleace click 'Cleanup Scene' button before loading.");

        GlyphLandscape landscape = levelManager.landscapeGlyph;

        if (landscape == null || biome == null || time == null)
        {
            throw new System.Exception("ERROR: Cannot load place when some of the glyphs are not specified.");
        }

        SetupLocalPlaces();

        int x = GlyphManager.GetIndex(levelManager.landscapeGlyph);
        int y = GlyphManager.GetIndex(biome);
        int z = GlyphManager.GetIndex(time);

        loadedLandscape = landscape;
        loadedBiome = biome;
        loadedTime = time;

        if (Application.isPlaying)
        {
            GameObject prefab = _places.LoadPlacePrefab(x, y, z);

            if (prefab == null)
            {
                Debug.Log("Prefab is null when loading!");
                currentPlace = InitializePlace();
                return;
            }

            if (currentPlace != null)
            {
                DestroyImmediate(currentPlace, false);
            }

            currentPlace = Instantiate<GameObject>(prefab);
            currentPlace.transform.position = new Vector3(0, 0, 0);
            currentPlace.transform.parent = transform;
        } else
        {
            // In Editor
            GameObject prefab = _places.LoadPlacePrefab(x, y, z);

            if (prefab == null)
            {
                Debug.Log("Prefab is null when loading!");
                currentPlace = InitializePlace();
                return;
            }

            if (currentPlace != null)
            {
                DestroyImmediate(currentPlace, false);
            }

            currentPlace = Instantiate<GameObject>(prefab);
            currentPlace.transform.position = new Vector3(0, 0, 0);
            currentPlace.transform.parent = transform;
        }
    }

    public void SavePlace()
    {
        GlyphLandscape landscape = levelManager.landscapeGlyph;

        if (landscape == null || biome == null || time == null)
        {
            throw new System.Exception("ERROR: Cannot save place when some of the glyphs are not specified.");
        }

        SetupLocalPlaces();

        int x = GlyphManager.GetIndex(landscape);
        int y = GlyphManager.GetIndex(biome);
        int z = GlyphManager.GetIndex(time);

        if (currentPlace == null)
        {
            currentPlace = InitializePlace();
        }

        currentPlace.name = $"Place of ({x},{y},{z})";
        _places.SavePlacePrefab(x, y, z, currentPlace);

        CleanupScene();
    }

    private GameObject InitializePlace()
    {
        GlyphLandscape landscape = levelManager.landscapeGlyph;

        int x = GlyphManager.GetIndex(landscape);
        int y = GlyphManager.GetIndex(biome);
        int z = GlyphManager.GetIndex(time);

        GameObject place = new GameObject($"Place of ({x},{y},{z})");
        place.transform.position = new Vector3(0, 0, 0);
        place.transform.parent = transform;
        return place;
    }

    private void SetupLocalPlaces()
    {
        if (_places == null)
        {
            if (places == null)
            {
                throw new System.Exception("The Places are not set! Setup places in PlaceManager in order to save/load.");
            }
            else
            {
                _places = places;
            }
        }
        if (places == null)
        {
            places = _places;
        }

        if (GlyphManager.collection == null)
        {
            GlyphManager.collection = collection;
        }
    }

    public void CleanupScene()
    {
        biome = null;
        time = null;

        loadedLandscape = null;
        loadedBiome = null;
        loadedTime = null;
        Destroy(currentPlace);
        currentPlace = null;
    }

    private void OnValidate()
    {
        if (currentPlace != null)
        {
            if (biome != loadedBiome)
                biome = loadedBiome;
            if (time != loadedTime)
                time = loadedTime;
        }
        
    }
}