using System;
using UnityEngine;
using UnityEngine.Events;

public class ChildHibox : IHittable
{
    public override event Action TellIWasKidnapped;

    public UnityEvent ChildTaken;
    public UnityEvent ChildDroped;
    public UnityEvent ChildKilled;
    public override void Damage()
    {
        TellIWasKidnapped?.Invoke();
        ChildKilled?.Invoke();
        Destroy(gameObject);
    }

    public override void Drop()
    {
        TellIWasKidnapped?.Invoke();
        ChildDroped?.Invoke();
    }

    public override GameObject OnHit()
    {
        TellIWasKidnapped?.Invoke();
        ChildTaken?.Invoke();
        return this.gameObject;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
