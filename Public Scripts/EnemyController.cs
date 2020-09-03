using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private PlayerHealth playerHealth;
    Transform player;
    NavMeshAgent nav;

    public GameObject coin;
    public GameObject effect;

    private GateScript gate;
    public float KillPlusOne = 1f;
    public float moveSpeed;
    private Vector3 moveVelocity;
    
    private PlayerController playercontroller;
    private Stats playerstats;

    public DropTable dropTable;
    public AudioClip deathSound;
    public AudioClip playerHurt;

    public float enemyhealth = 3f;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        playercontroller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        gate = GameObject.FindGameObjectWithTag("Finish").GetComponent<GateScript>();

        playerstats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();

        nav = GetComponent<NavMeshAgent>();
    }

    void DealDamage(float damageValue)
    {
        enemyhealth -= damageValue;
        if (enemyhealth <= 0)
            EnemyDeath();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
           enemyhealth = enemyhealth - playerstats.damage;
        }
        if (other.CompareTag("Explosive"))
        {
            enemyhealth = enemyhealth - 10f;
        }
        if (other.CompareTag("Sword"))
        {
            enemyhealth = enemyhealth - playerstats.damage;
        }
    }
    
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player.gameObject.GetComponent<AudioSource>().PlayOneShot(playerHurt);
            enemyhealth = enemyhealth - 10f;
        }
    }

    void DamagePlayer(Collider player)
    {
        Stats stats = player.GetComponent<Stats>();
        stats.health = stats.health - 10;
    }

    void IncrementKillCount()
    {
       gate.KillCount = gate.KillCount + 1f;
    }

    void DecrementKillCount()
    {

        if (playercontroller.score > 0)
        {
            playercontroller.score = playercontroller.score - 1;

            playercontroller.GetComponent<PlayerController>().UpdateScore();
        }
    }
    
    void EnemyDeath()
    {

        if (enemyhealth <= 0f)
        {
            dropTable.GetComponent<DropTable>().CalculateLoot();
            Instantiate(effect, transform.position, Quaternion.identity);
            IncrementKillCount();
            DecrementKillCount();
            player.gameObject.GetComponent<AudioSource>().PlayOneShot(deathSound);
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        nav.SetDestination(player.position);
        EnemyDeath();  
    }
    
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Fire")
        {
            DealDamage(0.5f);
            Debug.Log("FireDamage");
        }
    }
}
