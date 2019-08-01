using System.Collections;
using System.Collections.Generic;
using GoC;
using UnityEngine;
using UnityEngine.UI;

public class GamePause : MonoBehaviour {
    // Start is called before the first frame update
    public Image pauseImage;
    public Text timerText;
    ITransition Fader;
    void Start(){
        Fader = GetComponent<ITransition>();
    }
    public void HoverPauseGame () {
        StartCoroutine (Fader.TransitionUIElement (timerText, pauseImage.color.a, 0, 0, 0.75f));
        StartCoroutine (Fader.TransitionUIElement (pauseImage, pauseImage.color.a, 1, 0));
    }
    public void ExitHoverPauseGame () {
        StartCoroutine (Fader.TransitionUIElement (pauseImage, pauseImage.color.a, 0, 0, 0.75f));
        StartCoroutine (Fader.TransitionUIElement (timerText, pauseImage.color.a, 1, 0.5f, 1));
    }
}