using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Vector3 dashDir;
    [SerializeField] Vector3 mousePos;
    [SerializeField] Rigidbody playerRb;
    [SerializeField] float speed;
    [SerializeField] GameObject playerSprite;

    [SerializeField] bool didDamage;

    void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        Dash();
    }

    void Dash()
    {
        if (Input.GetKeyDown("z") || Input.GetKeyDown("x"))
        {
            didDamage = false;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dashDir = mousePos - transform.position;
            dashDir.z = 0f;
            dashDir.Normalize();
            playerRb.AddForce(dashDir * speed, ForceMode.Impulse);

            if (dashDir.x > 0)
            {
                playerSprite.transform.localScale = new Vector3(10f, 10f, 10f);
            }

            if (dashDir.x < 0)
            {
                playerSprite.transform.localScale = new Vector3(-10f, 10f, 10f);
            }
        }    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && !didDamage)
        {
            Debug.Log("Enemy Hit");
            didDamage = true;
            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;
            other.gameObject.GetComponent<EnemyController>().health--;
            other.gameObject.GetComponent<EnemyController>().Die(dashDir);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "DeathWall")
        {
            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;
            /*
            if (!didDamage)
            {
                Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
            }*/
        }
    }
}
