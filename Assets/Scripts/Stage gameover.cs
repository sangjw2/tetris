using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class Stage : MonoBehaviour {
  public void addscore(){

    int i = 4;
    while(score > PlayerPrefs.GetInt(i.ToString()) && i>=0){
        if(i == 4){
            PlayerPrefs.SetString((i+50).ToString(),overname.text);
            PlayerPrefs.SetInt(i.ToString(),score);
        } else{
            PlayerPrefs.SetString((i+51).ToString(),PlayerPrefs.GetString((i+50).ToString()));
            PlayerPrefs.SetInt((i+1).ToString(),PlayerPrefs.GetInt(i.ToString()));
            PlayerPrefs.SetString((i+50).ToString(),overname.text);
            PlayerPrefs.SetInt(i.ToString(),score);
        }
        i--;    
    }


  }
  
    public void overscorechange(){
        gameoverscore.text = "score : " + score.ToString();
    }
  


   
}
