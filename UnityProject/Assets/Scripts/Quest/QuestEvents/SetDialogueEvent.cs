using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDialogueEvent : QuestEvent
{
    public override QuestEventType questEventType => QuestEventType.SetDialogue;

    public GlyphLandscape landscape;
    public GlyphBiome biome;
    public GlyphTime time;

    public NPCController npc;
    public Dialogue dialogue;

    public override void Execute(SceneContext context)
    {
        foreach (NPCController npc in context.npcs)
        {
            if (npc == this.npc)
            {
                npc.SetDialogue(dialogue, CallBack);
            }
        }
    }

    public override bool ShouldExecute(SceneContext context)
    {
        if (GlyphManager.landscape != landscape || GlyphManager.biome != biome || GlyphManager.time != time)
            return false;

        return true;
    }
}
