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
        context.SpawnGameObject(prefab, position, scale);

        CallBack();
    }

    public override bool ShouldExecute(SceneContext context)
    {
        if (GlyphManager.landscape != landscape || GlyphManager.biome != biome || GlyphManager.time != time)
            return false;

        return true;
    }
}