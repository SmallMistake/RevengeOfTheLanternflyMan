using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimationHelper : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip musicToPlay;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("PlayIntro");

    }

    public void StartMusic()
    {
        //Replace with FMOD
        //audioSource.clip = musicToPlay;
        //audioSource.Play();
    }
}
