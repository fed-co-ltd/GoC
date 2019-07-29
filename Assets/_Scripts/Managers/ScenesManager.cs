using System.Collections;
using System.Collections.Generic;
using GoC;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesManager : MonoBehaviour {
    public float waitingTimeToLoadNextScene;
    public GameObject SplashTransition;

    ITransition fader;
    void Start () {
        
        fader = GetComponent<ITransition> ();
        if (SceneManager.GetActiveScene ().buildIndex > 0) {
            SplashTransition.SetActive (true);
            RemoveTransition ();
        }

    }

    public void LoadScene (int buildIndex) {
        StartCoroutine (ShiftScene (buildIndex));
    }

    IEnumerator ShiftScene (int buildIndex) {
        SplashTransition.SetActive (true);
        var elementImage = SplashTransition.gameObject.GetComponent<Image> ();
        StartCoroutine (fader.TransitionUIElement (elementImage, elementImage.color.a, 1, 0, waitingTimeToLoadNextScene));
        yield return new WaitForSeconds (waitingTimeToLoadNextScene);
        SceneManager.LoadScene (buildIndex);
    }
    public void GotoNextScene () {
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
    }

    public void RemoveTransition () {
        var splashScript = SplashTransition.GetComponentInChildren<Splasher> ();
        var wait = splashScript.FadeDuration;
        StartCoroutine (DeactivateGameObject (SplashTransition, wait));
    }
    IEnumerator DeactivateGameObject (GameObject removable, float waitTime) {
        yield return new WaitForSeconds (waitTime);
        removable.SetActive (false);
    }

    public void ChangeMusic (AudioClip clip) {
        var player = GameObject.Find ("*Music Player");
        if (player != null) {
            AudioSource MusicPlayer = player.GetComponentInChildren<AudioSource> ();
            MusicPlayer.clip = clip;
            MusicPlayer.Play ();
        }

    }

}