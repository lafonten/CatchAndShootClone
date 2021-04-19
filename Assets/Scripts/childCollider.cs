using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class childCollider : MonoBehaviour
{

    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet"))
        {
            GameControler.instance.isEnable = true;
            Debug.Log("tragger");
        }
    }
}
