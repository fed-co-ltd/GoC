using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScenesManager : MonoBehaviour
{
    public float waitingTimeToLoadNextScene;
    public GameObject SplashTransition;
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Invoke("GotoNextScene", waitingTimeToLoadNextScene);
        }else{
            RemoveTransition();
        }
        
    }

    public void LoadScene(string scene){
        SceneManager.LoadScene(scene);
    }

    public void GotoNextScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RemoveTransition(){
        var splashScript = SplashTransition.GetComponentInChildren<Splasher>();
        var wait = splashScript.FadeDuration;
        StartCoroutine(DeactivateGameObject(SplashTransition,wait));
    }
    IEnumerator DeactivateGameObject(GameObject removable,float waitTime){
        yield return new WaitForSeconds(waitTime);
        removable.SetActive(false);
    }


    
}
