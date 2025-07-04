using System;
using UnityEngine;

public interface IHittable
{

    public abstract GameObject OnHit();
    public abstract GameObject Damage();

    public event Action TellIWasKidnapped;

}
