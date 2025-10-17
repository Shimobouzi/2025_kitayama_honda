using UnityEngine;

public class StartButton : MonoBehaviour
{
    private GameManager gameManager;

    private void OnCollisionEnter(Collision collision)
    {
        gameManager.StartGame();
    }
}
