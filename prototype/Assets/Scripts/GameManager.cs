using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Vector3 posA;
    private Vector3 posB;

    private Vector3 nextPos;

    private bool checkLever;

    public GameObject Arrow_Right;
    public GameObject Arrow_Left;
    public GameObject Arrow_Up;
    public GameObject Arrow_Down;

    public GameObject Player;

    //using raycasting
    public RaycastHit2D left_hit;
    public RaycastHit2D right_hit;
    public RaycastHit2D up_hit;
    public RaycastHit2D down_hit;
    public Vector2 direction;
    public bool isStay;
    public GameObject CastingVectex;

    [SerializeField]
    private float speed;

    [SerializeField]
    private Transform childTransform;

    [SerializeField]
    private Transform transformB;


    // Use this for initialization
    void Start()
    {
        posA = childTransform.localPosition;
        posB = transformB.localPosition;
        nextPos = posB;

        checkLever = false;

        Arrow_Right.SetActive(false);
        Arrow_Left.SetActive(false);
        Arrow_Up.SetActive(false);
        Arrow_Down.SetActive(false);

        isStay = false;
    }

    // Update is called once per frame
    void Update()
    {
        //자동이동
        //if(Vector3.Distance(childTransform.localPosition, nextPos) <= 0.1)
        //{
        //    ChangeDestination();
        //}

        Move(checkLever);
        if (Input.GetKey(KeyCode.Q))
        {
            checkLever = true;
            if (Vector3.Distance(childTransform.localPosition, nextPos) <= 0.1)
            {
                ChangeDestination();
            }
        }

        Raycasting();

    }

    private void Move(bool flag)
    {
        if (checkLever == true)
        {
            childTransform.localPosition = Vector3.MoveTowards(childTransform.localPosition, nextPos, speed * Time.deltaTime);
        }
    }

    private void ChangeDestination()
    {
        //nextPos = nextPos != posA ? posA : posB;
        if (nextPos != posA)
        {
            nextPos = posA;
        }
        else
        {
            nextPos = posB;
        }
    }

    bool Raycasting()
    {
        right_hit = Physics2D.Raycast(CastingVectex.transform.position, Vector2.right, 5f, 1 << LayerMask.NameToLayer("Room"));
        left_hit = Physics2D.Raycast(CastingVectex.transform.position, Vector2.left, 5f, 1 << LayerMask.NameToLayer("Room"));
        up_hit = Physics2D.Raycast(CastingVectex.transform.position, Vector2.up, 5f, 1 << LayerMask.NameToLayer("Room"));
        down_hit = Physics2D.Raycast(CastingVectex.transform.position, Vector2.down, 5f, 1 << LayerMask.NameToLayer("Room"));

        if (isStay)
        {
            if (right_hit.collider != null)
            {
                Arrow_Right.SetActive(true);
            }
            else
                Arrow_Right.SetActive(false);

            if (left_hit.collider != null)
            {
                Arrow_Left.SetActive(true);
            }
            else
                Arrow_Left.SetActive(false);

            if (up_hit.collider != null)
            {
                Arrow_Up.SetActive(true);
            }
            else
                Arrow_Up.SetActive(false);

            if (down_hit.collider != null)
            {
                Arrow_Down.SetActive(true);
            }
            else
                Arrow_Down.SetActive(false);
            return true;
        }
        else
            return false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject == Player)
        {
            isStay = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == Player)
        {
            isStay = false;
        }
    }
}