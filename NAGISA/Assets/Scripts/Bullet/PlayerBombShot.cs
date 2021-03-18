using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//** ガワだけ作成（中身はPlayerShotと今の所同じ）
public class PlayerBombShot : PlayerShot
{

    void Start()
    {
        base.Start();  
        power = 2;      
    }

    void Update()
    {
        base.Update();
    }
}
