using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{

    public float bulletDamage, lifeTime;
    
    void Start()
    {
        Destroy(gameObject, lifeTime); // kurşunum oluşturulduktan sonra lifeTime süresi boyunca yaşayacak sonra yok olacak
    }

    
    void Update()
    {
        
    }
}
