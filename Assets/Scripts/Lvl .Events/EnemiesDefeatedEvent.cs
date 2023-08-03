using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemiesDefeatedEvent : MonoBehaviour
{
    public List<Health> enemiesToListenFor = new List<Health>();

    public UnityEvent onEnemiesDefeated;

    private void OnEnable()
    {
        foreach(Health enemy in enemiesToListenFor)
        {
            enemy.OnDeath += () =>
            {
                HandleEnemyDeath(enemy);
            };
        }
    }

    private void HandleEnemyDeath(Health enemy)
    {
        enemiesToListenFor.Remove(enemy);
        if(enemiesToListenFor.Count == 0)
        {
            onEnemiesDefeated.Invoke();
        } 
    }
}
