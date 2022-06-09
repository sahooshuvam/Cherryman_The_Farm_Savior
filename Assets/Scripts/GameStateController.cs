using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/* This Script is for Spider State machine and Spider Movement*/
public class GameStateController : MonoBehaviour
{
    public Animator anim;
    public GameObject target;
    NavMeshAgent agent;
    public float walkingSpeed;
    public float runningSpeed;
    public GameObject effect;
    public enum STATE { IDLE, WONDER, CHASE, ATTACK, DEAD }; //Taking state of the Spider
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
            target = GameObject.Find("Cherryman"); //Taking ref of the Cherryman
            return;
        }
        switch (state)
        {
            case STATE.IDLE:
                if (CanSeePlayer()) // If the Spider see the cherryman then its goes to the cherry state
                    state = STATE.CHASE;
                else if (Random.Range(0, 1000) < 5) // There will be chance for the spider to go wonder state
                {
                    state = STATE.WONDER;
                }
                break;
            case STATE.WONDER:
                if (!agent.hasPath) // if spider does not get a path then find a path
                {
                    float randValueX = transform.position.x + Random.Range(-5f, 5f);
                    float randValueZ = transform.position.z + Random.Range(-5f, 5f);
                    Vector3 destination = new Vector3(randValueX, transform.position.y, randValueZ);
                    agent.SetDestination(destination);
                    agent.stoppingDistance = 0f;
                    agent.speed = walkingSpeed;
                    TurnOffAllTriggerAnim();
                    anim.SetBool("IsWalking", true);
                }
                if (CanSeePlayer())// if spider see the cherryman thn goes to chase state 
                {
                    state = STATE.CHASE;
                }
                else if (Random.Range(0, 1000) < 7) // There is a chance for the spider to go Idle state
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
                agent.SetDestination(target.transform.position);// spider taking the Cherryman position
                agent.stoppingDistance = 2f;
                TurnOffAllTriggerAnim();// Turning off all the animation
                anim.SetBool("IsWalking", true);// Now Spider Walk animation are activated.
                agent.speed = runningSpeed;
                if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending) // If  the Spider is nearer to cherryman  come to the attack state
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
                anim.SetBool("IsAttacking", true);// Spider attack animation is Activated.
                transform.LookAt(target.transform.position);//Spider should look at  The Player
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

    public bool CanSeePlayer() //with a minimum Range spider is see the  Cherryman
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

    private float DistanceToPlayer() // Checking the distace between spider and Cherryman Position
    {
        return Vector3.Distance(target.transform.position, this.transform.position);
    }

    public bool CannotSeePlayer() // In which range  Spider can't see the cherryman
    {
        if (DistanceToPlayer() > 10f)
        {
            return true;
        }
        else
            return false;
    }

    public void KillSpider() // if cherryman being able to kill the spider 
    {
        TurnOffAllTriggerAnim();
        anim.SetBool("IsDieing", true); //Spider Die animation is activated.
        state = STATE.DEAD;// its goes to the dead state
        Destroy(this.gameObject,4f);// After 4 sec of the die animation Spider will destroy
        GameObject temp = Instantiate(effect,this.transform.position,Quaternion.identity); // When Cherry will hit the Spider Particle effect Instantiate
        //this.transform.position = temp.transform.position;
        Destroy(temp, 2f);// After 2 Sec Particle effect will Destroy
    }


    public void DamagePlayer()
    {
        if (target != null)
        {
            target.GetComponent<PlayerMovement>().TakeHit();//create a method Random sound when player takes damage
        }
    }
}

public class GameStart
{
    public static bool isGameOver = false;
}
