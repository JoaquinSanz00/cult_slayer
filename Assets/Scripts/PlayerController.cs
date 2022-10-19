using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float dashSpeed;
    [SerializeField] GameObject playerSprite;

    [SerializeField] bool didDamage;
    [SerializeField] bool canDash;
    [SerializeField] ParticleSystem stunParticles;
    [SerializeField] EnemySpawner enemySpawner;

    GameObject currentEnemy;
    Vector3 dashDir;

    private Ray mouseRay;
    private RaycastHit hit;

    void Start()
    {
        canDash = true;
    }

    void Update()
    {
        Dash();
    }

    void Dash()
    {
        mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetKeyDown("z") || Input.GetKeyDown("x"))
        {
            if (enemySpawner.gameOver) return;

            if (Physics.Raycast(mouseRay.origin, mouseRay.direction, out hit))
            {
                didDamage = false;

                if (hit.collider.transform.tag == "Enemy" && !didDamage && canDash)
                {
                    canDash = false;
                    currentEnemy = hit.collider.gameObject;
                    dashDir = hit.collider.gameObject.transform.position - transform.position;
                    dashDir.z = 0f;
                    dashDir.Normalize();
                    LeanTween.move(gameObject, hit.collider.gameObject.transform.position + (dashDir * 1.2f), dashSpeed).setOnComplete(DealDamage);
                }

                if (hit.collider.transform.tag == "DeathWall" && canDash)
                {
                    dashDir = hit.collider.gameObject.transform.position - transform.position;
                    canDash = false;
                    LeanTween.move(gameObject, hit.point, 0.1f).setOnComplete(GetStunned);
                }

                if (dashDir.x > 0) playerSprite.transform.localScale = new Vector3(10f, 10f, 10f);
                if (dashDir.x < 0) playerSprite.transform.localScale = new Vector3(-10f, 10f, 10f);
            }
        }
    }

    void DealDamage()
    {
        if (!didDamage)
        {
            EnemyController enemy = currentEnemy.GetComponent<EnemyController>();
            didDamage = true;
            enemy.health--;
            enemy.Die(dashDir);
            LeanTween.color(enemy.damageSprite.gameObject, Color.white, 0.025f).setLoopPingPong(1);
            LeanTween.scale(currentEnemy, currentEnemy.transform.localScale * 1.1f, 0.025f).setLoopPingPong(1);
            canDash = true;
        }
    }

    void GetStunned()
    {
        StartCoroutine(GetStunnedCR());
    }
    IEnumerator GetStunnedCR()
    {
        stunParticles.Play();
        canDash = false;

        yield return new WaitForSeconds(0.5f);

        canDash = true;
    }
}
