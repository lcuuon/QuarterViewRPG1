using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class EnemyArcher : MonoBehaviour
{
    //Move
    public bool isAggro;
    public bool isAttack;
    private bool canMove;


    //Component
    public GameObject player;
    private NavMeshAgent nav;
    private Animator anim;
    private CapsuleCollider collider;
    Rigidbody rb;
    [SerializeField] GameObject arrow;
    PlayerLevelManager levelManager;

    //HP Bar
    [SerializeField] Canvas hpBarCanvas;
    [SerializeField] GameObject hpBarPrefeb;
    [SerializeField] Slider hpBarSlider;
    private bool setHpBar;

    //Info
    public float MaxHP;
    public float curHP;
    private bool isDeath;
    private bool isDamaged;
    public float Exp;

    //CC
    private bool isknockback;

    private float des;
    private float rotxPlayer;
    private float rotyPlayer;
    private float rotxTarget;
    private float rotyTarget;
    private Vector3 arrowSet;


    void Start()
    {
        levelManager = GameObject.Find("PlayerCurState").GetComponent<PlayerLevelManager>();
        player = GameObject.Find("Character");
        isDamaged = true;
        canMove = true;
        curHP = MaxHP;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();
        float a = Mathf.Acos(0 / 10) * (180 / Mathf.PI);
        Debug.Log(a);
        float x = Mathf.Cos((a - 90) * (Mathf.PI / 180)) * 10;
        Debug.Log(x);
    }

    void Update()
    {
        if (!isDeath)
        {
            if (curHP <= 0)
            {
                Death();
            }

            if (setHpBar)
            {
                hpBarSlider.value = curHP;
            }

            if (canMove)
            {
                if (isAttack)
                {
                    des = Vector3.Distance(transform.position, player.transform.position);
                    rotxPlayer = Mathf.Acos((player.transform.position.x - transform.position.x) / des) * (180 / Mathf.PI);
                    rotyPlayer = Mathf.Asin((player.transform.position.z - transform.position.z) / des) * (180 / Mathf.PI);
                    if (player.transform.position.z < transform.position.z)
                    {
                        if (player.transform.position.x < transform.position.x)
                        {
                            rotxTarget = (Mathf.Cos((-rotxPlayer - 90) * (Mathf.PI / 180)) * des) + transform.position.x;
                            rotyTarget = (Mathf.Sin((-rotxPlayer - 90) * (Mathf.PI / 180)) * des) + transform.position.z;
                        }
                        else
                        {
                            rotxTarget = (Mathf.Cos((rotyPlayer - 90) * (Mathf.PI / 180)) * des) + transform.position.x;
                            rotyTarget = (Mathf.Sin((rotyPlayer - 90) * (Mathf.PI / 180)) * des) + transform.position.z;
                        }
                    }
                    else
                    {
                        rotxTarget = (Mathf.Cos((rotxPlayer - 90) * (Mathf.PI / 180)) * des) + transform.position.x;
                        rotyTarget = (Mathf.Sin((rotxPlayer - 90) * (Mathf.PI / 180)) * des) + transform.position.z;
                    }
                    transform.LookAt(new Vector3(rotxTarget, transform.position.y, rotyTarget));
                    anim.SetBool("isAttack", true);
                    anim.SetBool("isRun", false);
                    nav.SetDestination(transform.position);
                }
                else if (isAggro && !isAttack)
                {
                    anim.SetBool("isRun", true);
                    anim.SetBool("isAttack", false);
                    nav.SetDestination(player.transform.position);
                }
                if (!isAggro && !isAttack)
                {
                    anim.SetBool("isAttack", false);
                    anim.SetBool("isRun", false);
                    nav.SetDestination(transform.position);

                }
            }


            if (isknockback)
            {
                nav.SetDestination(transform.position);
            }
        }
    }

    //Attack    
    private void Shoot()
    {
        var arrow = ArrowPooling.GetObject();
        arrow.transform.position = transform.position + new Vector3(0, 3f, 0);
        arrow.Shoot(player.transform.position, arrowSet);       
    }
    private void Set()
    {
        arrowSet = transform.localRotation.eulerAngles;
    }

    //Death
    private void Death()
    {
        isDeath = true;
        canMove = false;
        nav.enabled = false;
        collider.enabled = false;
        isknockback = false;
        isAggro = false;
        isAttack = false;
        anim.SetBool("isAttack", false);
        anim.SetBool("isRun", false);
        anim.CrossFade("Death", 0f);
        Invoke("Destroy", 15f);
        levelManager.CurExp += Exp;
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }

    private void Deathoffset1()
    {
        transform.position = new Vector3(transform.position.x, -1.57f, transform.position.z);

    }

    private void Deathoffset2()
    {
        transform.position = new Vector3(transform.position.x, -2.57f, transform.position.z);
    }

    //Set HPbar
    private void SetHPBar()
    {
        GameObject hpbar = Instantiate(hpBarPrefeb, hpBarCanvas.transform);
        hpBarSlider = hpbar.GetComponent<Slider>();
        hpBarSlider.maxValue = MaxHP;
        
        var _hpbar = hpbar.GetComponent<EnemyHPbar>();
        _hpbar.enemyPos = this.transform;
    }


    //Knockback
    public void Knockback(Vector3 knockbackDir)
    {
        canMove = false;
        //Debug.Log("Damage");
        isknockback = true;
        rb.velocity = knockbackDir;
        Invoke("KnockbackEnd", 0.2f);
        anim.SetBool("canMove", false);
        anim.CrossFade("Fall", 0f);
    }
    private void KnockbackEnd()
    {
        canMove = true;
        isknockback = false;
        rb.velocity = Vector3.zero;
        anim.SetBool("canMove", true);
    }

    //Collision
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Attack")
        {
            if (isDamaged)
            {
                isDamaged = false;
                SetHPBar();
                setHpBar = true;
            }   
        }
    }

}
