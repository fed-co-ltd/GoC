using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour {
    public GameObject openStart;
    public GameObject StartOptions;
    public GameObject MoreOptions;
    public Button ShowMoreOptionsButton;
    public void GoToScene (string scene) {
        SceneManager.LoadScene (scene);
    }

    public void ShowStart (bool isShow) {
        if (isShow) {
            openStart.SetActive (true);
        } else {
            StartOptions.transform.GetChild(0).gameObject.SetActive (false);
        }   

        var controller = openStart.GetComponentInChildren<Animator> ();
        controller.SetBool ("open", isShow);
        if (!isShow) {
            openStart.SetActive (false);
        }
    }

    public void ExitGame (string reason) {
        Debug.Log ("Explicit Exit : " + reason);
        Application.Quit ();

    }

    public void ShowMoreOptions (bool isShow) {
        MoreOptions.SetActive (isShow);
        ShowMoreOptionsButton.gameObject.SetActive (!isShow);
    }

}