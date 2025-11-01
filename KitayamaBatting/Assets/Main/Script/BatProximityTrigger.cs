using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class BatProximityTrigger : MonoBehaviour
{
    public BatController batController;
    public float radius = 1.0f;
    public float cooldown = 0.2f;
    private bool onCooldown = false;

    void Reset()
    {
        // ensure collider present and configured
        SphereCollider sc = GetComponent<SphereCollider>();
        sc.isTrigger = true;
        sc.radius = radius;
    }

    void Start()
    {
        SphereCollider sc = GetComponent<SphereCollider>();
        sc.isTrigger = true;
        sc.radius = radius;

        if (batController == null)
        {
            batController = GetComponentInParent<BatController>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (onCooldown) return;
        if (batController == null) return;
        if (!other.gameObject.CompareTag("Ball")) return;

        // call bat's hit processing
        batController.ProcessHit(other.gameObject);
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
    }
}
