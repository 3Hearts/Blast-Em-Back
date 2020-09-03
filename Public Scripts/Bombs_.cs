using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombs_ : MonoBehaviour
{
    public GameObject tnt;
    private PlayerController playercontroller;
    
    void Start()
    {
        playercontroller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (playercontroller.tntpack >= 1 && Input.GetKeyDown("space"))
        {
            //print("space key was pressed");

            Instantiate(tnt, transform.position, transform.rotation);

            playercontroller.tntpack = playercontroller.tntpack - 1;
            playercontroller.GetComponent<PlayerController>().UpdateTNTPack();
        }
    }
}
