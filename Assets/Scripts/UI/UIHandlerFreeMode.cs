using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIHandlerFreeMode : MonoBehaviour
{
    [SerializeField] private GameObject panelEnterScore;

    [SerializeField] private GameObject freeFish;
    [SerializeField] private GameObject panelShowScore;
    [SerializeField] private TextMeshProUGUI fishTextEnterScore;
    [SerializeField] private TextMeshProUGUI fishTextEnterScore2;
    [SerializeField] private TextMeshProUGUI recolectedFishGameUI;
        
            private void Start()
            {
                RemoveAllPanels();
            }
        
            public void RemoveAllPanels()
            {
                panelEnterScore.SetActive(false);
                panelShowScore.SetActive(false);
                GameManager.instance.Pause(false);
                freeFish.SetActive(false);
    }
    public void ShowPanel(GameResult result)
    {
        panelEnterScore.SetActive(false);

        switch (result)
        {
            case GameResult.Lose:
                panelEnterScore.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void ResumeGame()
    {
        RemoveAllPanels();
        GameManager.instance.Pause(false);
    }
    public void ShowScorePanel()
    {
        RemoveAllPanels();
        StartCoroutine(FreeFishCorrutine());
    }
    public void EnterScorePanel()
    {
        ShowPanel(GameResult.Lose);
        panelEnterScore.SetActive(true);
    }

    IEnumerator FreeFishCorrutine()
    {
        freeFish.SetActive(true);
        yield return new WaitForSeconds(3);
        freeFish.SetActive(false);
        panelShowScore.SetActive(true);
    }
    
    public void ShowFishRecolected(string fishInt)
    {
        fishTextEnterScore.text = $"X {fishInt} !";
        fishTextEnterScore2.text = $"X {fishInt} !";
    }
    public void ShowFishGameUI(string fishInt)
    {
        recolectedFishGameUI.text = $"X {fishInt}!";
    }
}
