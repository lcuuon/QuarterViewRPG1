using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    
    
    [SerializeField] GameObject ParentOb;
    [SerializeField] GameObject moveping;


    //Basic AttackSpeed
    [SerializeField] private float attackSpeed;

    //skill cooldown check
    public int comboCount;
    public bool basicAttack1;
    public bool basicAttack2 = false;

    //MoveLimit
    public bool canMove;

    //Component
    private Animator movePingAnim;
    private Rigidbody rb;
    private Animator anim;
    private NavMeshAgent nav;

    void Start()
    {
        basicAttack1 = true;
        canMove = true;
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        anim.SetFloat("AttackSpeed", attackSpeed);
    }

    void Update()
    {
        ParentOb.transform.position = transform.position;

        if (nav.velocity.magnitude <= 1)
        {
            
            anim.SetBool("ismove", false);
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
        if (Input.GetMouseButtonDown(0))
        {          
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            canMove = false;

            if (Physics.Raycast(ray, out hit, 100))
            {
                nav.SetDestination(transform.position);
                transform.LookAt(new Vector3(hit.point.x, 0, hit.point.z));
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
    }







    //MovePing Instance
    IEnumerator MovePing(GameObject movepinginstance)
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(movepinginstance);
    }
}
