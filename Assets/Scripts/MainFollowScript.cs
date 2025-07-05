using UnityEngine;

public class MainFollowScript : MonoBehaviour
{
    public Transform Player1;
    public Transform Player2;
    public GameObject PlayerHead1;
    public GameObject PlayerHead2;
    public float MaxDistance = 1f;
    public GameObject Hearts;
    public float distance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Hearts.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = (Player1.transform.position+ Player2.transform.position)/2;

        Hearts.SetActive(AreLookingAtEachOther(PlayerHead1, PlayerHead2, MaxDistance));
    }

    bool AreLookingAtEachOther(GameObject a, GameObject b,float maxDistance, float angleThreshold = 10f)
    {
        Vector3 dirAtoB = (b.transform.position - a.transform.position);
        Vector3 dirBtoA = (a.transform.position - b.transform.position);

        distance = dirAtoB.magnitude;
        if (distance > maxDistance)
            return false;

        dirAtoB.Normalize();
        dirBtoA.Normalize();

        float dotA = Vector3.Dot(a.transform.forward, dirAtoB);
        float dotB = Vector3.Dot(b.transform.forward, dirBtoA);

        float angleA = Mathf.Acos(dotA) * Mathf.Rad2Deg;
        float angleB = Mathf.Acos(dotB) * Mathf.Rad2Deg;

        return angleA < angleThreshold && angleB < angleThreshold;
    }
}
