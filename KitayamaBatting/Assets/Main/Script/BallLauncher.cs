using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform spawnPoint;
    public float launchForce = 10f;
    public float spawnInterval = 3f;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            LaunchBall();
            timer = 0f;
        }
    }

    void LaunchBall()
    {
        GameObject ball = Instantiate(ballPrefab, spawnPoint.position, spawnPoint.rotation);
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(spawnPoint.forward * launchForce, ForceMode.Impulse);
        }
    }
}
