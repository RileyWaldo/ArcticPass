using UnityEngine;
using UnityEngine.SceneManagement;

namespace ArcticPass.Control
{
    public class GameManager : MonoBehaviour
    {
        static GameManager id;

        GameState gameState = GameState.MainMenu;

        private void Awake()
        {
            SingletonPattern();
        }

        public void SetGameState(GameState state)
        {
            gameState = state;
        }

        public GameState GetGameState()
        {
            return gameState;
        }

        public static GameManager Get()
        {
            return id;
        }

        //execute appropriate actions depending on scene

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnLevelFinishedLoading;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        }

        private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
        {
            switch(gameState)
            {
                case GameState.MainMenu:
                    print("You are in the main menu!");
                    break;

                case GameState.Pass:
                    print("You are in the Arctic Pass!");
                    break;
            }
        }

        //singleton
        private void SingletonPattern()
        {
            if(FindObjectsOfType<GameManager>().Length > 1)
            {
                Destroy(gameObject);
            }
            else
            {
                id = this;
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}
