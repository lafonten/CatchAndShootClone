using System;
using UnityEngine;

public class enemyAI : MonoBehaviour
{
    
    [Header("About Bullet")]
    public Transform BulletTransform;
    public bool isTriggerBullet = false;


    [Header("Enemy Features")]
    public float EnemySpeed = 5f;
    private Animator myAnimator;


    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isTriggerBullet == true)
        {
            FollowToBullet();
        }
    }

    public void FollowToBullet()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(BulletTransform.position.x - transform.position.x, 0, BulletTransform.position.z - transform.position.z)), 5f * Time.deltaTime);
        transform.position += transform.forward * Time.deltaTime * EnemySpeed;
        myAnimator.SetTrigger("run");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet"))
        {
            isTriggerBullet = true;
        }
    }

    public void Kick()
    {
        if (enemyKickArea.eKAinstance.isinKickArea == true)
        {
            myAnimator.SetTrigger("kick");
        }
    }
}

public enum EnemyState
{
    idle,
    run,
    kick
}

