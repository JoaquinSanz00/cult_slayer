using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;
    [SerializeField] GameObject[] fires;
    [SerializeField] float spawnRate;
    [SerializeField] public int totalKills;
    [SerializeField] public int totalEnemies;
    [SerializeField] TextMeshProUGUI totalKillsText;
    void Start()
    {
        StartCoroutine(SpawnEnemy());

        spawnRate = 2f;
    }

    void Update()
    {
        if (totalEnemies >= 5)
        {
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        }

        totalKillsText.text = $"{totalKills}";

        IncreaseSpawnRate();
    }

    IEnumerator SpawnEnemy()
    {
        Vector3 randomLocation = new Vector3(Random.Range(-6.5f, 6.5f), Random.Range(-3.6f, 3.6f), 0f);

        GameObject newEnemy = Instantiate(enemies[Random.Range(0, 2)],randomLocation , Quaternion.identity);

        totalEnemies++;

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

    void IncreaseSpawnRate()
    {
        switch(totalKills)
        {
            case 5:
                spawnRate = 1.75f;
                break;

            case 15:
                spawnRate = 1.50f;
                break;

            case 30:
                spawnRate = 1.25f;
                break;

            case 40:
                spawnRate = 1f;
                break;

            case 50:
                spawnRate = 0.75f;
                break;

            case 60:
                spawnRate = 0.6f;
                break;

            case 70:
                spawnRate = 0.5f;
                break;

            case 90:
                spawnRate = 0.4f;
                break;

            case 120:
                spawnRate = 0.3f;
                break;

            case 150:
                spawnRate = 0.2f;
                break;

            case 200:
                spawnRate = 0.1f;
                break;
        }

        foreach(GameObject fire in fires)
        {
            fire.GetComponentInChildren<Animator>().speed = 1 / spawnRate;
        }
    }
}
