using Cinemachine;
using Cinemachine.Editor;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    private Animator myAnimator;
    public static cameraController instance;
    public CinemachineVirtualCamera playerCamera; 
    
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        instance = this;
    }


    void Update()
    {
       
    }

    public void SwitchState(cameraState newState)
    {
        switch (newState)
        {
            case cameraState.player:
                myAnimator.Play("PlayerState");
                break;
            case cameraState.bullet:
                myAnimator.Play("BulletState");
                break;
        }
    }

    public void SetNewTargetPlayer(Player player)
    {
        playerCamera.Follow = player.transform;
        playerCamera.LookAt = player.transform;
    }

}

public enum cameraState
{
    player,
    bullet
}
