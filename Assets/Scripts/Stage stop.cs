using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class Stage : MonoBehaviour

{
  
  public void gogame(){
    nextFallTime = 1;
    stop.SetActive(false);
  }
  public void restartgame(){
    SceneManager.LoadScene(0);
    speedUpEnd();
    doubleScoreEnd();
   }
   public void gomenu(){
    SceneManager.LoadScene("New Scene");

   }
  public void sdfsdfsdf(){
   if (Input.GetKeyDown(KeyCode.Return)){
    
  }
  }
}

