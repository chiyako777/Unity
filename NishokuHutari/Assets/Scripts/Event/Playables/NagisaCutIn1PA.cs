using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class NagisaCutIn1PA : PlayableAsset
{
    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        NagisaCutIn1PB behaviour = new NagisaCutIn1PB();
        return ScriptPlayable<NagisaCutIn1PB>.Create(graph, behaviour);
    }
}
