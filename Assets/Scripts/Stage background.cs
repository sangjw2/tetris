using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Stage : MonoBehaviour

{
    void ClearRows() {
        for (int i = 1; i < boardNode.childCount; ++i)
        {
            var column = boardNode.Find(i.ToString());

            // 이미 비어 있는 행은 무시
            if (column.childCount == 0)
                continue;

            int emptyCol = 0;
            int j = i - 1;
            while (j >= 0)
            {
                if (boardNode.Find(j.ToString()).childCount == 0)
                {
                    emptyCol++;
                }
                j--;
            }

            if (emptyCol > 0)
            {
                var targetColumn = boardNode.Find((i - emptyCol).ToString());

                while (column.childCount > 0)
                {
                    Transform tile = column.GetChild(0);
                    tile.parent = targetColumn;
                    tile.transform.position += new Vector3(0, -emptyCol, 0);
                }
                column.DetachChildren();
            }
        }
    }

     void CreateBackground()
    {
        Color color = Color.gray;

        // 타일 보드
        color.a = 0.5f;
        for (int x = -halfWidth; x < halfWidth; ++x)
        {
            for (int y = halfHeight; y > -halfHeight; --y)
            {
                CreateTile(backgroundNode, new Vector2(x, y), color, 0);
            }
        }

        //홀드와 넥스트 타일 생성
        for (int x = halfWidth+1; x < halfWidth+5; ++x)
        {
            for (int y = halfHeight-1; y > -halfHeight; --y)
            {
                CreateTile(backgroundNode, new Vector2(x, y), color, 0);
            }
        }

        for (int x = -halfWidth-2; x > -halfWidth-6; --x)
        {
            for (int y = halfHeight-1; y > halfHeight-5; --y)
            {
                CreateTile(backgroundNode, new Vector2(x, y), color, 0);
            }
        }

        // 좌우 테두리
        color.a = 1.0f;
        for (int y = halfHeight; y > -halfHeight; --y)
        {
            CreateTile(backgroundNode, new Vector2(-halfWidth - 1, y), color, 0);
            CreateTile(backgroundNode, new Vector2(halfWidth, y), color, 0);
            CreateTile(backgroundNode, new Vector2(halfWidth+5, y), color, 0); //우측 바깥
        }

        // 아래 테두리
        for (int x = -halfWidth - 1; x <= halfWidth+5; ++x)
        {
            CreateTile(backgroundNode, new Vector2(x, -halfHeight), color, 0);
        }

         for (int x = -halfWidth-2; x > -halfWidth-7; --x) //좌측 바깥 상하
        {
            CreateTile(backgroundNode, new Vector2(x, halfHeight), color, 0);
            CreateTile(backgroundNode, new Vector2(x, halfHeight-5), color, 0);
        }

        for (int x = halfWidth+1; x < halfWidth+5; ++x)//우측 바깥 가운데
        {
            for(int y = 0; y<4; y++){
                CreateTile(backgroundNode, new Vector2(x, halfHeight-y*5), color, 0);
            }
        }

        for (int y = halfHeight-1; y > halfHeight-5; --y)
        {
            CreateTile(backgroundNode, new Vector2(-halfWidth-6, y), color, 0);
        }
    
    
    
    
    }

    void UpdateGhostTetromino()
{
    // 고스트 블록 초기화
    foreach (Transform child in ghostTetrominoNode)
    {
        Destroy(child.gameObject);
    }

    // 고스트 테트로미노의 위치를 현재 테트로미노의 위치로 설정
    ghostTetrominoNode.position = tetrominoNode.position;
    ghostTetrominoNode.rotation = tetrominoNode.rotation;

    // 고스트 테트로미노의 타일 생성
    foreach (Transform child in tetrominoNode)
    {
        Vector3 localPos = child.localPosition;
        CreateTile(ghostTetrominoNode, localPos, new Color(1, 1, 1, 0.3f), 1, true); // 반투명 흰색
    }

    // 고스트 테트로미노를 가능한 가장 아래로 이동
    while (CanMoveTo(ghostTetrominoNode))
    {
        ghostTetrominoNode.position += Vector3.down;
    }
    ghostTetrominoNode.position -= Vector3.down; // 한 칸 위로 이동하여 정확한 위치로 설정
    }
    // 보드에 완성된 행이 있으면 삭제
    void CheckBoardColumn()
    {
        bool isCleared = false;

        // 완성된 행 == 행의 자식 갯수가 가로 크기
        foreach (Transform column in boardNode)
        {
            if (column.childCount == boardWidth)
            {


                foreach (Transform tile in column)
                {
                    if(tile.GetComponent<Tile>().color==Color.white && !itemActived) {
                        Destroy(tile.gameObject);
                        GetScore(2);
                        GrantItem();
                    }else {
                        Destroy(tile.gameObject);
                        GetScore();
                    }
                    
                }

                column.DetachChildren();
                isCleared = true;
            }
        }

        // 비어 있는 행이 존재하면 아래로 당기기
        if (isCleared)
        {
            ClearRows();
        }
    }



}
