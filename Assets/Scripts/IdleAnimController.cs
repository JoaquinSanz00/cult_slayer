using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAnimController : MonoBehaviour
{
    [SerializeField] enum npcType {boss, minion, enemy, player};
    [SerializeField] npcType NPCType;

    void Start()
    {
        StartCoroutine(CultAnim());
    }

    IEnumerator CultAnim()
    {
        switch(NPCType)
        {
            case npcType.boss:
                gameObject.GetComponent<Animator>().SetBool("idleBoss", true);
                break;

            case npcType.minion:
                yield return new WaitForSeconds(Random.Range(0.1f, 1f));
                gameObject.GetComponent<Animator>().SetBool("idle", true);
                break;

            case npcType.enemy:
                gameObject.GetComponent<Animator>().SetBool("idle", true);
                break;

            case npcType.player:
                gameObject.GetComponent<Animator>().SetBool("idle", true);
                break;
        }
    }
}
