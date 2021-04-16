using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EventStoryData
{
    public int id;
    public string timeline;
    public float finishtime;
    public List<string> track;
    public List<string> bindObject;
    public List<string> bindComponent;

}
