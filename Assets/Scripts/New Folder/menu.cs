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
   



 

}
