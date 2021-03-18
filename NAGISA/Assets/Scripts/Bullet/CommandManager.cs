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
        enemyInfo.x = 0.0f;
        enemyInfo.y = 50.0f;
        enemyInfo.life = 60;
        enemyInfo.graphType = 0;
        enemyInfo.waitTime = 180;
        enemyInfo.bulletPattern = 0;
        enemyInfo.bulletInterval = 60;
        enemyInfo.bulletType = 0;
        enemyInfo.bulletColor = 0;
        enemyInfo.bulletScriptType = 0;

        command.Add(new EnemyCreateCommand(enemyFunc[0],enemyInfo));

        //敵二個目
        // EnemyInfo enemyInfo2 = new EnemyInfo();
        // enemyInfo2.enemyObj = MainManager.resourcesLoader.GetObjectHandle("tekitou");
        // enemyInfo2.x = 50.0f;
        // enemyInfo2.y = 50.0f;
        // enemyInfo2.life = 60;
        // enemyInfo2.graphType = 0;
        // enemyInfo2.waitTime = 180;
        // enemyInfo2.bulletPattern = 0;
        // enemyInfo2.bulletInterval = 60;
        // enemyInfo2.bulletType = 0;
        // enemyInfo2.bulletColor = 0;
        // enemyInfo2.bulletScriptType = 0;
        // command.Add(new EnemyCreateCommand(enemyFunc[0],enemyInfo2));

        return true;
    }

}
