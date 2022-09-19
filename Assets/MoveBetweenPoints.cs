using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBetweenPoints : MonoBehaviour
{
    public Transform platform;
    public Transform destinationHolder;
    public float speed;

    public bool loopBackwards;
    private bool headingForwards;

    private List<Transform> destinations = new List<Transform>();
    private int currentDestination;


    private void Start()
    {
        foreach (Transform destination in destinationHolder)
        {
            destinations.Add(destination);
        }

        currentDestination = 0;
        platform.transform.position = destinations[currentDestination].position;
        currentDestination++;
        headingForwards = true;
    }

    private void Update()
    {
        if(platform.transform.position == destinations[currentDestination].position)
        {
            if(headingForwards == true) {  //Heading forwards
                currentDestination++;
                if(currentDestination >= destinations.Count)
                {
                    if (loopBackwards) //Loop backward;
                    {
                        headingForwards = false;
                        currentDestination -= 2;
                    }
                    else //Restart at begining
                    {
                        currentDestination = 0;
                        platform.transform.position = destinations[currentDestination].position;
                        currentDestination = 1;
                    }
                }
            }
            else  //Heading backwards
            {
                currentDestination--;
                if (currentDestination < 0)
                {
                    if (loopBackwards) //Loop backward;
                    {
                        headingForwards = true;
                        currentDestination += 2;
                    }
                    else //Restart at begining
                    {
                        currentDestination = destinations.Count;
                        platform.transform.position = destinations[currentDestination].position;
                        currentDestination = destinations.Count - 1;
                    }
                }
            }
        }
        float step = speed * Time.deltaTime;
        platform.transform.position = Vector3.MoveTowards(platform.transform.position, destinations[currentDestination].position, step);
    }
}
