using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject SettingMenu;
    private bool isSettingOpen;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isSettingOpen)
            {
                ExitSetting();
            }
        }
    }


    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }

    public void OpenSetting()
    {
        SettingMenu.SetActive(true);
        isSettingOpen = true;
    }

    public void ExitSetting()
    {
        SettingMenu.SetActive(false);
        isSettingOpen = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
