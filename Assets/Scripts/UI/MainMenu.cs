using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string playScene;
    [SerializeField] private GameObject mainMenuPage;
    [SerializeField] private GameObject creditsPage;
    [SerializeField] private GameObject creditsTexts;
    
    public void LoadSceneByReference()
    {
        SceneManager.LoadScene(playScene);
    }
    
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
    
    public void ShowCredits()
    {
        DG.Tweening.DOTween.KillAll();
        
        mainMenuPage.SetActive(false);
        creditsPage.SetActive(true);
        var parentRectTransform = creditsPage.GetComponentInParent<RectTransform>();
        var rectTransform = creditsTexts.GetComponentInParent<RectTransform>();
        var moveTween = rectTransform.DOMoveY(parentRectTransform.rect.height / 2f, 5f);
        var scaleTween = rectTransform.DOScale(1, .4f)
            .SetLoops(-1, LoopType.Yoyo);
        
        moveTween.OnComplete(() => scaleTween.Kill());
    }
    
    public void LeaveCredits()
    {
        DG.Tweening.DOTween.KillAll();
        
        var parentRectTransform = creditsPage.GetComponentInParent<RectTransform>();
        var rectTransform = creditsTexts.GetComponentInParent<RectTransform>();
        var moveTween = rectTransform.DOMoveY(parentRectTransform.rect.height + 500f, 2f);
        var scaleTween = rectTransform.DOScale(1, .4f)
            .SetLoops(-1, LoopType.Yoyo);
        
        moveTween.OnComplete(() => {
            mainMenuPage.SetActive(true);
            creditsPage.SetActive(false);
            scaleTween.Kill();
        });
    }
}
