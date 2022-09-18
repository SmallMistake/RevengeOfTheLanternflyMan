using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemUserController : MonoBehaviour
{
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Primary")){
            print("TODO Attack");
            animator.SetTrigger("Primary");
        }
    }
}
