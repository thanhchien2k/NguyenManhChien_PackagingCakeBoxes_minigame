using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="LevelConfig")]
public class LevelConfig : ScriptableObject
{
    public Vector2Int BoarSize;
    public Vector2Int[] CakeIndex;
    public Vector2Int[] GiftBoxIndex;
    public Vector2Int[] CandyIndex;
}
