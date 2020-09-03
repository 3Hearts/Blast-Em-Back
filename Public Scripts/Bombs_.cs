using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombs_ : MonoBehaviour
{

    public GameObject tnt;
    private PlayerController playercontroller;

    // Start is called before the first frame update
    void Start()
    {
        playercontroller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playercontroller.tntpack >= 1 && Input.GetKeyDown("space"))
        {
            print("space key was pressed");

            Instantiate(tnt, transform.position, transform.rotation);

            playercontroller.tntpack = playercontroller.tntpack - 1;
            playercontroller.GetComponent<PlayerController>().UpdateTNTPack();

            
        }
    }
}
