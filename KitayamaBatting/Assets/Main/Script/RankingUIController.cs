using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RankingUIController : MonoBehaviour
{
    public RankingManager rankingManager;
    public Text rankingText; // またはTextMeshProUGUI

    void Start()
    {
        DisplayRanking();
    }

    public void DisplayRanking()
    {
        List<ScoreEntry> ranking = rankingManager.GetRanking();
        string displayText = "--- RANKING ---\n\n";

        for (int i = 0; i < ranking.Count; i++)
        {
            displayText += $"{i + 1}. {ranking[i].playerName} : {ranking[i].score}\n";
        }
        
        rankingText.text = displayText;
    }
}