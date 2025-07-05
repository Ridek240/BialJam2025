using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SimpleMover : MonoBehaviour
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
        // Odbicie — zmiana kierunku na przeciwny
        moveDirection = -moveDirection;
    }
}