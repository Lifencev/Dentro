using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazinessBossIdle : StateMachineBehaviour
{
    [SerializeField] private int attackRange = 7;
    [SerializeField] private GameObject player;
    float timer;
    bool isReadyToShoot = true;
    [SerializeField] private Stats currentStats;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        currentStats = animator.GetComponent<LazinessBoss>().currentStats;
        player = GameObject.FindGameObjectWithTag("Player");
        timer = 2;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector3.Distance(player.transform.position, animator.GetComponent<Transform>().position) < attackRange && isReadyToShoot)
        {
            isReadyToShoot = false;
            timer = 2;
            animator.SetBool("Attack", true);
        }

        timer -= Time.deltaTime;
        if (timer < 0)
            isReadyToShoot = true;

        if (currentStats.health <= 0.5f * currentStats.maxhealth)
        {
            Debug.Log("set attack 2");
            animator.SetBool("SecondAttack", true);
        }


    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
