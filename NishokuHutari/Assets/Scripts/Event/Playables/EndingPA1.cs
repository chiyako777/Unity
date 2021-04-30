using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class EndingPA1 : PlayableAsset
{
    [SerializeField]
    private List<string> messageContents;

    // Factory method that generates a playable based on this asset
    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        EndingPB1 behaviour = new EndingPB1();
        behaviour.messageContents = messageContents;
        return ScriptPlayable<EndingPB1>.Create(graph, behaviour);
    }
}
