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
        ShowElementsManage();
    }
    
    public void Menu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void Pause()
    {
        ShowElementsManage();
    }

    private void ShowElementsManage()
    {
        pauseMenu.SetActive(!isOpenPause);
        buttonPause.SetActive(isOpenPause);
        textMenu.SetActive(isOpenPause);
        slider.SetActive(isOpenPause);
        isOpenPause = !isOpenPause;
        
        Time.timeScale = isOpenPause switch
        {
            true => 0f,
            false => 1f
        };
    }
}
