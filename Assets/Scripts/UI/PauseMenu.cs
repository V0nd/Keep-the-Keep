using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public InventoryObject inventory;
    public GameObject pauseMenuUI;
    public GameObject dialogBox;

    private void Start()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        PausingGame();
    }

    private void PausingGame()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !dialogBox.activeInHierarchy)
        {
            if(isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        inventory.Container.Clear();
    }

}
