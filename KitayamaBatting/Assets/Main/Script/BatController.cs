using UnityEngine;
public class BatController : MonoBehaviour
{
    public float hitForce = 10f; // ボールに与える基本的な力（インスペクタで増やすと飛距離が伸びます）
    [Tooltip("Additional multiplier applied to the impulse (for tuning flight distance)")]
    public float hitMultiplier = 1.0f;
    [Header("Proximity hit (larger hit range)")]
    [Tooltip("If true, use a trigger zone around the bat to register hits instead of relying only on physics collisions.")]
    public bool useProximityHit = false;
    [Tooltip("Radius of the proximity trigger (meters)")]
    public float proximityRadius = 1.0f;
    [Tooltip("Cooldown (sec) after a proximity hit to avoid duplicate hits")]
    public float proximityCooldown = 0.2f;
    //public AudioClip hitSound; // バットがボールに当たったときの音
    //private AudioSource audioSource; // 音を再生するためのコンポーネント
    private void Start()
    {
        // AudioSourceを追加
        //audioSource = gameObject.AddComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (useProximityHit)
        {
            // If using proximity trigger, ignore collision-based hits to avoid double-processing
            return;
        }

        if (collision.gameObject.CompareTag("Ball")) // ボールと衝突したら
        {
            ProcessHit(collision.gameObject);
        }
    }

    // Called by collision or proximity trigger
    public void ProcessHit(GameObject ball)
    {
        Debug.Log("ボールがバットに当たりました！");
        Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
        if (ballRigidbody == null) return;

        Rigidbody batRb = GetComponent<Rigidbody>() ?? GetComponentInParent<Rigidbody>();
        Vector3 batVelocity = Vector3.zero;
        if (batRb != null)
        {
            batVelocity = batRb.linearVelocity;
        }

        Vector3 impulse = batVelocity * hitForce * hitMultiplier;
        impulse += Vector3.up * (Mathf.Abs(Vector3.Dot(batVelocity.normalized, Vector3.forward)) * 2.0f);
        ballRigidbody.AddForce(impulse, ForceMode.VelocityChange);
    }
}