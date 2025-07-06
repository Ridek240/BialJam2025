using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RandomMover : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.freezeRotation = true;

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
        Vector3 dir = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
        rb.linearVelocity = dir * speed;
    }
}