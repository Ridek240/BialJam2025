using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RandomMover : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.freezeRotation = true;

        MoveInRandomDirection();
    }

    private void FixedUpdate()
    {
        // Jeśli obiekt się zatrzymał (np. po kolizji), wymuś ruch
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

        // Jeśli reflected wektor jest zbyt mały, wymuś losowy kierunek
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