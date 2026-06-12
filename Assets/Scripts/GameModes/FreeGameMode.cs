using DG.Tweening;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FreeGameMode : MonoBehaviour
{
    [SerializeField] private PlayerEvents playerEvents;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private UIHandlerFreeMode uiHandler;
    [SerializeField] private DockManager dockManager;
    [SerializeField] private FishManager fishManager;
    [SerializeField] private Scoreboard scoreboard;
    [SerializeField] private CanvasGroup fader;
    [SerializeField] public int fishRecolected;
    [SerializeField] public string playerName;
    [SerializeField] public Animator fish;
    void Start()
    {
        playerEvents.OnStopped.AddListener(ArrivedWithFish);
        playerEvents.OnCrashed.AddListener(Lose);
        playerEvents.OnFishRecolected.AddListener(RecolectedFish);
        scoreboard.OnEnterScore.AddListener(uiHandler.ShowScorePanel);
        uiHandler.ShowFishGameUI(fishRecolected.ToString());
        //SpawnFish();
        OnGameStart();
    }
    void OnGameStart()
    {
        StartCoroutine(FadeIn());
    }
    void SetValues()
    {
        fishRecolected = 0;
        uiHandler.ShowFishGameUI(fishRecolected.ToString());
    }
    public void QuitToMainMenu()
    {
        GameManager.instance.OpenMainMenu();
    }
    public void RestartGame()
    {
        StartCoroutine(FadeOut());
    }
    void SpawnFish()
    {
        fishManager.SpawnFish();
    }
    void RemoveFishes()
    {
        fishManager.DestroyAllFishes();
    }
    void ArrivedWithFish()
    {
        Debug.Log("Arrived");
        SpawnFish();
        fishRecolected++;
        fish.SetTrigger("GetFish");
        uiHandler.ShowFishGameUI(fishRecolected.ToString());
        StopCoroutine(DeactivateDockAfterPlayingFX());
        StartCoroutine(DeactivateDockAfterPlayingFX());
    }
    void Lose()
    {
        RemoveFishes();
        playerMovement.Stop();
        StartCoroutine(WaitAfterCrash());
        
    }
    void RecolectedFish()
    {
        dockManager.ActivateRandomDock();
    }
    IEnumerator DeactivateDockAfterPlayingFX()
    {
        yield return new WaitForSeconds(1f);
        dockManager.DeactivateDocks();
    }
    IEnumerator WaitAfterCrash()
    {
        yield return new WaitForSeconds(1f);
        uiHandler.EnterScorePanel();
        uiHandler.ShowFishRecolected(fishRecolected.ToString());
        GameManager.instance.SetResult(GameResult.Lose);
    }
    IEnumerator FadeIn()
    {
        SetValues();
        uiHandler.ShowFishRecolected(fishRecolected.ToString());
        playerMovement.RestartPosition();
        //dockManager.DeactivateDocks();
        uiHandler.RemoveAllPanels();
        dockManager.DeactivateDocks();
        SpawnFish();
        yield return fader.DOFade(0f, 1f).SetEase(Ease.InOutQuad).WaitForCompletion();
        playerMovement.canMove();
        GameManager.instance.gameResult = GameResult.Playing;
    }
    IEnumerator FadeOut()
    {
        yield return fader.DOFade(1f, 0f).SetEase(Ease.InOutQuad).WaitForCompletion();
        OnGameStart();
    }
    IEnumerator FadeToQuitMainMenu()
    {
        yield return fader.DOFade(1f, 0f).SetEase(Ease.InOutQuad).WaitForCompletion();
        GameManager.instance.OpenMainMenu();

    }
}
