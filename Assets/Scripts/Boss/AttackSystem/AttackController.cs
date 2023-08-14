using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public List<AttackBase> attacks;
    
    public void StartAttacks()
    {
        StartCoroutine(HandleAttacks());
    }

    IEnumerator HandleAttacks()
    {
        while (enabled)
        {
            int chosenAttack = ChooseAttack();
            attacks[chosenAttack].Attack();
            yield return new WaitForSeconds(attacks[chosenAttack].attackLength);
        }
    }

    private int ChooseAttack()
    {
        return 0;
    }
}
