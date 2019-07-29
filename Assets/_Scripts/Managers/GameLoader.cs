using System;
using System.Collections;
using System.Collections.Generic;
using GoC;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoader : MonoBehaviour {
    // Start is called before the first frame update
    public AudioClip GameMusic;
    public Text ProgressReport;
    void Start () {
        StartCoroutine (StartGame ());
    }

    // Update is called once per frame
    void UpdateProgress (float progress) {
       

    }
    IEnumerator StartGame () {
        yield return null;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync (9);
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone) {
            var progress = asyncLoad.progress;
            var percentProgress = Mathf.RoundToInt (progress * 100);
            int partitionProgress;
            if (percentProgress >= 75) {
                partitionProgress = Mathf.RoundToInt (((percentProgress - 75) / 25) * 100);
                ProgressReport.text = "Generating Player Data     " + partitionProgress.ToString () + "% ";
            } else if (percentProgress >= 50) {
                partitionProgress = Mathf.RoundToInt (((percentProgress - 50) / 25) * 100);
                ProgressReport.text = "Loading Game Resources     " + partitionProgress.ToString () + "% ";
            } else {
                partitionProgress = Mathf.RoundToInt ((percentProgress / 50) * 100);
                ProgressReport.text = "Creating Game Map     " + partitionProgress.ToString () + "% ";
            }

            if (percentProgress >= 0.99f) {
                var fader = GetComponent<ITransition> ();
                var SplashTransition = GameObject.Find ("SplashTransition");
                SplashTransition.SetActive (true);
                var elementImage = SplashTransition.gameObject.GetComponent<Image> ();
                StartCoroutine (fader.TransitionUIElement (elementImage, elementImage.color.a, 1, 0, 0.5f));
                yield return new WaitForSeconds (0.2f);
                int fakeProgress = 0;
                 while (fakeProgress < 100)  
                 {
                     fakeProgress += 1;
                     ProgressReport.text = "Building Game     " + fakeProgress.ToString () + "% ";
                     yield return new WaitForSeconds(0.05f);
                 }
                 ProgressReport.text = "Game Loaded Succefully";
                 yield return new WaitForSeconds (1f);
                var player = GameObject.Find ("*Music Player");
                if (player != null) {
                    AudioSource MusicPlayer = player.GetComponentInChildren<AudioSource> ();
                    MusicPlayer.clip = GameMusic;
                    MusicPlayer.Play ();
                }

                asyncLoad.allowSceneActivation = true;
                yield return null;
            }
        }

    }
}