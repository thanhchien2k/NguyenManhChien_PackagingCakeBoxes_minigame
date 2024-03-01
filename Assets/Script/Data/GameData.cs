using Framework;
using System.Collections.Generic;
using UnityEngine;

public class GameData : PDataBlock<GameData>
{
    [SerializeField] public List<LevelInfo> levelInfos; public static List<LevelInfo> LevelInfos { get { return Instance.levelInfos; } set { Instance.levelInfos = value; } }
    [SerializeField] public int highestLevelUnlocked; public static int HighestLevelUnlocked { get { return Instance.highestLevelUnlocked; } set { Instance.highestLevelUnlocked = value; } }

    protected override void Init()
    {
        base.Init();
        Instance.levelInfos = Instance.levelInfos ?? new List<LevelInfo>();
    }
}

[System.Serializable]
public class LevelInfo
{
    public bool IsComplete;
    public int starCollect;
}

