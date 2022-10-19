using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenMiniBoss : MonoBehaviour
{
    [SerializeField] public ParticleSystem[] flames;
    [SerializeField] public GameObject flamesParent;
    [SerializeField] public EnemyController enemyController;
    [SerializeField] public EnemySpawner enemySpawner;
    [SerializeField] public int flameIndex;

    void OnEnable()
    {
        flameIndex = 0;
        enemySpawner = FindObjectOfType<EnemySpawner>();
        enemyController = gameObject.GetComponent<EnemyController>();

        LeanTween.rotateZ(flamesParent, 360f, 4f);

        StartCoroutine(SpawnFlames());
    }

    IEnumerator SpawnFlames()
    {
        yield return new WaitForSeconds(1f);

        if (!enemyController.dead)
        {
            flames[flameIndex].Play();
            enemySpawner.currentEnemies++;

            if (flameIndex <= 5)
            {
                flameIndex++;
                StartCoroutine(SpawnFlames());
            }
        }
    }
}
