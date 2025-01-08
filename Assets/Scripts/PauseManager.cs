using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu; // Referensi ke Panel Pause Menu

    private bool isPaused = false;

    void Update()
    {
        // Deteksi jika tombol Escape ditekan
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // Memberhentikan waktu dalam game
        pauseMenu.SetActive(true); // Menampilkan Pause Menu
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // Mengembalikan waktu normal
        pauseMenu.SetActive(false); // Menyembunyikan Pause Menu
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game"); // Debug untuk uji
        Application.Quit(); // Keluar dari game (tidak berfungsi di editor)
    }
}
