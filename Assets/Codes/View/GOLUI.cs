using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GOLUI : MonoBehaviour
{
    public GOLView View;

    public Button Button_Clear;
    public Button Button_Play;
    public Button Button_Stop;
    public Button Button_Reset;
    public Slider Slider_RunInterval;

    public GameObject EditorSelectPoint;

    public AnimationCurve RunSpeedTransformation;
    
    public void EventClear()
    {
        this.View.EventClear();
        
        this.Button_Clear.interactable = true;
        this.Button_Play.interactable = true;
        
        this.Button_Stop.interactable = false;
        this.Button_Reset.interactable = false;
        
        this.EditorSelectPoint.SetActive(true);
    }
    
    public void EventPlay()
    {
        this.View.EventPlay();

        this.Button_Clear.interactable = false;
        this.Button_Play.interactable = false;
        
        this.Button_Stop.interactable = true;
        this.Button_Reset.interactable = true;
        
        this.EditorSelectPoint.SetActive(false);
    }
    
    public void EventReset2Edit()
    {
        this.View.EventReset2Edit();
        
        this.Button_Clear.interactable = true;
        this.Button_Play.interactable = true;
        
        this.Button_Stop.interactable = false;
        this.Button_Reset.interactable = false;
        
        this.EditorSelectPoint.SetActive(true);
    }
    
    public void EventStop()
    {
        this.View.EventStop();
        
        this.Button_Clear.interactable = false;
        this.Button_Play.interactable = true;
        
        this.Button_Stop.interactable = false;
        this.Button_Reset.interactable = true;
        
        this.EditorSelectPoint.SetActive(false);
    }

    public void EventRunInterval()
    {
        var runInterval = this.RunSpeedTransformation.Evaluate(this.Slider_RunInterval.value);
        this.View.EventRunInterval(runInterval);
    }
}
