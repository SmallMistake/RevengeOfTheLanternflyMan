using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    public GameObject canvas;
    public void Activate()
    {
        canvas.SetActive(true);
    }
}
