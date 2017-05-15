using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    //nextStage 
    static int nextStageLevel;
    public int stage;

    //Interaction Object 
    //public GameObject[] Objects = new GameObject[5];
    //static GameObject[] ObjectName = new GameObject[5];


	// Use this for initialization
	void Start () {
        if (nextStageLevel == 1)
            nextStageLevel = 0;
        else
            nextStageLevel = 1;
        stage = nextStageLevel;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(nextStageLevel, LoadSceneMode.Single);
        }
        stage = nextStageLevel;
	}
    
}
