using System;
using UnityEngine;

public abstract class IHittable : MonoBehaviour
{

    public abstract GameObject OnHit();
    public abstract void Damage();
    public abstract void Drop();

    public abstract event Action TellIWasKidnapped;

}
