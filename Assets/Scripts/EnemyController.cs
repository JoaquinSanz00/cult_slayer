using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] public enum EnemyType { normal, dark, golden, shadow, tower }
    [SerializeField] public EnemyType enemyType;

    [SerializeField] GameObject[] eyes;
    [SerializeField] int eyesAmount;

    [SerializeField] CrossHair crossHair;

    void OnEnable()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        crossHair = FindObjectOfType<CrossHair>();

        if (enemyType == EnemyType.tower)
        {
            foreach(GameObject eye in eyes)
            {
                LeanTween.color(eye, Color.red, 1f);
            }
        }
    }

    void Update()
    {
        ClearForBoss();
    }

    private void OnMouseEnter()
    {
        crossHair.isRed = true;
        crossHair.ChangeCrossHair();
    }

    private void OnMouseExit()
    {
        crossHair.isRed = false;
        crossHair.ChangeCrossHair();
    }

    public void TakeDamage(Vector3 dashDir)
    {
        if (enemyType != EnemyType.shadow)
        {
            health--;
            Die(dashDir);
            LeanTween.color(damageSprite.gameObject, Color.white, 0.025f).setLoopPingPong(1);
            LeanTween.scale(gameObject, gameObject.transform.localScale * 1.1f, 0.025f).setLoopPingPong(1);
        }

        if (enemyType == EnemyType.tower && eyesAmount < maxHealth)
        {
            LeanTween.color(eyes[eyesAmount], Color.clear, 0.1f);
            eyesAmount++;
        }

        if (enemyType == EnemyType.normal && eyesAmount <= maxHealth && !dead)
        {
            LeanTween.color(eyes[eyesAmount - 1], Color.gray, 0.1f);
            eyesAmount--;
        }

        if (enemyType == EnemyType.shadow)
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

            if (eyes != null)
            {
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
    }

    public void Die(Vector3 deathAngle)
    {
        if (health == 0 && !dead && (enemyType != EnemyType.shadow))
        {
            LeanTween.color(damageSprite.gameObject, Color.clear, 0.025f);
            damageSprite.gameObject.SetActive(false);

            gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;
            shadow.enabled = false;

            float particleAngle = Mathf.Atan2(deathAngle.y, deathAngle.x) * Mathf.Rad2Deg;
            deathParticle.gameObject.transform.rotation = Quaternion.Euler(particleAngle + 180f, -90f, 90f);
            deathParticle.Play();
            deathExplosion.Play();

            switch (enemyType)
            {
                case EnemyType.normal:
                    foreach (GameObject eye in eyes)
                    {
                        eye.SetActive(false);
                    }
                    enemySpawner.totalKills++;
                    enemySpawner.currentKills++;
                    enemySpawner.currentEnemies--;
                    break;

                case EnemyType.dark:
                    enemySpawner.currentEnemies--;
                    break;

                case EnemyType.golden:
                    enemySpawner.bossAlive = false;
                    foreach (ParticleSystem flame in gameObject.GetComponent<GoldenMiniBoss>().flames)
                    {
                        flame.Stop();
                    }
                    enemySpawner.currentEnemies = 0;
                    enemySpawner.totalKills++;
                    enemySpawner.currentKills++;
                    enemySpawner.ResumeSpawning();
                    break;

                case EnemyType.tower:
                    gameObject.GetComponentInChildren<ParticleSystem>().Stop();
                    break;

            }
            dead = true;
        }

        if (enemyType == EnemyType.shadow && !enemySpawner.bossAlive)
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
        if (enemySpawner.bossAlive && !dead && enemyType == EnemyType.normal)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;
            shadow.enabled = false;
            deathExplosion.Play();
            enemySpawner.currentEnemies--;
            dead = true;
        }

        if (!enemySpawner.bossAlive && !dead && enemyType == EnemyType.dark)
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
