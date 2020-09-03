using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_Script : MonoBehaviour
{
    public GameObject Explosion;

    public float radius = 100F;
    public float power = 500.0F;

   // public PlayerController playercontroller;

   // public GameObject[] canbreak;

    void Awake()
    {
        //playercontroller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        StartCoroutine(Bomblife());
    }

    IEnumerator Bomblife()
    {
       yield return new WaitForSeconds(2);

        Instantiate(Explosion, transform.position, transform.rotation);

        Explode();

        Destroy(this.gameObject);



    }

   void Explode()
    {
        Debug.Log("Blow up");
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null && rb.gameObject.tag == "Breakable")
            {
                rb.isKinematic = false;
                rb.AddExplosionForce(power, explosionPos, radius, 4.0F, ForceMode.Impulse);
            }
              
        }

    }
   
}
