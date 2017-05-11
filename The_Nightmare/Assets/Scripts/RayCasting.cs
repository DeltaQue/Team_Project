using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;                  //using JSON 
using UnityEngine.UI;           //Dialogue Text
using System;

public class RayCasting : MonoBehaviour {
    //using raycasting
    //public RaycastHit2D _hit;
    //public Vector2 direction;

    //using JSON 
    private string jsonString;
    private JsonData TextCommucation;

    //Dialogue Text
    public Text TheText;
    public GameObject TextBox;
    private int count;


    // Use this for initialization
    void Start () {
        jsonString = File.ReadAllText(Application.dataPath + "/Resources/ObjectInteraction.json");
        TextCommucation = JsonMapper.ToObject(jsonString);

        TextBox.SetActive(false);

        //count = 0;
        Debug.Log(TextCommucation["Obejct"][0]["id"]);
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
