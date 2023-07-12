using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<Animator>().SetFloat("CycleOffset", Random.Range(0,10));
    }

}
