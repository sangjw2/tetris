using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ranking : MonoBehaviour
{
    [Header("Editor Objects")]
    public GameObject exitbutton;
    public GameObject resetbutton;
    public Text firstname;
    public Text firstscore;
    public Text secondsname;
    public Text secondscore;
    public Text thirdname;
    public Text thirdscore;
    public Text fourthname;
    public Text fourthscore;
    public Text fifthname;
    public Text fifthscore;

    public void Start(){
        setscore();
        
    }
    
   


    public void gomainmenu(){
       SceneManager.LoadScene("New Scene");
    }
    public void setscore(){
        firstname.text = PlayerPrefs.GetInt("0").ToString();
        firstscore.text = PlayerPrefs.GetString("50");
        secondsname.text = PlayerPrefs.GetInt("1").ToString();
        secondscore.text = PlayerPrefs.GetString("51");
        thirdname.text = PlayerPrefs.GetInt("2").ToString();
        thirdscore.text = PlayerPrefs.GetString("52");
        fourthname.text = PlayerPrefs.GetInt("3").ToString();
        fourthscore.text = PlayerPrefs.GetString("53");
        fifthname.text = PlayerPrefs.GetInt("4").ToString();
        fifthscore.text = PlayerPrefs.GetString("54");
    }

    public void resetscore(){
        PlayerPrefs.SetInt(0.ToString(),0);
        PlayerPrefs.SetInt(1.ToString(),0);
        PlayerPrefs.SetInt(2.ToString(),0);
        PlayerPrefs.SetInt(3.ToString(),0);
        PlayerPrefs.SetInt(4.ToString(),0);
        PlayerPrefs.SetString(50.ToString(),"null");
        PlayerPrefs.SetString(51.ToString(),"null");
        PlayerPrefs.SetString(52.ToString(),"null");
        PlayerPrefs.SetString(53.ToString(),"null");
        PlayerPrefs.SetString(54.ToString(),"null");
         
        setscore();
    }
   

   
   

}




