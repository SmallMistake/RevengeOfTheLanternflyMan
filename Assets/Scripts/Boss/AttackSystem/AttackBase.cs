using UnityEngine;

public abstract class AttackBase : MonoBehaviour
{
    //This is the base attack other attacks should extend.
    public float attackLength;
    public abstract void Attack();
}
