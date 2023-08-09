using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectSystemController : MonoBehaviour
{
    public new ParticleSystem particleSystem;

    private void Start()
    {
        if (particleSystem == null)
        {
            particleSystem = GetComponent<ParticleSystem>();
        }
    }

    public void StartSystem()
    {
        particleSystem.Play();
    }
    public void StopSystem()
    {
        particleSystem.Stop();
    }
}
