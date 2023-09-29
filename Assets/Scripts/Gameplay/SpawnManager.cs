using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerSphere : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    
    private int enemyCount;
    [SerializeField] private float spawnRangeXMin;
    [SerializeField] private float spawnRangeXMax;
    [SerializeField] private float spawnRangeZMin;
    [SerializeField] private float spawnRangeZMax;
    [SerializeField] private float timeForNextWave=1.8f;
    
    private float spawnPosX;
    private float spawnPosZ;
    private int waveCount;
    private bool spawnValidation;
    
    // Start is called before the first frame update
    void Start()
    {
        waveCount = 0;
        spawnValidation = true;
    }

    private void SpawnNewWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            enemyCount = i;
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        spawnPosX = Random.Range(spawnRangeXMin, spawnRangeXMax);
        spawnPosZ = Random.Range(spawnRangeZMin, spawnRangeZMax);
        return new Vector3(spawnPosX, enemyPrefab.transform.position.y, spawnPosZ);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<EnemyRikayon>().Length;
        if (enemyCount == 0 && spawnValidation)
        {
            // Busca objetos con el tag "PackSpawn" y bórralos
            GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag("PackSpawn");
            foreach (GameObject obj in objectsToDestroy)
            {
                Destroy(obj);
            }
            waveCount += 1;
            
            
            spawnValidation = false;
            StartCoroutine(WaveSpawnAfterDelay(timeForNextWave));
        }
    }
    
    // Corutina que se inicia después de un retraso de 1.8 segundos
    
    private IEnumerator WaveSpawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnNewWave(waveCount);
        spawnValidation = true;
    }

}