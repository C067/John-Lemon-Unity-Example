using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    //Components
    public GameObject player;
    public CanvasGroup exitBackgroundImage;
    public CanvasGroup caughtBackgroundImage;
    public AudioSource exitAudio;
    public AudioSource caughtAudio;

    //Variables
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    private bool isPlayerAtExit;
    private bool isPlayerCaught;
    private bool hasAudioPlayed;
    private float timer;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            isPlayerAtExit = true;
        }
    }

    public void CaughtPlayer()
    {
        isPlayerCaught = true;
    }

    void Update()
    {
        if (isPlayerAtExit)
        {
            EndLevel(exitBackgroundImage, false, exitAudio);
        }
        else if (isPlayerCaught)
        {
            EndLevel(caughtBackgroundImage, true, caughtAudio);
        }
    }

    private void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        if (!hasAudioPlayed)
        {
            audioSource.Play();
            hasAudioPlayed = true;
        }

        timer += Time.deltaTime;
        imageCanvasGroup.alpha = timer / fadeDuration;

        if (timer > fadeDuration + displayImageDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                Application.Quit();
            }
        }
    }
}
