using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading.Tasks;

//** 各種リソース読み込み
//** ジェネリッククラス：使用時に型を指定（複数型で使いたい場合はその分だけこのクラスのインスタンスを作る）
public class ResourcesLoader<T> where T : UnityEngine.Object
{

    private Dictionary<string,T> resourcesHandles = new Dictionary<string,T>();
    
    public async Task LoadAllObjects(string labelName,string flgName){    
        //Debug.Log("LoadAllObjects");
        FlagManager.flagDictionary[flgName] = false;
        
        var handle = Addressables.LoadAssetsAsync<T>(labelName, null);
        await handle.Task;
        if (handle.Status == AsyncOperationStatus.Succeeded){
            IList<T> resultList = handle.Result;
            foreach(var result in resultList){
                if(!resourcesHandles.ContainsKey(result.name)){
                    resourcesHandles.Add(result.name,result);
                }
            }
            //Debug.Log("LoadAllObjects:Comp");
            FlagManager.flagDictionary[flgName] = true;
        }else{
        }

    }

    public T GetObjectHandle(string name){
        if(resourcesHandles.ContainsKey(name)){
            return resourcesHandles[name];
        }else{
            return null;
        }    
    }

	public bool ContainsKey(string key_name)
	{
		return resourcesHandles.ContainsKey(key_name);
	}
    
}