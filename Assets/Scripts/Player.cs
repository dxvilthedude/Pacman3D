using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Health = 5;
    private GameManager manager;
    private AnimationStateController animationController;
    private ThirdPersonMovement movement;
    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
        animationController = GetComponent<AnimationStateController>();
        movement = GetComponent<ThirdPersonMovement>();
    }
    public void TakeDamage()
    {
        if(Health > 0)
        Health--;

        manager.HealthUpdate();
        animationController.takesDamage(Health);
        CheckHealth();
    }

    private void CheckHealth()
    {
        if (Health == 0)
            manager.GameOver(false);
    }

    public void Win()
    {
        GetComponent<AnimationStateController>().VictoryDance();
    }
    
    private void Unboost()
    { movement.speed = 6; }
}
