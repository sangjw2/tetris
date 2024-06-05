using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Stage : MonoBehaviour

{

        void numsupple(){
        
        for (int i = 7 - 1; i > 0; i--) {
        int j = Random.Range(0, 7); // 0부터 i까지의 무작위 인덱스 선택
        int temp = numbers[i]; // 현재 요소를 temp에 저장
        numbers[i] = numbers[j]; // 현재 요소에 무작위로 선택한 요소의 값을 할당
        numbers[j] = temp; // 무작위로 선택한 요소에 temp의 값을 할당
        }
    }

        void killnum(){
            
            for (int i = 0; i < next.childCount; i++) {
                Transform child = next.GetChild(i);

                foreach (Transform grandChild in child) {
                    Destroy(grandChild.gameObject);
                }
            }

        
        }
   
      // 테트로미노를 보드에 추가
    void AddToBoard(Transform root)
    {
        while (root.childCount > 0)
        {
            var node = root.GetChild(0);

            int x = Mathf.RoundToInt(node.transform.position.x + halfWidth);
            int y = Mathf.RoundToInt(node.transform.position.y + halfHeight - 1);

            node.parent = boardNode.Find(y.ToString());

            if(isItem == true) { // 아이템 블럭을 생성해야할 경우
                var tile = node.GetComponent<Tile>();
                tile.color = Color.white; // 아이템 속성을 지닌 블럭의 색상을 흰색으로 변경
            }

            node.name = x.ToString();
        }
        isItem = false; // 아이템 블럭임을 나타내는 속성 초기화 
    }


     void minohold() {
        dohold = true;
        if(hold.childCount > 0){
            for(int i = 0; i<4; i++){
                Destroy(hold.GetChild(i).gameObject);
            }
            int maxax = holdnum;
            Color32 color = Color.white;

            positionfunc(hold, -8, halfHeight - 3, maxax);
        
            MakeMino(hold, maxax, color);
            for(int i = 0; i<4; i++){
                Destroy(tetrominoNode.GetChild(i).gameObject);
            }

            maxax = holdnum2;
            color = Color.white;
            tetrominoNode.rotation = Quaternion.identity;

            positionfunc(tetrominoNode, 0, halfHeight, maxax);
            MakeMino(tetrominoNode, maxax, color);
            holdnum3 = holdnum2;
            holdnum2 = holdnum;
            holdnum = holdnum3;
 
        } else {
            int maxax = holdnum;
            holdnum2 = holdnum;
            Color32 color = Color.white;

            positionfunc(hold, -8, halfHeight - 3, maxax);
          
        
            MakeMino(hold, maxax, color);
            for(int i = 0; i<4; i++){
                Destroy(tetrominoNode.GetChild(i).gameObject);
            }

            CreateTetromino();
        }
    }
    

    void numaddd() {
        
        nextnumbers[3] = nextnumbers[2];
        nextnumbers[2] = nextnumbers[1];
        nextnumbers[1] = nextnumbers[0];
        nextnumbers[0] = numbers[changenum];

        changenum++;
        if(changenum > 6){
            changenum = 0;
            numsupple();
        }
    }

    void MakeMino(Transform parent, int index, Color color){
        switch (index)
        {
            // I : 하늘색
            case 0:
                color = new Color32(115, 251, 253, 255);
                CreateTile(parent, new Vector2(-1.5f, 0.5f), color);
                CreateTile(parent, new Vector2(-0.5f, 0.5f), color);
                CreateTile(parent, new Vector2(0.5f, 0.5f), color);
                CreateTile(parent, new Vector2(1.5f, 0.5f), color);
                break;

            // J : 파란색
            case 1:
                color = new Color32(0, 33, 245, 255);
                CreateTile(parent, new Vector2(-1f, 0.0f), color);
                CreateTile(parent, new Vector2(0f, 0.0f), color);
                CreateTile(parent, new Vector2(1f, 0.0f), color);
                CreateTile(parent, new Vector2(-1f, 1.0f), color);
                break;

            // L : 귤색
            case 2:
                color = new Color32(243, 168, 59, 255);
                CreateTile(parent, new Vector2(-1f, 0.0f), color);
                CreateTile(parent, new Vector2(0f, 0.0f), color);
                CreateTile(parent, new Vector2(1f, 0.0f), color);
                CreateTile(parent, new Vector2(1f, 1.0f), color);
                break;

            // O : 노란색
            case 3:
                color = new Color32(255, 253, 84, 255);
                CreateTile(parent, new Vector2(-0.5f, -0.5f), color);
                CreateTile(parent, new Vector2(0.5f, -0.5f), color);
                CreateTile(parent, new Vector2(-0.5f, 0.5f), color);
                CreateTile(parent, new Vector2(0.5f, 0.5f), color);
                break;

            // S : 녹색
            case 4:
                color = new Color32(117, 250, 76, 255);
                CreateTile(parent, new Vector2(-1f, 0f), color);
                CreateTile(parent, new Vector2(0f, 0f), color);
                CreateTile(parent, new Vector2(0f, 1f), color);
                CreateTile(parent, new Vector2(1f, 1f), color);
                break;

            // T : 자주색
            case 5:
                color = new Color32(155, 47, 246, 255);
                CreateTile(parent, new Vector2(-1f, 0f), color);
                CreateTile(parent, new Vector2(0f, 0f), color);
                CreateTile(parent, new Vector2(1f, 0f), color);
                CreateTile(parent, new Vector2(0f, 1f), color);
                break;

            // Z : 빨간색
            case 6:
                color = new Color32(235, 51, 35, 255);
                CreateTile(parent, new Vector2(-1f, 1f), color);
                CreateTile(parent, new Vector2(0f, 1f), color);
                CreateTile(parent, new Vector2(0f, 0f), color);
                CreateTile(parent, new Vector2(1f, 0f), color);
                break;
        }

    }
   
    void CreateNext2(){
        for(int i = 0; i<4; i++){
            
            
           
            CreateNext(i);
            while (tetrominoqueue.childCount > 0) {

                var node = tetrominoqueue.GetChild(0);

                int x = Mathf.RoundToInt(node.transform.position.x);

                node.parent = next.Find(i.ToString());
                node.name = x.ToString();
            }

        }
            
    }

    void CreateNext(int j)
    {
        int maxax = nextnumbers[j];
        Color32 color = Color.white;

        
        if(maxax == 0){
            tetrominoqueue.position = new Vector2(halfWidth+2.5f, -halfHeight + 1.5f + j*5);
        }
        else if(maxax ==3){
            tetrominoqueue.position = new Vector2(halfWidth+2.5f, -halfHeight + 2.5f + j*5);
        }
        else{
            tetrominoqueue.position = new Vector2(halfWidth+2, -halfHeight + 2 + j*5);
        }
        
        MakeMino(tetrominoqueue, maxax, color);
    }
    
    void positionfunc(Transform parent, int ax, int ay, int sort){
        
        
        if(sort == 3){
            parent.position = new Vector2(ax-0.5f, ay+0.5f);
        } 
        else if(sort == 0) {
            parent.position = new Vector2(ax-0.5f, ay-0.5f);
        }
        
        else {
            parent.position = new Vector2(ax-1, ay);
            
        }
    }
    

    // 테트로미노 생성
    void CreateTetromino()
    {   
        if(Random.Range(0,10) < 1 && itemActived == false) { // 10%로 아이템을 포함한 테트로미노 생성 && itemActived==false
            isItem = true;
        }
       
        
        int maxax = nextnumbers[3];
        Color32 color = Color.white;
        tetrominoNode.rotation = Quaternion.identity;
        positionfunc(tetrominoNode, 0, halfHeight, maxax);
        holdnum = nextnumbers[3];
        numaddd();
        killnum();
        CreateNext2();
        MakeMino(tetrominoNode, maxax, color);
    }
}


