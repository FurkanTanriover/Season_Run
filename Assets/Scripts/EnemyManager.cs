using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public float health;
    public float damage;
    public float speed; //düşman objemin hareket hızı
    public float turnDelay; // aşağı veya yukarı hareketini sürdüreceği zaman
    bool enemyİsGrounded = false; //
    bool enemyFaceRight = false;
    bool colliderBusy = false; // collider ımın şuan meşgul olup olmadığını kontrol edecek bir boolean değişken oluşturuyorum(değen objemdeki her collider için trigger çalışmaması adına)
    
    public Slider slider;//can sistemim için slider cinsi değişkenimi tanımladım
    void Start()
    {
        slider.maxValue = health;  //sliderımın max değerini düşman objemin canına eşitliyorum
        slider.value = health;
        StartCoroutine(SwitchDirections()); //düşman hareketlerimde gidip gelmeyi sağlamak için startcoroutine mi çağırıyoruz
      }

    
    void Update()
    {
        if (tag == "Enemy")
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime); //aşağı in diyorum
            print("Update enemy");
        }

        if (tag == "Enemy1")
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime); //sağa git diyorum
            print("Update enemy1");
        }
     
        
    }
     IEnumerator SwitchDirections()
    {
        if (tag == "Enemy") //obje tagıme göre yönlendirme ayarlıyorum
        {
            yield return new WaitForSeconds(turnDelay); // burda turndelay değişkenime verdiğim süre kadar dikey eksende hareketini yapacak
            LowerIt();
            print("enemy");
        }


        if (tag == ("Enemy1")) // obje tagıma göre yönlendirme ayarlıyorum
        {
            yield return new WaitForSeconds(turnDelay); // burda turndelay değişkenime verdiğim süre kadar yatay eksende hareketini yapacak
            LowerIt();
            print("Enemy1");
        }
    }


    private void Flipp() // düşman objemi  yatay yönde hareket ettiriyorum
    {
        enemyFaceRight = !enemyFaceRight;
        Vector3 scaler = transform.localScale;
        scaler.y *= -1; // scalerimin x ini -1 ile çarpıyorum objem yön değiştirkçe yüzü dönsün
        transform.localScale = scaler; // eşitliyorum

        speed *= -1; // speedimizi -1 ile çarpıyoruz

        StartCoroutine(SwitchDirections()); // switchdirectionumu çağırıyorum tekrardan öünkü aşağı inip yukarı çıkınca tekrar inmesi lazım yoksa yukarı devam eder

        print("flip");
    }


    private void LowerIt() // düşman objemi dikey yönde hareket ettiriyorum
    {
        enemyİsGrounded = !enemyİsGrounded; // burada düşman objem yerde olup olmayışı true ise false false ise true
        Vector3 scaler = transform.localScale;
        scaler.y *= 1; // scalerimin y sini 1 ile çarpıyorum objem yukarı dönerken ters dönmesin
        transform.localScale = scaler; // eşitliyorum

        speed *= -1; // speedimizi -1 ile çarpıyoruz

        StartCoroutine(SwitchDirections()); // switchdirectionumu çağırıyorum tekrardan çünkü aşağı inip yukarı çıkınca tekrar inmesi lazım yoksa yukarı devam eder

        print("lower");
    }


    private void OnTriggerEnter2D(Collider2D other) //karakterimin enemy objesinin colliderine değdiğinde olacak şeyleri buraya yazıyorum
    {
        /* başka objelerim değdiğinde hata almamak için playerime verdiğim player tagını kontrolde kullanıyorum
         * ve eğer değen objenin tagı player ise ve enemy objem müsait ise aşağıdaki ulaşımı yapmasını istiyorum*/
        if (other.tag == "Player" && !colliderBusy) 
        {
            colliderBusy = true; // bu sayede player objem enemy objeme değdiğinde player objemin değen ilk colliderini işleme alıyorum sadece
            other.GetComponent<PlayerManager>().GetDamage(damage); //karakterimin playerManager scriptinde bulunan getDamage metodunu çalıştırmasını istiyorum
        }
        else if(other.tag=="Bullet")
        {
            GetDamage(other.GetComponent<BulletManager>().bulletDamage); // getDamage metodumu çalıştırıyorum ve parametre olarak BulletManager içindeki bulletDamage i almasını istiyorum
            Destroy(other.gameObject );// enemy objeme değmiş olan kurşun objelerimi yo kediyorum
        }
    }
    private void OnTriggerExit2D(Collider2D player)  // burada eğer düşman objemden çıkan obje player objesi ise içini çalıştırmasını istiyorum
    {
        colliderBusy = false;  // düşman objem bir daha karakterime hasar verebilsin diye false hale getiriyorum
    }

    public void GetDamage(float damage)  //karakterim için hasar al metodunu oluşturuyorum parametre olarakta enemy objemden gelecek damage yi koyuyorum
    {
        /*burada rakipten gelen hasarın karakterimin sağlığından fazla mı az mı olduğunu kontrol ediyorum
         * ve altta oluşturduğum ben ölü müyüm metodumu çağırıyorum aldığı health değerine göre bana bool ifade döndürücek
         */
        if ((health - damage) >= 0)
        {
            health -= damage;
        }
        else
        {
            health = 0;
        }
        slider.value = health;// her hasar aldığımda slider değerimi enemy objemin health değeriyle güncelliyorum
        AmIDead();
    }

    void AmIDead()
    {
        if (health <= 0)
        {
            DataManager.Instance.EnemyKilled++;
            Destroy(gameObject); // canı sıfırlanan düşman objelerimi yok ediyorum
        }
    }
}
