using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menu : MonoBehaviour
{
   


   public void gogameplay(){
       SceneManager.LoadScene("SampleScene");
      }

   public void goranking(){
       SceneManager.LoadScene("ranking");
      }
    public void exitGame(){
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit(); // 어플리케이션 종료
    #endif
   }
   



 

}
