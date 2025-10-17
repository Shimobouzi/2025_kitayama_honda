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

    [SerializeField]
    private int gameCnt = 5;
    private int playCount = 0;

    [SerializeField]
    private GameObject currentBall; // 現在フィールドにあるボール

    public class gameManagerUiObjects
    {
        public GameObject title;
        public GameObject anten;
        public GameObject ranking;
        public GameObject start;
        public GameObject playBall;
        public GameObject cntGame;
    }
    [SerializeField]
    private gameManagerUiObjects objectsUi;

    private class gameManager3dObjects
    {
        public pitcher pitcher;
    }
    [SerializeField]
    private gameManager3dObjects objects3d;


    void Start()
    {
        TitleObjects();
    }

    // 判定スクリプトから呼び出されるメイン処理
    public void ProcessResult(JudgeType result)
    {
        switch (result)
        {
            case JudgeType.HomeRun:
                homeRunCount++;
                Debug.Log("ホームラン！HR数: " + homeRunCount);
                break;
            case JudgeType.Hit:
                hitCount++;
                Debug.Log("ヒット！内野ヒット数: " + hitCount);
                break;
            case JudgeType.ThreeBaseHit:
                hitCount++;
                Debug.Log("3ベースヒット！外野ヒット数: " + hitCount);
                break;
            case JudgeType.Foul:
                foulCount++;
                Debug.Log("ファール。ファール数: " + foulCount);
                break;
            default:
                Debug.Log("不明な判定: " + result);
                break;
        }

        // 判定処理後、次の投球のためにボールをリセット
        StartCoroutine(PlayBallAgain()); // 3秒後にリセット（演出時間）
    }

    private IEnumerator PlayBallAgain()
    {
        Coroutine col = StartCoroutine(ResetBallForNextPitch());
        yield return col;
        if (playCount < gameCnt)
        {
            StartCoroutine(throwBall());
        }
        else
        {

        }
    }

    private IEnumerator ResetBallForNextPitch()
    {
        yield return new WaitForSeconds(3f);
        Destroy(currentBall);
        Debug.Log(currentBall.name+"を破壊しました");
    }

    private void TitleObjects()
    {
        objectsUi.title.SetActive(true);
        objectsUi.anten.SetActive(false);
        objectsUi.ranking.SetActive(true);
        objectsUi.start.SetActive(true);
        objectsUi.playBall.SetActive(false);
        objectsUi.cntGame.SetActive(false);
    }

    private void StartObjects()
    {
        objectsUi.title.SetActive(false);
        objectsUi.ranking.SetActive(false);
        objectsUi.start.SetActive(false);
        objectsUi.playBall.SetActive(true);
        objectsUi.cntGame.SetActive(false);
    }

    private IEnumerator GameStart()
    {
        StartObjects();
        yield return new WaitForSeconds(1.5f);
        objectsUi.playBall.SetActive(false);
        StartCoroutine(throwBall());
    }

    private IEnumerator throwBall()
    {
        playCount++;
        objects3d.pitcher.Ball();
        currentBall = GameObject.FindGameObjectWithTag("Ball");
        if (currentBall == null)
        {
            Debug.LogWarning("GameManager: タグ'Ball'を持つオブジェクトが見つかりません。");
        }
        yield return new WaitForSeconds(0f);
    }
}