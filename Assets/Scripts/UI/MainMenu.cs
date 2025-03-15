using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPage;
    [SerializeField] private GameObject creditsPage;
    [SerializeField] private GameObject optionsPage;
    [SerializeField] private GameObject creditsTexts;
    
    public void LoadSceneByReference()
    {
        SceneManager.LoadSceneAsync(1);
    }
    
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
    
    public void ShowCredits()
    {
        creditsPage.SetActive(true);
        mainMenuPage.SetActive(false);
        optionsPage.SetActive(false);
    }
    
    public void LeaveCredits()
    {
        optionsPage.SetActive(true);
        mainMenuPage.SetActive(false);
        creditsPage.SetActive(false);
    }
}
