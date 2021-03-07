using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Değişkenlerimi public tanımladım unityden erişebilmek adına
    Rigidbody2D playerRB; // karakterimin rigidbody sine ulaşmak için değişkenimi tanımladım
    Animator playerAnimator; // animator için değişkenimi tanımladım
    public float moveSpeed = 1f;  //hareket hızı değişkenim
    public float jumpSpeed = 1f, jumpFrequency=1f, nextJumpTime; //zıplama değişkenlerim
    bool facingRight = true; //ilerlenen yöne göre karakterimi çevirme (ilk başta sağa baktığından true verdim)
    public bool isGrounded = false; // zeminde olup olmadığımı kontrol edeceğim değişkenim
    public Transform groundCheckPosition; //yere değip değmediğimi kontrol eden dairenin pozisyonu
    public float groundCheckRadius;    //çapı   
    public LayerMask groundCheckLayer; //katmanı

    void Awake() //startla awake birebir aynı gibidir tek farkları awake sahnede oluşturulduğu an çalışmaya başlar
    {

    }

    
    void Start()  // scriptin aktifleştirilmesinden itibaren çalışır ilk update metodundan önce çalışır
    {
        playerRB = GetComponent<Rigidbody2D>();  //rigidbody ile playercontroller scriptim aynı game objesi üzerinde olduğundan bu şekilde tanımlayabiliyorum
        playerAnimator = GetComponent<Animator>(); // karakterimizin içindeki animator componentini bu değişkenime aldım
    }

    
    void Update()  //oyun başladığından itibaren her bir frame frame adına bir defa çalışır
    {
        OnGroundCheck();
        HorizontalMove();
        if(playerRB.velocity.x < 0 && facingRight) //velocity değerim 0 dan küçük ise veya sağa bakıyorsam çevirmem gerekli
        {
            FlipFace();
        }
        else if(playerRB.velocity.x > 0 && !facingRight) // aynı şekilde değerim 0 dan büyük veya sola bakıyorsam yine gittiğim yöne çevirmeliyim
                {
            FlipFace();
        }

        if(Input.GetAxis("Vertical")>0 && isGrounded && (nextJumpTime<Time.timeSinceLevelLoad)) //kullanıcı yukarı ok tuşuna basıyorsa yere değiyorsa ve bir sonraki zıplayışımın zamanı oyun başladığından beri geçen süreden küçük ise
        {
            /*update metodu çok hızlı çalıştığından dolayı zıplamalarım bazen bir öncekine göre daha fazla olabiliyordu
             * bu yüzden gelecek zıplama zamanı ve zıplama sıklığı değişkenlerimle oluşturduğum algoritma ile bunun kontrolünü sağladım
             */
            nextJumpTime = Time.timeSinceLevelLoad + jumpFrequency; //bir sonraki zıplayışım şuanki zaman + zıplama sıklığım olarak belirledim
            jump();
        }
    }

     void jump()
    {
        //karakterimin rigidBody'sine addForce metodu ile kuvvet uyguluyorum
        playerRB.AddForce(new Vector2(0f, jumpSpeed)); //sadece dikeyde zıplayacğından x değeri 0 y değeri ise zıplama değişkenim
    }

    void FixedUpdate()  // zaman odaklı çalışır her 0.02 saniyede 1 defa çalışmak üzere ayarlıdır (bu aralık değişebilir)
    {
        
    }

    void FlipFace() //burada yüzümü ilerlediğim yönde çevirmek için karakterimin  localS cale'inin x değerini değiştiriyorum
    {
        facingRight = !facingRight;
        Vector3 tempLocalScale = transform.localScale; //localScale imi düzenlemek için bir vektör oluşturuyorum ve içine localScale imi atıyorum
        tempLocalScale.x *= -1; //değerimi negatif hale getiriyorum
        transform.localScale = tempLocalScale;//sonradan bu değeri karakterimizin localScale2ine geri atıyorum
    }

    void HorizontalMove()
    {
        /* hızımı 2 boyutlu bir vektör oluşturdumarak tanımladım vektörün yatay yönde x değerini kullanıcının girmesini istiyorum
         * bunun için GetAxis methodunu kullanıyorum ve horizontal parametresini veriyorum ve ordan gelecek olan -1-1 arasındaki değeri movespeedim ile çarpıyorum
         * y yi ise karakterin o esnadaki y hızı olarak tanımlıyorum yani dokunmuyorum.
         * Bir alt satırda ise playerSpeed parametreme karakterimin o esnadaki velocitysinin x değerini atıyorum
         */
        playerRB.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, playerRB.velocity.y);
        playerAnimator.SetFloat("playerSpeed", Math.Abs(playerRB.velocity.x));
    }

    void OnGroundCheck()
    {
        /*burada yere değip değmediğimi kontrol edeceğim metodumu oluşturuyorum
         * metodun içinde Physics2D nin overlapCircle metodunu çağırıyorum ve içine yere değip değmediğimi kontrol edecek olan dairenin
         * pozisyonunu. çapını ve katmanını veriyorum buna göre bana true veya false değer döndürüyor bu değeride isGrounded değişkenimin içine atıyorum
         */
        isGrounded = Physics2D.OverlapCircle(groundCheckPosition.position,groundCheckRadius,groundCheckLayer);
        playerAnimator.SetBool("isGroundedAnim", isGrounded);
    }
}
