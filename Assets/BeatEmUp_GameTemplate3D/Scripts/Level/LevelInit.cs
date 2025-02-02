using UnityEngine;

public class LevelInit : MonoBehaviour 
{
    [Space(5)]
    [Header("Settings")]
    public string LevelMusic = "Music";
    public string showMenuAtStart = "";
    public bool playMusic = true;
    public bool createInputManager;
    public bool createUI;
    public bool createAudioPlayer;
    public bool createGameCamera;
    private GameObject audioplayer;
    private GameSettings settings;

    void Awake() 
    {
        // Load settings
        settings = Resources.Load("GameSettings", typeof(GameSettings)) as GameSettings;

        if (settings != null) 
        {
            Time.timeScale = settings.timeScale;
        }

        // Force FPS lock at 60
        QualitySettings.vSyncCount = 0; // Disable VSync to manually set FPS
        Application.targetFrameRate = 60; // Force FPS to 60

        Debug.Log($"Target Frame Rate: {Application.targetFrameRate}"); // Debug log to check FPS

        // Create Audio Player
        if (!GameObject.FindObjectOfType<BeatEmUpTemplate.AudioPlayer>() && createAudioPlayer) 
        {
            audioplayer = GameObject.Instantiate(Resources.Load("AudioPlayer"), Vector3.zero, Quaternion.identity) as GameObject;
        }

        // Create InputManager
        if (!GameObject.FindObjectOfType<InputManager>() && createInputManager) 
        {
            GameObject.Instantiate(Resources.Load("InputManager"), Vector3.zero, Quaternion.identity);
        }

        // Create UI
        if (!GameObject.FindObjectOfType<UIManager>() && createUI) 
        {
            GameObject.Instantiate(Resources.Load("UI"), Vector3.zero, Quaternion.identity);
        }

        // Create Game Camera
        if (!GameObject.FindObjectOfType<CameraFollow>() && createGameCamera) 
        {
            GameObject.Instantiate(Resources.Load("GameCamera"), Vector3.zero, Quaternion.identity);
        }

        // Start music
        if (playMusic && createAudioPlayer) 
        {
            Invoke("PlayMusic", 1f);
        }

        // Open a menu at level start
        if (!string.IsNullOrEmpty(showMenuAtStart)) 
        {
            ShowMenuAtStart();
        }
    }

    void PlayMusic() 
    {
        if (audioplayer != null) 
        {
            audioplayer.GetComponent<BeatEmUpTemplate.AudioPlayer>().playMusic(LevelMusic);
        }
    }

    void ShowMenuAtStart() 
    {
        GameObject.FindObjectOfType<UIManager>().ShowMenu(showMenuAtStart);
    }
}
