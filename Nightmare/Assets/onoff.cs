using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onoff : MonoBehaviour {
    public GameObject DynamicLight;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetKey(KeyCode.A))
        {
            if(DynamicLight.GetComponent<DynamicLight2D.DynamicLight>().enabled == false)
            {
                DynamicLight.GetComponent<DynamicLight2D.DynamicLight>().enabled = true;
                DynamicLight.SetActive(true);
            }
            else if(DynamicLight.GetComponent<DynamicLight2D.DynamicLight>().enabled == true)
            {
                DynamicLight.GetComponent<DynamicLight2D.DynamicLight>().enabled = false;
                DynamicLight.SetActive(false);
            }
           
        }

    }
}
