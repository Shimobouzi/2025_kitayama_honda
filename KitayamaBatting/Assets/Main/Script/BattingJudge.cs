using UnityEngine;

public class BattingJudge : MonoBehaviour
{
    [Header("判定設定")]
    [Tooltip("Inspectorで 'HomeRun', 'Hit', 'Foul' のいずれかを設定")]
    public string judgeType = "Unknown";
    
    private GameManager gameManager;

    void Start()
    {
        // GameManagerを探して参照を取得
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("BattingJudge: GameManagerが見つかりません。シーンに配置してください。");
        }
    }

    // ボールがTriggerエリアに進入したことを検出
    void OnTriggerEnter(Collider other)
    {
        // 進入したのが「Ball」（ボール）タグを持つオブジェクトか確認
        if (other.gameObject.CompareTag("Ball"))
        {
            ExecuteJudge(other.gameObject);
        }
    }

    void ExecuteJudge(GameObject ball)
    {
        // 1. GameManagerに判定結果を伝達
        if (gameManager != null)
        {
            gameManager.ProcessResult(judgeType);
        }

        // 2. ボールを強制的に停止させ、次の判定や投球への干渉を防ぐ
        Rigidbody ballRb = ball.GetComponent<Rigidbody>();
        if (ballRb != null)
        {
            ballRb.velocity = Vector3.zero;
            ballRb.angularVelocity = Vector3.zero;
        }

        Debug.Log("⚾️ 判定確定: " + judgeType);
    }
}