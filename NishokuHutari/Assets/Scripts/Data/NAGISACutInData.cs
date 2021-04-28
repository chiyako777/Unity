using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NAGISACutInData
{
    public int nagisaCutInId;
    public GameObject timeline;
    public List<string> trackName;
    public List<string> bindObjectName;
    public List<string> componentName;
    public bool compFlg;

    public NAGISACutInData(int nagisaCutInId,GameObject timeline,List<string> trackName,List<string> bindObjectName,List<string> componentName){
        this.nagisaCutInId = nagisaCutInId;
        this.timeline = timeline;
        this.trackName = trackName;
        this.bindObjectName = bindObjectName;
        this.componentName = componentName;
        compFlg = false;
    }

}
