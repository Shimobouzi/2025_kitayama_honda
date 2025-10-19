using UnityEngine;

public class StartButton : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    private void OnTriggerEnter(Collider other)
    {
        gameManager.StartGame();
    }
}
