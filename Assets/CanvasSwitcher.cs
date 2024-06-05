//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class NewBehaviourScript : MonoBehaviour
//{
//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }
//}


using UnityEngine;

public class CanvasSwitcher : MonoBehaviour
{
    public Canvas Main;  // 시작 화면 캔버스
    public Canvas Canvas;   // 게임 화면 캔버스

    public void StartGame()
    {
        // 시작 화면 캔버스를 비활성화하고 게임 화면 캔버스를 활성화
        Main.gameObject.SetActive(false);
        Canvas.gameObject.SetActive(true);
    }
}
