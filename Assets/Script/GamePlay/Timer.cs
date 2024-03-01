using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [Range(10f, 59f)]
    [SerializeField]private float timeCountDown;

    private float timer;
    [SerializeField] private TextMeshProUGUI textMeshPro;

    private void Start()
    {
        timer = timeCountDown;
        UpdateTime((int)timer);
    }

    private void Update()
    {
        if(GamePlayControl.Instance.Result.Item1) return;
        timer -= Time.deltaTime;
        UpdateTime((int)timer);
        if(timer <= 0)
        {
            timer = 0;
            GamePlayControl.Instance.Result = (true,0);
            GamePlayControl.Instance.PopupFinishGameScreen();
            Debug.Log("Failded");
        }
    }

    private void UpdateTime(int time)
    {
        textMeshPro.text = "00 : " + time /10 + time%10;
    }

    public int GetNumberOfStar()
    {
        return (int)((timer / timeCountDown) * 3);
    }
}
