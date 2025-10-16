using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Leaderboards;
using System.Collections.Generic;
using System.Linq;

public class UIscript : MonoBehaviour
{
   public Transform contentParent;
    public GameObject scoreItemPrefab;

    private const string LeaderboardId = "high-score-leaderboard";

    public async void RefreshRankingUI()
    {
        // 既存のランキングUIをクリア
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        var scores = await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId, new GetScoresOptions { Offset = 0, Limit = 10 });

        foreach (var score in scores.Results)
        {
            GameObject item = Instantiate(scoreItemPrefab, contentParent);
            item.GetComponentInChildren<Text>().text = $"{score.Rank}. {score.PlayerName}: {score.Score}";
        }
    }
}
