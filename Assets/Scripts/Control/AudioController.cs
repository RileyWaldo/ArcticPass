using UnityEngine;
using UnityEngine.SceneManagement;
using ArcticPass.Control;

namespace ArcticPass.Audio
{
    public class AudioController : MonoBehaviour
    {
        [Header("Volume:")]
        [Range(0f, 1f)] [SerializeField] float masterVolume = 1f;
        [Range(0f, 1f)] [SerializeField] float musicVolume = 1f;
        [Range(0f, 1f)] [SerializeField] float sfxVolume = 1f;

        [Header("Tunables:")]
        [SerializeField] float waitBetweenSong = 2f;

        [Header("Audio Source:")]
        [SerializeField] AudioClip[] menuMusic;
        [SerializeField] AudioClip[] passMusic;
        [SerializeField] AudioClip[] caveMusic;

        AudioSource backgroundSource;
        AudioSource sfxSource;

        float songWaitTime = 0f;

        static AudioController id;

        public static AudioController Get()
        {
            return id;
        }

        private void Awake()
        {
            SingletonPattern();
        }

        private void Start()
        {
            AudioSource[] audioSources = GetComponents<AudioSource>();
            backgroundSource = audioSources[0];
            sfxSource = audioSources[1];
        }

        private void Update()
        {
            BackgroundMusic();
        }

        private void BackgroundMusic()
        {
            if(!backgroundSource.isPlaying)
            {
                songWaitTime += Time.deltaTime;
                if(songWaitTime >= waitBetweenSong)
                {
                    songWaitTime = 0f;
                    ShuffleMusic();
                }
            }
        }

        private void ShuffleMusic()
        {
            AudioClip clip = null;
            switch (GameManager.Get().GetGameState())
            {
                case GameState.MainMenu:
                    clip = menuMusic[Random.Range(0, menuMusic.Length - 1)];
                    break;

                case GameState.Pass:
                    clip = passMusic[Random.Range(0, passMusic.Length - 1)];
                    break;
            }
            if (clip)
            {
                PlayBackground(clip);
            }
        }

        public void PlayBackground(AudioClip clip)
        {
            backgroundSource.clip = clip;
            backgroundSource.Play();
        }

        public void PlaySound(AudioClip clip)
        {
            backgroundSource.clip = clip;
            backgroundSource.PlayOneShot(clip);
        }

        public void PlaySoundPos(AudioClip clip, Vector3 pos)
        {
            Vector3 playFrom = pos - PlayerController.GetPlayer().transform.position;
            AudioSource.PlayClipAtPoint(clip, playFrom);
        }

        public void SetVolumeAll(float master, float music, float sfx)
        {
            masterVolume = master;
            musicVolume = music;
            sfxVolume = sfx;
        }

        public void SetVolumeMaster(float master)
        {
            masterVolume = master;
        }

        public void SetVolumeMusic(float music)
        {
            musicVolume = music;
        }

        public void SetVolumeSFX(float sfx)
        {
            sfxVolume = sfx;
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            ShuffleMusic();
        }

        private void SingletonPattern()
        {
            if (FindObjectsOfType<AudioController>().Length > 1)
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
