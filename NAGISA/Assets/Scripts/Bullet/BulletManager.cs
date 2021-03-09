using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    
    public class BulletFactory{
        public GameObject Bullet;

        public void CreateBullet(){
            GameObject newParent = new GameObject("Empty");
            Bullet = Instantiate(newParent);

            Bullet.AddComponent<Bullet>();
        }
    }

    public BulletFactory[] bulletFactory;   //中身はステージによる
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
