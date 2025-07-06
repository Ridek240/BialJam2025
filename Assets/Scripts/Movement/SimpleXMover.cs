using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SimpleXMover : MonoBehaviour
{
    public float speed = 3f;

    private Rigidbody rb;
    private Vector3 moveDirection = Vector3.right;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * speed;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Zmiana kierunku przy kolizji
        moveDirection = -moveDirection;
    }
}