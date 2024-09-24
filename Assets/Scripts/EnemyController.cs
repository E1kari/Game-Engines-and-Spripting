using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject character;
    public int health;
    public int attackCooldown;
    public Material hitMaterial;
    private int hitCooldown;
    private Material defaultMaterial;
    public int attackTimer;
    NavMeshAgent agent;

    
    // Start is called before the first frame update
    void Start()
    {
        attackTimer = attackCooldown;
        agent = GetComponent<NavMeshAgent>();
        defaultMaterial = gameObject.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    private void Update()
    {
        agent.destination = character.transform.position;

        hitCooldown--;
        if (hitCooldown == 0)
        {
            gameObject.GetComponent<Renderer>().material = defaultMaterial;
        }

        if (health <= 0)
        {
            Destroy(gameObject, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        attackTimer--;
        if (attackTimer <= 0)
        {
            attack(collision);
            attackTimer = attackCooldown;
        }
    }

    private void attack(Collision collision)
    {
        GameObject other = collision.gameObject;
            if (other.tag == "Player")
            {
                other.GetComponent<PlayerCharacterController>().health--;
            }
    }

    public void getHit()
    {
        gameObject.GetComponent<Renderer>().material = hitMaterial;
        hitCooldown = 10;
        health--;
    }
}
