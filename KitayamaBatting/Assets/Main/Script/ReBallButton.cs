using UnityEngine;

public class ReBallButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private GameManager gameManager;
    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.SetActive(false);
        gameManager.throwBallVoid();
    }
}
