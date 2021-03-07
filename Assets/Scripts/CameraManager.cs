using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public Transform target; // kameramın takip etmesini isteyeceğim değişkenimi tanımlıyorum

    public float cameraSpeed; //kameramın hızını belirleyeceğim değişkenimi tanımlıyorum
    
    void Start()
    {
        
    }

    
    void Update()
    {
        /* 3 boyutlu bir vektör oluşturuyoruz parametrelerim ise
         * ilk olarak başlangıç noktamı yani kameramızın pozisyonunu ve bitiş noktamı yani hedefimin pozisyonunu ve bu 2 nokta arasındaki geçişin hızını belirlemek için cameraspeed veriyorum
         * slerp metodu ile kameramızın pozisyonundan karakterimizin pozisyonuna doğru yumuşak bir geçiş yapıyoruz
         */
        transform.position = Vector3.Slerp(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z),cameraSpeed);
    }
}
