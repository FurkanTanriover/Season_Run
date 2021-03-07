using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Bu scriptim üzerinde bulunduğu game objesini bir süre sonra yok edecek
public class DestroyMe : MonoBehaviour
{
    public int lifeTime;
    
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    
    void Update()
    {
        
    }
}
