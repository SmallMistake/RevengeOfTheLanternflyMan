using System.Collections;
using UnityEngine;

public class BurnUpTimerController : MonoBehaviour
{
    public float timeTillBurnUp;
    private Coroutine timer;

    public GameObject objectToDestroyOnBurnUp;

    public void StartTimer()
    {
        timer = StartCoroutine(HandleTimer());
    }

    public void StopTimer()
    {
        if(timer != null)
        {
            StopCoroutine(timer);
            timer = null;
        }
    }

    IEnumerator HandleTimer()
    {
        yield return new WaitForSeconds(timeTillBurnUp);
        BurnUp();
    }

    private void BurnUp()
    {
        if(objectToDestroyOnBurnUp != null){
            Destroy(objectToDestroyOnBurnUp);
        }
        else
        {
            Destroy(gameObject);
        }
    }


}
