using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PortalSpawner : MonoBehaviour
{
    [SerializeField] private GameObject portal;

    public void SpawnPortal(Vector3 position)
    {
        Instantiate(portal, position, Quaternion.identity);
    }
}
