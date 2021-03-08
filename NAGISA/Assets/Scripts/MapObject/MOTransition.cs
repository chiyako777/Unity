using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MOTransition : MapObjectBase
{
    public string dist;
    protected override IEnumerator OnAction(){

        yield return null;

        SceneManager.LoadScene(dist);

        yield return new WaitForSeconds(1);

        //※暗転等の演出は後で入れる

    }
}
