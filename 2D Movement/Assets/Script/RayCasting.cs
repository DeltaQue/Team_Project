using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;                  //using JSON 
using UnityEngine.UI;           //Dialogue Text
using System;

public class RayCasting : MonoBehaviour {
    //using raycasting
    public RaycastHit2D _hit;
    public Vector2 direction;

    //using JSON 
    private string jsonString;
    private JsonData TextCommucation;

    //Dialogue Text
    public Text TheText;
    public GameObject TextBox;


    // Use this for initialization
    void Start () {
        jsonString = File.ReadAllText(Application.dataPath + "/Resources/ObjectInteraction.json");
        TextCommucation = JsonMapper.ToObject(jsonString);

        //TextBox.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        //if (Input.GetKey(KeyCode.Z))
        //{
        //    if (Raycasting(direction)) { }
        //    else
        //        TextBox.SetActive(false);
        //}
    }
    /*
     * 캐릭터의 앞에 사물의 태그를 리턴하게 함
     */
    void rayCasting(Vector3 direction)
    {
        _hit = Physics2D.Raycast(transform.position, direction, 0.2f, 1 << LayerMask.NameToLayer("Object"));
        //Layer가 Object인 오브젝트만 캐스팅
        if(_hit.collider != null)
        {
            //Object의 이름과 Json의 id를 비교하여 출력
            for (int i = 0; i < 10; i++)
            {
                if ((string)TextCommucation["Object"][i]["id"] == _hit.collider.gameObject.name)
                {
                    TextBox.SetActive(true);
                    TheText.text = (string)TextCommucation["Object"][i]["id"];
                }
            }
        }
    }
    //bool Raycasting(Vector3 direction)
    //{
    //    _hit = Physics2D.Raycast(transform.position, direction, 0.2f, 1 << LayerMask.NameToLayer("Object"));
    //    if (_hit.collider != null)
    //    {
    //        if ((string)TextCommucation["Object"][count]["id"] == _hit.collider.gameObject.name)
    //        {
    //            TextBox.SetActive(true);
    //            TheText.text = (string)TextCommucation["Object"][count]["interaction"];
    //            //_hit.collider.gameObject.SetActive(false);
    //        }
    //        else
    //        {
    //            count++;
    //        }
    //    }
    //    return true;
    //}
}
