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

    void OnEnable()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    void Update()
    {

    }

    public void Die(Vector3 deathAngle)
    {
        if (health == 0)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;
            shadow.enabled = false;
            float particleAngle = Mathf.Atan2(deathAngle.y, deathAngle.x) * Mathf.Rad2Deg;
            deathParticle.gameObject.transform.rotation = Quaternion.Euler(particleAngle + 180f, -90f, 90f);
            deathParticle.Play();
            deathExplosion.Play();
            enemySpawner.totalEnemies--;
            enemySpawner.totalKills++;
            enemySpawner.currentKills++;
        }
    }
}
