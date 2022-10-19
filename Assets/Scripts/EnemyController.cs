using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] public int health;
    [SerializeField] public ParticleSystem deathParticle;
    [SerializeField] public ParticleSystem deathExplosion;
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] SpriteRenderer shadow;
    [SerializeField] public SpriteRenderer damageSprite;
    [SerializeField] public bool dead;
    [SerializeField] public bool isGolden;

    void OnEnable()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    void Update()
    {
        ClearForGoldenSpawn();
    }

    public void Die(Vector3 deathAngle)
    {
        if (health == 0 && !dead)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;
            shadow.enabled = false;

            float particleAngle = Mathf.Atan2(deathAngle.y, deathAngle.x) * Mathf.Rad2Deg;
            deathParticle.gameObject.transform.rotation = Quaternion.Euler(particleAngle + 180f, -90f, 90f);
            deathParticle.Play();
            deathExplosion.Play();

            enemySpawner.currentEnemies--;
            enemySpawner.totalKills++;
            enemySpawner.currentKills++;
            dead = true;

            if (isGolden)
            {
                enemySpawner.goldenSpawn = false;
                foreach (ParticleSystem flame in gameObject.GetComponent<GoldenMiniBoss>().flames)
                {
                    flame.Stop();                 
                }
                enemySpawner.currentEnemies = 0;
                enemySpawner.ResumeSpawning();
            } 
        }
    }

    void ClearForGoldenSpawn()
    {
        if (enemySpawner.goldenSpawn && !dead && !isGolden)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;
            shadow.enabled = false;
            //float particleAngle = Mathf.Atan2(deathAngle.y, deathAngle.x) * Mathf.Rad2Deg;
            //deathParticle.gameObject.transform.rotation = Quaternion.Euler(particleAngle + 180f, -90f, 90f);
            //deathParticle.Play();
            deathExplosion.Play();
            enemySpawner.currentEnemies--;
            dead = true;
        }
    }
}
