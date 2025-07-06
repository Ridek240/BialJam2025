using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RoombaMover : MonoBehaviour
{
    private Rigidbody rb;

    //Movement Speed
    public float speed = 1f;
    public float minSpeed = 0.5f;
    public float maxSpeed = 2f;

    //Movement Direction
    private Vector3 moveDirection;

    //Movement Timer
    private float timer;
    public float changeDirectionInterval = 2f;

    //Light
    public Light glowLight; //Inspector
    public float baseLightIntensity = 1f;
    public float collisionLightIntensity = 3f;
    public float lightFadeSpeed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        //Movement Speed
        speed = Random.Range(minSpeed, maxSpeed);
        ChooseRandomDirection();
        timer = changeDirectionInterval;

        //Light
        if (glowLight != null) glowLight.intensity = baseLightIntensity;
    }

    void Update()
    {
        //Light
        if (glowLight.intensity > baseLightIntensity)
        {
            glowLight.intensity = Mathf.MoveTowards(
                glowLight.intensity,
                baseLightIntensity,
                lightFadeSpeed * Time.deltaTime
            );
        }
    }

    void FixedUpdate()
    {
        //Movement Speed
        rb.linearVelocity = moveDirection * speed;

        //Movement Timer
        timer -= Time.fixedDeltaTime;
        if (timer <= 0f)
        {
            ChooseRandomDirection();
            timer = changeDirectionInterval;
        }
    }

    private void ChooseRandomDirection()
    {
        //Movement Direction
        switch (Random.Range(0, 4))
        {
            case 0: moveDirection = Vector3.right; break;      // +X
            case 1: moveDirection = Vector3.left; break;       // -X
            case 2: moveDirection = Vector3.forward; break;    // +Z
            case 3: moveDirection = Vector3.back; break;       // -Z
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 normal = collision.contacts[0].normal;

        //Movement Direction
        if (Mathf.Abs(moveDirection.x) > 0.1f)
            moveDirection = new Vector3(-moveDirection.x, 0f, 0f);
        else if (Mathf.Abs(moveDirection.z) > 0.1f)
            moveDirection = new Vector3(0f, 0f, -moveDirection.z);

        //Light
        if (glowLight != null) glowLight.intensity = collisionLightIntensity;
    }
}