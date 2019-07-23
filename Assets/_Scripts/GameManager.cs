using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Text ProgressReport;
    float mapProgress = 0;
    void Start()
    {
        StartCoroutine(UpdateProgress());
    }

    void Update(){
        if (mapProgress < 20 || mapProgress > 35 )
        {
            ProgressReport.text = "Creating Tile Map     " + mapProgress.ToString() + "% ";
        }
    }

    // Update is called once per frame
    IEnumerator UpdateProgress(){
        while (mapProgress < 100)
        {
            mapProgress += 1;
            yield return new WaitForSeconds(0.1f);
        }
        StartGame();
    }
    void StartGame(){
        SceneManager.LoadScene(9);
    }
}
