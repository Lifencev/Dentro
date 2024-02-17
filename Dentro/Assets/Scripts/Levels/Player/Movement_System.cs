using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace Level.player {

    public class Movement_System : MonoBehaviour
    {
        private Rigidbody2D rb;
        private Vector2 direction;

        private Transform gun;
        [SerializeField] private Camera cam;
        private Vector2 mousePos;
        [SerializeField] private Stats playerStats;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            gun = transform.GetChild(0).gameObject.transform;
            cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            playerStats = GetComponent<Health_System>().currentStats;
        }

        void Update()
        {
            direction.x = Input.GetAxisRaw("Horizontal");
            direction.y = Input.GetAxisRaw("Vertical");

            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        void FixedUpdate()
        {
            rb.MovePosition(rb.position + direction * playerStats.speed * Time.fixedDeltaTime);
            Vector2 lookDir = (mousePos - (Vector2) transform.position).normalized;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            gun.eulerAngles = new Vector3(0, 0, angle);
        }
    }
}