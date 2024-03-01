using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeManager : MonoBehaviour
{
    [SerializeField] private GameObject homeScreen;
    [SerializeField] private GameObject selectLevelScreen;

    public void OnClickPlayBtn()
    {
        homeScreen.SetActive(false);
        selectLevelScreen.SetActive(true);
    }

    public void OnClickBackToHomeBtn()
    {
        selectLevelScreen.SetActive(false);
        homeScreen.SetActive(true);
    }
}
