using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
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
    [SerializeField] GameObject attack;
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


    void Start()
    {
        levelManager = GameObject.Find("PlayerCurState").GetComponent<PlayerLevelManager>();
        isDamaged = true;
        attack.SetActive(false);
        canMove = true;
        curHP = MaxHP;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();
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
                    transform.LookAt(player.transform.position);
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
    private void AttackStart()
    {
        attack.SetActive(true);
    }
    private void AttackEnd()
    {
        attack.SetActive(false);
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
        var ExpText = DamagedTextPool.GetExp();
        var ExpTextValue = ExpText.GetComponent<TMP_Text>();
        var ExpTextCs = ExpText.GetComponent<GetExpText>();
        ExpTextCs.Death(this.transform.position);
        ExpTextValue.text = ($"<b>+{Exp}</b> °æÇèÄ¡");
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
    public void DamageText(float Value, bool isCritical)
    {
        var DamagedText = DamagedTextPool.GetDamaged();
        var DamagedTextValue = DamagedText.GetComponent<TMP_Text>();
        var DamagedTextCs = DamagedText.GetComponent<DamageTxt>();
        DamagedTextCs.Damaged(this.transform.position);

        if (isCritical)
        {
            DamagedTextValue.color = Color.red;
            DamagedTextValue.fontSize = 57;
            DamagedTextValue.text = ($"<b>-{Value}</b>");
        }
        else
        {
            DamagedTextValue.fontSize = 47;
            DamagedTextValue.color = Color.white;
            DamagedTextValue.text = ($"-{Value}");
        }


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
