 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public float health,bulletSpeed;  // sağlık değişkenimi tanımlıyorum    
    bool dead = false;  //ölü olup olmadığımmı kontrol edeceğim boolean değişkenim
    Transform muzzle; // muzzle objemin transfromuna erişmek için tanımlıyorum
    public Transform bullet,floatingText,bloodParticle; // kurşunum, hasartextim ve kananimasyonum için public bir game objesi oluşturdum ve unity üzerinden objemi bu değişkene atıcam
    public Slider slider; //can barı gösterimim için slider değişkenim
    bool mouseIsNotOverUI;
    GameObject CoinPrefab; // altın toplama animasyonu ayarlamadaki değişkenlerim
    GameObject CoinPanel;
    Rigidbody2D rgb;
    public GameObject inGameScreen, pauseScreen;

    void Start()
    {
        muzzle = transform.GetChild(0); // scriptimin üstünde olduğu objemin transformunun 0 indexli child transformuna atıyorum
        slider.maxValue = health;
        slider.value = health;
        rgb = GetComponent<Rigidbody2D>(); // rgb değişkenimi rigidbodyme veriyorum
    }

    
    void Update() 
    {
        mouseIsNotOverUI = EventSystem.current.currentSelectedGameObject == null;
        if(Input.GetMouseButtonDown(0) && mouseIsNotOverUI)
        {
            ShootBullet();
        }
    }

    public void GetDamage(float damage)  //karakterim için hasar al metodunu oluşturuyorum parametre olarakta enemy objemden gelecek damage yi koyuyorum
    {

        /*karakterim hasar aldığında aldığı hasarı göstermek için instantiate metodumu çağırıyorum ver parametrelerini giriyorum ve textimi oluşturuyorum
* ve bu textimdeki sayıyuı aldığım hasara eşitlemem lazım byüzden return edilen değerimi textmesh metodu ile damageime eşitliyorum
*/
        Instantiate(floatingText, transform.position, Quaternion.identity).GetComponent<TextMesh>().text=damage.ToString();

        /*burada rakipten gelen hasarın karakterimin sağlığından fazla mı az mı olduğunu kontrol ediyorum
         * ve altta oluşturduğum ben ölü müyüm metodumu çağırıyorum aldığı health değerine göre bana bool ifade döndürücek
         */
        if ((health-damage)>=0)  
        {
            health -= damage;
        }
        else
        {
            health = 0;
        }
        slider.value = health;
        AmIDead();
    }

    void AmIDead()
    {
        if(health<=0)
        {
            Destroy(Instantiate(bloodParticle, transform.position, Quaternion.identity),3f); //ölünce çıkan partiküllerimiz
            DataManager.Instance.LoseProcess();
            dead = true;
            Destroy(gameObject);
            inGameScreen.SetActive(false); //gamescreen ekranımı görünmez hale getirip  
            pauseScreen.SetActive(true);
        }
    }

    //public void Retry()
    //{
    //    inGameScreen.SetActive(false); //gamescreen ekranımı görünmez hale getirip  
    //    pauseScreen.SetActive(true);// pausescreen ekranımı görünür hale getiriyorum
    //}
    void ShootBullet()
    {
        Transform tempBullet; // oluşturduğum objeye rahat erişmek için değişken oluşturdum
        tempBullet = Instantiate(bullet, muzzle.position, Quaternion.identity);// ne oluşturacağımı(kurşun),bu kurşun nerde oluşacak ve kurşunun rotasyonu ne olucak bunları belirliyorum
        tempBullet.GetComponent<Rigidbody2D>().AddForce(muzzle.forward * bulletSpeed);
        DataManager.Instance.ShotBullet++;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Coin")  //değdiğim obje gold tagına sahipse objemi yok ediyorum
        {
            DataManager.Instance.FarmCoin++;
            Destroy(other.gameObject);
        //    Instantiate(CoinPrefab, Camera.main.WorldToScreenPoint(transform.position), CoinPanel.transform.rotation, CoinPanel.transform);
            //burada çoğaltmak istediğim coinprefab objemi verdim kameramın yakalamasını istedim açısını belirtmek için coinpanel transfromunu verdim ve hangi objenin altında var olacağını belirttim
        }
    }
}
