using UnityEngine;

public class MainFollowScript : MonoBehaviour
{
    public Transform Player1;
    public Transform Player2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = (Player1.transform.position+ Player2.transform.position)/2;
    }
}
