using UnityEngine;
using UnityEngine.Video;

public class DeathScreen : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    private bool videoPlayerEnabled = false;

    void Update()
    {
        // Check if the 'R' key is pressed and the video player is enabled
        if (Input.GetKeyDown(KeyCode.R) && videoPlayerEnabled)
        {
            // Disable the video player
            videoPlayer.Stop();
            videoPlayer.enabled = false;
            videoPlayerEnabled = false;
        }

        // Check if the 'E' key is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Quit"); // Print "Quit" in the console
            Application.Quit(); // Quit the application
        }
    }

    // OnTriggerEnter is called when the Collider other enters the trigger
    void OnTriggerEnter(Collider other)
    {
        // Check if the collider is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            // Enable the video player
            videoPlayer.enabled = true;
            videoPlayer.Play();
            videoPlayerEnabled = true;
        }
    }
}
