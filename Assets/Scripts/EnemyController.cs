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
    private EnemyState state;
    private Animator animator;
    public AudioClip[] randomSounds_;
    private AudioSource audioSource_;

    protected enum EnemyState
    {
        following,
        attacking,
        dieing
    }
    NavMeshAgent agent;


    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.FindGameObjectsWithTag("Player")[0];

        attackTimer = attackCooldown;
        state = EnemyState.following;
        agent = GetComponent<NavMeshAgent>();
        agent.destination = character.transform.position;
        defaultMaterial = gameObject.GetComponent<Renderer>().material;
        animator = GetComponentInChildren<Animator>();

        audioSource_ = GetComponent<AudioSource>();
        StartCoroutine(PlaySounds());

    }

    // Update is called once per frame
    private void Update()
    {

        hitCooldown--;
        if (hitCooldown == 0)
        {
            gameObject.GetComponent<Renderer>().material = defaultMaterial;
        }

        switch (state)
        {
            case (EnemyState.following):
                //print("following");
                agent.destination = character.transform.position;
                animator.SetBool("IsWalking", true);
                //Debug.Log("Walking");
                break;

            case (EnemyState.attacking):
                //print("attacking");
                Vector3 lookTarget = character.transform.position;
                lookTarget.y = transform.position.y;
                transform.LookAt(lookTarget);
                attackTimer--;
                attack(character);
                animator.SetBool("IsWalking", false);
                //Debug.Log("Stopping");
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            state = EnemyState.attacking;

            Vector3 difference = character.transform.position - transform.position;
            agent.destination = transform.position + (difference / 2);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            attackTimer = attackCooldown;
            state = EnemyState.following;
        }
    }

    private void attack(GameObject attackObject)
    {
        if (attackTimer <= 0)
        {
            attackObject.GetComponent<PlayerCharacterController>().getHit();
            attackTimer = attackCooldown;
        }
    }

    public void getHit()
    {
        gameObject.GetComponent<Renderer>().material = hitMaterial;
        hitCooldown = 10;
        health--;
    }

    public void die()
    {
        state = EnemyState.dieing;
        animator.SetBool("IsDead", true);
        Destroy(gameObject, 5);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(gameObject.transform.position, gameObject.transform.position + gameObject.transform.forward);

        SphereCollider sphere = gameObject.GetComponent<SphereCollider>();
        Gizmos.DrawWireSphere(gameObject.transform.position + sphere.center, sphere.radius);
    }

    IEnumerator PlaySounds()
    {
        while (true)
        {
            audioSource_.clip = randomSounds_[Random.Range(0, randomSounds_.Length)];
            audioSource_.Play();
            yield return new WaitForSeconds(audioSource_.clip.length + 5);
        }
    }
}
