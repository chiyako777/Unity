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
                //Debug.Log("重複:" + obj.name);
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

	public bool ContainsKey(string key_name)
	{
		return resourcesHandles.ContainsKey(key_name);
	}
    
}

//** 音声用ラッパー
public class SoundPlayer{
    private static ResourcesLoader<AudioClip> resourcesLoader = new ResourcesLoader<AudioClip>();
    [HideInInspector]
    public static GameObject BGMPlayer,SEPlayer;
    private static AudioSource BGMAudioSource,SEAudioSource;

    public static bool LoadAllSounds(string path){
        return resourcesLoader.LoadAllObjects(path);
    }

    public static AudioClip GetAudioClip(string name){
        return resourcesLoader.GetObjectHandle(name);
    }

    public static bool PlayBGM(string bgmName,float volume,bool loop){
        if(!resourcesLoader.ContainsKey(bgmName)){
            //Debug.Log("PlayBGM : false");
            return false;
        }

        BGMPlayer = new GameObject("BGMPlayer");
        BGMPlayer.tag = "BGMPlayer";
        BGMAudioSource = BGMPlayer.AddComponent<AudioSource>();
        BGMAudioSource.clip = resourcesLoader.GetObjectHandle(bgmName);
        //Debug.Log("AudioClip.name = " + BGMAudioSource.clip);
        BGMAudioSource.volume = volume;
        BGMAudioSource.loop = loop;
        BGMAudioSource.Play();

        return true;
    }

    public static bool PlaySE(string seName){
        if(!resourcesLoader.ContainsKey(seName)){
            return false;
        }

        SEPlayer = new GameObject("SEPlayer");
        SEPlayer.tag = "SEPlayer";
        SEAudioSource = SEPlayer.AddComponent<AudioSource>();
        SEAudioSource.PlayOneShot(resourcesLoader.GetObjectHandle(seName));

        return true;
    }

    public static bool StopBGM(){
        if(BGMPlayer == null || BGMAudioSource == null){
            //Debug.Log("StopBGM : null");
            return false;
        }
        //Debug.Log("StopBGM : exec");
        BGMAudioSource.Stop();
        return true;
    }

    public static bool IsPlayingBGM(string bgmName){
        if(BGMAudioSource == null){ 
            //Debug.Log("BGM null");
            return false; 
        }
        //Debug.Log("BGMAudioSource.isPlaying = " + BGMAudioSource.isPlaying);
        //Debug.Log("BGMAudioSource.clip.name == bgmName = " + (BGMAudioSource.clip.name == bgmName));
        return ( BGMAudioSource.isPlaying && BGMAudioSource.clip.name == bgmName);
    }


}
