using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject SettingMenu;
    public GameObject difficultyMenu;
    public GameObject confirmationMenu;

    private bool isSettingOpen;
    private bool isDifficultyOpen;
    private bool isConfirmationOpen;

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

    public void ExitSetting()
    {
        SettingMenu.SetActive(false);
        isSettingOpen = false;
    }

    public void OpenSetting()
    {
        SettingMenu.SetActive(true);
        isSettingOpen = true;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }



}
