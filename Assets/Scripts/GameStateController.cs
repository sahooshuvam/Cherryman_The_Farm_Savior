using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameStateController : MonoBehaviour
{
    public Animator anim;
    public GameObject target;
    NavMeshAgent agent;
    public float walkingSpeed;
    public float runningSpeed;
    public enum STATE { IDLE, WONDER, CHASE, ATTACK, DEAD };
    public STATE state = STATE.IDLE;//default state
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
        target = GameObject.Find("Cherryman");
    }

    // Update is called once per frame
    void Update()
    {

        if (target == null && GameStart.isGameOver == false)
        {
            target = GameObject.Find("Cherryman");
            return;
        }
        switch (state)
        {
            case STATE.IDLE:
                if (CanSeePlayer())
                    state = STATE.CHASE;
                else if (Random.Range(0, 1000) < 5)
                {
                    state = STATE.WONDER;
                }
                break;
            case STATE.WONDER:
                if (!agent.hasPath)
                {
                    float randValueX = transform.position.x + Random.Range(-5f, 5f);
                    float randValueZ = transform.position.z + Random.Range(-5f, 5f);
                   // float ValueY = Terrain.activeTerrain.SampleHeight(new Vector3(randValueX, 0f, randValueZ));
                    Vector3 destination = new Vector3(randValueX, transform.position.y, randValueZ);
                    agent.SetDestination(destination);
                    agent.stoppingDistance = 0f;
                    agent.speed = walkingSpeed;
                    TurnOffAllTriggerAnim();
                    anim.SetBool("IsWalking", true);
                }
                if (CanSeePlayer())
                {
                    state = STATE.CHASE;
                }
                else if (Random.Range(0, 1000) < 7)
                {
                    state = STATE.IDLE;
                    TurnOffAllTriggerAnim();
                    agent.ResetPath();
                }
                break;

            case STATE.CHASE:
                if (GameStart.isGameOver)
                {
                    TurnOffAllTriggerAnim();
                    state = STATE.WONDER;
                    return;
                }
                agent.SetDestination(target.transform.position);
                agent.stoppingDistance = 2f;
                TurnOffAllTriggerAnim();
                anim.SetBool("IsWalking", true);
                agent.speed = runningSpeed;
                if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
                {
                    state = STATE.ATTACK;
                }
                if (CannotSeePlayer())
                {
                    state = STATE.WONDER;
                    agent.ResetPath();
                }
                break;

            case STATE.ATTACK:
                if (GameStart.isGameOver)
                {
                    TurnOffAllTriggerAnim();
                    state = STATE.WONDER;
                    return;
                }
                TurnOffAllTriggerAnim();
                anim.SetBool("IsAttacking", true);
                transform.LookAt(target.transform.position);//Zombies should look at Player
                if (DistanceToPlayer() > agent.stoppingDistance)
                {
                    state = STATE.CHASE;
                }
               break;

            case STATE.DEAD:
                Destroy(agent);
                break;
        }
    }
    public void TurnOffAllTriggerAnim()//All animation are off
    {
        anim.SetBool("IsWalking", false);
        anim.SetBool("IsAttacking", false);
        anim.SetBool("IsDieing", false);
    }

    public bool CanSeePlayer()
    {
        if (DistanceToPlayer() < 10f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private float DistanceToPlayer()
    {
        return Vector3.Distance(target.transform.position, this.transform.position);
    }

    public bool CannotSeePlayer()
    {
        if (DistanceToPlayer() > 10f)
        {
            return true;
        }
        else
            return false;
    }

    public void KillZombie()
    {
        TurnOffAllTriggerAnim();
        anim.SetBool("isDead", true);
        state = STATE.DEAD;
    }


    //public void DamagePlayer()
    //{
    //    int damageAmount = 5;
    //    if (target != null)
    //    {
    //        //target.GetComponent<PlayerMovement>().TakeHit(damageAmount);//create a method Random sound when player takes damage
    //    }
    //}
}

public class GameStart
{
    public static bool isGameOver = false;
}
