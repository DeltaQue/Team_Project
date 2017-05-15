using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;          //Scene Move Header

public class PlayerMovement : MonoBehaviour
{
    //Character Movement
    Animator anim;
    static Vector3 Hero;
    static Vector3 TeddyBear;

    //nextStage 
    static int nextSceneNum;

    //using raycasting
    public RaycastHit2D _hit;
    public Vector2 direction;

    // Use this for initialization
    void Start()
    {
        //nextScene Check
        if (nextSceneNum == 1)
        {
            nextSceneNum = 0;
            gameObject.transform.position = Hero;
        }
        else
        {
            nextSceneNum = 1;
            gameObject.transform.position = TeddyBear;
        }
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        //Level Character movemnet
        CharacterMovement();
        rayCasting();

        //next level move
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene(nextSceneNum, LoadSceneMode.Single);
        }
    }

    //player move
    void CharacterMovement()
    {
        float input_x = Input.GetAxisRaw("Horizontal");
        float input_y = Input.GetAxisRaw("Vertical");

        if (input_x > 0)
        {
            direction = Vector2.right;
        }
        else if (input_x < 0)
        {
            direction = Vector2.left;
        }
        else if (input_y > 0)
        {
            direction = Vector2.up;
        }
        else if (input_y < 0)
        {
            direction = Vector2.down;
        }


        bool isWalking = (Mathf.Abs(input_x) + Mathf.Abs(input_y)) > 0;

        anim.SetBool("isWalking", isWalking);
        if (isWalking)
        {
            anim.SetFloat("x", input_x);
            anim.SetFloat("y", input_y);

            transform.position += new Vector3(input_x, input_y, 0).normalized * Time.deltaTime;
        }

        //nextStageLevel + 1(다음 스테이지 
        if (nextSceneNum == 0)
            Hero = transform.position;
        else
            TeddyBear = transform.position;
    }

    //Raycasting 
    void rayCasting()
    {
        _hit = Physics2D.Raycast(transform.position, direction, 0.2f, 1 << LayerMask.NameToLayer("Object"));
        //Layer가 Object인 오브젝트만 캐스팅
        if (_hit.collider != null)
        {
            Debug.Log(_hit.collider.name);
            //RayCasting에 걸리는 오브젝트의 Layer가 Object이고, 태그가 KeyObject일 경우
            if(_hit.collider.tag == "KeyObject")
            {

            }

            //RayCasting에 걸리는 오브젝트의 Layer가 Object이고, 이름이 Door일 경우
            if (_hit.collider.name == "Door")
            {

            }
        }

    }
}