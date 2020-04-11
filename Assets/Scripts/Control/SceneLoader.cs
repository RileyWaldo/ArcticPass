using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ArcticPass.Control
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] Image screenOverlay = null;
        [SerializeField] float transitionTime = 2f;

        int transitionIndex = 0;
        float transitionAlpha = 0f;

        private void Awake()
        {
            SingletonPattern();
        }

        private void Start()
        {
            screenOverlay.gameObject.SetActive(false);
        }

        IEnumerator TransitionScreen()
        {
            screenOverlay.gameObject.SetActive(true);
            Color color = new Color(1, 1, 1, 0);
            while(transitionAlpha < 1f)
            {
                transitionAlpha += Time.deltaTime / transitionTime;
                if (transitionAlpha > 1f)
                    transitionAlpha = 1f;
                color.a = transitionAlpha;
                screenOverlay.color = color;
                yield return null;
            }

            yield return SceneManager.LoadSceneAsync(transitionIndex);

            while(transitionAlpha > 0f)
            {
                transitionAlpha -= Time.deltaTime / transitionTime;
                if (transitionAlpha < 0f)
                    transitionAlpha = 0f;
                color.a = transitionAlpha;
                screenOverlay.color = color;
                yield return null;
            }
            screenOverlay.gameObject.SetActive(false);
        }

        //UI Buttons

        public void ButtonPlay()
        {
            if(GameManager.Get().GetGameState() == GameState.MainMenu)
            {
                GameManager.Get().SetGameState(GameState.Pass);
                transitionIndex = 1;
                StartCoroutine(TransitionScreen());
            }
        }

        //singleton pattern

        private void SingletonPattern()
        {
            if (FindObjectsOfType<SceneLoader>().Length > 1)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}
