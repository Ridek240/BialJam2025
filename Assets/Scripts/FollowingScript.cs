using System.Collections.Generic;
using UnityEngine;

public class FollowingScript : MonoBehaviour
{
    public bool FollowRotation;
    public List<TransformPair> follow;


    private void FixedUpdate()
    {
        foreach (var t in follow)
        {
            t.first.position = t.second.position;
        }
    }
}

[System.Serializable]
public class TransformPair
{
    public Transform first;
    public Transform second;
}