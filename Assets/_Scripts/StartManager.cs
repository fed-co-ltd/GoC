using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class StartManager : MonoBehaviour
{
    public GameObject openStart;
    public GameObject MoreOptions;
    public Button ShowMoreOptionsButton;
    public void GoToScene(string scene){
        SceneManager.LoadScene(scene);
    }   
    
    public void ShowStart(bool isShow){
        var controller = openStart.GetComponentInChildren<Animator>();
        controller.SetBool("open", isShow);
    }

    public void ExitGame(string reason){
        Debug.Log("Explicit Exit : " + reason);
        Application.Quit();

    }

    public void ShowMoreOptions(bool isShow){
        MoreOptions.SetActive(isShow);
        ShowMoreOptionsButton.gameObject.SetActive(!isShow);
    }
    
}
