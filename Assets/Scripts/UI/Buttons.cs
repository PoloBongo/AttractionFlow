using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    private bool isResume = true;
    private bool isOpenPause = false;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject textMenu;
    [SerializeField] private GameObject buttonPause;
    [SerializeField] private GameObject slider;
    
    public void Quit()
    {
        Application.Quit();
    }
    
    public void Resume()
    {
        isResume = !isResume;
        Time.timeScale = isResume switch
        {
            true => 0f,
            false => 1f
        };
        Pause();
    }
    
    public void Menu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void Pause()
    {
        pauseMenu.SetActive(!isOpenPause);
        buttonPause.SetActive(isOpenPause);
        textMenu.SetActive(isOpenPause);
        slider.SetActive(isOpenPause);
        isOpenPause = !isOpenPause;
    }
}
