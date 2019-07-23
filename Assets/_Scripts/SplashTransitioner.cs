using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashTransitioner : MonoBehaviour
{
    // Start is called before the first frame update
   
    void Awake()
    {
        var rectTransform = GetComponent<RectTransform>();
        rectTransform.offsetMin += new Vector2(900,0);
        rectTransform.offsetMax -= new Vector2(-900,0);
    }
}
