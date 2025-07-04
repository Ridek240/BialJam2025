using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RoombaMover : MonoBehaviour
{
    public float speed = 1f;
    public float changeDirectionInterval = 2f; // co ile sekund zmienia kierunek

    private Rigidbody rb;
    private Vector3 moveDirection;
    private float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        ChooseRandomDirection();
        timer = changeDirectionInterval;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * speed;

        timer -= Time.fixedDeltaTime;
        if (timer <= 0f)
        {
            ChooseRandomDirection();
            timer = changeDirectionInterval;
        }
    }

    private void ChooseRandomDirection()
    {
        int dir = Random.Range(0, 4); // 0, 1, 2, 3

        switch (dir)
        {
            case 0: moveDirection = Vector3.right; break;      // +X
            case 1: moveDirection = Vector3.left; break;       // -X
            case 2: moveDirection = Vector3.forward; break;    // +Z
            case 3: moveDirection = Vector3.back; break;       // -Z
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Odbijamy kierunek po kolizji ze ścianą
        Vector3 normal = collision.contacts[0].normal;

        // Sprawdzamy, czy poruszaliśmy się w X czy Z
        if (Mathf.Abs(moveDirection.x) > 0.1f)
            moveDirection = new Vector3(-moveDirection.x, 0f, 0f);
        else if (Mathf.Abs(moveDirection.z) > 0.1f)
            moveDirection = new Vector3(0f, 0f, -moveDirection.z);
    }
}