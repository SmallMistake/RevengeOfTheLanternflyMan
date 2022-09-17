using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GBJam.Player;

public class FallScript : MonoBehaviour
{
    private List<string> groundTags = new List<string>() {"Ground", "SecondFloor", "Stairs"};
    public PlayerStateMachine playerStateMachine;
    public bool active = false;

    public List<GameObject> groundStoodOn = new List<GameObject>();



    private void FixedUpdate()
    {
        if (active)
        {
            StartCoroutine(GetLastPostionAfterXSeconds(0.1f, playerStateMachine.transform.position));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (groundTags.Contains(collision.tag) && !groundStoodOn.Contains(collision.gameObject))
        {
            groundStoodOn.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (groundTags.Contains(collision.tag))
        {
            groundStoodOn.Remove(collision.gameObject);
            if (groundStoodOn.Count <= 0)
            {
                Fall();
            }
        }
    }


    public void Fall()
    {
        if (active  && playerStateMachine.GetState().GetStateName() != "Fall"){
            playerStateMachine.fall();
        }
    }

    IEnumerator GetLastPostionAfterXSeconds(float seconds, Vector3 position)
    {
        yield return new WaitForSeconds(seconds);
        if (active)
        {
            playerStateMachine.lastPositionOnSolidGround = position;
        }
    }

}
