using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoC;

public class Splasher : MonoBehaviour
{
    GameObject container;
    List<GameObject> childrens = new List<GameObject>();
    ITransition ScreenFader;
    public bool isFadeInOut = false;
    public float FadeDelay;
    public float FadeDuration;
    public float FadeEnd = 0;
    public float SplashDelay;
    public float SplashTime;
    void Start()
    {
        if (isFadeInOut)
        {
            var waitTime = SplashDelay + FadeDelay + FadeDuration + SplashTime;
            StartCoroutine(Splash(SplashDelay,1));
            StartCoroutine(Splash(waitTime,0));
        }else{
            StartCoroutine(Splash(0,FadeEnd));
        }
        
    }

    IEnumerator Splash(float delay, float percent){

        yield return new WaitForSeconds(delay);
        container = this.gameObject;
        ScreenFader = GetComponent<ITransition>();
        for (int i = -1; i < container.transform.childCount; i++)
        {
            var element = container;
            if (i > -1)
            {
                childrens.Add(container.transform.GetChild(i).gameObject);
                element = childrens[i];
            }
            var elementImage = element.gameObject.GetComponent<Image>();
            StartCoroutine(ScreenFader.TransitionUIElement(elementImage, elementImage.color.a, percent, FadeDelay, FadeDuration));  
        }
    }

    
}
