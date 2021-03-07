using System.Collections;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{

    public static DataManager Instance;
    private int shotBullet; //bu değişkenimi private yaptım çünkü bu değişkenim için bir prop oluşturacağım ve erişimi prop üzeirnden olacak
    public int totalShotBullet; // bu değişkenime her oyun elde edilen ateşelenen mermi sayısını vericem
    private int enemyKilled; // bu değişkenimi private yaptım çünkü bu değişkenim için bir prop oluşturacağım ve erişimi prop üzerinden olacak
    public int totalEnemyKilled;// bu değişkenime her oyun elde edilen düşman öldürme sayısını vericem
    public int totalFarmCoin;
    private int farmCoin;
    EasyFileSave myFile; // uygulamamın indiği cihazda veri depolamam için oluşturucağım dosyam 

    // Start is called before the first frame update
    void Awake() 
    {
        if(Instance==null) //eğer oyunum başladığında instance değerinin dğeişkeni null ise
        {
            Instance = this; //instance değişkenime bu scriptimi atıyorum
            StartProcess();
        }
        else  //eğer atanmışsa zaten datamanagerim oluşturulmuş
        {
            Destroy(gameObject);//verilerde karışıklık olmaması için gameobjemi destroy ediyorum
        }
        DontDestroyOnLoad(gameObject);//oluşturduğum instanceı her sahnede kullanabilmek için dontdestroyonload metodunu kullanıyorum
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int ShotBullet   //shotbullet değişkenime erişim için propertymi oluşturuyorum
    { 
        get
        {
            return shotBullet;
        }
        set
        {
            shotBullet = value;
            GameObject.Find("ShotBulletText").GetComponent<Text>().text = "SHOT BULLET : " + shotBullet.ToString();
        }
    }

    public int EnemyKilled  //enemykilled değişkenime erişim için propertmi oluşturuyorum
    {
        get
        {
            return enemyKilled;
        }
        set
        {
            enemyKilled = value;
            GameObject.Find("EnemyKilledText").GetComponent<Text>().text = "ENEMY KILLED : " + enemyKilled.ToString();
            WinProcess();
        }
    }
    public int FarmCoin
    {
        get
        {
            return farmCoin;
        }
        set
        {
            farmCoin = value;
            GameObject.Find("FarmCoin").GetComponent<Text>().text = "COİN : " + farmCoin.ToString();
        }
    }

    void StartProcess()
    {
        myFile = new EasyFileSave(); //myfile değişkenime easyfilesave nesnesi oluşturuyorum ve atıyorum
        LoadData();
    }

    public void SaveData()
    {
        totalShotBullet += shotBullet; //mevcut oyunda ateşlenen mermilerimi totele ekliyorum       
        totalEnemyKilled += enemyKilled;//mevcut oyunda öldürrülen düşmanımı totele ekliyorum
        totalFarmCoin += farmCoin;
        myFile.Add("totalShotBullet", totalShotBullet); //myfile objemin add metodunu çağırıyorum input olarakta keyimi ve valuemi veriyorum
        myFile.Add("totalEnemyKilled", totalEnemyKilled);//aynı işlemi tekrarlıyorum
        myFile.Add("totalFarmCoin", totalFarmCoin);

        myFile.Save(); // verileri cihazıma kaydediyorum
    }

    public void LoadData() //burada kaydedilmiş verilerimi geri yüklemek için loaddata metodumu oluşturuyorum
    {
        if(myFile.Load())  //kullandığım hazır save assetinde load yappısı bu şekilde oluşturulduğundan böyle oluşturdum
        {
            totalShotBullet = myFile.GetInt("totalShotBullet"); //totalshotbullet değişkenime myfiledan geri getirdiğim totalshotbulletımı  ,    
            totalEnemyKilled = myFile.GetInt("totalEnemyKilled");//totalenemykilled değişkenime myfiledan geri getirdiğim totalenemykilled değerimi atıyorum
            totalFarmCoin = myFile.GetInt("totalFarmCoin");
        }
    }

    public void WinProcess()
    {
        if(enemyKilled>=5)
        {
            print("KAZANDINIZ!!");
        }
    }

    public void LoseProcess()
    {
        print("KAYBETTİNİZ.");
    }
}
