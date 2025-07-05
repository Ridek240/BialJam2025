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


        if(Throwing)
        {
            Power += 500 * Time.fixedDeltaTime;
            Power = Mathf.Clamp(Power, 0, 5000);
            if(!Jaws.HasTarget())
            {
                Throwing = false;
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementVector = context.ReadValue<Vector2>();
    }

    public void OnJaws(InputAction.CallbackContext context)
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

    public void OnDash(InputAction.CallbackContext context)
    {
        Maw.Dash();
    }

    public void OnMaw(InputAction.CallbackContext context)
    {
        Maw.Chomp();
    }


    //throwing Childern

    [Header("Throwing Children")]
    public float Power;
    public bool Throwing;

    public void OnThrow(InputAction.CallbackContext context)
    {
        if (Jaws.HasTarget())
        {
            if (context.performed)
            {
                Power = 0;
                Throwing = true;
            }
            if (context.canceled)
            {
                if (Throwing)
                {
                    Jaws.ThrowChild(Power);
                }
                Throwing = false;

            }
        }
    }
}
