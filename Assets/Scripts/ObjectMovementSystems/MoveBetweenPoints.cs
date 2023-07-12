using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBetweenPoints : MonoBehaviour
{
    public Transform movingObject;
    public Transform destinationHolder;
    public float speed;

    public bool loopBackwards;
    private bool headingForwards;

    protected List<Transform> destinations = new List<Transform>();
    protected int currentDestination;

    private float waitAtEnd = 1f;

    protected bool stopped = false;


    internal void Awake()
    {
        foreach (Transform destination in destinationHolder)
        {
            destinations.Add(destination);
        }
    }


    private void OnEnable()
    {
        stopped = false;
        currentDestination = 0;
        movingObject.transform.position = destinations[currentDestination].position;
        currentDestination++;
        headingForwards = true;
    }


    private void Update()
    {
        if (!stopped)
        {
            if (movingObject.transform.position == destinations[currentDestination].position)
            {
                if (headingForwards)
                { 
                    currentDestination++;
                    if (currentDestination >= destinations.Count)
                    {
                        HandleReachedEnd(headingForwards);
                    }
                }
                else
                {
                    currentDestination--;
                    if (currentDestination < 0)
                    {
                        HandleReachedEnd(headingForwards);
                    }
                }
            }
            HandleMovement();
        }
    }

    internal void HandleReachedEnd(bool headingForwards)
    {
        if (headingForwards)
        {
            if (loopBackwards) //Loop backward;
            {
                StartCoroutine(WaitAtEnd());
                this.headingForwards = false;
                currentDestination -= 2;
            }
            else //Restart at begining
            {
                currentDestination = 0;
                movingObject.transform.position = destinations[currentDestination].position;
                currentDestination = 1;
            }
        }
        else
        {
            if (loopBackwards) //Loop backward;
            {
                StartCoroutine(WaitAtEnd());
                this.headingForwards = true;
                currentDestination += 2;
            }
            else //Restart at begining
            {
                currentDestination = destinations.Count;
                movingObject.transform.position = destinations[currentDestination].position;
                currentDestination = destinations.Count - 1;
            }
        }
    }

    protected void HandleMovement()
    {
        float step = speed * Time.deltaTime;
        movingObject.transform.position = Vector3.MoveTowards(movingObject.transform.position, destinations[currentDestination].position, step);
    }

    IEnumerator WaitAtEnd()
    {
        stopped = true;
        yield return new WaitForSeconds(waitAtEnd);
        stopped = false;
    }
}
