using UnityEngine;
using UnityEngine.UI;

public class PostureManager : MonoBehaviour
{
    [Header("Player Posture")]
    public Slider playerPostureSlider;
    public float playerPostureValue = 0f;

    [Header("Enemy Posture")]
    public Slider enemyPostureSlider;
    public float enemyPostureValue = 0f;

    [Header("Special Attack Button")]
    public GameObject specialAttackButton;  // Reference to the Special Attack button (GameObject)
    public GameObject specialAttackButtonDummy;

    private void Start()
    {
        // Initialize Player Slider
        if (playerPostureSlider != null)
        {
            playerPostureSlider.minValue = 0;
            playerPostureSlider.maxValue = 20;
            playerPostureSlider.value = playerPostureValue;
            playerPostureSlider.interactable = false; // Non-adjustable
        }
        else
        {
            Debug.LogWarning("Player Posture Slider is not assigned!");
        }

        // Initialize Enemy Slider
        if (enemyPostureSlider != null)
        {
            enemyPostureSlider.minValue = 0;
            enemyPostureSlider.maxValue = 20;
            enemyPostureSlider.value = enemyPostureValue;
            enemyPostureSlider.interactable = false; // Non-adjustable
        }
        else
        {
            Debug.LogWarning("Enemy Posture Slider is not assigned!");
        }

        // Initialize Special Attack Button (Disabled by default)
        if (specialAttackButton != null)
        {
            specialAttackButton.SetActive(false); // Disable button at start
            specialAttackButtonDummy.SetActive(true); // Disable button at start
        }
        else
        {
            Debug.LogWarning("Special Attack Button is not assigned!");
        }
    }

    // Add value to Player's posture (clamped between 0-20)
    public void AddPlayerPosture(float value)
    {
        playerPostureValue = Mathf.Clamp(playerPostureValue + value, 0, 20);
        UpdatePlayerSlider();

        // If player posture reaches 20, enable the special attack button
        if (playerPostureValue >= 20 && specialAttackButton != null)
        {
            specialAttackButton.SetActive(true); // Activate button when posture is 20
            specialAttackButtonDummy.SetActive(false); // Disable button at start
        }
    }

    // Add value to Enemy's posture (clamped between 0-20)
    public void AddEnemyPosture(float value)
    {
        enemyPostureValue = Mathf.Clamp(enemyPostureValue + value, 0, 20);
        UpdateEnemySlider();
    }

    // Reset Player's posture to 0
    public void ResetPlayerPosture()
    {
        playerPostureValue = 0;
        UpdatePlayerSlider();

        // Deactivate special attack button when posture resets
        if (specialAttackButton != null)
        {
            specialAttackButton.SetActive(false); // Deactivate button when posture is 0
            specialAttackButtonDummy.SetActive(true); // Disable button at start
        }
    }

    // Reset Enemy's posture to 0
    public void ResetEnemyPosture()
    {
        enemyPostureValue = 0;
        UpdateEnemySlider();
    }

    // Update Player Slider
    private void UpdatePlayerSlider()
    {
        if (playerPostureSlider != null)
        {
            playerPostureSlider.value = playerPostureValue;
        }
    }

    // Update Enemy Slider
    private void UpdateEnemySlider()
    {
        if (enemyPostureSlider != null)
        {
            enemyPostureSlider.value = enemyPostureValue;
        }
    }
}
