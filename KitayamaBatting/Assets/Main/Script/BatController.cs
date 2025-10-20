using UnityEngine;
public class BatController : MonoBehaviour
{
    public float hitForce = 10f; // ボールに与える基本的な力
    //public AudioClip hitSound; // バットがボールに当たったときの音
    //private AudioSource audioSource; // 音を再生するためのコンポーネント
    private void Start()
    {
        // AudioSourceを追加
        //audioSource = gameObject.AddComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball")) // ボールと衝突したら
        {
            Debug.Log("ボールがバットに当たりました！");
            // 音を再生
            // if (hitSound != null && audioSource != null)
            // {
            //     audioSource.PlayOneShot(hitSound);
            // }
            // ボールのRigidbodyを取得
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (ballRigidbody != null)
            {
                // バットの移動方向（スイングの向き）を計算
                Vector3 batVelocity = GetComponentInParent<Rigidbody>().linearVelocity;
                // ボールにバットの速度を加えて飛ばす
                ballRigidbody.linearVelocity = batVelocity * hitForce;
            }
        }
    }
}