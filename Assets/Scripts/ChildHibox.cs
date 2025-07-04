using System;
using UnityEngine;

public class ChildHibox : IHittable
{
    public override event Action TellIWasKidnapped;

    public override void Damage()
    {
        TellIWasKidnapped?.Invoke();
        Destroy(gameObject);
    }

    public override GameObject OnHit()
    {
        TellIWasKidnapped?.Invoke();
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
