using UnityEngine;
using System.Collections.Generic;
using Unity.Services.Core;
using Unity.Services.Leaderboards;

public class Leaderbord : MonoBehaviour
{
 private const string LeaderboardId = "high-score-leaderboard";

    private async void Start()
    {
        await UnityServices.InitializeAsync();
    }

    public async void SubmitScore(int score)
    {
        await LeaderboardsService.Instance.AddPlayerScoreAsync(LeaderboardId, score);
        Debug.Log("スコアを送信しました: " + score);
    }

    public async void GetScores()
    {
        var scoresPage = await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId);
foreach (var score in scoresPage.Results) // .Resultsプロパティを追加
{
    Debug.Log($"ランク: {score.Rank}, プレイヤーID: {score.PlayerId}, スコア: {score.Score}");
}
    }
}
