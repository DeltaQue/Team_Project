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
    Animator[] anim = new Animator[2];
    static Vector3 Hero;
    static Vector3 TeddyBear;
    //0 : Hero,  1 : teddy
    public GameObject[] Player = new GameObject[2];
    private bool CharacterSwitch;

    //nextStage 
    static int nextSceneNum;

    //using raycasting
    public RaycastHit2D _hit;
    public Vector2 direction;

    //using JSON 
    private string jsonString;
    private JsonData TextCommucation;

    //Object Move
    public float smoothing = 1f;
    public GameObject[] gObjectArray = new GameObject[2];
    private bool[] bObject = new bool[2];
    public Transform[] ObjectDestination = new Transform[2];
    

    void AWake(){
		//Character initialize
		Hero = Player[0].transform.position;
		TeddyBear = Player[1].transform.position;

        CharacterSwitch = true;
    }
    // Use this for initialization
    void Start()
    {
        //Json Initialize
        jsonString = File.ReadAllText(Application.dataPath + "/Resources/ObjectInteraction.json");
        TextCommucation = JsonMapper.ToObject(jsonString);

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
        anim[0] = Player[0].GetComponent<Animator>();
        anim[1] = Player[1].GetComponent<Animator>();

        bObject[0] = false;
        bObject[1] = false;
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
            if (CharacterSwitch == true)
                CharacterSwitch = false;
            else
                CharacterSwitch = true;
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

        if(CharacterSwitch == true)
            anim[0].SetBool("isWalking", isWalking);
        else if(CharacterSwitch == false)
            anim[1].SetBool("isWalking", isWalking);

        if (isWalking)
        {
            if (CharacterSwitch == true)
            {
                anim[0].SetFloat("x", input_x);
                anim[0].SetFloat("y", input_y);
            }
            else if (CharacterSwitch == false)
            {
                anim[1].SetFloat("x", input_x);
                anim[1].SetFloat("y", input_y);
            }

            if (CharacterSwitch == true)
            {
                Player[0].transform.position += new Vector3(input_x, input_y, 0).normalized * Time.deltaTime;
            }
            else if(CharacterSwitch == false)
            {
                Player[1].transform.position += new Vector3(input_x, input_y, 0).normalized * Time.deltaTime;
            }
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

            for (int i = 0; i < 2; i++)
            {
                if ((string)TextCommucation["Object"][i]["id"] == _hit.collider.name)
                {
                    Debug.Log(TextCommucation["Object"][i]["interaction"]);
                    bObject[i] = true;
                }
            }
        }
    }

    private void ObjectMove()
    {
        // 0 : Door
        if (bObject[0] == true)
        {
            StartCoroutine(MyCoroutine(gObjectArray[0].transform, 0));
        }
        // 1 : Key
        if (bObject[1] == true)
        {
            StartCoroutine(MyCoroutine(gObjectArray[1].transform, 1));
        }
    }

    IEnumerator MyCoroutine(Transform target, int array)
    {

        while (Vector3.Distance(target.position, ObjectDestination[array].position) > 0.05f)
        {
            target.transform.position = Vector3.Lerp(target.position, ObjectDestination[array].position, smoothing * Time.deltaTime);

            yield return null;
        }
    }

    void Activate()
    {
        CharacterSwitch = true;
    }

    void Deactivate()
    {
        CharacterSwitch = false;
    }
}