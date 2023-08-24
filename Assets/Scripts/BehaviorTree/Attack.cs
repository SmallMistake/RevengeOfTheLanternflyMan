using BehaviorDesigner.Runtime.Tasks;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IntronDigital.BehaviorTree {

    [TaskIcon("Assets/Art/BehaviorTree/attack-icon.png")]
    [TaskName("Attack")]
    [TaskCategory("Attack")]
    [TaskDescription("Call all weapons linked and trigger them")]
    public class Attack : Action
    {
        public List<WeaponHandler> weapons;
        public float waitTime;

        private bool finished;

        public override void OnStart()
        {
            finished = false;
            StartCoroutine(HandleAttacks());
            System.Console.WriteLine("Attack");
        }

        IEnumerator HandleAttacks()
        {
            foreach(WeaponHandler weapon in weapons)
            {
                weapon.StartShooting();
                //weapon.WeaponInputStart();
                //weapon.WeaponUse();
            }
            yield return new WaitForSeconds(waitTime);
            finished = true;
        }

        public override TaskStatus OnUpdate()
        {
            return finished ? TaskStatus.Success : TaskStatus.Inactive;
        }
    }
}