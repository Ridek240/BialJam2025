using UnityEngine;

public class JawsScript : MonoBehaviour
{
    public Animator animator;
    public GameObject Target;
    public float attackRange = 2;
    public LayerMask hittableLayers;
    public Transform attackOrigin;

    public AttackTrigger trigger;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if(Target !=null)
        {
            Target.transform.position = attackOrigin.position;
        }
    }

    public void Jaws(bool Activate)
    {
        if(Activate)
        {
            animator.SetBool("Jaws",Activate);
            TryHit();
        }
        if (!Activate)
        {
            animator.SetBool("Jaws", Activate);
            if(Target!=null)
            {

                Target.GetComponent<IHittable>().Drop();
                Target = null;
            }
        }
    }

    private void TryHit()
    {
        Target = trigger.TryHitFirst();
    }

    public void TheChildWasTaken()
    {
        Target.GetComponent<IHittable>().TellIWasKidnapped -= TheChildWasTaken;
        Target = null;
    }
}
