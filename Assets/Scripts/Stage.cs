using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public partial class Stage : MonoBehaviour
{
    [Header("Editor Objects")]
    public GameObject tilePrefab;
    public GameObject ghostTilePrefab; // 고스트 타일 프리팹 추가
    public Transform backgroundNode;
    public Transform boardNode;
    public Transform tetrominoNode;
    public Transform tetrominoqueue;
    public Transform next;
    public Transform hold;
    public GameObject gameoverPanel;
    public GameObject stop;
    public GameObject scoreobject;
    public GameObject nameinput;
    public Text scoretext;
    public Slider doubleTimeSlider; // 슬라이더 추가
    public Text sliderText;
    public Text gameoverscore;
    public Text overname;
    int score = 0;
    
    int changenum = 0;
    int holdnum = 0;
    int holdnum2 = 0;
    int holdnum3 = 0;
    bool dohold = false;
    int[] numbers = { 0, 1, 2, 3, 4, 5, 6 };
    int[] nextnumbers = { 0, 1, 2, 3 };

    [Header("Game Settings")]
    [Range(4, 40)]
    public int boardWidth = 40;
    [Range(5, 20)]
    public int boardHeight = 20;
    public float fallCycle = 1.0f;
    public float firstdelay = 1.0f;
    public float moveCycle = 0.1f; // 추가: 이동 사이클
    public float repeatDelay = 0.3f; // 추가: 키를 누르고 있을 때의 초기 지연 시간

    public int halfWidth;
    public int halfHeight;

    public float nextFallTime;
    public float nextMoveTime; // 추가: 다음 이동 시간
    public float nextRepeatTime; // 추가: 다음 반복 시간

    public Transform ghostTetrominoNode; // 추가: 고스트 테트로미노 노드

    // 아이템 관련
    public bool isItem = false;
    public bool itemActived = false;

    

    public void Start()
    {
        gameoverPanel.SetActive(false);
        stop.SetActive(false);
        scoreobject.SetActive(true);
        SetText();
        halfWidth = Mathf.RoundToInt(boardWidth * 0.5f);
        halfHeight = Mathf.RoundToInt(boardHeight * 0.5f);

        nextFallTime = Time.time + fallCycle;
        nextMoveTime = 0f; // 초기화: 다음 이동 시간
        nextRepeatTime = 0f; // 초기화: 다음 반복 시간

        ghostTetrominoNode = new GameObject("GhostTetromino").transform; // 고스트 테트로미노 노드 초기화
        ghostTetrominoNode.SetParent(transform);

        CreateBackground();

        // 아이템 UI 초기화
        itemIcon.enabled = false;
        // 슬라이더 아이콘 숨기기
        doubleTimeSlider.gameObject.SetActive(false);
        sliderText.gameObject.SetActive(false);

        for (int i = 0; i < boardHeight; ++i)
        {
            var col = new GameObject((boardHeight - i - 1).ToString());
            col.transform.position = new Vector3(0, halfHeight - i, 0);
            col.transform.parent = boardNode;
        }

        numsupple();

        for (int i = 0; i < 4; ++i)
        {
            numaddd();
        }

        CreateTetromino();
    }

    void doubleScoreEnd() {
        Debug.Log("30초 지남");
        itemActived = false;
        doubleTimeSlider.gameObject.SetActive(false); // 30초 후 슬라이더 숨기기
        sliderText.gameObject.SetActive(false);
        currentItem = ItemType.None;
        itemIcon.enabled = false; 
    }

    void speedUpEnd() {
        Debug.Log("25초 지남");
        itemActived = false;
        Time.timeScale = 1;
        currentItem = ItemType.None;
        itemIcon.enabled = false;
        moveCycle = 0.1f;
        repeatDelay = 0.3f;
        speedUpEndTime = 0f;
    }
    
    void Update()
{
    

    UpdateGhostTetromino(); // 고스트 블록 업데이트

    Vector3 moveDir = Vector3.zero;
    bool isRotate = false;
    bool isClockwise = true;

    // 스페이스바 입력 처리
    if (Input.GetKeyDown(KeyCode.Space) && gameoverPanel.activeSelf == false && stop.activeSelf == false)
    {
        UseItem();
    }

    // 점수 두배 아이템 효과 시간 체크
    if (itemActived && currentItem == ItemType.DoubleScore && Time.time > doubleScoreEndTime)
    {
        doubleScoreEnd();
    } else if (itemActived && Time.time <= doubleScoreEndTime) {doubleTimeSlider.value = doubleScoreEndTime - Time.time;Debug.Log(Time.time);}

    // 속도 증가 아이템 효과 시간 체크
    if (itemActived && currentItem == ItemType.SpeedUp && Time.time > speedUpEndTime && gameoverPanel.activeSelf == false && stop.activeSelf == false)
    {
        speedUpEnd();
        
    }

    // 이동 입력 처리
    if (Input.GetKey(KeyCode.LeftArrow) && Time.time >= nextMoveTime && gameoverPanel.activeSelf == false && stop.activeSelf == false)
    {
        moveDir.x = -1;
        nextMoveTime = Time.time + moveCycle;
        nextRepeatTime = Time.time + repeatDelay; // 초기 지연 시간 설정
    }
    else if (Input.GetKey(KeyCode.RightArrow) && Time.time >= nextMoveTime && gameoverPanel.activeSelf == false && stop.activeSelf  == false)
    {
        moveDir.x = 1;
        nextMoveTime = Time.time + moveCycle;
        nextRepeatTime = Time.time + repeatDelay; // 초기 지연 시간 설정
    }

    // 빠르게 반복하는 이동 처리
    if (Input.GetKey(KeyCode.LeftArrow) && Time.time >= nextRepeatTime && gameoverPanel.activeSelf == false && stop.activeSelf == false)
    {
        moveDir.x = -1;
        nextRepeatTime = Time.time + moveCycle;
    }
    else if (Input.GetKey(KeyCode.RightArrow) && Time.time >= nextRepeatTime && gameoverPanel.activeSelf == false && stop.activeSelf  == false)
    {
        moveDir.x = 1;
        nextRepeatTime = Time.time + moveCycle;
    }

    // 회전 입력 처리
    if (Input.GetKeyDown(KeyCode.C) && gameoverPanel.activeSelf == false && stop.activeSelf == false)
    {
        isRotate = true;
        isClockwise = true; // 시계 방향 회전
    }
    else if (Input.GetKeyDown(KeyCode.Z) && gameoverPanel.activeSelf == false && stop.activeSelf == false)
    {
        isRotate = true;
        isClockwise = false; // 반시계 방향 회전
    }
    else if (Input.GetKey(KeyCode.DownArrow) && Time.time >= nextMoveTime && gameoverPanel.activeSelf == false && stop.activeSelf == false)
    {
        moveDir.y = -1;
        nextMoveTime = Time.time + moveCycle;
    }

    if (Input.GetKeyDown(KeyCode.UpArrow) && gameoverPanel.activeSelf == false && stop.activeSelf == false)
    {
        while (MoveTetromino(Vector3.down, false))
        {
        }
    }

    if (Input.GetKeyDown(KeyCode.Q) && gameoverPanel.activeSelf == false && stop.activeSelf == false)
    {
        if (dohold == false)
        {
            minohold();
        }
    }

    if (Input.GetKeyDown(KeyCode.Return) && nameinput.activeSelf && gameoverPanel.activeSelf == true && stop.activeSelf == false) 
    {
       addscore();
       nameinput.SetActive(false);
    }

    if (Input.GetKeyDown(KeyCode.Escape) && gameoverPanel.activeSelf == false && stop.activeSelf == false)
        {
            nextFallTime = 60000000000000000;
            stop.SetActive(true);
        }

    // 아래로 떨어지는 경우는 강제로 이동시킵니다.
    if (Time.time > nextFallTime)
    {
        nextFallTime = Time.time + fallCycle;
        moveDir = Vector3.down;
        isRotate = false;
    }

    if (moveDir != Vector3.zero || isRotate)
    {
        MoveTetromino(moveDir, isRotate, isClockwise);
    }
}

    bool MoveTetromino(Vector3 moveDir, bool isRotate, bool isClockwise = true)
{
    Vector3 oldPos = tetrominoNode.transform.position;
    Quaternion oldRot = tetrominoNode.transform.rotation;

    tetrominoNode.transform.position += moveDir;
    if (isRotate)
    {
        float angle = isClockwise ? -90 : 90;
        tetrominoNode.transform.rotation *= Quaternion.Euler(0, 0, angle);
    }

    if (!CanMoveTo(tetrominoNode))
    {
        tetrominoNode.transform.position = oldPos;
        tetrominoNode.transform.rotation = oldRot;

        if ((int)moveDir.y == -1 && (int)moveDir.x == 0 && isRotate == false)
        {
            AddToBoard(tetrominoNode);
            CheckBoardColumn();
            CreateTetromino();
            dohold = false;
            if (!CanMoveTo(tetrominoNode))
            {
                gameoverPanel.SetActive(true);
                scoreobject.SetActive(false);
                overscorechange();
                nextFallTime = 60000000000000000;
            }
        }

        return false;
    }

    return true;
}

  

    // 이동 가능한지 체크
    bool CanMoveTo(Transform root)
    {
        for (int i = 0; i < root.childCount; ++i)
        {
            var node = root.GetChild(i);
            int x = Mathf.RoundToInt(node.transform.position.x + halfWidth);
            int y = Mathf.RoundToInt(node.transform.position.y + halfHeight - 1);

            if (x < 0 || x > boardWidth - 1)
                return false;

            if (y < 0)
                return false;

            var column = boardNode.Find(y.ToString());

            if (column != null && column.Find(x.ToString()) != null)
                return false;
        }

        return true;

    }

    // 타일 생성
    Tile CreateTile(Transform parent, Vector2 position, Color color, int order = 1, bool isGhost = false)
{
    GameObject prefab = isGhost ? ghostTilePrefab : tilePrefab;
    var go = Instantiate(prefab);
    go.transform.parent = parent;
    go.transform.localPosition = position;

    var tile = go.GetComponent<Tile>();
    tile.color = color;
    tile.sortingOrder = order;

    return tile;
}


    // 배경 타일을 생성
    
    public void GetScore(int bonus=1)
    {
        if (itemActived && currentItem == ItemType.DoubleScore)
        {
            bonus *= 2;
        }
        score += 100*bonus;
        SetText();
    }
    
    public void SetText()
    {
        scoretext.text = score.ToString();
    }
}