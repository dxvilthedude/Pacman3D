using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Menu;
    public TMP_Text MenuTitle;
    public GameObject Player;
    public GameObject EnemyPrefab;
    public GameObject SpawnPoint;
    public GameObject CoinPrefab;
    public TMP_Text Scoreboard;
    public TMP_Text HealthPoints;
    public BoostManager boostManager;
    public int PlayerHealth;
    public int Points = 0;
    public GameObject[] enemies;
    public Transform CoinsGO;
    public Player _player;
    private int maxPoints = 50;
    private float spawnCheckCollision = .5f;

    private void Start()
    {
        _player = Player.GetComponent<Player>();
        SpawnEnemy();
        SpawnCoins();
        ScoreUpdate();
        HealthUpdate();       
    }

    private void SpawnCoins()
    {
        for (int i = 0; i < maxPoints; i++)
        {
            Vector3 spawnPoint = new Vector3(Random.Range(4, 44), 1, Random.Range(6, 60));
            while (Physics.CheckSphere(spawnPoint, spawnCheckCollision))
            {
                spawnPoint = new Vector3(Random.Range(4, 44), 1, Random.Range(6, 60));
            }
            Instantiate(CoinPrefab, spawnPoint, Random.rotation,CoinsGO);
        }
    }
    private void SpawnEnemy()
    {
        for (int i = 0; i<4; i++)
        {
            enemies[i] = Instantiate(EnemyPrefab, SpawnPoint.transform.position + new Vector3(Random.Range(-3, 3), 0, Random.Range(-3, 3)), Quaternion.identity);
        }
    }
    public void ScoreUpdate()
    {
        Scoreboard.text = Points.ToString();
    }
    public void HealthUpdate()
    {
        HealthPoints.text = _player.Health.ToString();
    }
    public void CollectCoin()
    {
        Points++;
        ScoreUpdate();
        if (Points == 50)
            GameOver(true);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.P))
            CollectCoin();
    }
    public void GameOver(bool win)
    {
        
        MenuTitle.text = win ? "CONGRATULATIONS!!! \n YOU WON" : "GAME OVER";
        Menu.SetActive(true);
        
        Player.GetComponent<ThirdPersonMovement>().CanMove = false;
        if (win) _player.Win();
        foreach (var enemy in enemies)
        {
            enemy.GetComponent<Enemy>().PlayerWon();
            enemy.GetComponent<Enemy>().playerAlive = win;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
