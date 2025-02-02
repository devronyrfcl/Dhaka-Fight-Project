using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScrn : UISceneLoader {

    public Text text;
    public Text subtext;
    public string TextRestart = "Press any key to restart";
    public string TextNextLevel = "Press any key to continue";
    public Gradient ColorTransition;
    public float speed = 3.5f;
    private bool restartInProgress = false;
    public string mainMenuSceneName = "MainMenu";  // Name of the main menu scene

    private void OnEnable() {
        InputManager.onInputEvent += OnInputEvent;

        // Display subtext based on whether it's the last level or not
        if (subtext != null) {
            subtext.text = (GlobalGameSettings.LevelData.Count > 0 && !lastLevelReached()) ? TextNextLevel : TextRestart;
        } else {
            Debug.Log("no subtext assigned");
        }

        restartInProgress = false;
    }

    private void OnDisable() {
        InputManager.onInputEvent -= OnInputEvent;
    }

    // Input event
    private void OnInputEvent(string action, BUTTONSTATE buttonState) {
        if(buttonState != BUTTONSTATE.PRESS) return;

        // Instead of loading the next level, load the Main Menu scene
        LoadMainMenu();
    }

    void Update() {

        // Text color effect
        if (text != null && text.gameObject.activeSelf) {
            float t = Mathf.PingPong(Time.time * speed, 1f);
            text.color = ColorTransition.Evaluate(t);
        }

        // Alternative input events (mouse click or enter key)
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return)) { 
            OnInputEvent("AnyKey", BUTTONSTATE.PRESS);
        }
    }

    // Load the Main Menu scene
    void LoadMainMenu() {
        if (!restartInProgress) {
            restartInProgress = true;

            // Play the start sound effect
            GlobalAudioPlayer.PlaySFX("ButtonStart");

            // Start the button flicker effect
            ButtonFlicker bf = GetComponentInChildren<ButtonFlicker>();
            if (bf != null) bf.StartButtonFlicker();

            // Load the main menu scene
            LoadScene(mainMenuSceneName);
        }
    }

    // Returns true if we are currently at the last level
    bool lastLevelReached() {
        int totalNumberOfLevels = Mathf.Clamp(GlobalGameSettings.LevelData.Count - 1, 0, GlobalGameSettings.LevelData.Count);
        return GlobalGameSettings.currentLevelId == totalNumberOfLevels;
    }
}
