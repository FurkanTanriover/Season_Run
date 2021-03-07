using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManagerInGame : MonoBehaviour
{
    public GameObject inGameScreen, pauseScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseButton()  //oyundu durdurma butonum için metodumu oluşturuyorum
    {
        Time.timeScale = 0;   // durdurma işlemi için time.timescale i 0 a eşitlişyorum    
        inGameScreen.SetActive(false); //gamescreen ekranımı görünmez hale getirip  
        pauseScreen.SetActive(true);// pausescreen ekranımı görünür hale getiriyorum
    }

    public void PlayButton() //oyunu başlatma butonum için metodumu oluşturuyorum   
    {
        Time.timeScale = 1;  //yürütme işlemi için time.timescale i 1 e tekrardan getiriyorum
        inGameScreen.SetActive(true); //gamescreen ekranımı aktif ediyorum
        pauseScreen.SetActive(false);//pausescreen ekranımı deaktif ediyorum
    }

    public void RePlayButton()
    {
        Time.timeScale = 1;  // replay yaptığımda oyunum devam etsin diye tekrardan 1 e eşitliyorum çünkü pause basınca 0 a dönüyor
        SceneManager.LoadScene(int.Parse(SceneManager.GetActiveScene().name)); //oyun ekranımı tekrardan yüklüyorum
    }

    public void HomeButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        DataManager.Instance.SaveData();//datalarımı save ediyorum
    }

    
}
