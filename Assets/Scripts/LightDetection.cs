using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
public class LightDetection : MonoBehaviour
{

    public List<PlayerBase> segments = new List<PlayerBase> ();

    //public UnityEvent<int> EnlightedPlayerChanged; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DetectionSystemUI.instance.AddToList (this);
    }


    private void OnTriggerEnter(Collider other)
    {
        var hittable = other.GetComponent<PlayerBase>();
        if (hittable != null && !segments.Contains(hittable))
        {
            segments.Add(hittable);
        }

        //DetectionSystemUI.instance.Modifier = segments.Count;


    }

    private void OnTriggerExit(Collider other)
    {
        var hittable = other.GetComponent<PlayerBase>();
        if (hittable != null)
        {
            segments.Remove(hittable);
        }
       // DetectionSystemUI.instance.Modifier = segments.Count;
    }
}
