using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class pitcher : MonoBehaviour
{
     [SerializeField] GameObject sphere;
    private float speed = 60;
 
    void Update ()
    {
        
    }
   public void ball(InputAction.CallbackContext context){
    if(!context.performed) return;
        GameObject ball = (GameObject)Instantiate(sphere, transform.position, Quaternion.identity);
            Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
            ballRigidbody.AddForce(transform.forward * speed);
    }

}
