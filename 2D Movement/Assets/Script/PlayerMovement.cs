using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;          //Scene Move Header

using LitJson;                  //using JSON 
using System.IO;
using UnityEngine.UI;           //Dialogue Text
using System;

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

    //using JSON 
    private string jsonString;
    private JsonData TextCommucation;

    public float smoothing = 5f;
    public GameObject[] gObjectArray = new GameObject[2];
    private bool[] bObject = new bool[2];

    public bool Control;
    // Use this for initialization
    void Start()
    {
        ////Json Initialize
        jsonString = File.ReadAllText(Application.dataPath + "/Resources/ObjectInteraction.json");
        TextCommucation = JsonMapper.ToObject(jsonString);

        //Character initialize
        Hero = gameObject.transform.position;
        TeddyBear = gameObject.transform.position;

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

        //Control = true  => 주인공
        //Control = false => 곰인형
        Control = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Level Character movemnet
        CharacterMovement();


        //next level move
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //SceneManager.LoadScene(nextSceneNum, LoadSceneMode.Single);
            if (Control == true)
                Control = false;
            else if (Control == false)
                Control = true;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            rayCasting();
            ObjectMove();
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
            //Debug.Log(_hit.collider.name);
            //RayCasting에 걸리는 오브젝트의 Layer가 Object이고, 태그가 KeyObject일 경우
            if (_hit.collider.tag == "KeyObject")
            {

            }

            //RayCasting에 걸리는 오브젝트의 Layer가 Object이고, 이름이 Door일 경우
            if (_hit.collider.name == "Door")
            {

            }

            for (int i = 0; i < 2; i++)
            {
                if ((string)TextCommucation["Object"][i]["id"] == _hit.collider.name && gObjectArray[i].name == _hit.collider.name)
                {
                    Debug.Log(TextCommucation["Object"][i]["interaction"]);
                    //gObjectArray[i].transform.position = Vector3.Lerp(gObjectArray[i].transform.position, new Vector3(gObjectArray[i].transform.position.x + 1, gObjectArray[i].transform.position.y + 1, gObjectArray[i].transform.position.z), smoothing * Time.deltaTime);
                    //StartCoroutine(MyCoroutine(gObjectArray[i].transform));
                    bObject[i] = true;
                }
            }
        }
    }

    private void ObjectMove()
    {
        if (bObject[0] == true)
        {
            gObjectArray[0].transform.position = Vector3.Lerp(gObjectArray[0].transform.position, new Vector3(gObjectArray[0].transform.position.x + 1, gObjectArray[0].transform.position.y + 1, gObjectArray[0].transform.position.z), smoothing * Time.deltaTime);
        }
        if (bObject[1] == true)
        {
            gObjectArray[1].transform.position = Vector3.Lerp(gObjectArray[1].transform.position, new Vector3(gObjectArray[1].transform.position.x + 1, gObjectArray[1].transform.position.y + 1, gObjectArray[1].transform.position.z), smoothing * Time.deltaTime);
            // StartCoroutine(MyCoroutine(gObjectArray[1].transform));
            bObject[1] = false;
        }
    }

    IEnumerator MyCoroutine(Transform target)
    {
        Transform destination = target.transform;
        destination.position += new Vector3(1, 0, 0);

        //while (Vector3.Distance(target.position, destination.position) > 0.05f)
        //{
            target.transform.position = Vector3.Lerp(target.position, destination.position , smoothing * Time.deltaTime);
        
            yield return new WaitForSeconds(5f);
        //}
    }
}