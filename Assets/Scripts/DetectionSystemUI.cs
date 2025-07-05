using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.HableCurve;

public class DetectionSystemUI : MonoBehaviour
{

    public Slider slider;
    public float MAXValue = 100;
    public float Value = 0;
    public int Modifier = 0;

    public static DetectionSystemUI instance;

    List<LightDetection> lightDetections = new List<LightDetection>();
    
    public void FixedUpdate()
    {
        Modifier = 0;

        foreach (LightDetection lightDetection in lightDetections)
        {
            Modifier += lightDetection.segments.Count;
        }

        if (Modifier <= 0)
        {
            Value -= 1 * Time.fixedDeltaTime;
            Value = Mathf.Max(Value, 0f);
        }
        else
        {
            Value += Modifier * Time.fixedDeltaTime;
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
