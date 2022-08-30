using System.Collections.Generic;
using Entities;
using Interactables;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Global
{
    public static class GlobalData
    {
        public static List<Player> Players = new List<Player>();
        public static List<GameObject> PlayerObjects = new List<GameObject>();
        public static List<GameObject> PlayerPrefabs;

        private static int WaveNum = 1;

        public static bool IsWaveActive = false;

        public static PlayerInputManager mgr;
        
        /// <summary>
        /// Whether the game has begun or not
        /// </summary>
        private static bool IsGameActive = false;

        public static PauseMenu pauseMenu;

        public static InputActionAsset InputActionAsset;

        private static int EnemiesKilled = 0;

        /// <summary>
        /// Instantiate all values to their starting values
        /// </summary>
        public static void RestartGame()
        {
            Players = new List<Player>();
            PlayerObjects = new List<GameObject>();
            WaveNum = 1;
            IsWaveActive = false;
            IsGameActive = false;
            mgr.EnableJoining();
            GlobalShop.Reset();
            EnemySpawnerInteractable.ResetNumRemEnemies();
            UpdateCurrentPlayerPrefab(0);
            UpdateInputActions();
            EnemiesKilled = 0;
        }
        
        /// <summary>
        /// Determines if the player exists in this game or not
        /// </summary>
        /// <returns>True if there is a player existing in the game, false otherwise</returns>
        public static bool DoesPlayerExist()
        {
            if (Players == null)
            {
                return false;
            }
            if (PlayerObjects == null)
            {
                return false;
            }
        
            //Still here, player exists
            return true;
        }

        /// <summary>
        /// Increment the wave number. If a value is provided as a parameter then it will increase by that amount, otherwise it will increase by 1
        /// </summary>
        /// <param name="val">Amount to increase by. Defaults to 1 if a value is not provided</param>
        public static void IncrementWaveNum(int val = 1)
        {
            WaveNum += val;
        }

        /// <summary>
        /// Gives the current wave number, an int
        /// </summary>
        /// <returns>Current wave number</returns>
        public static int GetWaveNum()
        {
            return WaveNum;
        }

        /// <summary>
        /// Begin the game
        /// </summary>
        public static void StartGame()
        {
            IsGameActive = true;
            //Don't let anyone else join now that we've begun
            mgr.DisableJoining();
            //Reset all player animators
            foreach (var PlayerObject in PlayerObjects)
            {
                var anim = PlayerObject.GetComponent<Animator>();
                anim.Rebind();
                anim.Update(0f);
            }
        }

        /// <summary>
        /// Has the first wave of the game begun yet
        /// </summary>
        /// <returns>true if the first wave has begun, false otherwise</returns>
        public static bool IsGameStarted()
        {
            return IsGameActive;
        }

        public static void UpdateCurrentPlayerPrefab(int numPlayers)
        {
            mgr.playerPrefab = PlayerPrefabs[numPlayers];
        }

        /// <summary>
        /// Save reference of the pause menu
        /// </summary>
        public static void SavePauseMenu(PauseMenu menu)
        {
            pauseMenu = menu;
        }

        private static void UpdateInputActions()
        {
            foreach (var playerPrefab in PlayerPrefabs)
            {
                playerPrefab.GetComponent<PlayerInput>().actions = InputActionAsset;
            }
        }

        public static void IncrementEnemiesKilled()
        {
            EnemiesKilled++;
        }

        public static int GetEnemiesKilled()
        {
            return EnemiesKilled;
        }
    }
}
