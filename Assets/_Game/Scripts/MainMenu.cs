using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Ser pỉate
    [SerializeField] GameObject pauseMenu;

    //chon level tuong ung//
    public void OpenLevel(int levelId)
    {
        string LevelName = "Level " + levelId;
        SceneManager.LoadScene(LevelName);
    }

    //choi game default level 1//
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    //thoat man hinh//
    public void QuitGame()
    {
        Application.Quit();
    }

    //pause game//
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    //tro ve trang chinh//
    public void Home()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    //tiep tuc choi//
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    //choi lai tu dau//
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    //next level//
    public void Next()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
            Time.timeScale = 1; 
        }
        else
        {
            Debug.LogWarning("No next scene available.");
        }
        Time.timeScale = 1;
    }
}
