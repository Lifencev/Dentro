using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleShooting : MonoBehaviour
{
    Gun[] guns;

    private void Start()
    {
        guns = GetComponentsInChildren<Gun>();
    }

    //bool shoot = false;
    //private void Update()
    //{
    //    shoot = Input.GetKeyDown(KeyCode.LeftControl);
    //    if (shoot)
    //    {
    //        shoot = false;
    //        foreach (Gun gun in guns)
    //        {
    //            gun.MultiShoot(gameObject.tag);
    //        }
    //    }
    //}

    public void MultiShoot()
    {
        foreach (Gun gun in guns)
        {
            gun.Shoot(gameObject.tag);
        }
    }
}
 