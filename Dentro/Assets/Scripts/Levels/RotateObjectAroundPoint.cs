using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjectAroundPoint : MonoBehaviour
{
    public float a;
    public float b;
    public float x;
    public float y;
    public float X;
    public float Y;
    public float alpha;
    public float delta;

    private void Start()
    {
        x = GameObject.FindGameObjectWithTag("LazinessBullet").transform.position.x - Mathf.Sqrt(Mathf.Abs(a*a - b*b)) + 3;
        y = GameObject.FindGameObjectWithTag("LazinessBullet").transform.position.y;

    }

    void Update()
    {
        alpha += delta;
        X = x + (a * Mathf.Cos(alpha * 0.005f));
        Y = y + (b * Mathf.Sin(alpha * 0.005f));
        transform.position = new Vector3(X, Y, 0);
    }
}
