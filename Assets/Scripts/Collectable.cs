using UnityEngine;

public class Collectable : MonoBehaviour
{
    public Player Player;
    public GameManager manager;
    
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        Player = FindObjectOfType<Player>();
    }
}
