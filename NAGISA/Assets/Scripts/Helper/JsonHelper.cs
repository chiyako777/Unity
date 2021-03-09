using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//** 配列をルートに持つjsonをシリアライズするための補助クラス
// https://takap-tech.com/entry/2021/02/02/222406
public static class JsonHelper
{

    public static T[] FromJson<T>(string json){
        //** 処理概要
        //     引数として渡された配列がルートのjson文字列に、一旦ダミーのルート要素を加えてデシリアライズする
        //     デシリアライズ後に、ダミーのルート要素は無視して、本来の中身データだけを配列として返却
        //     以下の★の行がダミーとして挿入される
        //     ★{
        //        ★"array":
        //           [・・・（本来の中身）・・]
        //     ★}
        //}

        string dummy_json = $"{{\"{DummyNode<T>.ROOT_NAME}\": {json}}}";
        var obj = JsonUtility.FromJson<DummyNode<T>>(dummy_json);
        return obj.array;

    }

    public static string ToJson<T>(IEnumerable<T> collection){
        string json = JsonUtility.ToJson(new DummyNode<T>(collection)); // ダミールートごとシリアル化する
        int start = DummyNode<T>.ROOT_NAME.Length + 4;
        int len = json.Length - start - 1;
        return json.Substring(start, len); // 追加したダミールートの文字を取り除いて返す
    }

    //** ダミーのルート要素含めたjsonを受け取る構造体
    [Serializable]
    private struct DummyNode<T>{
        
        public const string ROOT_NAME = nameof(array);  //jsonに付与するダミールートの名称
        public T[] array;

        //(コンストラクタ)コレクション要素を指定してオブジェクトを作成する
        public DummyNode(IEnumerable<T> collection) => this.array = collection.ToArray();
    }
}
