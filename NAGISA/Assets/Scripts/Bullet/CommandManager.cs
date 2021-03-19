using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void CREATE_ENEMY_FUNC(EnemyInfo enemyInfo);
public class CommandManager
{
    private List<ICommand> command = new List<ICommand>();
    private int commandIndex {set; get;}
    public float waitTime {set; get;}    

    private CREATE_ENEMY_FUNC[] enemyFunc = 
    {
        EnemyWhite.New
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
        //** 暫定べた書き
        //敵一個目
        EnemyInfo enemyInfo = new EnemyInfo();
        enemyInfo.enemyObj = MainManager.resourcesLoader.GetObjectHandle("tekitou");
        enemyInfo.lifeGage = MainManager.resourcesLoader.GetObjectHandle("enemy_lifegage");
        enemyInfo.bulletController = MainManager.resourcesLoader.GetObjectHandle("TestSpell");
        enemyInfo.enemyStatus = 0;
        enemyInfo.enemyLocation = new Vector3(0.0f,50.0f,0.0f);
        enemyInfo.life = 150;
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
