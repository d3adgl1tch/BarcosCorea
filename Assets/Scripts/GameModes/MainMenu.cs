using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;


public class MainMenu : MonoBehaviour
{
   // [SerializeField] private Animator transitionAnimator;
    [SerializeField] private CanvasGroup faderPanel;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private GameObject freeTutorial;
    [SerializeField] private GameObject timeTutorial;
    [SerializeField] private GameObject timonTutorial;

    private void Start()
    {
        freeTutorial.SetActive(false);
        timeTutorial.SetActive(false);
        FadeIn();
    }
    public void ChangeScene()
    {
      StartCoroutine(OpenGameMode());
       
    }
    public void FadeIn()
    {
        faderPanel.alpha = 0f;
        faderPanel.DOFade(1f, fadeDuration);
    }
    public void FadeOut()
    {
        faderPanel.alpha = 1f;
        faderPanel.DOFade(0f, fadeDuration);
    }
    IEnumerator OpenGameMode()
    {
        timonTutorial.SetActive(true);
        yield return new WaitForSeconds(3f);
        timonTutorial.SetActive(false);
        //Tutoriales
        if (GameManager.instance.gameMode == Mode.TimeMode)
        {
            timeTutorial.SetActive(true);
        }
        else if (GameManager.instance.gameMode == Mode.FreeTime)
        {
            freeTutorial.SetActive(true);

        }
        else if (GameManager.instance.gameMode == Mode.VersusMode)
        {


        }
        yield return new WaitForSeconds(5f);
        yield return faderPanel.DOFade(0f, fadeDuration).SetEase(Ease.InOutQuad).WaitForCompletion();
        //Escena
        if (GameManager.instance.gameMode == Mode.TimeMode)
        {
            GameManager.instance.OpenTimeGameMode();
        }
        else if (GameManager.instance.gameMode == Mode.FreeTime)
        {

            GameManager.instance.OpenFreeGameMode();
            
        }
        else if (GameManager.instance.gameMode == Mode.VersusMode)
        {
            GameManager.instance.OpenVersusGameMode();
            
        }
        faderPanel.blocksRaycasts = true;
    }

    public void ReturnButton()
    {
        GameManager.instance.OpenGameModeChoser();
    }
    
}
