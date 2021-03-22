using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public delegate void CREATE_ENEMY_FUNC(EnemyInfo enemyInfo);
public class CommandManager
{
    private List<ICommand> command = new List<ICommand>();
    private int commandIndex {set; get;}
    public float waitTime {set; get;}    

    private CREATE_ENEMY_FUNC[] enemyFunc = 
    {
        EnemyTest.New
    };

    public void Initialize(){
        command.Clear();
        commandIndex = 0;
        waitTime = 0.0f;
    }

    public void Run(){
        while(commandIndex < command.Count){
            command[commandIndex].Run();
            commandIndex++;
        }
    }

    public bool LoadScript(string fileName){

        //** あとで複数データ対応
        if(!File.Exists(fileName)){
            Debug.Log("CommandManager.LoadScript ファイル存在しない");
            return false;
        }

        StringBuilder sb = new StringBuilder();
        using(StreamReader sr = File.OpenText(fileName)){    
            string s = "";
            while((s = sr.ReadLine()) != null){
                //Debug.Log("LoadScript : " + s);
                sb.Append(s);
            }
            //Debug.Log("LoadScript終了");
        }

        string[] data = sb.ToString().Split(',');

        EnemyInfo enemyInfo = new EnemyInfo();
        enemyInfo.enemyObj = MainManager.resourcesLoader.GetObjectHandle(data[1]);
        enemyInfo.lifeGage = MainManager.resourcesLoader.GetObjectHandle("enemy_lifegage");
        enemyInfo.bulletController = MainManager.resourcesLoader.GetObjectHandle(data[2]);
        enemyInfo.spellName = data[3];
        enemyInfo.defeatEffect = MainManager.resourcesLoader.GetObjectHandle(data[4]);
        enemyInfo.enemyStatus = 0;
        enemyInfo.enemyLocation = new Vector3(float.Parse(data[5]),float.Parse(data[6]),0.0f);
        enemyInfo.life = float.Parse(data[7]);
        enemyInfo.graphType = 0;
        enemyInfo.waitTime = 180;
        enemyInfo.bulletPattern = 0;
        enemyInfo.bulletInterval = 60;
        enemyInfo.bulletType = 0;
        enemyInfo.bulletColor = 0;
        enemyInfo.bulletScriptType = 0;

        command.Add(new EnemyCreateCommand(enemyFunc[0],enemyInfo));

        return true;
    }

}
