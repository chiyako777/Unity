using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class StoryPA : PlayableAsset
{
    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        StoryPB behaviour = new StoryPB();
        return ScriptPlayable<StoryPB>.Create(graph, behaviour);
    }
}
