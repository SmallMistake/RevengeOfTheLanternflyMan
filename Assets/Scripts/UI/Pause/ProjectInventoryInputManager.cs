using UnityEngine;
using MoreMountains.Tools;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
	using UnityEngine.InputSystem;
#endif

namespace MoreMountains.InventoryEngine
{
    // Based on InventoryInputManager
    /// <summary>
    /// Example of how you can call an inventory from your game. 
    /// I suggest having your Input and GUI manager classes handle that though.
    /// </summary>
    public class ProjectInventoryInputManager : MonoBehaviour, MMEventListener<MMInventoryEvent>
	{
        [Tooltip("The main inventory display")]
        public InventoryDisplay TargetInventoryDisplay;
        [Tooltip("The toggle inventory button")]
		private bool InventoryIsOpen = false;

        /// <summary>
        /// On start, we grab references and prepare our hotbar list
        /// </summary>
        protected virtual void Start()
		{
			/*
			foreach (InventoryHotbar go in FindObjectsOfType(typeof(InventoryHotbar)) as InventoryHotbar[])
			{
				_targetInventoryHotbars.Add(go);
			}
			if (HideContainerOnStart)
			{
				if (TargetInventoryContainer != null) { TargetInventoryContainer.alpha = 0; }
				if (Overlay != null) { Overlay.alpha = OverlayInactiveOpacity; }
				EventSystem.current.sendNavigationEvents = false;
				if (_canvasGroup != null)
				{
					_canvasGroup.blocksRaycasts = false;
				}
			}
			*/
		}

		/// <summary>
		/// Every frame, we check for input for the inventory, the hotbars and we check the current selection
		/// </summary>
		protected virtual void Update()
		{
			//HandleInventoryInput();
		}
		/// <summary>
		/// Opens or closes the inventory panel based on its current status
		/// </summary>
		public virtual void ToggleInventory()
		{
			if (InventoryIsOpen)
			{
				CloseInventory();
			}
			else
			{
				OpenInventory();
			}
		}

		/// <summary>
		/// Opens the inventory panel
		/// </summary>
		public virtual void OpenInventory()
		{
            print("TODO Open Inventory");
			/*
            InventoryIsOpen = true;
            if (CloseList.Count > 0)
			{
				foreach (string playerID in CloseList)
				{
					MMInventoryEvent.Trigger(MMInventoryEventType.InventoryCloseRequest, null, "", null, 0, 0, playerID);
				}
			}
            
			if (_canvasGroup != null)
			{
				_canvasGroup.blocksRaycasts = true;
			}

			// we open our inventory
			MMInventoryEvent.Trigger(MMInventoryEventType.InventoryOpens, null, TargetInventoryDisplay.TargetInventoryName, TargetInventoryDisplay.TargetInventory.Content[0], 0, 0, TargetInventoryDisplay.PlayerID);
			MMGameEvent.Trigger("inventoryOpens");
			InventoryIsOpen = true;

			StartCoroutine(MMFade.FadeCanvasGroup(TargetInventoryContainer, 0.2f, 1f));
			StartCoroutine(MMFade.FadeCanvasGroup(Overlay, 0.2f, OverlayActiveOpacity));
			*/
		}

		/// <summary>
		/// Closes the inventory panel
		/// </summary>
		public virtual void CloseInventory()
		{
			print("TODO Close Inventory");
            InventoryIsOpen = false;
            /*
			if (_canvasGroup != null)
			{
				_canvasGroup.blocksRaycasts = false;
			}
			// we close our inventory
			MMInventoryEvent.Trigger(MMInventoryEventType.InventoryCloses, null, TargetInventoryDisplay.TargetInventoryName, null, 0, 0, TargetInventoryDisplay.PlayerID);
			MMGameEvent.Trigger("inventoryCloses");

			StartCoroutine(MMFade.FadeCanvasGroup(TargetInventoryContainer, 0.2f, 0f));
			StartCoroutine(MMFade.FadeCanvasGroup(Overlay, 0.2f, OverlayInactiveOpacity));
			*/
        }

		/// <summary>
		/// Handles the inventory related inputs and acts on them.
		/// </summary>
		protected virtual void HandleInventoryInput()
		{

		}



		/// <summary>
		/// Catches MMInventoryEvents and acts on them
		/// </summary>
		/// <param name="inventoryEvent">Inventory event.</param>
		public virtual void OnMMEvent(MMInventoryEvent inventoryEvent)
		{
			if (inventoryEvent.PlayerID != TargetInventoryDisplay.PlayerID)
			{
				return;
			}
            
			if (inventoryEvent.InventoryEventType == MMInventoryEventType.InventoryCloseRequest)
			{
				CloseInventory();
			}
		}

		/// <summary>
		/// On Enable, we start listening for MMInventoryEvents
		/// </summary>
		protected virtual void OnEnable()
		{
			this.MMEventStartListening<MMInventoryEvent>();
			
		}

		/// <summary>
		/// On Disable, we stop listening for MMInventoryEvents
		/// </summary>
		protected virtual void OnDisable()
		{
			this.MMEventStopListening<MMInventoryEvent>();
		}
	}
}