using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCenter : MonoBehaviour
{
    public GameObject bossCenter;
	private int sign = 1;
    
    void Update()
    {
        bossCenter.transform.Rotate(0, 0, 70 * Time.deltaTime * (sign));
    }
	
	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Block")
			sign *= -1;
	}
	
}
