using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    /*
    private static DebugController debugInstance; //used to delete if one already exists

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    */
    private void Start()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("h") && Input.GetKeyDown("k"))
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(!child.gameObject.activeSelf);
            }
        }
    }
    public void GoToLevel(int levelID)
    {
        switch (levelID)
        {
            case 0:
                FindObjectOfType<LevelLoader>().LoadLevel("MainMenu");
                break;
            case 1:
                FindObjectOfType<LevelLoader>().LoadLevel("Sandbox");
                break;
            case 2:
                FindObjectOfType<LevelLoader>().LoadLevel("Level1");
                break;
        }
    }
}
