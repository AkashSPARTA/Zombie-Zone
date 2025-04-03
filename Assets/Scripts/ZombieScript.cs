using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ZombieScript : MonoBehaviour
{
    public enum ZombieType
    {
        shuffle,
        dizzy,
        alert
    }
    public enum ZombieState
    {
        Idle,
        Walking,
        Eating
    }

    public ZombieType zombieStyle;
    public ZombieState chooseState;
    public float yAdjustment = 0.0f;
    private Animator anim;
    private AnimatorStateInfo animInfo;
    private NavMeshAgent agent;
    public bool randomState = false;
    public float randomTiming = 5f;
    private int newState = 0;
    private int currentState;
    private GameObject[] targets;
    private float[] walkSpeed = { 0.15f, 1.0f, 0.75f };
    private float distanceToTarget;
    private int currentTarget = 0;
    private float distanceToPlayer;
    private GameObject player;
    private float zombieAlertRange = 20f;
    private bool awareOfPlayer = false;
    private bool adding = true;
    private AudioSource chaseMusicPlayer;
    private float currentChaseVolume = 0;
    private float attackDistance = 2.0f;
    private float rotateSpeed = 2.5f;
    private AudioSource zombieSound;

    private float gunAlertRange = 100;
    public bool isAngry = false;

    private float hiddenRange = 2f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        zombieSound = GetComponent<AudioSource>();
        targets = GameObject.FindGameObjectsWithTag("Target");
        player = GameObject.Find("FPSController");
        chaseMusicPlayer = GameObject.Find("ChaseMusic").GetComponent<AudioSource>();
        zombieAlertRange = Random.Range(5.1f, 35f);


        anim.SetLayerWeight(((int)zombieStyle + 1), 1);
        if(zombieStyle == ZombieType.shuffle)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + yAdjustment, transform.position.z);

        }
        anim.SetTrigger(chooseState.ToString());
        currentState = (int)chooseState;

        if(randomState == true)
        {
            InvokeRepeating("SetAnimState", randomTiming, randomTiming);
        }
        agent.destination = targets[0].transform.position;
        agent.speed = walkSpeed[(int)zombieStyle];
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetBool("isDead") == false)
        {
            distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (SaveScript.bottlePos == Vector3.zero)
            {
                isAngry = false;
            }
            if (SaveScript.bottlePos != Vector3.zero && distanceToPlayer > attackDistance && isAngry == false)
            {
                agent.destination = SaveScript.bottlePos;
                anim.SetBool("attacking", false);
                chooseState = ZombieState.Walking;
            }
            else
            {

                if (distanceToPlayer <= attackDistance)
                {
                    agent.isStopped = true;
                    anim.SetBool("attacking", true);

                    Vector3 pos = (player.transform.position - transform.position).normalized;
                    Quaternion posRotation = Quaternion.LookRotation(new Vector3(pos.x, 0, pos.z));
                    transform.rotation = Quaternion.Slerp(transform.rotation, posRotation, rotateSpeed * Time.deltaTime);
                }
                else
                {
                    anim.SetBool("attacking", false);
                    if (SaveScript.zombiesChasing.Count > 0)
                    {
                        if (chaseMusicPlayer.volume < 0.4f)
                        {
                            if (chaseMusicPlayer.isPlaying == false)
                            {
                                chaseMusicPlayer.Play();
                            }
                            chaseMusicPlayer.volume += 0.5f * Time.deltaTime;

                        }
                    }
                    if (SaveScript.zombiesChasing.Count == 0)
                    {
                        if (chaseMusicPlayer.volume > 0.0f)
                        {
                            chaseMusicPlayer.volume -= 0.5f * Time.deltaTime;
                        }
                        if (chaseMusicPlayer.volume == 0.0f)
                        {
                            chaseMusicPlayer.Stop();
                        }
                    }

                    distanceToTarget = Vector3.Distance(transform.position, targets[currentTarget].transform.position);
                    animInfo = anim.GetCurrentAnimatorStateInfo((int)zombieStyle);

                    if (distanceToPlayer < zombieAlertRange && chooseState == ZombieState.Walking)
                    {
                        agent.destination = player.transform.position;
                        awareOfPlayer = true;
                        if (adding == true)
                        {
                            if (SaveScript.zombiesChasing.Contains(this.gameObject))
                            {
                                adding = false;
                                return;
                            }
                            else
                            {
                                SaveScript.zombiesChasing.Add(this.gameObject);
                                adding = false;
                            }
                        }
                    }
                    if (distanceToPlayer > zombieAlertRange)
                    {
                        awareOfPlayer = false;
                        if (SaveScript.zombiesChasing.Contains(this.gameObject))
                        {
                            SaveScript.zombiesChasing.Remove(this.gameObject);
                            adding = false;
                        }
                    }

                    if (animInfo.IsTag("motion"))
                    {
                        if (anim.IsInTransition((int)zombieStyle))
                        {
                            agent.isStopped = true;
                        }
                    }

                    if (chooseState == ZombieState.Walking)
                    {
                        if (distanceToTarget < 1.5f)
                        {
                            if (currentTarget < targets.Length)
                            {
                                currentTarget = Random.Range(0, targets.Length);

                            }
                        }
                    }
                }
            }
        }
        else
        {
            if (SaveScript.zombiesChasing.Contains(this.gameObject))
            {
                SaveScript.zombiesChasing.Remove(this.gameObject);
                adding = false;
            }

            if (SaveScript.zombiesChasing.Count == 0)
            {
                if (chaseMusicPlayer.volume > 0.0f)
                {
                    chaseMusicPlayer.volume -= 0.5f * Time.deltaTime;
                }
                if (chaseMusicPlayer.volume == 0.0f)
                {
                    chaseMusicPlayer.Stop();
                }
            }
            CancelInvoke();
            Destroy(gameObject, 20);
        }

        if(SaveScript.gunUsed == true)
        {
            zombieAlertRange = gunAlertRange;
            StartCoroutine(ResetGunRange());
        }
        else if (SaveScript.isHidden == true)
        {
            zombieAlertRange = hiddenRange;
        }
        else
        {
            zombieAlertRange = 20;
        }
    }

    void SetAnimState()
    {
        if (awareOfPlayer == false)
        {
            newState = Random.Range(0, 3);
            if (newState != currentState)
            {
                chooseState = (ZombieState)newState;
                currentState = (int)chooseState;
                anim.SetTrigger(chooseState.ToString());
            }
        }
        zombieSound.Play();
    }

    public void WalkOn()
    {
        agent.isStopped = false;
        agent.destination = targets[currentTarget].transform.position;
    }

    public void WalkOff()
    {
        agent.isStopped = true;
    }

    IEnumerator ResetGunRange()
    {
        yield return new WaitForSeconds(10);
        SaveScript.gunUsed = false;
    }

}
