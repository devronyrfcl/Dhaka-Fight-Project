using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    // Start a coroutine to wait 3 seconds before calling LevelComplete
    public void TriggerGameOver()
    {
        StartCoroutine(LevelCompleteWithDelay());
    }

    private IEnumerator LevelCompleteWithDelay()
    {
        // Wait for 3 seconds
        yield return new WaitForSeconds(4f);

        // Call LevelComplete logic after 3 seconds
        LevelComplete();
    }

    // The LevelComplete logic
    public void LevelComplete()
    {
        // For example, show the LevelComplete screen
        UIManager UI = GameObject.FindObjectOfType<UIManager>();
        if (UI != null)
        {
            UI.DisableAllScreens();
            UI.ShowMenu("LevelComplete");
        }
        else
        {
            Debug.LogError("UIManager not found!");
        }
    }
}
