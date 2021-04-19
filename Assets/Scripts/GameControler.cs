using System;
using System.Collections;
using UnityEngine;

public class GameControler : MonoBehaviour
{

    public Player activePlayer;
    public static GameControler instance;
    public Bullet bullet;
    public GameObject bulletObj;
    private float distanceY;
    private GameObject playerGameObject;
    public bool isEnable;

    

    void Start()
    {
        playerGameObject = GameObject.FindWithTag("Player");
        instance = this;
        bulletObj = GameObject.FindGameObjectWithTag("bullet");
    }

    
    void Update()
    {
        CheckUserInput();
    }

    public void StartGame()
    {
        activePlayer.StartRunning();
    }

    void CheckUserInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            activePlayer.RotateLeft();
            
        }else if (Input.GetKey(KeyCode.D))
        {
            activePlayer.RotateRight();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartGame();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            activePlayer.StartThrowing();
        }
    }

    public void OnPlayerThrow(Player player)
    {
        cameraController.instance.SwitchState(cameraState.bullet);
    }

    public void OnPlayerCatch(Player player)
    {
        isEnable = false;
        activePlayer = player;
        Tracking.trackingInstance.enabled = false;
        bullet.SnapToPlayer(activePlayer);
        activePlayer.transform.rotation = Quaternion.identity;
        activePlayer.StartRunning();
        cameraController.instance.SetNewTargetPlayer(activePlayer);
        cameraController.instance.SwitchState(cameraState.player);
    }

    //IEnumerator Catch(Player activePlayer)
    //{
    //    playerGameObject.GetComponent<Animator>().SetTrigger("catch");   
    //    bullet.SnapToPlayer(activePlayer);
    //    activePlayer.transform.rotation = Quaternion.identity;
    //    activePlayer.StartRunning();
    //}

}
