using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultistBossController : MonoBehaviour
{
    [SerializeField] GameObject[] pillars;
    [SerializeField] GameObject[] darkEnemies;
    [SerializeField] GameObject chairCultist;
    [SerializeField] EnemySpawner enemySpawner;

    [SerializeField] ParticleSystem deathExplosion;
    [SerializeField] ParticleSystem chairCultistParticles;
    bool spawning;
    bool bossDead;

    void OnEnable()
    {
        spawning = true;

        chairCultist = GameObject.Find("CultBoss");
        chairCultistParticles = chairCultist.GetComponentInChildren<ParticleSystem>();

        enemySpawner = FindObjectOfType<EnemySpawner>();

        StartCoroutine(SpawnBoss());
    }

    void Update()
    {
        if (!spawning)
        {
            BossDie();
        }
    }

    IEnumerator SpawnBoss()
    {
        chairCultist.GetComponent<Animator>().enabled = false;
        chairCultist.GetComponent<IdleAnimController>().enabled = false;
        chairCultistParticles.Play();
        LeanTween.color(chairCultist, Color.clear, 0.5f);  

        yield return new WaitForSeconds(2f);

        gameObject.GetComponentInChildren<Animator>().SetBool("spawn", true);

        SpawnPillars();

        yield return new WaitForSeconds(2f);

        StartSpawning();

        spawning = false;
    }

    void StartSpawning()
    {
        StartCoroutine(SpawnDarkEnemy());
    }

    void SpawnPillars()
    {
        foreach (GameObject pillar in pillars)
        {
            pillar.SetActive(true);
            pillar.transform.localScale = Vector3.zero;
            LeanTween.scale(pillar, new Vector3(1f, 1f, 1f), 0.3f).setEase(LeanTweenType.easeOutBack);
            pillar.GetComponentInChildren<Animator>().SetBool("spawn", true);
        }
    }

    public IEnumerator SpawnDarkEnemy()
    {
        if (!bossDead)
        {
            Vector3 randomLocation = new Vector3(Random.Range(-5f, 5f), Random.Range(-3f, 3f), 0f);

            GameObject newEnemy = Instantiate(darkEnemies[Random.Range(0, darkEnemies.Length)], randomLocation, Quaternion.identity);

            enemySpawner.currentEnemies++;

            newEnemy.transform.localScale = Vector3.zero;
            LeanTween.scale(newEnemy, new Vector3(1f, 1f, 1f), 0.3f).setEase(LeanTweenType.easeOutBack);
            newEnemy.GetComponentInChildren<Animator>().SetBool("spawn", true);

            yield return new WaitForSeconds(0.9f);

            if (!enemySpawner.gameOver) StartCoroutine(SpawnDarkEnemy());
        }
    }

    void BossDie()
    {
        if (pillars[0].GetComponent<EnemyController>().dead && pillars[1].GetComponent<EnemyController>().dead && pillars[2].GetComponent<EnemyController>().dead && pillars[3].GetComponent<EnemyController>().dead && !bossDead)
        {
            deathExplosion.Play();
            LeanTween.color(gameObject, Color.clear, 1.5f);
            bossDead = true;
        }
    }
}
