using NUnit.Framework;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;

[System.Serializable]
public class ScoreEntry
{
    public string name;
    public int score;
}
[System.Serializable]
public class ScoreboardList
{
    public List<ScoreEntry> scores = new List<ScoreEntry>();
}
public class Scoreboard : MonoBehaviour
{
    private string filePath;
    private ScoreboardList scoreboard = new ScoreboardList();
    [SerializeField]private TextMeshProUGUI scoreBoardText;
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private FreeGameMode gameMode;
    
    [SerializeField] private CanvasGroup fadeOut;
    [Header("Tests")]
    public string testName;
    public int testScore;
    public UnityEvent OnEnterScore;
    private void OnEnable()
    {
        filePath = Path.Combine(Application.persistentDataPath, "scoreboard.json");
        LoadScoreboard();
    }
    public void AddName()
    {
        if (string.IsNullOrWhiteSpace(nameInput.text))
        {
            Debug.LogWarning("No hay nombere");
            return;
        }

        gameMode.playerName = nameInput.text.Trim();
        AddScore(gameMode.playerName, gameMode.fishRecolected);
        nameInput.text = "";
        OnEnterScore?.Invoke();

    }
    public void AddScore(string playerName, int score)
    {
        ScoreEntry entry = new ScoreEntry();
        entry.name = playerName;
        entry.score = score;

    
        scoreboard.scores.Add(entry);

        SortScoreBoard();

        if (scoreboard.scores.Count > 5)
        {
            scoreboard.scores.RemoveAt(scoreboard.scores.Count - 1);
        }
        ShowScoreBoard();
        SaveScoreboard();
    }
    public void AddScore()
    {
        AddName();
    }
    void SaveScoreboard()
    {
        string json = JsonUtility.ToJson(scoreboard, true);
        File.WriteAllText(filePath, json);
    }
    void LoadScoreboard()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            scoreboard = JsonUtility.FromJson<ScoreboardList>(json);
            ShowScoreBoard();
        }
        else
        {
            scoreboard = new ScoreboardList();
            ShowScoreBoard();
        }

    }
    void SortScoreBoard()
    {
        scoreboard.scores = scoreboard.scores.OrderByDescending(s => s.score).ToList();
    }
    public void QuitToMainMenu()
    {
        StartCoroutine(FadeToQuitMainMenu());
    }
    
    IEnumerator FadeToQuitMainMenu()
    {
        yield return new WaitForSeconds(5);
        yield return fadeOut.DOFade(1,1).WaitForCompletion();
        GameManager.instance.OpenMainMenu();

    }
    
    void ShowScoreBoard()
    {
        
        scoreBoardText.text = "";
        for (int i = 0; i < scoreboard.scores.Count; i++)
        {
            ScoreEntry entry = scoreboard.scores[i];
            scoreBoardText.text += $"{i + 1}- {entry.name}   {entry.score}\n";
        }
    }
    public void EareseScoreBoard()
    {
        scoreboard.scores.Clear();
        SortScoreBoard();
        ShowScoreBoard();
        SaveScoreboard();
    }
}
