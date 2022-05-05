using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveGlyphEvent : QuestEvent
{
    public override QuestEventType questEventType => QuestEventType.GiveGlyph;

    public Glyph glyph;

    public override void Execute(SceneContext context)
    {
        Debug.Log("Giving!");
        GlyphManager.playerGlyphs.Add(glyph);
        WaystoneUI.instance.Resetup();
        CallBack();
    }

    public override bool ShouldExecute(SceneContext context)
    {
        Debug.Log("Should give?");
        return !GlyphManager.playerGlyphs.Contains(glyph);
    }
}
