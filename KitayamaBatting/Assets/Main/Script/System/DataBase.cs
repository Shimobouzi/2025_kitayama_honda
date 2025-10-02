using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class DataBase : MonoBehaviour
{
    /// <summary>
    /// シングルトンインスタンス
    /// </summary>
    public static DataBase Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
