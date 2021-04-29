using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryData
{
    public int storyId;
    public GameObject timeline;
    public List<string> trackName;
    public List<string> bindObjectName;
    public List<string> componentName;

    public StoryData(int storyId,GameObject timeline,List<string> trackName,List<string> bindObjectName,List<string> componentName){
        this.storyId = storyId;
        this.timeline = timeline;
        this.trackName = trackName;
        this.bindObjectName = bindObjectName;
        this.componentName = componentName;
    }
}
