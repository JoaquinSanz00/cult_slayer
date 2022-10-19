using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] public int health;
    [SerializeField] public int maxHealth;
    [SerializeField] public ParticleSystem deathParticle;
    [SerializeField] public ParticleSystem deathExplosion;
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] SpriteRenderer shadow;
    [SerializeField] public SpriteRenderer damageSprite;
    [SerializeField] public bool dead;
    [SerializeField] public bool isShadow;
    [SerializeField] public bool isGolden;
    [SerializeField] GameObject[] eyes;

    void OnEnable()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    void Update()
    {
        ClearForBoss();
    }

    public void TakeDamage(Vector3 dashDir)
    {
        if (!isShadow)
        {
            health--;
            Die(dashDir);
            LeanTween.color(damageSprite.gameObject, Color.white, 0.025f).setLoopPingPong(1);
            LeanTween.scale(gameObject, gameObject.transform.localScale * 1.1f, 0.025f).setLoopPingPong(1);
        }

        if (isShadow)
        {
            LeanTween.color(damageSprite.gameObject, Color.white, 0.025f).setLoopPingPong(1);
            LeanTween.scale(gameObject, gameObject.transform.localScale * 1.1f, 0.025f).setLoopPingPong(1);

            if (health == maxHealth)
            {
                health = 0;
            }

            if (health <= maxHealth)
            {
                health++;
            }

            switch (health)
            {
                case 0:
                    LeanTween.color(eyes[0], Color.clear, 0.1f);
                    LeanTween.color(eyes[1], Color.clear, 0.1f);
                    LeanTween.color(eyes[2], Color.clear, 0.1f);
                    LeanTween.color(eyes[3], Color.clear, 0.1f);
                    break;

                case 1:
                    LeanTween.color(eyes[0], Color.red, 0.1f);
                    break;

                case 2:
                    LeanTween.color(eyes[1], Color.red, 0.1f);
                    break;

                case 3:
                    LeanTween.color(eyes[2], Color.red, 0.1f);
                    break;

                case 4:
                    LeanTween.color(eyes[3], Color.red, 0.1f);
                    break;
            }
        }
    }

    public void Die(Vector3 deathAngle)
    {
        if (health == 0 && !dead && !isShadow)
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
                enemySpawner.bossAlive = false;
                foreach (ParticleSystem flame in gameObject.GetComponent<GoldenMiniBoss>().flames)
                {
                    flame.Stop();                 
                }
                enemySpawner.currentEnemies = 0;
                enemySpawner.ResumeSpawning();
            } 
        }
        if (isShadow && !enemySpawner.bossAlive)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;
            shadow.enabled = false;
            deathExplosion.Play();
            dead = true;
        }
    }

    void ClearForBoss()
    {
        if (enemySpawner.bossAlive && !dead && !isGolden)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;
            shadow.enabled = false;
            deathExplosion.Play();
            enemySpawner.currentEnemies--;
            dead = true;
        }
    }
}
