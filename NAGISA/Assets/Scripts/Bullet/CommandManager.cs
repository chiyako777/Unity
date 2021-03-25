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
    [HideInInspector]
    public float waitTime {set; get;}    
    [HideInInspector]
    public static bool nextCommandFlg;
    

    private CREATE_ENEMY_FUNC[] enemyFunc = 
    {
        EnemyTest.New
    };

    public void Initialize(){
        command.Clear();
        commandIndex = 0;
        waitTime = 0.0f;
        nextCommandFlg = true;
    }

    public void Run(){

        //待ち時間がなし、かつ次コマンド実行可能であれば、実行
        if(waitTime > 0) waitTime--;

        if(nextCommandFlg && waitTime == 0 && commandIndex < command.Count){
            command[commandIndex].Run();
            commandIndex++;
        }

    }

    public bool LoadScript(string fileName){

        if(!File.Exists(fileName)){
            Debug.Log("CommandManager.LoadScript ファイル存在しない");
            return false;
        }

        using(StreamReader sr = File.OpenText(fileName)){
            string s = "";
            while((s = sr.ReadLine()) != null){
                //Debug.Log("Script = " + s);

                string[] data = s.Split(',');

                switch(data[0]){
                    case "wait" :
                        command.Add(new WaitCommand(this,float.Parse(data[1])));
                        break;
                    case "enemy" :
                        EnemyInfo enemyInfo = new EnemyInfo();
                        enemyInfo.enemyObj = BulletMainManager.resourcesLoader.GetObjectHandle(data[2]);
                        enemyInfo.lifeGage = BulletMainManager.resourcesLoader.GetObjectHandle("enemy_lifegage");
                        enemyInfo.bulletController = BulletMainManager.resourcesLoader.GetObjectHandle(data[3]);
                        enemyInfo.spellName = data[4];
                        enemyInfo.defeatEffect = BulletMainManager.resourcesLoader.GetObjectHandle(data[5]);
                        enemyInfo.enemyStatus = 0;
                        enemyInfo.enemyLocation = new Vector3(float.Parse(data[6]),float.Parse(data[7]),0.0f);
                        enemyInfo.life = float.Parse(data[8]);
                        enemyInfo.graphType = 0;
                        enemyInfo.waitTime = 180;
                        enemyInfo.bulletPattern = 0;
                        enemyInfo.bulletInterval = 60;
                        enemyInfo.bulletType = 0;
                        enemyInfo.bulletColor = 0;
                        enemyInfo.bulletScriptType = 0;

                        command.Add(new EnemyCreateCommand(enemyFunc[int.Parse(data[1])],enemyInfo));
                        break;
                    case "clear" :
                        command.Add(new ClearCommand());
                        break;
                    case null :
                        break;
                }
            }
        }
        return true;
    }

}
