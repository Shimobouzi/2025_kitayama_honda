using System.Collections;
using System.Collections.Generic;
using UniGLTF.Extensions.VRMC_node_constraint;
using UnityEngine;
using UnityEngine.InputSystem;

public class pitcher : MonoBehaviour
{
    [SerializeField] GameObject point;
    [SerializeField] GameObject sphere;
    [SerializeField]
    private float speed = 60;
    [SerializeField]
    Animator model;
 
   public void ball(InputAction.CallbackContext context){
    if(!context.performed) return;
        GameObject ball = (GameObject)Instantiate(sphere, point.transform.position, Quaternion.identity);
            Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
            ballRigidbody.AddForce(point.transform.forward * speed);
    }

    public void Ball()
    {
        StartCoroutine(BallCol());
    }

    IEnumerator BallCol()
    {
        model.SetBool("Pitching", true);
        yield return new WaitForSeconds(2.5f);
        model.SetBool("Pitching", false);
        SoundManager.PlaySE("throw");
        GameObject ball = (GameObject)Instantiate(sphere, point.transform.position, Quaternion.identity);
        Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
        ballRigidbody.AddForce(point.transform.forward * speed);
    }

}
