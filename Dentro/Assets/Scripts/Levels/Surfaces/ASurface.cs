using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASurface : MonoBehaviour
{
    public void Play(string name){
        FindObjectOfType<AudioManager>().Play(name, Vector3.Distance(this.transform.position,GameObject.FindGameObjectWithTag("Player").transform.position));
    }
}
