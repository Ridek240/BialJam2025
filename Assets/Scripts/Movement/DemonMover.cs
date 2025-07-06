using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DemonMover : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Ruch")]
    public float minSpeed = 1f;
    public float maxSpeed = 3f;
    private float speed;

    [Header("Obrót")]
    public float rotationSpeed = 180f; // stopni/sekundê

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
        Vector3 dir = new Vector3(Random.Range(-1f, 1f), rb.linearVelocity.y, Random.Range(-1f, 1f)).normalized;
        rb.linearVelocity = dir * speed;
    }
}