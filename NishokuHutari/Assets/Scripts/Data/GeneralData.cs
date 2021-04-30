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
    public List<StoryData> storyData;
    public List<ItemData> itemData;

    public GeneralData(){

        //** 固定データの設定

        //** MapData
        mapData = new List<MapData>();
        mapData.Add(new MapData(9999,new Vector3(0.0f,0.0f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_9999")));
        mapData.Add(new MapData(0,new Vector3(1.71f,0.03f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_1")));
        mapData.Add(new MapData(1,new Vector3(3.73f,-2.29f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_2")));
        mapData.Add(new MapData(2,new Vector3(3.98f,2.4f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_17")));
        mapData.Add(new MapData(3,new Vector3(2.52f,-0.78f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_27")));
        mapData.Add(new MapData(4,new Vector3(-0.17f,2.13f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_26")));
        mapData.Add(new MapData(5,new Vector3(-4.0f,1.09f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_5")));
        mapData.Add(new MapData(6,new Vector3(0.01f,-2.97f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_8")));
        mapData.Add(new MapData(7,new Vector3(-3.84f,1.44f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_22")));
        mapData.Add(new MapData(8,new Vector3(-0.04f,2.64f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_33")));
        mapData.Add(new MapData(9,new Vector3(2.4f,1.95f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_7")));
        mapData.Add(new MapData(10,new Vector3(3.21f,2.15f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_29")));
        mapData.Add(new MapData(11,new Vector3(-4.15f,-2.76f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_9")));
        mapData.Add(new MapData(12,new Vector3(-3.87f,2.27f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_25")));
        mapData.Add(new MapData(13,new Vector3(2.48f,2.93f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_14")));
        mapData.Add(new MapData(14,new Vector3(-0.16f,-2.23f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_4")));
        mapData.Add(new MapData(15,new Vector3(-3.57f,2.98f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_3")));
        mapData.Add(new MapData(16,new Vector3(0.8f,0.5f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_12")));
        mapData.Add(new MapData(17,new Vector3(-4.14f,-2.98f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_15")));
        mapData.Add(new MapData(18,new Vector3(1.67f,-0.2f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_35")));
        //mapData.Add(new MapData(19,new Vector3(0.0f,0.0f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_36")));
        mapData.Add(new MapData(20,new Vector3(-0.27f,-1.84f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_20")));
        mapData.Add(new MapData(21,new Vector3(-3.79f,-2.31f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_10")));
        mapData.Add(new MapData(22,new Vector3(-4.15f,-2.76f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_9")));
        mapData.Add(new MapData(23,new Vector3(-0.08f,-1.45f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_34")));
        mapData.Add(new MapData(24,new Vector3(3.63f,0.25f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_23")));
        mapData.Add(new MapData(25,new Vector3(-3.94f,2.74f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_28")));
        mapData.Add(new MapData(26,new Vector3(-3.57f,2.98f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_3")));
        mapData.Add(new MapData(27,new Vector3(1.67f,-0.2f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_35")));
        mapData.Add(new MapData(28,new Vector3(-4.0f,1.09f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_5")));
        mapData.Add(new MapData(29,new Vector3(-0.56f,-0.22f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_21")));
        mapData.Add(new MapData(30,new Vector3(2.48f,2.93f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_14")));
        mapData.Add(new MapData(31,new Vector3(-0.17f,2.13f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_26")));
        mapData.Add(new MapData(32,new Vector3(2.48f,0.19f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_6")));
        mapData.Add(new MapData(33,new Vector3(-0.36f,2.18f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_18")));
        mapData.Add(new MapData(34,new Vector3(3.69f,1.82f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_24")));
        mapData.Add(new MapData(35,new Vector3(-4.14f,-2.98f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_15")));
        mapData.Add(new MapData(36,new Vector3(-3.87f,2.27f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_25")));
        mapData.Add(new MapData(37,new Vector3(-0.04f,2.64f,0.0f),Manager.gameObjectLoader.GetObjectHandle("Map_33")));

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

        //** StoryData
        storyData = new List<StoryData>();
        //オープニング
        storyData.Add(new StoryData(
                            1,
                            Manager.gameObjectLoader.GetObjectHandle("OpeningStory"),
                            new List<string>{"Ohana Anime","Usagi Anime","Usagi Walk"},
                            new List<string>{"FallenOhana","Player(Clone)","Player(Clone)"},
                            new List<string>{"Animator","Animator","Animator"}));
        //bad end
        storyData.Add(new StoryData(
                            2,
                            Manager.gameObjectLoader.GetObjectHandle("Ending_Bad_Event"),
                            new List<string>{"Red Usagi Anime","Red Usagi Move","Nagisa Anime","Ohana Anime"},
                            new List<string>{"Player_Mistake","Player_Mistake","Nagisa","FallenOhana"},
                            new List<string>{"Animator","Animator","Animator","Animator"}));
        //true end
        storyData.Add(new StoryData(
                            3,
                            Manager.gameObjectLoader.GetObjectHandle("Ending_True_Event"),
                            new List<string>{"Usagi Anime","Usagi Move","Nagisa Anime"},
                            new List<string>{"Player","Player","Nagisa"},
                            new List<string>{"Animator","Animator","Animator"}));


        //** ItemData
        itemData = new List<ItemData>();
        //しおれた花：道端アイテム
        itemData.Add(new ItemData(
                            1,
                            Manager.gameObjectLoader.GetObjectHandle("ShioretaHana"),
                            4,
                            new Vector3(0.0f,-2.0f,0.0f),
                            "test_itemget",
                            new List<string>{"そうだった。彼女はもう、","死んでしまった。"}));
        //片割れハート：道端アイテム（正解√でしか入手不可）
        //15マップは2通りから遷移できるので、とりあえず二つ定義（でもCompFlg周りが遷移元が違うと別になっちゃうからちゃんと対応必要）
        itemData.Add(new ItemData(
                            2,
                            Manager.gameObjectLoader.GetObjectHandle("KatawareHeart1"),
                            17,
                            new Vector3(0.0f,0.0f,0.0f),
                            "test_itemget",
                            new List<string>{"片割れハートの","キャプションテストだよ"}));
        itemData.Add(new ItemData(
                            3,
                            Manager.gameObjectLoader.GetObjectHandle("KatawareHeart2"),
                            35,
                            new Vector3(0.0f,0.0f,0.0f),
                            "test_itemget",
                            new List<string>{"片割れハートの","キャプションテストだよ"}));
        //銃：選択肢アイテム
        itemData.Add(new ItemData(
                            4,
                            null,
                            6,
                            new Vector3(0.0f,0.0f,0.0f),
                            "test_itemget",
                            new List<string>{"ジオメトリックガンの","キャプションテストだよ"}));
        //論文：選択肢アイテム
        itemData.Add(new ItemData(
                            5,
                            null,
                            22,
                            new Vector3(0.0f,0.0f,0.0f),
                            "test_itemget",
                            new List<string>{"論文の","キャプションテストだよ"}));
        itemData.Add(new ItemData(
                            6,
                            null,
                            26,
                            new Vector3(0.0f,0.0f,0.0f),
                            "test_itemget",
                            new List<string>{"論文の","キャプションテストだよ"}));


    }
}




