using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultistBossController : MonoBehaviour
{
    [SerializeField] Transform[] spawnPositions;
    [SerializeField] GameObject[] darkEnemies;
    [SerializeField] EnemySpawner enemySpawner;
    int spawnIndex;

    void OnEnable()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        spawnIndex = 0;
        LeanTween.delayedCall(2f, StartSpawning);
    }

    void Update()
    {
        
    }

    void StartSpawning()
    {
        StartCoroutine(SpawnDarkEnemy());
    }

    public IEnumerator SpawnDarkEnemy()
    {
        GameObject newEnemy = Instantiate(darkEnemies[Random.Range(0, darkEnemies.Length)], spawnPositions[spawnIndex].position, Quaternion.identity);

        enemySpawner.currentEnemies++;

        newEnemy.transform.localScale = Vector3.zero;
        LeanTween.scale(newEnemy, new Vector3(1f, 1f, 1f), 0.3f).setEase(LeanTweenType.easeOutBack);
        newEnemy.GetComponentInChildren<Animator>().SetBool("spawn", true);

        yield return new WaitForSeconds(1f);

        spawnIndex++;

        if (!enemySpawner.gameOver) StartCoroutine(SpawnDarkEnemy()); 
    }
}
