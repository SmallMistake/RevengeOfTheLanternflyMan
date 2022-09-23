using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelWarp : MonoBehaviour
{
    public string levelNameToGoTo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FindObjectOfType<LevelLoader>().LoadLevel(levelNameToGoTo);
        }
    }
}
