using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//** スペカ実装指針
//   BulletController継承必須

public class TestSpell5 : BulletController
{
    private bool flg = false;
    void Start()
    {
        base.Start();
    }

    void Update()
    {
        base.Update();
        if(!flg){
            bulletList.Add(Instantiate(prefabs[3],enemyLocation,Quaternion.identity));
            bulletList[bulletList.Count-1].AddComponent<Laser>();
            Laser laser = bulletList[bulletList.Count-1].GetComponent<Laser>();
            laser.angle = -45.0f;
            laser.length = 100.0f;
            laser.startPos = new Vector3(enemyLocation.x,enemyLocation.y,0.0f);
            
            flg = true;
        }
    }
}
