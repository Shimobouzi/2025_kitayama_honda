using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    public GameObject title, ranking, start, next, playBall, cntGame;
    [SerializeField]
    private Image anten;

    [SerializeField]
    private pitcher pitcher;
    


    void Start()
    {
        TitleObjects();
        SoundManager.PlayBgm("bgm1");
#if UNITY_EDITOR
        StartCoroutine(GameStart());
#endif
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
            NextObjects();
        }
        else
        {

        }
    }

    public void StartGame()
    {
        StartCoroutine(GameStart());
    }

    private IEnumerator ResetBallForNextPitch()
    {
        yield return new WaitForSeconds(3f);
        Destroy(currentBall);
        Debug.Log(currentBall.name+"を破壊しました");
    }

    private void TitleObjects()
    {
        title.SetActive(true);
        anten.gameObject.SetActive(false);
        ranking.SetActive(true);
        start.SetActive(true);
        playBall.SetActive(false);
        cntGame.SetActive(false);
    }

    private void StartObjects()
    {
        title.SetActive(false);
        ranking.SetActive(false);
        start.SetActive(false);
        next.SetActive(false);
        playBall.SetActive(true);
        cntGame.SetActive(false);
    }

    public void NextObjects()
    {
        next.SetActive(true);
    }

    private IEnumerator GameStart()
    {
        anten.gameObject.SetActive(true);
        for (float i = 0; i < 1; i = i + 0.01f)
        {
            anten.color = new Color(0, 0, 0, i);
            yield return new WaitForSeconds(0.01f);
        }
        StartObjects();
        yield return new WaitForSeconds(1f);
        for (float i = 1; i >= 0; i = i - 0.01f)
        {
            anten.color = new Color(0, 0, 0, i);
            yield return new WaitForSeconds(0.01f);
        }
        anten.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        playBall.SetActive(false);
        StartCoroutine(throwBall());
    }

    private IEnumerator GameSet()
    {
        anten.gameObject.SetActive(true);
        for (int i = 0; i < 256; i++)
        {
            anten.color = new Color(0, 0, 0, i);
        }
        TitleObjects();
        yield return new WaitForSeconds(1f);
        for (int i = 255; i >= 0; i--)
        {
            anten.color = new Color(0, 0, 0, i);
        }
        anten.gameObject.SetActive(false);

    }

    public void throwBallVoid()
    {
        next.SetActive(false);
        StartCoroutine(throwBall());
    }

    private IEnumerator throwBall()
    {
        playCount++;
        pitcher.Ball();
        yield return new WaitForSeconds(2.8f);
        currentBall = GameObject.FindGameObjectWithTag("Ball");
        if (currentBall == null)
        {
            Debug.LogWarning("GameManager: タグ'Ball'を持つオブジェクトが見つかりません。");
        }
    }
}