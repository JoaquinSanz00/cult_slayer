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
    [SerializeField] public int currentKills;
    [SerializeField] public int totalKills;
    [SerializeField] public int totalEnemies;
    [SerializeField] TextMeshProUGUI totalKillsText;
    [SerializeField] int enemy1RNG, enemy2RNG, enemy3RNG, enemy4RNG;
    int enemyIndex;
    void Start()
    {
        spawnRate = 1.5f;

        enemyIndex = 0;
        enemy1RNG = 100;
        enemy2RNG = 105;
        enemy3RNG = 105;
        enemy4RNG = 105;

        StartCoroutine(SpawnEnemy());
    }

    void Update()
    {
        if (totalEnemies >= 6)
        {
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        }

        totalKillsText.text = $"{totalKills}";

        IncreaseSpawnRate();
    }

    IEnumerator SpawnEnemy()
    {
        int enemyRange = Random.Range(1, 100);
        Debug.Log(enemyRange);
        if (enemyRange <= enemy1RNG) enemyIndex = 0;
        if (enemyRange > enemy1RNG && enemyRange < enemy3RNG) enemyIndex = 1;
        if (enemyRange > enemy2RNG && enemyRange < enemy4RNG) enemyIndex = 2;
        if (enemyRange >= enemy3RNG) enemyIndex = 3;

        Vector3 randomLocation = new Vector3(Random.Range(-6.5f, 6.5f), Random.Range(-3.6f, 3.6f), 0f);

        GameObject newEnemy = Instantiate(enemies[enemyIndex],randomLocation , Quaternion.identity);

        totalEnemies++;

        newEnemy.transform.localScale = Vector3.zero;
        LeanTween.scale(newEnemy, new Vector3(1f, 1f, 1f), 0.3f).setEase(LeanTweenType.easeOutBack);
        newEnemy.GetComponentInChildren<Animator>().SetBool("spawn", true);

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
        if (currentKills == 5)
        {
            spawnRate *= 0.95f;
            currentKills = 0;
        }

        switch (totalKills)
        {
            case 5:
                enemy1RNG = 90;
                enemy2RNG = 100;
                enemy3RNG = 101;
                enemy4RNG = 101;
                break;
            case 15:
                enemy1RNG = 80;
                enemy2RNG = 95;
                enemy3RNG = 100;
                enemy4RNG = 101;
                break;
            case 20:
                enemy1RNG = 70;
                enemy2RNG = 90;
                enemy3RNG = 100;
                enemy4RNG = 101;
                break;
            case 30:
                enemy1RNG = 65;
                enemy2RNG = 90;
                enemy3RNG = 100;
                enemy4RNG = 101;
                break;
            case 40:
                enemy1RNG = 60;
                enemy2RNG = 85;
                enemy3RNG = 95;
                enemy4RNG = 100;
                break;
            case 55:
                enemy1RNG = 50;
                enemy2RNG = 80;
                enemy3RNG = 90;
                enemy4RNG = 100;
                break;
            case 70:
                enemy1RNG = 35;
                enemy2RNG = 70;
                enemy3RNG = 85;
                enemy4RNG = 100;
                break;
            case 90:
                enemy1RNG = 20;
                enemy2RNG = 60;
                enemy3RNG = 80;
                enemy4RNG = 100;
                break;
            case 100:
                enemy1RNG = 10;
                enemy2RNG = 50;
                enemy3RNG = 75;
                enemy4RNG = 100;
                break;
        }

        foreach (GameObject fire in fires)
        {
            fire.GetComponentInChildren<Animator>().speed = 1 / spawnRate;
        }
    }
}
