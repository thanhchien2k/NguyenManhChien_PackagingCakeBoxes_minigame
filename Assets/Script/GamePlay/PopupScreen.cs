using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupScreen : MonoBehaviour
{
    [SerializeField] private Transform winPopup;
    [SerializeField] private Transform failedPopup;
    [SerializeField] private GameObject bg;
    [SerializeField] private float timePopup;
    [SerializeField] private Image[] stars;
    [SerializeField] private Sprite completeStar;

    private void Start()
    {
        bg.SetActive(true);

        if(GamePlayControl.Instance.Result.Item2 == 0)
        {
            PopupAnimation(failedPopup, timePopup);
        }
        else
        {
            for(int i =0; i<= GamePlayControl.Instance.Result.Item2; i++)
            {
                stars[i].sprite = completeStar;
            }
            PopupAnimation(winPopup, timePopup);
        }
    }

    private void PopupAnimation(Transform transform, float time)
    {
        transform.DOScale(Vector2.one, time);
    }
}
