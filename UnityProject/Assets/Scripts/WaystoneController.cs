using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaystoneController : MonoBehaviour, IInteractant
{

    public BoxCollider2D triggerCollider;

    public SpriteRenderer left;
    public SpriteRenderer right;

    public GameObject submitButton;

    public bool InRange { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        WaystoneUI.instance.landscapeSlot = left;
        WaystoneUI.instance.biomeSlot = right;

        //submitButton.GetComponent<InteractableUI>().OnClicked += delegate { Submit(); };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Submit()
    {
        if (GlyphManager.biome == null || GlyphManager.landscape == null)
        {
            Debug.Log("Invalid glyphs");
            return;
        }

        if (left.sprite == null || right.sprite == null)
            return;

        print("Submit! ChangeScene in WaystoneUI");
        LevelManager.ChangeScene();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // when player enters waystone activation radius
            GameManager.instance.setIsUIopen(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // when player exits waystone activation radius
            GameManager.instance.setIsUIopen(false);
        }
    }

    private void OnDestroy()
    {
        
    }

    public void Interact()
    {
        Submit();
    }
}
