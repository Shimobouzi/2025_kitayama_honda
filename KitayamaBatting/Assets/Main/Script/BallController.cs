using UnityEngine;

public class BallController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bat"))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb == null)
            {
                Debug.LogWarning("BallController: Rigidbody not found on ball.");
                return;
            }
            Vector3 hitDirection = (transform.position - collision.transform.position).normalized;
            // 調整用に Inspector から変更できるようにする
            float hitSpeed = 10f;
            rb.linearVelocity = hitDirection * hitSpeed;
        }
    }
}