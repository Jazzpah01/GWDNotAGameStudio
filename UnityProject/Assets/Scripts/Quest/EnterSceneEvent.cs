using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterSceneEvent : QuestEvent
{
    public GlyphLandscape landscape;
    public GlyphBiome biome;
    public GlyphTime time;

    public override QuestEventType questEventType => QuestEventType.EnterScene;

    public override void Execute(SceneContext context)
    {
        if (!LevelManager.instance.loadingQuestEvents)
            CallBack();
    }

    public override bool ShouldExecute(SceneContext context)
    {
        if (GlyphManager.landscape != landscape || GlyphManager.biome != biome || GlyphManager.time != time)
            return false;

        return true;
    }
}