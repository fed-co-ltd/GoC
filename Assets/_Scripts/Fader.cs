using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoC;
public class Fader : MonoBehaviour, ITransition
{
    // Start is called before the first frame update

    public IEnumerator TransitionUIElement(Image element, float start, float end,float delay = 0, float lerpTime = 0.5f){
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
