using System.Collections;
using System.Collections.Generic;
using GoC;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class OpenPanel : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject CanvasPanel;
    public float FadeDuration;
    float TransitionDuration;
    GameObject PanelContent;
    List<MaskableGraphic> UIContents;
    MaskableGraphic PanelGraphic;
    Animator Controller;
    ITransition Fader;

    void Start(){
        TransitionDuration = FadeDuration;
        UIContents = new List<MaskableGraphic>();
        Controller = CanvasPanel.GetComponentInChildren<Animator> ();
        Fader = GetComponent<ITransition> ();
        PanelContent = CanvasPanel.transform.GetChild(0).gameObject;
        PanelGraphic = CanvasPanel.GetComponent<MaskableGraphic>();
        GetAllUIContent();
    }

    public void Panel(bool isShow){
        if (isShow)
        {
            StartCoroutine(PanelOpen());
        }else{
            StartCoroutine(PanelClose());
        }
    }

    IEnumerator PanelOpen(){
        CanvasPanel.SetActive(true);
        StartCoroutine(Fader.TransitionUIElement(PanelGraphic, PanelGraphic.color.a, 1, 0, TransitionDuration));  
        Controller.SetBool ("open", true);
        yield return new WaitForSeconds(0.15f);
        
        foreach (var item in UIContents)
        {
            StartCoroutine(Fader.TransitionUIElement(item, item.color.a, 1, 0, TransitionDuration));  
        }
    }

    void GetAllUIContent(){
        for (int i = 0; i < PanelContent.transform.childCount; i++)
        {
            var child = PanelContent.transform.GetChild(i).gameObject;
            
            for (int j = 0; j < child.transform.childCount; j++)
            {
                UIContents.Add(child.transform.GetChild(j).gameObject.GetComponentInChildren<MaskableGraphic>());
            }
            UIContents.Add(child.GetComponentInChildren<MaskableGraphic>());
        }
    }
    IEnumerator PanelClose(){
        foreach (var item in UIContents)
        {
            StartCoroutine(Fader.TransitionUIElement(item, item.color.a, 0, 0, TransitionDuration));  
        }
        yield return new WaitForSeconds(TransitionDuration);
        StartCoroutine(Fader.TransitionUIElement(PanelGraphic, PanelGraphic.color.a, 0, 0, TransitionDuration)); 
        Controller.SetBool ("open", false);
        yield return new WaitForSeconds(0.15f);
        CanvasPanel.SetActive(false);
    }
}
