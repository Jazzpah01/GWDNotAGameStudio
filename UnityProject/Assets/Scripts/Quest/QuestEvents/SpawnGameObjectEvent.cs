using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGameObjectEvent : QuestEvent
{
    public override QuestEventType questEventType => QuestEventType.SpawnGameObject;

    public GlyphLandscape landscape;
    public GlyphBiome biome;
    public GlyphTime time;

    public GameObject prefab;
    public Vector2 position;
    public Vector2 scale = new Vector2(1,1);

    public override void Execute(SceneContext context)
    {
        GameObject ngo = Instantiate(prefab);
        ngo.transform.position = position;
        ngo.transform.localScale = scale;

        if (!context.EventObjects.ContainsKey(prefab))
            context.EventObjects.Add(prefab, new List<GameObject>());

        context.EventObjects[prefab].Add(ngo);

        CallBack();
    }

    public override bool ShouldExecute(SceneContext context)
    {
        if (GlyphManager.landscape != landscape || GlyphManager.biome != biome || GlyphManager.time != time)
            return false;

        return true;
    }
}