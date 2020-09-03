using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableScript : MonoBehaviour
{

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        DisableRagdoll();
    }

    // Let the rigidbody take control and detect collisions.
    void EnableRagdoll()
    {
        rb.isKinematic = false;
      //  rb.detectCollisions = true;
    }

    // Let animation control the rigidbody and ignore collisions.
    void DisableRagdoll()
    {
        rb.isKinematic = true;
       // rb.detectCollisions = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Explosive")
        {
            StartCoroutine(Breakabledeath());
        }
    }

    IEnumerator Breakabledeath()
    {
        yield return new WaitForSeconds(1.5f);

        Destroy(this.gameObject);
    }
}
