using UnityEngine;

public class MawScript : MonoBehaviour
{
    public Transform leftTarget;
    public Transform rightTarget;
    public Rigidbody rigidbody;
    public AttackTrigger trigger;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


    public float rotationSpeed = 5f;
    public float dashForce = 15f;

    public void Chomp()
    {
        trigger.AttackAll();
    }
    public void Dash()
    {
        StartDash();
    }

    void StartDash()
    {


        // Kierunek dasha � np. prz�d obiektu
        Vector3 dashDirection = transform.forward;

        // Resetuj pr�dko�� i dodaj si�� (impuls)
        rigidbody.linearVelocity = Vector3.zero;
        rigidbody.AddForce(dashDirection * dashForce, ForceMode.Impulse);
    }
    void Update()
    {
        if (leftTarget == null || rightTarget == null)
            return;

        // Kierunek mi�dzy lewym a prawym
        Vector3 leftToRight = (rightTarget.position - leftTarget.position).normalized;

        Vector3 forward = Vector3.Cross(leftToRight, Vector3.up); // forward pokazuje tak, �eby lewy by� po lewej

        Vector3 lookAtPoint = transform.position + forward;

        // Zr�b p�aski kierunek (ignoruj Y)
        Vector3 lookDir = lookAtPoint - transform.position;
        lookDir.y = 0f;

        if (lookDir.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(lookDir, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * rotationSpeed);
        }
    }
}


