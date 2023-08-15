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
		public int MaxStars;
		public bool[] CollectedStars;
	}

	[System.Serializable]
	/// <summary>
	/// A serializable entity used to store progress : a list of scenes with their internal status (see above), how many lives are left, and how much we can have
	/// </summary>
	public class GameProgress
	{
		public string StoredCharacterName;
		public int InitialMaximumLives = 0;
		public int InitialCurrentLives = 0;
		public int MaximumLives = 0;
		public int CurrentLives = 0;
		public GameScene[] Scenes;
		public string[] Collectibles;
	}

	/// <summary>
	/// The DeadlineProgressManager class acts as an example of how you can implement progress management in your game.
	/// There's no general class for that in the engine, for the simple reason that no two games will want to save the exact same things.
	/// But this should show you how it's done, and you can then copy and paste that into your own class (or extend this one, whatever you prefer).
	/// </summary>
	public class ProgressManager : MMSingleton<ProgressManager>, MMEventListener<TopDownEngineEvent>, MMEventListener<TopDownEngineStarEvent>
	{
		public int currentSaveFile = 1;
		public int InitialMaximumLives { get; set; }
		public int InitialCurrentLives { get; set; }

		/// the list of scenes that we'll want to consider for our game
		[Tooltip("the list of scenes that we'll want to consider for our game")]
		public GameScene[] Scenes;

		[MMInspectorButton("CreateSaveGame")]
		/// A test button to test creating the save file
		public bool CreateSaveGameBtn;

        [MMInspectorButton("LoadSaveGame")]
        /// A test button to test creating the save file
        public bool LoadSaveGameBtn;

        /// the current amount of collected stars
        public int CurrentStars { get; protected set; }

		public UnityEvent onSaveStarted;
		public UnityEvent onSaveFinished;
		
		public List<string> FoundCollectibles { get; protected set; }

		protected const string _saveFolderName = "ProgressData";

		public string getCurrentSaveFolderName()
		{
			return "Player" + currentSaveFile + _saveFolderName;

        }
		protected const string _saveFileName = "Progress.data";

		/// <summary>
		/// On awake, we load our progress and initialize our stars counter
		/// </summary>
		protected override void Awake()
		{
			base.Awake ();
			LoadSavedProgress ();
			InitializeStars ();
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
		/// Goes through all the scenes in our progress list, and updates the collected stars counter
		/// </summary>
		protected virtual void InitializeStars()
		{
			foreach (GameScene scene in Scenes)
			{
				if (scene.SceneName == SceneManager.GetActiveScene().name)
				{
					int stars = 0;
					foreach (bool star in scene.CollectedStars)
					{
						if (star) { stars++; }
					}
					CurrentStars = stars;
				}
			}
		}

		/// <summary>
		/// Saves the progress to a file
		/// </summary>
		protected virtual void SaveProgress()
		{
			onSaveStarted?.Invoke();
            GameProgress progress = new GameProgress ();
			progress.StoredCharacterName = "characterName"; // GameManager.Instance.StoredCharacter.name;
			progress.MaximumLives = GameManager.Instance.MaximumLives;
			progress.CurrentLives = GameManager.Instance.CurrentLives;
			progress.InitialMaximumLives = InitialMaximumLives;
			progress.InitialCurrentLives = InitialCurrentLives;
			progress.Scenes = Scenes;
			if (FoundCollectibles != null)
			{
				progress.Collectibles = FoundCollectibles.ToArray();	
			}

			MMSaveLoadManager.Save(progress, _saveFileName, getCurrentSaveFolderName());
            onSaveFinished?.Invoke();
        }

		/// <summary>
		/// A test method to create a test save file at any time from the inspector
		/// </summary>
		protected virtual void CreateSaveGame()
		{
			SaveProgress();
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
        protected virtual void LoadSavedProgress()
		{
			GameProgress progress = (GameProgress)MMSaveLoadManager.Load(typeof(GameProgress), _saveFileName, getCurrentSaveFolderName());
			if (progress != null)
			{
				GameManager.Instance.MaximumLives = progress.MaximumLives;
				GameManager.Instance.CurrentLives = progress.CurrentLives;
				InitialMaximumLives = progress.InitialMaximumLives;
				InitialCurrentLives = progress.InitialCurrentLives;
				Scenes = progress.Scenes;
				if (progress.Collectibles != null)
				{
					FoundCollectibles = new List<string>(progress.Collectibles);	
				}
			}
			else
			{
				InitialMaximumLives = GameManager.Instance.MaximumLives;
				InitialCurrentLives = GameManager.Instance.CurrentLives;
			}
		}

		public virtual void FindCollectible(string collectibleName)
		{
			FoundCollectibles.Add(collectibleName);
		}

		/// <summary>
		/// When we grab a star event, we update our scene status accordingly
		/// </summary>
		/// <param name="deadlineStarEvent">Deadline star event.</param>
		public virtual void OnMMEvent(TopDownEngineStarEvent deadlineStarEvent)
		{
			foreach (GameScene scene in Scenes)
			{
				if (scene.SceneName == deadlineStarEvent.SceneName)
				{
					scene.CollectedStars [deadlineStarEvent.StarID] = true;
					CurrentStars++;
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
					SaveProgress ();
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
			ResetLives ();
		}

		/// <summary>
		/// Resets the number of lives to its initial values
		/// </summary>
		protected virtual void ResetLives()
		{
			GameManager.Instance.MaximumLives = InitialMaximumLives;
			GameManager.Instance.CurrentLives = InitialCurrentLives;
		}

		/// <summary>
		/// A method used to remove all save files associated to progress
		/// </summary>
		public virtual void ResetProgress()
		{
			MMSaveLoadManager.DeleteSaveFolder (getCurrentSaveFolderName());			
		}

		/// <summary>
		/// OnEnable, we start listening to events.
		/// </summary>
		protected virtual void OnEnable()
		{
			this.MMEventStartListening<TopDownEngineStarEvent> ();
			this.MMEventStartListening<TopDownEngineEvent>();
		}

		/// <summary>
		/// OnDisable, we stop listening to events.
		/// </summary>
		protected virtual void OnDisable()
		{
			this.MMEventStopListening<TopDownEngineStarEvent> ();
			this.MMEventStopListening<TopDownEngineEvent>();
		}		
	}
}