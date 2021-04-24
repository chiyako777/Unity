using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading.Tasks;

//** 各種リソース読み込み
public class ResourcesLoader<T> where T : UnityEngine.Object
{

    Dictionary<string,T> resourcesHandles = new Dictionary<string,T>();
    private T tempObj;

    //他の所でまだ呼んでるからとりあえず残す
    public T GetObjectHandle(string name){
        //とりあえずコンパイルとおすため
        UnityEngine.Object obj = new UnityEngine.Object();
        T tobj = (T)obj;
        return tobj;
    }

    public async Task LoadObject(string name,T dist){
        Debug.Log("loadObject");
        var handle = Addressables.LoadAssetAsync<T>(name);
        //yield return handle;    //ロード結果が出るまでreturnしない
        await handle.Task;
        if (handle.Status == AsyncOperationStatus.Succeeded){
            Debug.Log("handle.status = succeeded");
            dist = handle.Result;
            //tempObj = handle.Result;
            //return handle.Result;
        }else{
            Debug.Log("handle.status = none");
            dist = null;
            //tempObj = null;
            //return null;
        }
        //yield return null;
    }

	public bool ContainsKey(string key_name)
	{
		return resourcesHandles.ContainsKey(key_name);
	}
    
}

//** 音声用ラッパー
public class SoundPlayer{
    [HideInInspector]
    public static GameObject BGMPlayer,SEPlayer;
    private static AudioSource BGMAudioSource,SEAudioSource;

    public static async Task PlayBGM(string bgmName,float volume,bool loop){

        //** 音源ロード
        var handle = Addressables.LoadAssetAsync<AudioClip>(bgmName);
        await handle.Task;
        if (handle.Status == AsyncOperationStatus.Succeeded){
            AudioClip clip = handle.Result;
            //** プレイヤーオブジェクト生成
            var handle1 = Addressables.LoadAssetAsync<GameObject>("BGMPlayer");
            await handle1.Task;
            if (handle1.Status == AsyncOperationStatus.Succeeded){
                BGMPlayer = GameObject.Instantiate(handle1.Result,new Vector3(0.0f,0.0f,0.0f),Quaternion.identity);
                BGMAudioSource = BGMPlayer.GetComponent<AudioSource>();
                BGMAudioSource.clip = clip;
                BGMAudioSource.volume = volume;
                BGMAudioSource.loop = loop;
                BGMAudioSource.Play();
                //シーン遷移してもプレイヤーを破棄しないようにする
                Object.DontDestroyOnLoad(SoundPlayer.BGMPlayer);
            }
        }

    }

    public static async Task PlaySE(string seName){

        //** 音源ロード
        var handle = Addressables.LoadAssetAsync<AudioClip>(seName);
        await handle.Task;
        if (handle.Status == AsyncOperationStatus.Succeeded){
            //Debug.Log("SE音源ロード成功");
            AudioClip clip = handle.Result;

            //** プレイヤーオブジェクト生成
            var handle1 = Addressables.LoadAssetAsync<GameObject>("SEPlayer");
            await handle1.Task;
            if (handle1.Status == AsyncOperationStatus.Succeeded){
                //Debug.Log("SEPlayer生成成功");
                SEPlayer = GameObject.Instantiate(handle1.Result,new Vector3(0.0f,0.0f,0.0f),Quaternion.identity);
                SEAudioSource = SEPlayer.GetComponent<AudioSource>();
            }

            SEAudioSource.PlayOneShot(clip);

        }
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
