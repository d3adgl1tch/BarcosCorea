using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class VersusGameMode : MonoBehaviour
{
    [SerializeField] private PlayerEvents playerEvents1;
    [SerializeField] private PlayerEvents playerEvents2;
    [SerializeField] private PlayerMovementVersus playerMovement1;
    [SerializeField] private PlayerMovementVersus playerMovement2;
    [SerializeField] private UIHandlerVersusMode uiHandler;
    [SerializeField] private FishManager fishSpawneer;
    [SerializeField] private CanvasGroup fader;
    [SerializeField] private float maxTime;
    [SerializeField] private bool playerArrived;

    [SerializeField] private CanvasGroup fadeOut;
    private float timer = 0;

    private int fishesPlayer1;
    private int fishesPlayer2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerEvents1.OnCrashed.AddListener(OnPlayerCrashed1);
        playerEvents2.OnCrashed.AddListener(OnPlayerCrashed2);
        playerEvents1.OnFishRecolected.AddListener(RecolectedFishPlayer1);
        playerEvents2.OnFishRecolected.AddListener(RecolectedFishPlayer2);
        GameManager.instance.OnGameResultChanged.AddListener(OnGameResultChanged);
        ResetValues();
    }

    private void RecolectedFishPlayer1()
    {
        fishesPlayer1++;
        fishSpawneer.SpawnFish();
        UpdateUI();
    }
    private void RecolectedFishPlayer2()
    {
        fishesPlayer2++;
        fishSpawneer.SpawnFish();
        UpdateUI();
    }
    private void UpdateUI()
    {
        uiHandler.UpdateFishes(fishesPlayer1,fishesPlayer2);
    }
    void ResetValues()
    {
        //StopAllCoroutines();
        StartCoroutine(FadeIn());
    }

    void Lose()
    {
        GameManager.instance.SetResult(GameResult.Lose);
        playerMovement1.Stop();
        playerMovement2.Stop();
        QuitToMainMenu();
    }
    public float timerspawneer;
    public float nextSpawnTime;
    void Update()
    {
        if (GameManager.instance.gameResult != GameResult.Playing)
            return;

        if (playerArrived == true)
            return;

        timerspawneer += Time.deltaTime;

        if (timerspawneer >= nextSpawnTime)
        {
            fishSpawneer.SpawnFish();
            SetNextSpawn();
        }
        Clock();
    }

void SetNextSpawn()
{
    timerspawneer = 0f;
    nextSpawnTime = UnityEngine.Random.Range(2f, 3f);
}
public void QuitToMainMenu()
    {
        StartCoroutine(FadeToQuitMainMenu());
    }
    public void RestartGame()
    {
        StartCoroutine(FadeOut());
    }
    void OnGameResultChanged(GameResult result)
    {
        uiHandler.ShowPanel(result);
    }
    private void Clock()
    {
        timer -= Time.deltaTime;

        uiHandler.sliderTime.value = timer;

        if (timer <= 0f)
        {
            timer = 0f;
            uiHandler.sliderTime.value = 0f;

            if(fishesPlayer1 > fishesPlayer2)
            {
               // uiHandler.ShowPanel(GameResult.WinPlayer1);
                GameManager.instance.SetResult(GameResult.WinPlayer1);

            }
            else if(fishesPlayer2> fishesPlayer1)
            {
                //uiHandler.ShowPanel(GameResult.WinPlayer2);
                GameManager.instance.SetResult(GameResult.WinPlayer2);

            }
            else
            {
                //uiHandler.ShowPanel(GameResult.Empate);
                GameManager.instance.SetResult(GameResult.Empate);

            }
            playerMovement1.Stop();
            playerMovement2.Stop();
            playerArrived = true;
            //QuitToMainMenu();
        }
    }
    private void OnPlayerCrashed1()
    {
        StartCoroutine(RecoverFromCrash1());
    }
    private void OnPlayerCrashed2()
    {
        StartCoroutine(RecoverFromCrash2());
    }
    IEnumerator RecoverFromCrash1()
    {
        playerMovement1.Stop();
        yield return new WaitForSeconds(.5f);

        playerMovement1.RecoverFromCrash();
    }
    IEnumerator RecoverFromCrash2()
    {
        playerMovement2.Stop();
        yield return new WaitForSeconds(.5f);

        playerMovement2.RecoverFromCrash();
    }
    IEnumerator FadeIn()
    {
        fishSpawneer.SpawnFish();
        playerMovement1.Stop();
        playerMovement2.Stop();
        timer = maxTime;
        uiHandler.sliderTime.minValue = 0f;
        uiHandler.sliderTime.maxValue = maxTime;
        uiHandler.sliderTime.value = maxTime;
        playerArrived = true;
        playerMovement1.RestartPosition();
        playerMovement2.RestartPosition();
        uiHandler.RemoveAllPanels();
        yield return fader.DOFade(0f, 1f).SetEase(Ease.InOutQuad).WaitForCompletion();
        yield return new WaitForSeconds(2f);
        playerMovement1.canMove();
        playerMovement2.canMove();
        playerArrived = false;
        GameManager.instance.SetResult(GameResult.Playing);
        timer = maxTime;
        uiHandler.sliderTime.minValue = 0f;
        uiHandler.sliderTime.maxValue = maxTime;
        uiHandler.sliderTime.value = maxTime;
    }

    IEnumerator FadeOut()
    {
        yield return fader.DOFade(1f, 0f).SetEase(Ease.InOutQuad).WaitForCompletion();
        ResetValues();
    }
    IEnumerator FadeToQuitMainMenu()
    {
        yield return new WaitForSeconds(3);
        yield return fadeOut.DOFade(1, 1).WaitForCompletion();
        GameManager.instance.OpenMainMenu();

    }
}
