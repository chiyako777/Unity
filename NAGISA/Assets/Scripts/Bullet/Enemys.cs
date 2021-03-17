using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWhite : Enemy
{
    void Start()
    {
        base.Start();
        //Debug.Log("Start : 敵white生成");
    }

    void Update()
    {
        base.Update();
        //Debug.Log("Start : 敵white:update");
    }

    public static void New(EnemyInfo enemyInfo){
        //Debug.Log("EnemyWhite.New");
        GameObject enemyObj = Instantiate(enemyInfo.enemyObj,new Vector3(enemyInfo.x,enemyInfo.y,0.0f),Quaternion.identity);
        //GameObject lifeGage = Instantiate(enemyInfo.lifeGage,new Vector3(enemyInfo.x,enemyInfo.y,0.0f),Quaternion.identity);
        enemyObj.AddComponent<EnemyWhite>().enemyInfo = enemyInfo;
        //enemyObj.GetComponent<EnemyWhite>().enemyInfo.lifeGage = lifeGage;

        //GameObject lifeGage = Instantiate(enemyInfo.lifeGage,new Vector3(enemyInfo.x,enemyInfo.y,0.0f),Quaternion.identity);
        //Debug.Log("lifeGage : " + lifeGage);
        //enemyObj.GetComponent<EnemyWhite>().enemyInfo.lifeGage = lifeGage;
        //Debug.Log("enemyInfo.lifeGage : " + enemyObj.GetComponent<EnemyWhite>().enemyInfo.lifeGage);
        //Destroy(lifeGage);
        //⇒ここのタイミングで、Enemy.Start Enemy.Updateを呼ぶ
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
