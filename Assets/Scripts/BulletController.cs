using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        Physics.IgnoreCollision(GetComponent<Collider>(), player.GetComponent<CharacterController>());
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().getHit();
            Destroy(gameObject, 0);
        }
    }
}
