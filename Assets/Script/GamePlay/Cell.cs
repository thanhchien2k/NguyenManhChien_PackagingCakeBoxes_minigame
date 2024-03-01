using DG.Tweening;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private bool occupied;
    private Vector2Int cellPosition;
    private MoveObject moveObject;
    public bool Occupied { get => occupied; set => occupied = value; }
    public Vector2Int CellPosition { get => cellPosition; set => cellPosition = value; }
    public MoveObject MoveObject { get => moveObject; set => moveObject = value; }

    private void Awake()
    {
        Occupied = false;
    }
    public Vector2 GetScreenPosition()
    {
        return transform.position;
    }

    public void MoveAnimaion(float _time)
    {
        MoveObject.transform.DOMove(transform.position, _time);
    }

    public void MoveAnimaion(Transform transform, float _time)
    {
        MoveObject.transform.DOMove(transform.position, _time);
    }
}
