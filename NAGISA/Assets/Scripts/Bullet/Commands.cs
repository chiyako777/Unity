using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//** ICommandを実装するクラス群

//** 敵生成コマンド
public class EnemyCreateCommand : ICommand{
    private EnemyInfo enemyInfo;
    CREATE_ENEMY_FUNC createEnemyFunc;

    public EnemyCreateCommand(CREATE_ENEMY_FUNC func,EnemyInfo enemyInfo){
        createEnemyFunc = func;
        this.enemyInfo = enemyInfo;
    }

    public void Run(){
        createEnemyFunc(enemyInfo);
    }
}

//** ウェイトコマンド
public class WaitCommand : ICommand{

    private CommandManager commandManager;
    private float waitTime;

    public WaitCommand(CommandManager commandManager,float waitTime){
        this.commandManager = commandManager;
        this.waitTime = waitTime;
    }

    public void Run(){
        commandManager.waitTime = waitTime;
    }
}

//** クリアコマンド
public class ClearCommand : ICommand{

    public ClearCommand(){

    }

    public void Run(){
        SceneManager.LoadScene(MainManager.beforeScene);
    }

}

