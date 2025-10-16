using System.Collections;
using UnityEngine;

/*
    ゲーム全体の管理を行うスクリプト
    - バッティングの判定結果を受け取り、各種カウントを更新
    - 次の投球に向けてボールをリセットする
*/

public class GameManager : MonoBehaviour
{
    private int homeRunCount = 0;
    private int hitCount = 0;
    private int foulCount = 0;

    [Header("ボールのリセット設定")]
    [Tooltip("ボールを初期位置に戻す際の座標")]
    public Vector3 ballSpawnPosition = new Vector3(0, 1, 10);
    [SerializeField]
    private GameObject Ball; // 現在フィールドにあるボール
    [SerializeField]
    private GameObject start;
    private GameObject cnt;

    void Start()
    {
        // シーン内のボールオブジェクトを探す
        if (Ball == null)
        {
            Debug.LogWarning("GameManager: タグ'Ball'を持つオブジェクトが見つかりません。");
        }
    }

    // 判定スクリプトから呼び出されるメイン処理
    public void ProcessResult(string result)
    {
        switch (result)
        {
            case "HomeRun":
                homeRunCount++;
                Debug.Log("ホームラン！HR数: " + homeRunCount);
                break;
            case "Hit":
                hitCount++;
                Debug.Log("ヒット！（内野/外野）ヒット数: " + hitCount);
                break;
            case "Foul":
                foulCount++;
                Debug.Log("ファール。ファール数: " + foulCount);
                break;
            default:
                Debug.Log("不明な判定: " + result);
                break;
        }
        
        // 判定処理後、次の投球のためにボールをリセット
        Invoke("ResetBallForNextPitch", 3f); // 3秒後にリセット（演出時間）
    }

    private void ResetBallForNextPitch(GameObject ball)
    {
        Destroy(ball);
        Debug.Log(ball.name+"を破壊しました");
    }

    private IEnumerator GameStart()
    {
        yield return new WaitForSeconds(1.5f);
    }
}