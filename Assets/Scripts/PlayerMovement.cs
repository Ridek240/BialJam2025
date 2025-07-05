using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : PlayerBase
{
    public Vector2 MovementVector;
    public float speed = 5.0f;
    private Rigidbody controller;
    public GameObject Head;
    public JawsScript Jaws;
    public MawScript Maw;
    public float rotateSpeed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 move = new Vector3(MovementVector.x, 0, MovementVector.y);
        if(move != Vector3.zero) 
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotateSpeed * Time.fixedDeltaTime
            );
        }
        controller.linearVelocity = (move) * speed;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementVector = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Jaws.Jaws(true);
        }
        else if(context.canceled) 
        {
            Jaws.Jaws(false);
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        Maw.Dash();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        Maw.Chomp();
    }

}
