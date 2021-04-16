using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayableAssetMy : PlayableAsset
{
    [SerializeField]
    public ExposedReference<GameObject> eobjMy;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject game_object){
        PlayableBehaviourMy behaviour = new PlayableBehaviourMy();
        behaviour.objMy = eobjMy.Resolve(graph.GetResolver());
        return ScriptPlayable<PlayableBehaviourMy>.Create(graph, behaviour);
    }
}