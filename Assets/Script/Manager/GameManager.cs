using UnityEngine;
using Framework;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class GameManager : Singleton<GameManager>
{

    [SerializeField] private List<LevelConfig> levelConfigs;
    public int curLevelIndex;

    private  void Start()
    {
        //GameData.LevelInfos = new();
        //GameData.HighestLevelUnlocked = 0;
        for ( int i = GameData.LevelInfos.Count; i< levelConfigs.Count; i++)
        {
            GameData.LevelInfos.Add(new LevelInfo { IsComplete = false, starCollect = 0 }); 
        }
    }
    public void LoadScene(int _index)
    {
        SceneManager.LoadScene(_index);
    }

    public void LoadScene(string _name)
    {
        SceneManager.LoadScene(_name);
    }

    public void ReloadScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public LevelConfig GetLevelConfig()
    {
        if (curLevelIndex >= levelConfigs.Count) curLevelIndex =0;
        return levelConfigs[curLevelIndex];
    }
}
