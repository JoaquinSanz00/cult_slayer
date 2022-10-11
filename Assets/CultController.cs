using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultController : MonoBehaviour
{
    [SerializeField] bool isBoss;

    void Start()
    {
        StartCoroutine(CultAnim());
    }

    IEnumerator CultAnim()
    {
        if (isBoss)
        {
            gameObject.GetComponent<Animator>().SetBool("idleBoss", true);
        }
        else
        {
            yield return new WaitForSeconds(Random.Range(0.1f, 1f));
            gameObject.GetComponent<Animator>().SetBool("idle", true);
        }
    }
}
