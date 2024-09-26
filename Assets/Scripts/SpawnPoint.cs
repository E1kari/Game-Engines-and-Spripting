using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public RaycastHit hit;


    // Start is called before the first frame update
    void Start()
    {
        Physics.Raycast(transform.position, -Vector3.up, out hit);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDrawGizmos()
    {
        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(transform.position, hit.point);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(hit.point, 0.5f);
        }
    }
}
