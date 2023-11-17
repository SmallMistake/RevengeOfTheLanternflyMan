using MoreMountains.InventoryEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IntronDigital { 
    [Serializable]
    public class Transition
    {
        public string transitionName;
        public GameObject transitionObject;
    }

    /// <summary>
    /// A list of the possible Transitions
    /// </summary>
    public enum TransitionEventTypes
    {
        ToBlack,
        FinishedToBlack,
        OutOfBlack,
        FinishedOutOfBlack
    }

    /// <summary>
    /// A type of events used to signal the need to play a transition
    /// </summary>
    public struct TransitionEvent
    {
        public TransitionEventTypes EventType;
        public string TransitionName;

        /// <summary>
        /// Initializes a new instance of the TransitionEvent"/> struct.
        /// </summary>
        /// <param name="eventType">Event type.</param>
        public TransitionEvent(TransitionEventTypes eventType, string transitionName)
        {
            EventType = eventType;
            TransitionName = transitionName;
        }

        static TransitionEvent e;
        public static void Trigger(TransitionEventTypes eventType, string transitionName)
        {
            e.EventType = eventType;
            e.TransitionName = transitionName;
            MMEventManager.TriggerEvent(e);
        }
    }


    public class AdvancedTransitionController : MonoBehaviour, MMEventListener<TransitionEvent>
    {
        public GameObject backgroundObject;
        public List<Transition> transitions = new List<Transition>();
        protected void OnEnable()
        {
            this.MMEventStartListening<TransitionEvent>();
        }

        protected void OnDisable()
        {
            this.MMEventStopListening<TransitionEvent>();
        }

        public virtual void OnMMEvent(TransitionEvent transitionEvent)
        {
            switch (transitionEvent.EventType)
            {
                case TransitionEventTypes.ToBlack:
                case TransitionEventTypes.OutOfBlack:
                    SpawnTransitionEffect(transitionEvent);
                    break;
            }
        } 

        private void SpawnTransitionEffect(TransitionEvent transitionEvent)
        {
            GameObject transitionObject = GetTransitionObject(transitionEvent.TransitionName);
            GameObject spawnedObject = Instantiate(transitionObject);
            spawnedObject.transform.SetParent(transform, false);
            spawnedObject.GetComponent<Animator>().SetTrigger(transitionEvent.EventType.ToString());
        }

        //Search defined Transtions and get the object that needs to be spawned 
        private GameObject GetTransitionObject(string transitionName)
        {
            foreach (Transition transition in transitions)
            {
                if(transition.transitionName == transitionName)
                {
                    return transition.transitionObject;
                }
            }
            return transitions[0].transitionObject; //Default Transition in case of error
        }
    }
}