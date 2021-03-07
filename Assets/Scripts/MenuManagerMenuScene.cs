using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManagerMenuScene : MonoBehaviour
{
    public GameObject dataBoard;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton() // unity arayüzünden erişebilmek için public bir playbutton metod tanımlıyorum. 
    {
        SceneManager.LoadScene(1);
    }

    public void DataBoardButton()
    {
        DataManager.Instance.LoadData();//datamanagerdeki loaddata metodumu çağırıyorum böylelikle en son kaydedilen verileri çağırıyorum
        dataBoard.transform.GetChild(1).GetComponent<Text>().text ="TotalShotBullet : " + DataManager.Instance.totalShotBullet.ToString();//buralarda ise databoardumda oluşturduğum textlere,
        dataBoard.transform.GetChild(2).GetComponent<Text>().text = "TotalEnemyKilled : " + DataManager.Instance.totalEnemyKilled.ToString();//datamanagerımda bulunan değerleri atıyorum
        dataBoard.transform.GetChild(3).GetComponent<Text>().text = "TotalFarmCoin : " + DataManager.Instance.totalFarmCoin.ToString();
        dataBoard.SetActive(true);
       
    }

    public void XButton()
    {
        dataBoard.SetActive(false);
    }

}
