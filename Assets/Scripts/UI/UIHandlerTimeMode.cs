using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHandlerTimeMode : MonoBehaviour
{
    [SerializeField] public TMP_Text timerText;
    [SerializeField] public Slider sliderTime;
    [SerializeField] private GameObject panelWin;
    [SerializeField] private GameObject panelLose;

    private void Start()
    {
        //RemoveAllPanels();
    }
    public void UpdateSlider(float value)
    {
        sliderTime.value = value;
    }
    public void UpdateTimer(string text)
    {
        if (timerText != null)
        {
            timerText.text = text;
        }
    }
    public void RemoveAllPanels()
    {
        panelLose.SetActive(false);
        panelWin.SetActive(false);
        
        GameManager.instance.Pause(false);
    }
    public void ShowPanel(GameResult result)
    {
        panelLose.SetActive(false);

        panelWin.SetActive(false);

        switch (result)
        {
            case GameResult.Win:
                panelWin.SetActive(true);
                panelWin.GetComponent<Animator>().SetTrigger("win");
                break;
            case GameResult.Lose:
                panelLose.SetActive(true);
                panelWin.GetComponent<Animator>().SetTrigger("lose");
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
