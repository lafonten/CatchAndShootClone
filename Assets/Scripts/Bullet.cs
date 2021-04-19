using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject player;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        
    }

    public void relase(Vector3 throwForce)
    {
        transform.SetParent(null);
        rb.isKinematic = false;
        rb.AddForce(throwForce,ForceMode.VelocityChange);
    }

    public void SnapToPlayer(Player player)
    {
        rb.isKinematic = true;
        transform.SetParent(player.bulletPlace);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
}
