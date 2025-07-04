using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 MovementVector;
    public float speed = 5.0f;
    private Rigidbody controller;
    public GameObject Head;
    public JawsScript Jaws;
    public MawScript Maw;


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
            transform.rotation = Quaternion.LookRotation(move);
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
}
