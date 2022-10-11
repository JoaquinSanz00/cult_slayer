using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] public int health;
    [SerializeField] public ParticleSystem deathParticle;

    void Start()
    {
        
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
            float particleAngle = Mathf.Atan2(deathAngle.y, deathAngle.x) * Mathf.Rad2Deg;
            deathParticle.gameObject.transform.rotation = Quaternion.Euler(particleAngle + 180f, -90f, 90f);
            deathParticle.Play();
        }
    }
}
