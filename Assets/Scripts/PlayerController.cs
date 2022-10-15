using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Vector3 dashDir;
    [SerializeField] float speed;
    [SerializeField] GameObject playerSprite;

    [SerializeField] bool didDamage;
    [SerializeField] bool canDash;
    [SerializeField] GameObject currentEnemy;
    [SerializeField] ParticleSystem stunParticles;

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
            if (Physics.Raycast(mouseRay.origin, mouseRay.direction, out hit))
            {
                didDamage = false;

                if (hit.collider.transform.tag == "Enemy" && !didDamage && canDash)
                {
                    currentEnemy = hit.collider.gameObject;
                    dashDir = hit.collider.gameObject.transform.position - transform.position;
                    dashDir.z = 0f;
                    dashDir.Normalize();
                    LeanTween.move(gameObject, hit.collider.gameObject.transform.position + (dashDir * 1.2f), 0.1f).setOnComplete(DealDamage);
                }

                if (hit.collider.transform.tag == "DeathWall" && canDash)
                {
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
            didDamage = true;
            currentEnemy.GetComponent<EnemyController>().health--;
            currentEnemy.gameObject.GetComponent<EnemyController>().Die(dashDir);
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
