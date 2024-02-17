using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayAttack : MonoBehaviour
{
    private bool active = false;
    [SerializeField] private GameObject player;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform shootPoint;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        lineRenderer = GameObject.FindGameObjectWithTag("FearLine").GetComponent<LineRenderer>();
        shootPoint = transform.GetChild(0).transform;
    }

    public IEnumerator Attack(float damage)
    {
        active = true;
        lineRenderer.SetPosition(0, shootPoint.position);

        Vector3 newPos = player.transform.position + new Vector3(0, Random.Range(-2, 3));

        lineRenderer.SetPosition(1, newPos);

        RaycastHit2D hitInfo = Physics2D.Raycast(shootPoint.position, newPos);

        if (hitInfo.collider == null)
        {
            hitInfo.transform.GetComponent<Health_System>().TakeDamage(damage);
        }

        FindObjectOfType<AudioManager>().Play("fear_projectile");
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.01f);
        lineRenderer.enabled = false;

        yield return new WaitForSeconds(2);

        active = false;
    }
}
