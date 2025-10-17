using UnityEngine;

public class kariRanking : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public RankingManager rank;

    

    public void kariJiro()
    {
        rank.AddScore("jiro", 200);
    }

    public void kariSaburo()
    {
        rank.AddScore("saburo", 100);
    }
}
