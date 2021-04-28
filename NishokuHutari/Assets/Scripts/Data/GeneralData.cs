using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//** ゲーム開始時に一括ロードしちゃうデータのイメージで
public class GeneralData
{
    public List<MapData> mapData;
    public List<SelectData> selectData;
    public List<NAGISACutInData> nagisaCutInData;
    public Dictionary<int,int> nagisaCutInIndex;

    public GeneralData(){

        //** 固定データの設定

        //** MapData
        //(メモ：transactionId,playerPosition,to(飛び先マッププレハブ))
        mapData = new List<MapData>();
        mapData.Add(new MapData(9999,new Vector3(0.0f,0.0f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_9999")));

        mapData.Add(new MapData(1,new Vector3(2.0f,-1.0f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_2")));
        mapData.Add(new MapData(2,new Vector3(0.0f,0.0f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_17")));
        mapData.Add(new MapData(3,new Vector3(0.0f,0.0f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_27")));
        mapData.Add(new MapData(4,new Vector3(0.0f,0.0f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_26")));
        mapData.Add(new MapData(5,new Vector3(0.0f,0.0f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_5")));
        mapData.Add(new MapData(6,new Vector3(0.0f,0.0f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_8")));
        mapData.Add(new MapData(7,new Vector3(0.0f,0.0f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_22")));
        mapData.Add(new MapData(8,new Vector3(0.0f,0.0f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_33")));
        mapData.Add(new MapData(9,new Vector3(0.0f,0.0f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_7")));
        mapData.Add(new MapData(10,new Vector3(0.0f,0.0f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_29")));
        mapData.Add(new MapData(11,new Vector3(0.0f,0.0f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_9")));
        mapData.Add(new MapData(12,new Vector3(0.0f,0.0f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_25")));
        mapData.Add(new MapData(13,new Vector3(0.0f,0.0f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_14")));
        mapData.Add(new MapData(14,new Vector3(0.0f,0.0f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_4")));
        mapData.Add(new MapData(15,new Vector3(0.0f,0.0f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_3")));
        mapData.Add(new MapData(16,new Vector3(0.0f,0.0f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_12")));
        mapData.Add(new MapData(17,new Vector3(0.0f,0.0f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_15")));
        mapData.Add(new MapData(18,new Vector3(0.0f,0.0f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_35")));
        mapData.Add(new MapData(19,new Vector3(0.0f,0.0f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_36")));
        mapData.Add(new MapData(20,new Vector3(0.0f,0.0f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_20")));
        mapData.Add(new MapData(21,new Vector3(0.0f,0.0f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_10")));
        mapData.Add(new MapData(22,new Vector3(0.0f,0.0f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_9")));
        mapData.Add(new MapData(23,new Vector3(0.0f,0.0f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_34")));
        mapData.Add(new MapData(24,new Vector3(0.0f,0.0f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_23")));
        mapData.Add(new MapData(25,new Vector3(0.0f,0.0f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_28")));
        mapData.Add(new MapData(26,new Vector3(0.0f,0.0f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_3")));
        mapData.Add(new MapData(27,new Vector3(0.0f,0.0f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_35")));
        mapData.Add(new MapData(28,new Vector3(0.0f,0.0f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_5")));
        mapData.Add(new MapData(29,new Vector3(0.0f,0.0f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_21")));
        mapData.Add(new MapData(30,new Vector3(0.0f,0.0f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_14")));
        mapData.Add(new MapData(31,new Vector3(0.0f,0.0f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_26")));
        mapData.Add(new MapData(32,new Vector3(0.0f,0.0f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_6")));
        mapData.Add(new MapData(33,new Vector3(0.0f,0.0f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_18")));
        mapData.Add(new MapData(34,new Vector3(0.0f,0.0f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_24")));
        mapData.Add(new MapData(35,new Vector3(0.0f,0.0f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_15")));
        mapData.Add(new MapData(36,new Vector3(0.0f,0.0f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_25")));
        mapData.Add(new MapData(37,new Vector3(0.0f,0.0f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_33")));

        //** SelectData
        selectData = new List<SelectData>();
        selectData.Add(new SelectData(9999,9999,9999,"black"));

        selectData.Add(new SelectData(1,5,6,"white"));
        selectData.Add(new SelectData(2,8,7,"black"));
        selectData.Add(new SelectData(3,12,11,"black"));
        selectData.Add(new SelectData(4,17,16,"white"));
        selectData.Add(new SelectData(5,33,32,"black"));
        selectData.Add(new SelectData(6,37,36,"white"));
        selectData.Add(new SelectData(7,28,29,"white"));
        selectData.Add(new SelectData(8,31,30,"black"));
        selectData.Add(new SelectData(9,23,22,"black"));
        selectData.Add(new SelectData(10,26,27,"white"));

        //** NAGISACutInData
        nagisaCutInData = new List<NAGISACutInData>();
        //nagisaCutInData.Add(new NAGISACutInData(9999,Manager.gameObjectLoader.GetObjectHandle("TestTimeLine")));
        nagisaCutInData.Add(new NAGISACutInData(
                                    1,
                                    Manager.gameObjectLoader.GetObjectHandle("NagisaCutIn_1"),
                                    new List<string>{"Nagisa_CloseEye"},
                                    new List<string>{"Nagisa"},
                                    new List<string>{"Animator"}));

        //** NAGISAカットイン発生トリガ(残りターン数、イベントID)
        nagisaCutInIndex = new Dictionary<int,int>();
        nagisaCutInIndex.Add(23,1);
        nagisaCutInIndex.Add(15,9999);
        nagisaCutInIndex.Add(10,9999);
        nagisaCutInIndex.Add(5,9999);
        nagisaCutInIndex.Add(2,9999);

    }
}
