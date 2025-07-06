using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SimpleZMover : MonoBehaviour
{
    public float speed = 3f;

    private Rigidbody rb;
    private Vector3 moveDirection = Vector3.forward;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * speed;
    }

    void OnCollisionEnter(Collision collision)
    {
        moveDirection = -moveDirection;
    }
}