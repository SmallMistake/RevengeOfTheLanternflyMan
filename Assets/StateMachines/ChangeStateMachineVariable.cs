using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

//Used to allow access to visual scripting machines variables. Expand to include more types as needed.
public class ChangeStateMachineVariable : MonoBehaviour
{
    public string variableNameToTarget;
    public StateMachine machineToTarget;
    VariableDeclarations variableDeclarations;

    // Start is called before the first frame update
    void Start()
    {
        variableDeclarations = Variables.Object(machineToTarget.gameObject);
    }

    public void updateBoolean(bool newValue)
    {
        variableDeclarations.Set(variableNameToTarget, newValue);
    }
}
