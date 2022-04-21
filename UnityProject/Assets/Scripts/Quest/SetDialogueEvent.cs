using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDialogueEvent : QuestEvent
{
    public override QuestEventType questEventType => QuestEventType.SetDialogue;

    public GlyphLandscape landscape;
    public GlyphBiome biome;
    public GlyphTime time;

    public List<string> dialogue;

    public override void Execute(SceneContext context)
    {
        throw new System.NotImplementedException();
    }

    public override bool ShouldExecute(SceneContext context)
    {
        if (GlyphManager.landscape != landscape || GlyphManager.biome != biome || GlyphManager.time != time)
            return false;

        return true;
    }
}
