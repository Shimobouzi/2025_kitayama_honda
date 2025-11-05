using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class RankingManager : MonoBehaviour
{
    private string filePath;
    [SerializeField]
    public ScoreData scoreData = new ScoreData();
    private const int maxEntries = 10; // ランキングに表示する最大数

    void Awake()
    {
        // アプリケーションの永続データパスを取得
        filePath = Path.Combine(Application.persistentDataPath, "ranking.json");
        Debug.Log("Ranking file path: " + filePath);
        LoadRanking();
    }

    public void AddScore(string name, int score)
    {
        scoreData.scores.Add(new ScoreEntry { playerName = name, score = score });

        // スコアを降順でソート
        scoreData.scores.Sort((a, b) => b.score.CompareTo(a.score));

        // 最大表示数を超えた分を削除
        if (scoreData.scores.Count > maxEntries)
        {
            scoreData.scores.RemoveRange(maxEntries, scoreData.scores.Count - maxEntries);
        }

        SaveRanking();
    }

    public List<ScoreEntry> GetRanking()
    {
        return scoreData.scores;
    }

    public void ResetRanking()
    {
       scoreData.scores.Clear();
        string json = JsonUtility.ToJson(scoreData, true); // trueでJSONを整形
        File.WriteAllText(filePath, json);
    }

    private void SaveRanking()
    {
        string json = JsonUtility.ToJson(scoreData, true); // trueでJSONを整形
        File.WriteAllText(filePath, json);
    }

    private void LoadRanking()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            scoreData = JsonUtility.FromJson<ScoreData>(json);
        }
    }
}