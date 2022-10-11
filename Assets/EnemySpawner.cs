using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;
    [SerializeField] int spawnRate;
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    void Update()
    {
        
    }

    IEnumerator SpawnEnemy()
    {
        Vector3 randomLocation = new Vector3(Random.Range(-6.5f, 6.5f), Random.Range(-3.6f, 3.6f), 0f);

        GameObject newEnemy = Instantiate(enemies[Random.Range(0, 2)],randomLocation , Quaternion.identity);

        newEnemy.transform.localScale = Vector3.zero;
        LeanTween.scale(newEnemy, new Vector3(1f, 1f, 1f), 0.3f).setEase(LeanTweenType.easeOutBack);

        if (randomLocation.x > 0)
        {
            newEnemy.GetComponentInChildren<SpriteRenderer>().gameObject.transform.localScale = new Vector3(10f, 10f, 10f);
        }

        if (randomLocation.x < 0)
        {
            newEnemy.GetComponentInChildren<SpriteRenderer>().gameObject.transform.localScale = new Vector3(-10f, 10f, 10f);
        }

        yield return new WaitForSeconds(spawnRate);

        StartCoroutine(SpawnEnemy());
    }
}
