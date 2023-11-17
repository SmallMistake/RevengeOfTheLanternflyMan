using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using UnityEngine.SceneManagement;
using MoreMountains.TopDownEngine;
using UnityEngine.Events;

namespace IntronDigital
{	
	[System.Serializable]
	/// <summary>
	/// A serializable entity to store deadline demo scenes, whether they've been completed, unlocked, how many stars can and have been collected
	/// </summary>
	public class GameScene
	{
		public string SceneName;
		public bool LevelComplete = false;
		public bool LevelUnlocked = false;
		public bool[] CollectedCurrency;

		//Calculate Number of Coins Collected
		public int CollectedCurrencyCount { 
			get {
				int i = 0;
				foreach (bool item in CollectedCurrency)
				{
					if (item)
					{
						i++;
					}
				}
				return i;
			} 
		}
	}

	[System.Serializable]
	/// <summary>
	/// A serializable entity used to store progress : a list of scenes with their internal status (see above), how many lives are left, and how much we can have
	/// </summary>
	public class GameProgress
	{
		public string playerName;
        public int commonCurrency;
        public GameScene[] Scenes;
		public string[] Collectibles;
	}

	/// <summary>
	/// The DeadlineProgressManager class acts as an example of how you can implement progress management in your game.
	/// There's no general class for that in the engine, for the simple reason that no two games will want to save the exact same things.
	/// But this should show you how it's done, and you can then copy and paste that into your own class (or extend this one, whatever you prefer).
	/// </summary>
	public class ProgressManager : MMSingleton<ProgressManager>, MMEventListener<TopDownEngineEvent>, MMEventListener<AreaCurrencyEvent>
	{
		public int currentSaveFile = 1;
		public string playerName;

        [Tooltip("the common currency of the game")]
        public int commonCurrency;

		/// the list of scenes that we'll want to consider for our game
		[Tooltip("the list of scenes that we'll want to consider for our game")]
		public GameScene[] Scenes;

		[MMInspectorButton("CreateSaveGame")]
		/// A test button to test creating the save file
		public bool CreateSaveGameBtn;

        [MMInspectorButton("LoadSaveGame")]
        /// A test button to test creating the save file
        public bool LoadSaveGameBtn;

		public UnityEvent onSaveStarted;
		public UnityEvent onSaveFinished;

		private int initAreaCurrencyAmount = 100;
		
		public List<string> FoundCollectibles { get; protected set; }

		protected const string _saveFolderName = "ProgressData";

		public string getCurrentSaveFolderName(int? saveFileIndex = null)
		{
			if(saveFileIndex == null)
			{
                return "Player" + currentSaveFile + _saveFolderName;
            }
			else {
                return "Player" + saveFileIndex + _saveFolderName;
            }

        }
		protected const string _saveFileName = "Progress.data";

		/// <summary>
		/// On awake, we load our progress and initialize our stars counter
		/// </summary>
		protected override void Awake()
		{
			base.Awake ();
			LoadSavedProgress ();
			//InitializeStars (); Replace this with Init Level Specific Currency
			if (FoundCollectibles == null)
			{
				FoundCollectibles = new List<string> ();
			}
		}

		/// <summary>
		/// When a level is completed, we update our progress
		/// </summary>
		protected virtual void LevelComplete()
		{
			for (int i = 0; i < Scenes.Length; i++)
			{
				if (Scenes[i].SceneName == SceneManager.GetActiveScene().name)
				{
					Scenes[i].LevelComplete = true;
					Scenes[i].LevelUnlocked = true;
					if (i < Scenes.Length - 1)
					{
						Scenes [i + 1].LevelUnlocked = true;
					}
				}
			}
		}

		/// <summary>
		/// Saves the progress to a file
		/// </summary>
		protected virtual void SaveProgress(int saveFileIndex)
		{
			onSaveStarted?.Invoke();
            GameProgress progress = new GameProgress ();
			progress.playerName = GameManager.Instance.playerName; // GameManager.Instance.StoredCharacter.name;
			progress.Scenes = Scenes;
			if (FoundCollectibles != null)
			{
				progress.Collectibles = FoundCollectibles.ToArray();	
			}

			MMSaveLoadManager.Save(progress, _saveFileName, getCurrentSaveFolderName(saveFileIndex));
            onSaveFinished?.Invoke();
            TopDownEngineSaveFilesChangedEvent.Trigger();
        }

