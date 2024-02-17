using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRoundMovement : MonoBehaviour
{

    [SerializeField] private Transform boss;

    private void Start()
    {

        boss = GameObject.FindGameObjectWithTag("LazinessBullet").transform;
    }
    
    int quarter = 1;
    int radius = 1;

    float delta = 0.25f;

    float x = 0;
    float y = 0;

    float timer = 2;


    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            quarter = GetQuarter();
            GetNewPosition();
            Move();
            timer = 2;
        }
    }

    float xOnCircle()
    {
        Debug.Log(Mathf.Sqrt(radius * radius + transform.position.y * transform.position.y) + " - x on circle");
        return Mathf.Sqrt(radius * radius + transform.position.y * transform.position.y);
    }

    float yOnCircle()
    {
        Debug.Log(Mathf.Sqrt(radius * radius + transform.position.x * transform.position.x) + " - y on circle");
        return Mathf.Sqrt(radius * radius + transform.position.x * transform.position.x);
    }

    float diffWithCenter(bool xAxes)
    {
        if (xAxes) Debug.Log(Mathf.Abs(transform.position.x - boss.position.x) + " - x difference");
        else Debug.Log(Mathf.Abs(transform.position.y - boss.position.y) + " - y difference");
        return xAxes ? Mathf.Abs(transform.position.x - boss.position.x) : Mathf.Abs(transform.position.y - boss.position.y);
    }

    void GetNewPosition()
    {
        Debug.Log(quarter + " - quarter");

        if (quarter == 1)
        {
            x = Random.Range(boss.position.x + (1 - delta) * diffWithCenter(true), xOnCircle());
            y = Random.Range(yOnCircle(), boss.position.y + (1 - delta) * diffWithCenter(false));
        }
        else if (quarter == 2)
        {
            Debug.Log(transform.position.x + (1 - delta) * diffWithCenter(true));
            Debug.Log(boss.position.y + (1 - delta) * diffWithCenter(false));
            x = Random.Range(xOnCircle(), transform.position.x + (1 - delta) * diffWithCenter(true));
            y = Random.Range(boss.position.y + (1 - delta) * diffWithCenter(false), yOnCircle());
        }
        else if (quarter == 3)
        {
            x = Random.Range(xOnCircle(), transform.position.x + (1 - delta) * diffWithCenter(true));
            y = Random.Range(yOnCircle(), boss.position.y - (1 - delta) * diffWithCenter(false));
        }
        else if (quarter == 4)
        {
            x = Random.Range(transform.position.x - (1 - delta) * diffWithCenter(true), xOnCircle());
            y = Random.Range(yOnCircle(), boss.position.y - (1 - delta) * diffWithCenter(false));
        }

        Debug.Log(boss.position.x + " - boss x");
        Debug.Log(boss.position.y + " - boss y");
    }

    private void Move()
    {
        Vector3 oldPosition = transform.position;
        Vector2 newPosition = new Vector3(x, y);

        Debug.Log(oldPosition + " - old position");
        Debug.Log(newPosition + " - new position");
        LeanTween.move(gameObject, newPosition, 2);
    }

    int GetQuarter()
    {
        Vector3 difference = transform.position - boss.position;
        if (difference.y > 0)
        {
            return difference.x > 0 ? 1 : 2;
        }
        else return difference.x < 0 ? 3 : 4;
    }
}

// Vector3[] path = new Vector3[] { oldPosition, new Vector3(newPosition.x, oldPosition.y, 0), new Vector3(oldPosition.x, newPosition.y, 0), newPosition };
