using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{

    [Header("Prefeb and component")]
    [SerializeField] GameObject ParentOb;
    [SerializeField] GameObject moveping;
    [SerializeField] Slider hpSlider;
    [SerializeField] SkillCoolTime skill;
    private Animator movePingAnim;
    private Rigidbody rb;
    private Animator anim;
    private NavMeshAgent nav;

    //Basic AttackSpeed
    [Header("Info")]
    [SerializeField] private float attackSpeed;
    [SerializeField] float PlayerMaxHP;
    public float PlayerCurHP;

    //Current curser position
    [Header("Other")]
    public Vector3 curPointerPos;

    //Skill
    public bool isdash;
    public bool dashError;

    //skill cooldown check
    public int comboCount;
    public bool basicAttack1;
    public bool basicAttack2 = false;
    public bool canDash;
    public bool canBasicAttack;

    //MoveLimit
    public bool canMove;

    //Component
    

    void Start()
    {
        PlayerCurHP = PlayerMaxHP;
        hpSlider.maxValue = PlayerMaxHP;
        canBasicAttack = true;
        basicAttack1 = true;
        canMove = true;
        canDash = true;
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        anim.SetFloat("AttackSpeed", attackSpeed);
    }

    void Update()
    {
        hpSlider.value = PlayerCurHP;

        ParentOb.transform.position = transform.position;

        anim.SetFloat("AttackSpeed", attackSpeed);

        if (nav.velocity.magnitude <= 1)
        {
            anim.SetBool("ismove", false);
        }

        //Dash
        if (canDash)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                skill.Trigger_Skill();
                basicAttack1 = false;
                basicAttack2 = false;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                canMove = false;

                RaycastHit hit;

                isdash = true;
                if (Physics.Raycast(ray, out hit, 100))
                {

                    curPointerPos = new Vector3(hit.point.x, 0, hit.point.z);
                    nav.SetDestination(transform.position);
                    transform.LookAt(curPointerPos);
                    anim.CrossFade("DashForward", 0f);
                    Vector3 DashDir = transform.localRotation * Vector3.forward;
                    rb.velocity = DashDir * 30;
                    Invoke("StopDash", 0.3f);

                }
            }
        }
        if (isdash)
        {
            nav.SetDestination(transform.position);
            transform.LookAt(curPointerPos);
        }

        //Moveping Control
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                GameObject movepinginstance = Instantiate(moveping);
                movepinginstance.transform.parent = transform.parent;
                movePingAnim = movepinginstance.GetComponent<Animator>();
                movePingAnim.SetBool("isClick", true);
                movepinginstance.transform.position = new Vector3(hit.point.x, moveping.transform.position.y, hit.point.z);
                StartCoroutine(MovePing(movepinginstance));
            }
        }

        //Player Movement
        if (Input.GetMouseButton(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (canMove == true)
                {
                    nav.SetDestination(new Vector3(hit.point.x, 1.8f, hit.point.z));
                    anim.SetBool("ismove", true);
                }
            }
        }

        //BasicAttack
        if (canBasicAttack)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;

                //MoveLimit
                canMove = false;
                canDash = false;

                if (Physics.Raycast(ray, out hit, 100))
                {
                    curPointerPos = new Vector3(hit.point.x, 0, hit.point.z);
                    nav.SetDestination(transform.position);
                    transform.LookAt(curPointerPos);
                    if (basicAttack1)
                    {
                        comboCount = 1;
                        anim.CrossFade("Attack2", 0f);
                        basicAttack1 = false;
                        basicAttack2 = true;
                    }
                    else if (basicAttack2)
                    {
                        comboCount = 2;

                    }
                }
            }
            //if (dashError)
            //{
            //    Invoke("ErrorFix", (0.867f / attackSpeed) * 0.45f);
            //    dashError = false;
            //}
        }
        
    }
    
    //ErrorFix
    private void ErrorFix()
    {
        if (dashError == true)
        {
            Debug.Log("Error");
            dashError = false;
            basicAttack1 = true;
            basicAttack2 = false;
            canMove = true;
        }
    }

    //StopDash
    private void StopDash()
    {
        rb.velocity = Vector3.zero;
        isdash = false;
        canMove = true;
        basicAttack1 = true;
        basicAttack2 = false;
    }


    //MovePing Instance
    IEnumerator MovePing(GameObject movepinginstance)
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(movepinginstance);
    }
}