		/// <summary>
		/// A test method to create a test save file at any time from the inspector
		/// </summary>
		public virtual void CreateSaveGame(int saveFileIndex, string playerName = "Test")
		{
			this.playerName = playerName;
			GameManager.Instance.playerName = playerName;
			SaveProgress(saveFileIndex);
		}

        /// <summary>
        /// A test method to create a test save file at any time from the inspector
        /// </summary>
        protected virtual void LoadSaveGame()
        {
			print("TODO Load Test Button");
            //SaveProgress();
        }

        /// <summary>
        /// Loads the saved progress into memory
        /// </summary>
        public virtual void LoadSavedProgress(int playerIndex = 1)
		{
			currentSaveFile = playerIndex;
			GameProgress progress = (GameProgress)MMSaveLoadManager.Load(typeof(GameProgress), _saveFileName, getCurrentSaveFolderName());
			if (progress != null)
			{
				GameManager.Instance.playerName = progress.playerName;
				/*
				GameManager.Instance.CurrentLives = progress.CurrentLives;
				InitialMaximumLives = progress.InitialMaximumLives;
				InitialCurrentLives = progress.InitialCurrentLives;
				*/
				Scenes = progress.Scenes;
				if (progress.Collectibles != null)
				{
					FoundCollectibles = new List<string>(progress.Collectibles);	
				}
			}
			else
			{
				playerName = GameManager.Instance.playerName;
				//InitialCurrentLives = GameManager.Instance.CurrentLives;
			}
            TopDownEngineSaveFilesChangedEvent.Trigger();

        }

		public GameProgress GetSaveFile(int saveFileIndex)
		{
            return (GameProgress)MMSaveLoadManager.Load(typeof(GameProgress), _saveFileName, getCurrentSaveFolderName(saveFileIndex));
        }

        public virtual void FindCollectible(string collectibleName)
		{
			FoundCollectibles.Add(collectibleName);
		}

		/// <summary>
		/// When we grab a currency event, we update our scene status accordingly
		/// </summary>
		/// <param name="deadlineStarEvent">Deadline star event.</param>
		public virtual void OnMMEvent(AreaCurrencyEvent areaCurrencyEvent)
		{
			foreach (GameScene scene in Scenes)
			{
				if (scene.SceneName == LevelManager.Instance.areaName)
				{
					if(scene.CollectedCurrency == null || scene.CollectedCurrency.Length <= areaCurrencyEvent.CurrencyID)
					{
						scene.CollectedCurrency = new bool[initAreaCurrencyAmount]; //Later on change this to be more variable.
					}
                    scene.CollectedCurrency[areaCurrencyEvent.CurrencyID] = true;
				}
			}
		}

		/// <summary>
		/// When we grab a level complete event, we update our status, and save our progress to file
		/// </summary>
		/// <param name="gameEvent">Game event.</param>
		public virtual void OnMMEvent(TopDownEngineEvent gameEvent)
		{
			switch (gameEvent.EventType)
			{
				case TopDownEngineEventTypes.LevelComplete:
					LevelComplete ();
					SaveProgress (currentSaveFile);
					break;
				case TopDownEngineEventTypes.GameOver:
					GameOver ();
					break;
			}
		} 

		/// <summary>
		/// This method describes what happens when the player loses all lives. In this case, we reset its progress and all lives will be reset.
		/// </summary>
		protected virtual void GameOver()
		{
			ResetProgress ();
		}

		/// <summary>
		/// A method used to remove all save files associated to progress
		/// </summary>
		public virtual void ResetProgress(int fileIndex = 1)
		{
			MMSaveLoadManager.DeleteSaveFolder(getCurrentSaveFolderName(fileIndex));
            TopDownEngineSaveFilesChangedEvent.Trigger();
        }

		/// <summary>
		/// OnEnable, we start listening to events.
		/// </summary>
		protected virtual void OnEnable()
		{
			this.MMEventStartListening<AreaCurrencyEvent>();
			this.MMEventStartListening<TopDownEngineEvent>();
		}

		/// <summary>
		/// OnDisable, we stop listening to events.
		/// </summary>
		protected virtual void OnDisable()
		{
			this.MMEventStopListening<AreaCurrencyEvent>();
			this.MMEventStopListening<TopDownEngineEvent>();
		}		
	}

    public struct TopDownEngineSaveFilesChangedEvent
    {

        static TopDownEngineSaveFilesChangedEvent e;
        public static void Trigger()
        {
            MMEventManager.TriggerEvent(e);
        }
    }
}