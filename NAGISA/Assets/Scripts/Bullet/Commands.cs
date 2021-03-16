using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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