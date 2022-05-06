using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveGlyphEvent : QuestEvent
{
    public override QuestEventType questEventType => QuestEventType.GiveGlyph;

    public Glyph glyph;

    public override void Execute(SceneContext context)
    {
        GlyphManager.playerGlyphs.Add(glyph);
        WaystoneUI.instance.Resetup();
        FMODUnity.RuntimeManager.PlayOneShot(LevelManager.instance.playerPrefab.GetComponent<CharacterController>().aquiredGlyph);
        CallBack();
    }

    public override bool ShouldExecute(SceneContext context)
    {
        return !GlyphManager.playerGlyphs.Contains(glyph);
    }
}
