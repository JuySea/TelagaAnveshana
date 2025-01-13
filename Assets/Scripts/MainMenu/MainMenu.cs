using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        Debug.Log("New Game started");
        // Ganti "GameScene" dengan nama scene game utama
        SceneManager.LoadScene("Level 1");
    }

    public void LoadGame()
    {
        Debug.Log("Load Game selected");
        // Tambahkan logika untuk memuat data game yang disimpan
        // Contoh sederhana:
        // Load data dari file atau PlayerPrefs
    }

    public void OpenSettings()
    {
        Debug.Log("Settings menu opened");
        // Tambahkan logika untuk menampilkan menu pengaturan
        // Misalnya, aktifkan panel pengaturan
    }

    public void ExitGame()
    {
        Debug.Log("Game exited");
        Application.Quit();
    }
}
