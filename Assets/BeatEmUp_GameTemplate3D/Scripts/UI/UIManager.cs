using UnityEngine;

public class UIManager : MonoBehaviour 
{
    public UIFader UI_fader;
    public UI_Screen[] UIMenus;

    void Awake() 
    {
        DisableAllScreens();
        DontDestroyOnLoad(gameObject); // Prevents UIManager from being destroyed when changing scenes
    }

    // Shows a menu by name
    public void ShowMenu(string name, bool disableAllScreens) 
    {
        if (disableAllScreens) 
            DisableAllScreens();

        foreach (UI_Screen UI in UIMenus) 
        {
            if (UI.UI_Name == name) 
            {
                if (UI.UI_Gameobject != null) 
                {
                    UI.UI_Gameobject.SetActive(true);
                    SetTouchScreenControls(UI);

                    // Ensure TouchScreenControls is also activated when HUD or TrainingRoom is activated
                    if (name == "HUD" || name == "TrainingRoom") 
                    {
                        ShowMenu("TouchScreenControls", false);
                    }
                } 
                else 
                {
                    Debug.Log("No menu found with name: " + name);
                }
            }
        }

        // Apply fade-in effect
        if (UI_fader != null) 
        {
            UI_fader.gameObject.SetActive(true);
            UI_fader.Fade(UIFader.FADE.FadeIn, .5f, .3f);
        }
    }

    public void ShowMenu(string name) 
    {
        ShowMenu(name, true);
    }

    // Closes a menu by name
    public void CloseMenu(string name) 
    {
        foreach (UI_Screen UI in UIMenus) 
        {
            if (UI.UI_Name == name) 
                UI.UI_Gameobject.SetActive(false);
        }
    }

    // Disables all UI screens
    public void DisableAllScreens() 
    {
        foreach (UI_Screen UI in UIMenus) 
        { 
            if (UI.UI_Gameobject != null) 
                UI.UI_Gameobject.SetActive(false);
            else 
                Debug.Log("Null reference found in UI with name: " + UI.UI_Name);
        }
    }

    // Show or hide touch screen controls
    void SetTouchScreenControls(UI_Screen UI) 
    {
        if (UI.UI_Name == "TouchScreenControls") return;

        InputManager inputManager = GameObject.FindObjectOfType<InputManager>();

        if (inputManager != null && inputManager.inputType == INPUTTYPE.TOUCHSCREEN) 
        {
            if (UI.showTouchControls) 
            {
                ShowMenu("TouchScreenControls", false);
            } 
            else 
            {
                CloseMenu("TouchScreenControls");
            }
        }
    }
}

[System.Serializable]
public class UI_Screen 
{
    public string UI_Name;
    public GameObject UI_Gameobject;
    public bool showTouchControls;
}
