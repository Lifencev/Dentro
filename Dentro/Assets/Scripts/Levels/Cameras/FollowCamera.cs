using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform objToFollow;
    private Vector3 deltaPos;

    void Start()
    {
        objToFollow = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = objToFollow.position + new Vector3(0, 0, -10);
        deltaPos = transform.position - objToFollow.position;
    }

    void Update()
    {
        if (objToFollow != null)
            transform.position = objToFollow.position + deltaPos;
    }
}
