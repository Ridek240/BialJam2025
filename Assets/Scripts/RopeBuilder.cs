using UnityEngine;
using System.Collections.Generic;

public class RopeBuilder : MonoBehaviour
{
    public GameObject segmentPrefab;
    public Transform playerA;
    public Transform playerB;
    public int segmentCount = 10;

    private List<Transform> segments = new List<Transform>();
    private LineRenderer lineRenderer;

    void Start()
    {
        GenerateRope();
    }

    void GenerateRope()
    {
        Vector3 startPoint = playerA.position;
        Vector3 endPoint = playerB.position;

        Vector3 direction = (endPoint - startPoint).normalized;
        float totalLength = Vector3.Distance(startPoint, endPoint);
        float segmentLength = totalLength / (segmentCount + 1); // +2 koñce, +1 segmentów

        Transform prev = null;

        for (int i = 0; i < segmentCount; i++)
        {
            Vector3 pos = startPoint + direction * segmentLength * (i + 1);
            GameObject seg = Instantiate(segmentPrefab, pos, Quaternion.identity, transform);
            Rigidbody rb = seg.GetComponent<Rigidbody>();
            rb.mass = 0.2f;

            if (i == 0)
            {
                // Po³¹cz z playerA
                SpringJoint sj = seg.AddComponent<SpringJoint>();
                sj.connectedBody = playerA.GetComponent<Rigidbody>();
                sj.autoConfigureConnectedAnchor = false;
                sj.anchor = Vector3.zero;
                sj.connectedAnchor = Vector3.zero;
                sj.minDistance = segmentLength;
                sj.maxDistance = segmentLength;
                sj.spring = 100f;
                sj.damper = 50f;
            }
            else
            {
                // Po³¹cz z poprzednim segmentem
                SpringJoint sj = seg.AddComponent<SpringJoint>();
                sj.connectedBody = prev.GetComponent<Rigidbody>();
                sj.autoConfigureConnectedAnchor = false;
                sj.anchor = Vector3.zero;
                sj.connectedAnchor = Vector3.zero;
                sj.minDistance = segmentLength;
                sj.maxDistance = segmentLength;
                sj.spring = 100f;
                sj.damper = 50f;
            }

            prev = seg.transform;
            segments.Add(seg.transform);
        }

        // Po³¹cz ostatni segment z playerB
        SpringJoint endJoint = prev.gameObject.AddComponent<SpringJoint>();
        endJoint.connectedBody = playerB.GetComponent<Rigidbody>();
        endJoint.autoConfigureConnectedAnchor = false;
        endJoint.anchor = Vector3.zero;
        endJoint.connectedAnchor = Vector3.zero;
        endJoint.minDistance = segmentLength;
        endJoint.maxDistance = segmentLength;
        endJoint.spring = 100f;
        endJoint.damper = 50f;


        /*
        // LineRenderer (uwzglêdnia równie¿ koñce)
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = segmentCount + 2;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.material.color = Color.black;
        */
    }

    void Update()
    {
        for (int i = 0; i < segments.Count; i++)
        {
            lineRenderer.SetPosition(i, segments[i].position);
        }
    }
}
