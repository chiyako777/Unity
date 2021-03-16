using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWhite : Enemy
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public static void New(EnemyInfo enemyInfo){
        GameObject enemyObj = Instantiate(enemyInfo.enemyObj,new Vector3(enemyInfo.x,enemyInfo.y,0.0f),Quaternion.identity);
        enemyObj.AddComponent<EnemyWhite>().enemyInfo = enemyInfo;
    }
}

public class EnemyLavender : Enemy
{
    // void Start()
    // {
        
    // }

    // void Update()
    // {
    //     transform.position += new Vector3(velocity.x,velocity.y,0.0f);
    // }

    // public static void New(EnemyInfo enemyInfo){
    //     GameObject enemyObj = Instantiate(enemyInfo.enemyObj,new Vector3(enemyInfo.x,enemyInfo.y,0.0f),Quaternion.identity);
    //     enemyObj.AddComponent<EnemyLavender>().enemyInfo = enemyInfo;
    // }
}
