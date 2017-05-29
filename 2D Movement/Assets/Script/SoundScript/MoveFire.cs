using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFire: MonoBehaviour {
    public GameObject Bullets;
    public Transform BulletSpawn;
	// Use this for initialization
	void Start () {
		
	}
	/*
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Instantiate(Bullets, BulletSpawn.position, Quaternion.identity);
            print(BulletSpawn.position);
            SoundManager.instance.PlaySound(); //사운드매니저의 PlaySound 를 불러옵니다.
        }
	}
	*/
}
