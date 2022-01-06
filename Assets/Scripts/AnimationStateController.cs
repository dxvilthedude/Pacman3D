using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    public Animation BloodImage;
    private Animator animator;
    private ThirdPersonMovement movement;
    private bool forwardPressed;
    private int isRunningHash;
    private int takesDamageHash;
    private int playerDeathHash;
    private int danceHash;
    void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<ThirdPersonMovement>();
        isRunningHash = Animator.StringToHash("isRunning");
        takesDamageHash = Animator.StringToHash("takesDamage");
        playerDeathHash = Animator.StringToHash("playerDeath");
        danceHash = Animator.StringToHash("isDancing");
    }

    

    void Update()
    {
        forwardPressed = (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D));
        if (forwardPressed)
        {
            animator.SetBool(isRunningHash, true);
        }
        else
            animator.SetBool(isRunningHash, false);
    }

    public void takesDamage(int health)
    {
        BloodImage.Play();
        movement.CanMove = false;
        if (health > 0)
            animator.SetBool(takesDamageHash, true);
        else
            animator.SetBool(playerDeathHash, true);
    }

    public void DamageTaken()
    {
        animator.SetBool(takesDamageHash, false);
        movement.CanMove = true;
    }
    public void VictoryDance()
    {
        animator.SetBool(danceHash, true);
    }
}
