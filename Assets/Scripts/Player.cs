using System;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public enum PlayerState
    {
        idle,
        catching,
        running,
        throwing,
        afterThrowing
    }

    [Space(20, order = 0)]
    [Header("Move", order = 2)]
    public Vector3 direction = Vector3.forward;
    public float playerSpeed = 2f;

    [Space(20, order = 0)]
    [Header("Rotation", order = 2)]
    public float lookAngle = 0;
    public float maxLookAngle = 30;
    public float turnSpeed = 1;

    [Space(20, order = 0)]
    [Header("Throw", order = 2)]
    public float throwHigh;
    public float throwDistance;

    [Space(20, order = 0)]
    [Header("bullet", order = 2)]
    public Transform bulletPlace;
    public Transform bulletTransform;
    public GameObject BulletGameObject;
    public Bullet bullet;

    [Space(20, order = 0)]
    [Header("Player", order = 2)]
    public GameObject player2;
    public static Player ÝnstancePlayer;

    [Space(20, order = 0)]
    [Header("Animation and Enum", order = 2)]
    public PlayerState playerState = PlayerState.idle;
    private Animator myAnimator;




    void Start()
    {
        BulletGameObject = GameObject.FindGameObjectWithTag("bullet");
        bullet = GameControler.instance.bullet;
        myAnimator = GetComponent<Animator>();
        player2 = GameObject.FindGameObjectWithTag("player1");
        ÝnstancePlayer = this;
    }


    void Update()
    {
        Move();
        

    }

    public void Move()
    {
        direction = transform.forward;
        if (BarrierChecker(transform.position) == null)
        {
            if (playerState == PlayerState.running)
            {
                transform.position += direction * playerSpeed * Time.deltaTime;
            }
        }
    }

    public void RotateLeft()
    {
        lookAngle -= turnSpeed * Time.deltaTime;
        lookAngle = Mathf.Clamp(lookAngle, -maxLookAngle, maxLookAngle);
        transform.rotation = Quaternion.Euler(0, lookAngle, 0);

        if (BarrierChecker(transform.position) == "barrier")
        {
            transform.position += new Vector3(-playerSpeed * Time.deltaTime, 0, 0);
        }
    }

    public void RotateRight()
    {
        lookAngle += turnSpeed * Time.deltaTime;
        lookAngle = Mathf.Clamp(lookAngle, -maxLookAngle, maxLookAngle);
        transform.rotation = Quaternion.Euler(0, lookAngle, 0);

        if (BarrierChecker(transform.position) == "barrier")
        {
            transform.position += new Vector3(playerSpeed * Time.deltaTime, 0, 0);
        }
    }

    string BarrierChecker(Vector3 position)
    {
        RaycastHit hit;

        Vector3 originLeft = new Vector3(position.x + 0.45f, position.y + 0.1f, position.z);
        Vector3 originRight = new Vector3(position.x - 0.45f, position.y + 0.1f, position.z);
        Vector3 originMiddle = new Vector3(position.x, position.y + 0.5f, position.z);

        Ray rayMiddle = new Ray(originMiddle, direction);
        Ray rayLeft = new Ray(originLeft, direction);
        Ray rayRight = new Ray(originRight, direction);


        if (Physics.Raycast(rayMiddle, out hit, 0.1f) || Physics.Raycast(rayRight, out hit, 0.1f) || Physics.Raycast(rayLeft, out hit, 0.1f))
        {
            if (hit.collider.name == "barrier")
            {
                return "barrier";

            }
            else
            {
                return null;
            }
        }

        Debug.DrawRay(originLeft, direction, Color.red);
        Debug.DrawRay(originMiddle, direction, Color.blue);
        Debug.DrawRay(originRight, direction, Color.green);

        return null;
    }

    public void StartThrowing()
    {
        myAnimator.SetTrigger("throw");
        playerState = PlayerState.throwing;
    }

    void ThrowTheBall()
    {
        Vector3 throwForce = direction * throwDistance;
        throwForce.y = throwHigh;
        bullet.relase(throwForce);
        GameControler.instance.OnPlayerThrow(this);
        enabled = true;
        Destroy(gameObject,1.5f);
        playerState = PlayerState.afterThrowing;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet"))
        {
            GetComponent<Collider>().enabled = false;
            GameControler.instance.OnPlayerCatch(this);
            GameControler.instance.isEnable = false;
        }
    }

    public void StartRunning()
    {
        playerState = PlayerState.running;
        myAnimator.SetTrigger("run");
        GameControler.instance.isEnable = false;
    }

    public void Death()
    {
        if (enemyKickArea.eKAinstance.isinKickArea == true)
        {
            myAnimator.SetTrigger("death");
            enabled = false;
        }
    }
}
