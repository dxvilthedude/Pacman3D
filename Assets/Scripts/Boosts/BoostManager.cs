using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class BoostManager : MonoBehaviour
{
    public TMP_Text BoostUI;
    public Player player;
    public TMP_Text BoostCounter;
    public TMP_Text BoostSpawnText;
    public GameObject[] BoostTypes;
    public bool BoostOnMap;
    public float SpawnBoostTime;
    private ThirdPersonMovement movement;
    private bool boostActive;
    private float currentTime = 0f;
    private float stargingTime = 20f;
    private GameManager gameManager;
    private float spawnCheckCollision = .5f;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        movement = player.GetComponent<ThirdPersonMovement>();
        InvokeRepeating("SpawnBoost",SpawnBoostTime,1f);
    }

    private void Update()
    {
        if (boostActive)
        {
            if (visionBoostActive)
            {
                SeeEnemies();
            }

            currentTime -= 1 * Time.deltaTime;
            if (currentTime < 0)
            {
                Unboost();                
            }
        }
        if (BoostUI.gameObject.activeSelf)
            BoostCounter.text = currentTime.ToString("0");   
    }
    //--------------------------
    //MMIEJSCE NA JOB DLA COINÓW
    //--------------------------

    private void SpawnBoost()
    {
        if (BoostOnMap)
            return;
       
        Vector3 spawnPoint = new Vector3(Random.Range(4, 44), 1, Random.Range(6, 60));
        while (Physics.CheckSphere(spawnPoint, spawnCheckCollision))
        {
            spawnPoint = new Vector3(Random.Range(4, 44), 1, Random.Range(6, 60));
        }
        Instantiate(BoostTypes[Random.Range(0, BoostTypes.Length)], spawnPoint, Quaternion.identity);
        BoostOnMap = true;        
    }
    public void SpawnInfo(string name)
    {
        BoostSpawnText.text = name + " APPEARS ON MAP!";
        BoostSpawnText.gameObject.SetActive(true);
        StartCoroutine(TurnOffAfterSeconds(3));
    }
    IEnumerator TurnOffAfterSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        BoostSpawnText.gameObject.SetActive(false);
    }
    public void SpeedBoost()
    {
        Unboost();
        currentTime = stargingTime;
        movement.speed = 10;
        boostActive = true;
        PanelActivate(true);
    }
    private bool isVisible;
    private bool visionBoostActive;
    private void SeeEnemies()
    {
        foreach (var enemy in gameManager.enemies)
        {
            var enemyScript = enemy.GetComponent<Enemy>();
            var enemyRenderer = enemy.GetComponentInChildren<Renderer>();
            if (Physics.Linecast(Camera.main.transform.position, enemy.transform.position))
            {
                    enemyRenderer.material = enemyScript.visionMaterial;
            }
            else
            {             
                    enemyRenderer.material = enemyScript.mainMaterial;
            }
        }
    }
    public void VisionBoost()
    {
        Unboost();
        currentTime = stargingTime;
        boostActive = true;
        visionBoostActive = true;
        PanelActivate(true);
    }
    private void Unboost()
    { 
        movement.speed = 6;
        PanelActivate(false);
        boostActive = false;
        visionBoostActive = false;
        MaterialCheck();
    }
    private void MaterialCheck()
    {
        foreach (var enemy in gameManager.enemies)
        {
            enemy.GetComponentInChildren<Renderer>().material = enemy.GetComponent<Enemy>().mainMaterial;
        }
    }
    public void PanelActivate(bool active)
    {
        BoostUI.gameObject.SetActive(active);
    }
}
