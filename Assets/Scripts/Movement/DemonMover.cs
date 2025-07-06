using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DemonMover : MonoBehaviour
{

    /*
    private Rigidbody rb;

    [Header("Ruch")]
    public float minSpeed = 1f;
    public float maxSpeed = 3f;
    private float speed;

    [Header("Obr�t")]
    public float rotationSpeed = 180f; // stopni/sekund�

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.freezeRotation = true;

        speed = Random.Range(minSpeed, maxSpeed);

        MoveInRandomDirection();
    }

    private void FixedUpdate()
    {
        if (rb.linearVelocity.magnitude < 0.1f)
        {
            MoveInRandomDirection();
        }
        else
        {
            rb.linearVelocity = rb.linearVelocity.normalized * speed;
            transform.Rotate(0f, rotationSpeed * Time.fixedDeltaTime, 0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 normal = collision.contacts[0].normal;
        Vector3 reflected = Vector3.Reflect(rb.linearVelocity.normalized, normal);

        if (reflected.magnitude < 0.1f)
        {
            MoveInRandomDirection();
        }
        else
        {
            rb.linearVelocity = reflected * speed;
        }
    }

    void MoveInRandomDirection()
    {
        Vector3 dir = new Vector3(Random.Range(-1f, 1f), 9f, Random.Range(-1f, 1f)).normalized;
        rb.linearVelocity = dir * speed;
    }*/


    public float speed = 5f;
    public float rotationChangeInterval = 3f;
    public float rotationAngleRange = 90f;
    public float minRotationChangeInterval = 1f;
    public float maxRotationChangeInterval = 5f;

    private Rigidbody rb;

    public bool CanMove = true;


    public Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        StartCoroutine(ChangeRotationRoutine());
    }

    void FixedUpdate()
    {
        // Zawsze jed� do przodu
        if(CanMove)
        {

            animator.SetBool("Walk",true);
            animator.SetBool("Fall",false);
            rb.linearVelocity = transform.forward * speed;
        }
    }

    IEnumerator ChangeRotationRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minRotationChangeInterval, maxRotationChangeInterval);
            yield return new WaitForSeconds(rotationChangeInterval);

            float randomY = Random.Range(-rotationAngleRange, rotationAngleRange);
            transform.Rotate(0f, randomY, 0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 normal = collision.contacts[0].normal;
        Vector3 reflectedDir = Vector3.Reflect(transform.forward, normal);

        if (reflectedDir.sqrMagnitude > 0.01f)
        {
            // Losowy kąt od -rotationAngleRange do +rotationAngleRange
            float randomOffset = Random.Range(-rotationAngleRange, rotationAngleRange);

            // Stwórz quaternion obrotu o ten kąt wokół osi Y
            Quaternion randomRotation = Quaternion.Euler(0f, randomOffset, 0f);

            // Pomnóż odbity kierunek przez ten obrót
            Vector3 finalDir = randomRotation * reflectedDir.normalized;

            Quaternion targetRotation = Quaternion.LookRotation(finalDir, Vector3.up);
            transform.rotation = targetRotation;
        }
    }

    public void IWasThrown()
    {
        StartCoroutine(ChildThrown()); 
    }

    IEnumerator ChildThrown()
    {
        CanMove = false;
        animator.SetBool("Walk", false);
        animator.SetBool("Fall", true);
        yield return new WaitForSeconds(2f);
        CanMove = true;
    }
}
