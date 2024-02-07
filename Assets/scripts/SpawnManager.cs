
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] powerups;
    [SerializeField]
    private bool _stopSpawing = false;
   
    public void StartSpawing()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

   


//spawn game object every 5 seconds
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while(_stopSpawing == false)
        {
        
        Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
        GameObject newEnemy = Instantiate(_enemyPrefab,posToSpawn, Quaternion.identity);
        newEnemy.transform.parent = _enemyContainer.transform;
        yield return new WaitForSeconds(3.0f);
        }

    }


    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while(_stopSpawing == false)
        {
            Vector3 postToSpawn = new Vector3(Random.Range(-8f,8f),7,0);
            int randomPowerUp = Random.Range(0,3);
            Instantiate(powerups[randomPowerUp],postToSpawn,Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3,8));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawing = true;
    }
}
