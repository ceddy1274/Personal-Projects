using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public RatController ratController;
    public InputField magnitudeInput;
    public Text resultText;
    public Text answerText;
    public Text coordinatesText;

    private bool isWaitingForInput = false;

    void Awake()
    {
        Instance = this;
        // Hide textboxes intially until needed
        resultText.gameObject.SetActive(false); 
        coordinatesText.gameObject.SetActive(false); 
        answerText.gameObject.SetActive(false); 
    }

    public void RatStopped()
    {
        // Enable the input field and button for user input
        isWaitingForInput = true;
        magnitudeInput.interactable = true;

        // Find how far the rat moved in graph coordinates
        Vector2 GraphPosition = ratController.GetGraphPosition();

        // Display how far the rat moved onto the screen
        string vectorTextToDisplay = "Magnitude = " + GraphPosition.x + "x + " + GraphPosition.y + "y";
        coordinatesText.text = vectorTextToDisplay;
        coordinatesText.gameObject.SetActive(true); 
    }

    public void OnSubmitButtonClick()
    {
        // Ignore input if the rat is still moving
        if (!isWaitingForInput) return; 

        // Parse user input
        float userMagnitude = float.Parse(magnitudeInput.text);

        // Get the actual magnitude
        float actualMagnitude = ratController.GetMagnitude();

        // Check if the input is correct (within a small margin of error)
        if (Mathf.Abs(userMagnitude - actualMagnitude) <= 0.5f)
        {
            // Show the result text
            resultText.text = "Correct!";
            resultText.gameObject.SetActive(true); 
            Invoke("reHideText", 1);

            // Spawn a new rat
            ratController.ResetRat(); 
        }
        else
        {
            // Show the result text
            resultText.text = "Game Over!";
            resultText.gameObject.SetActive(true);

            // Show answer text
            answerText.text = $"Answer: {actualMagnitude}";
            answerText.gameObject.SetActive(true); 
        }

        // Clear input field and disable interaction
        magnitudeInput.text = "";
        magnitudeInput.interactable = false;
        isWaitingForInput = false;
    }

    private void reHideText()
    {
        resultText.gameObject.SetActive(false);
        coordinatesText.gameObject.SetActive(false);
    }

}