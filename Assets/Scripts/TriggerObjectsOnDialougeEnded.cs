using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObjectsOnDialougeEnded : MonoBehaviour
{
    //This is bad, it sends a trigger on the second one as a cheat but it should not do this. Rework Event to send back which DialougeState just finished
    int tempBandaidForHiddenAttacker = 0;
    public List<GameObject> objectsToTrigger;
    public string dialougeToListenTo;
    private List<Triggerable> triggers = new List<Triggerable>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject triggerObject in objectsToTrigger)
        {
            triggers.Add(triggerObject.GetComponent<Triggerable>());
        }
        DialogueTrigger.DialougeEnded += TriggerObjects;
    }

    private void OnDestroy()
    {
        DialogueTrigger.DialougeEnded -= TriggerObjects;
    }

    private void TriggerObjects(string dialougeName)
    {
        if (dialougeName.Equals(dialougeToListenTo))
        {
            tempBandaidForHiddenAttacker += 1;
            if(tempBandaidForHiddenAttacker >= 2)
            {
                foreach (Triggerable triggerable in triggers)
                {
                    triggerable.trigger();
                }
            }
        }
    }
}
