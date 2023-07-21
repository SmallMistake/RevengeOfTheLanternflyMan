using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForSecondsThenAttack : AttackBase
{
    public List<AttackBase> attacks;
    public float timeBetweenAttacks;

    public override void Attack()
    {
        StartCoroutine(HandleSpawns());
    }

    IEnumerator HandleSpawns()
    {
        while (enabled)
        {
            CallAttacks();
            yield return new WaitForSeconds(timeBetweenAttacks);
        }
    }

    private void CallAttacks()
    {
        foreach (AttackBase attack in attacks)
        {
            attack.Attack();
        }
    }


}
