using UnityEngine;

public class DualRopeConnector : MonoBehaviour
{
    public Rigidbody playerA;
    public Rigidbody playerB;
    public Rigidbody targetObject;
    public float ropeLength = 5f;
    public float spring = 100f;
    public float damper = 10f;

    void Start()
    {
        ConnectWithSpring(playerA, targetObject);
        ConnectWithSpring(playerB, targetObject);
    }

    void ConnectWithSpring(Rigidbody from, Rigidbody to)
    {
        SpringJoint joint = from.gameObject.AddComponent<SpringJoint>();
        joint.connectedBody = to;
        joint.autoConfigureConnectedAnchor = false;
        joint.anchor = Vector3.zero;
        joint.connectedAnchor = Vector3.zero;
        joint.minDistance = 0;
        joint.maxDistance = ropeLength;
        joint.spring = spring;
        joint.damper = damper;
        joint.enableCollision = false;
    }
}

