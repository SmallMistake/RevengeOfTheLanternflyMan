using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;
using System.Linq;
using System;

namespace MoreMountains.TopDownEngine
{
	/// <summary>
	/// A class meant to be overridden that handles a character's ability. 
	/// </summary>
	//[RequireComponent(typeof(Character))]
	[Serializable]
	public class CallableCharacterAbility : CharacterAbility
	{
        //Used by the UI
        public string friendlyName;

        #region Special Cases for Secondary Input
        /// <summary>
        /// Called by classes like the CharacterSpecialButtonHandler To Simulate An Input return false if the input is not accepted.
        /// </summary>
        public virtual bool IsAbilityActivatable()
        {
            return false;
        }

        public virtual void Activate()
        {

        }

        //This is called to check if the current ability is still active and holding the next special button press.
        //Extend this in special cases like holding to make it so that it eats two inputs.
        public virtual bool IsStillActive()
        {
            return false;
        }

        #endregion
    }
}