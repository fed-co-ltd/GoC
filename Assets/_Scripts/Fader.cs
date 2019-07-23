using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoC;
public class Fader : MonoBehaviour, IFader
{
    // Start is called before the first frame update
    public void FadeIn(Image element, float percentage = 1, float lerpTime = 0.5f){
        StartCoroutine(FadeUIElement(element, element.color.a, percentage,lerpTime));
    }

    public void FadeOut(Image element, float percentage = 0, float lerpTime = 0.5f){
        StartCoroutine(FadeUIElement(element, element.color.a, percentage,lerpTime));
    }

    public IEnumerator FadeUIElement(Image element, float start, float end,float delay = 0, float lerpTime = 0.5f){
        float timeStartedLerping = Time.time;
        float timeSinceStarted = Time.time - timeStartedLerping;
        float percentageComplete = timeSinceStarted / lerpTime;
        yield return new WaitForSeconds(delay);
        while (true)
        {
            timeSinceStarted = Time.time - timeStartedLerping;
            percentageComplete = timeSinceStarted / lerpTime;

            float currentValue = Mathf.Lerp(start,end, percentageComplete);
            element.color = new Color(element.color.r,element.color.b,element.color.g, currentValue);
            if (percentageComplete >= 1) break;

            yield return new WaitForEndOfFrame();
        }
    }

}
