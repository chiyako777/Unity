using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData
{
    public int transitionId;
    public Vector3 playerPosition;
    public GameObject mapDetail;

    public MapData(int transitionId,Vector3 playerPosition,GameObject mapDetail){
        this.transitionId = transitionId;
        this.playerPosition = playerPosition;
        this.mapDetail = mapDetail;
    }
}
