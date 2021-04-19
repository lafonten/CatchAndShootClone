using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracking : MonoBehaviour
{
    public Transform BulletTransform;
    private Animator myAnimator;
    public static Tracking trackingInstance;
    public float trackinSpeed = 10;

    void Start()
    {
        trackingInstance = this;
        myAnimator = GetComponent<Animator>();
    }

    
    void Update()
    {
        Debug.Log("ISENABLE = " + GameControler.instance.isEnable);
        if (GameControler.instance.isEnable == true)
        {
            Tracked();
        }
        
    }

    void Tracked()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(BulletTransform.position.x - transform.position.x,0,0)), 5f * Time.deltaTime);
        //transform.position += transform.forward * Time.deltaTime *trackinSpeed;
        if (transform.rotation.w <= 0)
        {
            Debug.Log("Left");
            transform.position += Vector3.left * Time.deltaTime * trackinSpeed;
        }else if (transform.rotation.w >= 0)
        {
            transform.position += Vector3.right * Time.deltaTime * trackinSpeed;
            Debug.Log("right");
        }
        
        myAnimator.SetTrigger("run");
    }
}
