using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyKickArea : MonoBehaviour
{
    public bool isinKickArea = false;
    public static enemyKickArea eKAinstance;

    void Start()
    {
        eKAinstance = this;
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isinKickArea = true;
            Player.ÝnstancePlayer.enabled = false;
        }
    }
}
