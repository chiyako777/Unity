using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class FadePA : PlayableAsset
{
    [SerializeField]
    private Color fadeColor;

    // Factory method that generates a playable based on this asset
    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        FadePB behaviour = new FadePB();
        behaviour.fadeColor = fadeColor;
        return ScriptPlayable<FadePB>.Create(graph, behaviour);
    }
}
