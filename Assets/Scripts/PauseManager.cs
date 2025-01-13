using UnityEngine;
using UnityEngine.SceneManagement;  // Untuk mengganti scene (jika Anda ingin implementasi restart atau kembali ke menu utama)

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

    public void ShowHowToPlay()
    {
        // Logika untuk menampilkan instruksi atau tutorial
        Debug.Log("How to Play");
        // Contoh: Menampilkan panel lain yang berisi tutorial
        // howToPlayPanel.SetActive(true);
    }

    public void ShowSettings()
    {
        // Logika untuk menampilkan pengaturan
        Debug.Log("Settings");
        // Contoh: Menampilkan panel pengaturan
        // settingsPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit(); // Keluar dari game (tidak berfungsi di editor)
        // Jika Anda bekerja di editor, gunakan ini untuk simulasi:
        // UnityEditor.EditorApplication.isPlaying = false;
    }

    public void RestartGame()
    {
        // Fungsi untuk me-restart game atau kembali ke scene utama
        Debug.Log("Restart Game");
        // Misalnya, restart scene saat ini:
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
