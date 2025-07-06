using System.Collections.Generic;
using TMPro;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DetectionSystemUI : MonoBehaviour
{

    public Slider slider;
    public float MAXValue = 100;
    public float Value = 0;
    public int Modifier = 0;

    public static DetectionSystemUI instance;

    List<LightDetection> lightDetections = new List<LightDetection>();
    public List<ChildHibox> Children = new List<ChildHibox>();


    public UnityEvent PlayerLost;
    public UnityEvent PlayerWon;

    public TextMeshProUGUI ChildrenLeft;
    
    public void FixedUpdate()
    {
        Modifier = 0;

        foreach (LightDetection lightDetection in lightDetections)
        {
            Modifier += lightDetection.segments.Count;
        }

        if (Modifier <= 0)
        {
            Value -= 0.25f * Time.fixedDeltaTime;
            Value = Mathf.Max(Value, 0f);
        }
        else
        {
            Value += Modifier * Time.fixedDeltaTime;
            if(Value >= MAXValue)
            {
                PlayerLost?.Invoke();
            }
            Value = Mathf.Clamp(Value, 0f, MAXValue);
        }

        slider.value = Value;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
    }


    public void AddToList(LightDetection lightDetection)
    {
        
        if (lightDetection != null && !lightDetections.Contains(lightDetection))
        {
            lightDetections.Add(lightDetection);
        }
    }

    public void AddChildToList(ChildHibox childHibox)
    {
        if (childHibox != null && !Children.Contains(childHibox))
        {
            Children.Add(childHibox);
            ChildrenLeft.text = $"Children Left: \n {Children.Count}";
        }
    }

    public void RemoveChildToList(ChildHibox childHibox)
    {
        if (childHibox != null)
        {
            Children.Remove(childHibox);
            ChildrenLeft.text = $"Children Left: \n {Children.Count}";
            if(Children.Count<=0)
            {
                PlayerWon?.Invoke();
            }
        }
    }
}
