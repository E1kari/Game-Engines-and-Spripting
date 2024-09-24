using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    public float delayDestroy = 5;
    void Start()
    {
        //gameObject is a predefined object, which points to the component this script is attached to
        Destroy(gameObject,delayDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
