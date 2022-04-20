using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveGameObjectEvent : QuestEvent
{
    public override QuestEventType questEventType => throw new System.NotImplementedException();

    public GlyphLandscape landscape;
    public GlyphBiome biome;
    public GlyphTime time;

    public GameObject prefab;

    public override void Execute(SceneContext context)
    {
        if (context.EventObjects.ContainsKey(prefab))
        {
            foreach(GameObject go in context.EventObjects[prefab])
            {
                context.EventObjects[prefab].Remove(go);
                Destroy(go);
            }
        }
    }

    public override bool ShouldExecute(SceneContext context)
    {
        if (!base.ShouldExecute(context))
            return false;

        if (GlyphManager.landscape != landscape || GlyphManager.biome != biome || GlyphManager.time != time)
            return false;

        return true;
    }
}