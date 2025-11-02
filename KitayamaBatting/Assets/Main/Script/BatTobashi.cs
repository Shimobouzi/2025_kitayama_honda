using System.Collections;
using UnityEngine;

public class BatTobashi : MonoBehaviour
{
    public float hitForce = 10f; // ボールに与える基本的な力


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball")) // ボールと衝突したら
        {
            Debug.Log("ボールがバットに当たりました！");
            SoundManager.PlaySE("kakiin");
            // ボールのRigidbodyを取得
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (ballRigidbody != null)
            {
                // バットの移動方向（スイングの向き）を計算
                Vector3 batVelocity = this.GetComponent<Rigidbody>().linearVelocity;
                StartCoroutine(kakiin(ballRigidbody));
            }
        }
    }

    IEnumerator kakiin(Rigidbody rb )
    {
        yield return new WaitForSeconds(0.1f);
        // ボールにバットの速度を加えて飛ばす
        rb.linearVelocity = rb.linearVelocity * hitForce;
    }
}
