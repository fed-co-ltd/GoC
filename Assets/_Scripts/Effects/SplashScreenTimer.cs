using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenTimer : MonoBehaviour
{
    // Start is called before the first frame update
    public float WaitTimeBeforeGoingToStart;
    void Start()
    {
        Invoke("GoToStart", WaitTimeBeforeGoingToStart);
    }
    void GoToStart(){
        SceneManager.LoadScene(1);
    }
    // Update is called once per frame
    
}
