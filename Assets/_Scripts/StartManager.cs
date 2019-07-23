using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class StartManager : MonoBehaviour
{
    public GameObject openStart;
    private Text MusicLabel;
    public GameObject MoreOptions;
    public Button ShowMoreOptionsButton;
    public void GoToScene(string scene){
        SceneManager.LoadScene(scene);
    }   
    
    public void ShowStart(bool isShow){
        var controller = openStart.GetComponentInChildren<Animator>();
        controller.SetBool("open", isShow);
    }

    public void ChangeGameMusicLabel(Text label){
        MusicLabel = label;
    }

    public void ChangeGameMusicLabelValue(Slider slider){
        MusicLabel.text = slider.value.ToString();
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
