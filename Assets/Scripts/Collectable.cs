using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    public Player Player;
    public GameManager manager;
    
    protected virtual void Start()
    {
        manager = FindObjectOfType<GameManager>();
        Player = FindObjectOfType<Player>();
    }
}
