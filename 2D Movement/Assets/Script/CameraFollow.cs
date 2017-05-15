using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform Target;//스크립트 인스펙터 창에서 타겟 - 플레이어 설정 꼭 해주세요~ 
   
    Camera mycam;

    void Start () {

        mycam = GetComponent<Camera>();	
	}

    void Update() {
        mycam.orthographicSize = (Screen.height / 100f) / 4f;
        //카메라가 보여주는 화면사이즈 / 비율을 조정합니다. 현재는 높이 고정입니다. (어떤 플랫폼이든 같은 높이!)  
        if (Target) 
        {
            transform.position = Vector3.Lerp(transform.position, Target.position, 0.1f) + new Vector3(0,0,-10);
            //카메라 위치가 플레이어의 z값까지 따라가버려 플레이어가 보이지않아 z값을 낮춰줬습니다 
        } 
    }
}
