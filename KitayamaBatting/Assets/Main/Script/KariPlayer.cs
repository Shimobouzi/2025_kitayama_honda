using UnityEngine;

public class KariPlayer : MonoBehaviour
{
    Transform myTransform;
    public float kakudo = 30;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myTransform = this.transform;
    }

    // Update is called once per frame
    void Update()
    { 


    }

    public void Batting()
    {

            // ワールド座標を基準に、回転を取得
            Vector3 worldAngle = myTransform.eulerAngles;
            worldAngle.y = kakudo; // ワールド座標を基準に、y軸を軸にした回転を10度に変更
            myTransform.eulerAngles = worldAngle; // 回転角度を設定

    }
}
