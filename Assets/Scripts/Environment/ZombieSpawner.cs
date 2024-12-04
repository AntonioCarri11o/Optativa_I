using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{

    public GameObject zombie;
    public Transform[] spawnPoints;
    public float spawnInterval = 5f;
    public int maxZombies = 10;

    private int currentZombieCount = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(SpawnZombie), spawnInterval, spawnInterval);
    }
    private void SpawnZombie()
    {
        if(currentZombieCount >= maxZombies)
        {
            return;
        }

        Transform spawnPoint = spawnPoints[Random.RandomRange(0, spawnPoints.Length)];
        Instantiate(zombie, spawnPoint.position, spawnPoint.rotation);
        currentZombieCount++;
    }
}
