using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//** 各種リソース読み込み
public class ResourcesLoader<T> where T : UnityEngine.Object
{

    Dictionary<string,T> resourcesHandles = new Dictionary<string,T>();

    public bool LoadAllObjects(string fileName){
        T[] objs = Resources.LoadAll<T>(fileName);
        if(objs.Length <= 0){
            return false;
        }
        foreach(T obj in objs){
            if(resourcesHandles.ContainsKey(obj.name)){
                Debug.Log("重複");
            }else{
                resourcesHandles.Add(obj.name,obj);
            }
        }
        return true;
    }

    public T GetObjectHandle(string name){
        if(resourcesHandles.ContainsKey(name)){
            return resourcesHandles[name];
        }else{
            return null;
        }
    }

    
}
