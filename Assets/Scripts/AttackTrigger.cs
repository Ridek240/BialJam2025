using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    private List<IHittable> hittablesInRange = new List<IHittable>();

    private void OnTriggerEnter(Collider other)
    {
        var hittable = other.GetComponent<IHittable>();
        if (hittable != null && !hittablesInRange.Contains(hittable))
        {
            hittablesInRange.Add(hittable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var hittable = other.GetComponent<IHittable>();
        if (hittable != null)
        {
            hittablesInRange.Remove(hittable);
        }
    }

    public GameObject TryHitFirst()
    {
        if (hittablesInRange.Count > 0)
        {
            var target = hittablesInRange[0];
            if (target != null)
            {
                return target.OnHit();
            }
        }
        return null;
    }
    public void AttackAll()
    {
        foreach (var hittable in hittablesInRange)
        {
            hittable.Damage();
        }
    }
}
