using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class pitcher : MonoBehaviour
{
    [SerializeField] GameObject point;
    [SerializeField] GameObject sphere;
    [SerializeField]
    private float speed = 60;
 
   public void ball(InputAction.CallbackContext context){
    if(!context.performed) return;
        GameObject ball = (GameObject)Instantiate(sphere, point.transform.position, Quaternion.identity);
            Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
            ballRigidbody.AddForce(point.transform.forward * speed);
    }

    public void Ball()
    {
        GameObject ball = (GameObject)Instantiate(sphere, point.transform.position, Quaternion.identity);
        Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
        ballRigidbody.AddForce(point.transform.forward * speed);
    }

}
