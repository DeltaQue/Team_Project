using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {
    private float movePower = 3f;
    private float jumpPower = 5f;

    Rigidbody2D rigid;

    Vector3 movement;
    bool isJumping = false;

    //[Override Funtion]
	// Use this for initialization
	void Start () {
        rigid = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }
	}

    //physics engine Update
    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    //[movement Funtion]
    private void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        if (Input.GetAxis("Horizontal") < 0)
        {
            moveVelocity = Vector3.left;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            moveVelocity = Vector3.right;
        }

        transform.position += moveVelocity * movePower * Time.deltaTime;
    }

    void Jump()
    {
        if (!isJumping)
            return;

        //Prevent Velocity amplificantion.
        rigid.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);

        isJumping = false;
    }
   
}
