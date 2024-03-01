using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayControl : MonoBehaviour
{
    public static GamePlayControl Instance { get; set; }
    public (bool, float) Result { get => result; set => result = value; }

    [SerializeField] private Transform gameBoard;
    [SerializeField] private Transform objectRoot;
    [SerializeField] private GameObject cellPrefabs;
    [SerializeField] private GameObject cakePrefabs;
    [SerializeField] private GameObject giftBoxPrefabs;
    [SerializeField] private GameObject candyPrefabs;
    [SerializeField] private float timeTween;
    [SerializeField] private PopupScreen popupScreen;
    [SerializeField] private Timer timer;

    private (bool, float) result;
    private bool isTweening;
    private LevelConfig curLevelConfig;
    private Cell[,] cells;
    private Cell cakeCellTarget;
    private Cell giftBoxCellTarget;
    Vector2Int curSize;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        result = (false, 0);
    }

    private void Start()
    {
        isTweening = false;
        curLevelConfig = GameManager.Instance.GetLevelConfig();
        curSize = curLevelConfig.BoarSize;
        cells = new Cell[curSize.x, curSize.y];
        CreateCell(curSize);
        CreateObjectToBoard();
    }

    private void Update()
    {
        if (isTweening || result.Item1) return;
        CheckInputKey();
    }

    public void CreateCell(Vector2Int _sizeBoard)
    {
        for (int i = 0; i < _sizeBoard.x; i++)
        {
            for (int j = 0; j < _sizeBoard.y; j++)
            {
                cells[i, j] = Instantiate(cellPrefabs, gameBoard).GetComponent<Cell>();
                cells[i,j].CellPosition = new Vector2Int(i,j);
            }
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)gameBoard);
    }

    private void CreateObjectToBoard()
    {
        for (int i = 0; i < curLevelConfig.CakeIndex.Length; i++)
        {
            CreateMoveObject(cakePrefabs, curLevelConfig.CakeIndex[i], ObjectType.Cake);
        }

        for (int i = 0; i < curLevelConfig.GiftBoxIndex.Length; i++)
        {
            CreateMoveObject(giftBoxPrefabs, curLevelConfig.GiftBoxIndex[i], ObjectType.GiftBox);
        }

        for (int i = 0; i < curLevelConfig.CandyIndex.Length; i++)
        {
            CreateObject(candyPrefabs, curLevelConfig.CandyIndex[i]);
        }
    }

    private void CreateMoveObject(GameObject gameObject, Vector2Int _index, ObjectType _type)
    {
        GameObject obj = Instantiate(gameObject, objectRoot);
        Cell curCell = cells[_index.x, _index.y];
        curCell.Occupied = true;
        curCell.MoveObject = obj.GetComponent<MoveObject>();
        curCell.MoveObject.Type = _type;
        if (_type == ObjectType.Cake)
        {
            cakeCellTarget = curCell;
        }
        else
        {
            giftBoxCellTarget = curCell;
        }

        obj.transform.position = curCell.transform.position;
    }

    private void CreateObject(GameObject gameObject, Vector2Int _index)
    {
        GameObject obj = Instantiate(gameObject, objectRoot);
        cells[_index.x, _index.y].Occupied = true;
        obj.transform.position = cells[_index.x, _index.y].transform.position;
    }

    private void CheckInputKey()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(Vector2Int.up, 1, 1, 0, 1);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Vector2Int.left, 0, 1, 1, 1);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(Vector2Int.down, curSize.x - 2, -1, 0, 1);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(Vector2Int.right, 0, 1, curSize.y - 2, -1);
        }
    }

    private void Move(Vector2Int direction, int startX, int incrementX, int startY, int incrementY)
    {
        for (int x = startX; x >= 0 && x < curSize.x; x += incrementX)
        {
            for (int y = startY; y >= 0 && y < curSize.y; y += incrementY)
            {
                if (cells[x, y].Occupied && cells[x, y].MoveObject != null)
                {
                    MoveObjectInCell(direction, cells[x, y].MoveObject, x, y);
                }
            }
        }

        CheckGetCake(direction.x, direction.y);
    }

    private void MoveObjectInCell(Vector2Int direction, MoveObject moveObject, int posX, int posY)
    {
        Cell targetCell = FindCellCanMoveTo(direction, posX, posY);
        if (targetCell != null)
        {
            if(moveObject.Type == ObjectType.Cake)
            {
                cakeCellTarget = targetCell;
            }
            else
            {
                giftBoxCellTarget = targetCell;
            }
            //moveObject.transform.position = targetCell.transform.position;
            targetCell.MoveObject = moveObject;
            targetCell.Occupied = true;
            cells[posX,posY].MoveObject = null;
            cells[posX, posY].Occupied = false;
        }
    }

    private Cell FindCellCanMoveTo(Vector2Int direction, int posX, int posY)
    {
        Cell targetCell = null;
        Vector2Int newPos = new Vector2Int(posX - direction.y, posY + direction.x);
        while ( (newPos.x >= 0 && newPos.x < curSize.x) && (newPos.y >= 0 && newPos.y < curSize.y) )
        {
            if(!cells[newPos.x, newPos.y].Occupied)
            {
                targetCell = cells[newPos.x, newPos.y];
            }
            else
            {
                break;
            }
            newPos = new Vector2Int(newPos.x - direction.y, newPos.y + direction.x);
        }

        return targetCell;
    }
    
    private void CheckGetCake(int directionX, int directionY)
    {
        isTweening = true;
        if (directionX == 0 && cakeCellTarget.CellPosition.y == giftBoxCellTarget.CellPosition.y && cakeCellTarget.CellPosition.x < giftBoxCellTarget.CellPosition.x)
        {
            if(directionY == 1)
            {
                cakeCellTarget.MoveAnimaion(timeTween);
                giftBoxCellTarget.MoveAnimaion(cakeCellTarget.transform ,timeTween);
            }
            else
            {
                cakeCellTarget.MoveAnimaion(giftBoxCellTarget.transform,timeTween);
                giftBoxCellTarget.MoveAnimaion(timeTween);
            }
            
            result = (true, timer.GetNumberOfStar());

            DOVirtual.DelayedCall(timeTween, () =>
            {
                cakeCellTarget.MoveObject.gameObject.SetActive(false);
                PopupFinishGameScreen();
            });

            return;
        }

        cakeCellTarget.MoveAnimaion(timeTween);
        giftBoxCellTarget.MoveAnimaion(timeTween);

        DOVirtual.DelayedCall(timeTween, () =>
        {
            isTweening = false;
        });

    }

    public void PopupFinishGameScreen()
    {
        if(result.Item2 != 0 )
        {
            if(GameManager.Instance.curLevelIndex == GameData.HighestLevelUnlocked)
            {
                GameData.LevelInfos[GameData.HighestLevelUnlocked].IsComplete = true;
                GameData.LevelInfos[GameManager.Instance.curLevelIndex].starCollect = (int)result.Item2;
                GameData.HighestLevelUnlocked++;
            }
            else
            {
                if(result.Item2 > GameData.LevelInfos[GameManager.Instance.curLevelIndex].starCollect)
                {
                    GameData.LevelInfos[GameManager.Instance.curLevelIndex].starCollect = (int)result.Item2;
                }
            }
        }

        popupScreen.gameObject.SetActive(true);
    }

    public void OnClickBackBtn()
    {
        GameManager.Instance.LoadScene("Home");
    }

    public void OnClickRePlayBtn()
    {
        GameManager.Instance.ReloadScene();
    }

    public void OnClickNextBtn()
    {
        GameManager.Instance.curLevelIndex++;
        GameManager.Instance.ReloadScene();
    }
}

