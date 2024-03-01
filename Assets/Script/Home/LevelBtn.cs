using UnityEngine;
using UnityEngine.UI;

public class LevelBtn : MonoBehaviour
{
    [SerializeField] private GameObject lockImg;
    [SerializeField] private Transform starRoot;
    [SerializeField] private Sprite completeStar;

    private Button levelBtn;
    private Image[] stars;
    private int levelIndex;

    private void Awake()
    {
        stars = new Image[3];
        levelIndex = transform.GetSiblingIndex();
    }

    private void Start()
    {
        levelBtn = GetComponent<Button>();
        levelBtn.onClick.AddListener(OnClickLevelBtn);


        for(int i = 0; i < starRoot.childCount; i++)
        {
            stars[i] = starRoot.GetChild(i).GetComponent<Image>();
        }

        if (levelIndex > GameData.HighestLevelUnlocked)
        {
            levelBtn.interactable = false;
        }
        else
        {
            if (GameData.LevelInfos[levelIndex].IsComplete)
            {
                SetCompleteStar(GameData.LevelInfos[levelIndex].starCollect);
            }
            levelBtn.interactable = true;
            lockImg.gameObject.SetActive(false);
        }

    }

    private void OnClickLevelBtn() 
    {
        GameManager.Instance.curLevelIndex = transform.GetSiblingIndex();
        LoadGamePlayScene();
    }

    private void SetCompleteStar(int index)
    {
        for(int i = 0; i <= index; i++)
        {
            stars[i].sprite = completeStar;
        }
    }

    private void LoadGamePlayScene()
    {
        GameManager.Instance.LoadScene(1);
    }
}
