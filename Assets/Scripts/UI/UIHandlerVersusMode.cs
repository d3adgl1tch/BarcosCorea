using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHandlerVersusMode : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI player1Fishes; 
    [SerializeField] private TextMeshProUGUI player2Fishes; 
    [SerializeField] public Slider sliderTime;
    [SerializeField] private GameObject panelWinPlayer1;
    [SerializeField] private GameObject panelWinPlayer2;
    [SerializeField] private GameObject panelEmpate;

    private void Start()
    {
        //RemoveAllPanels();
    }
    public void UpdateSlider(float value)
    {
        sliderTime.value = value;
    }
    public void UpdateFishes(int player1,int player2)
    {
        player1Fishes.text = player1.ToString();
        player2Fishes.text = player2.ToString();
    }
    public void RemoveAllPanels()
    {
        panelWinPlayer1.SetActive(false);
        panelWinPlayer2.SetActive(false);
        
        GameManager.instance.Pause(false);
    }
    public void ShowPanel(GameResult result)
    {
        panelWinPlayer1.SetActive(false);

        panelWinPlayer2.SetActive(false);

        switch (result)
        {
            case GameResult.Win:
                panelWinPlayer1.SetActive(true);
                break;
            case GameResult.Lose:
                panelWinPlayer2.SetActive(true);
                break;
        }
    }

    public void ShowLosePanel()
    {
        ShowPanel(GameResult.Lose);
    }
    public void ShowWinPanel()
    {
        ShowPanel(GameResult.Win);
    }
    public void ResumeGame()
    {
        RemoveAllPanels();
        GameManager.instance.Pause(false);
    }
}
