using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public AudioClip SoundGun; //폭발소리 변수를 지정합니다.
	public AudioClip SoundExplosion; //테스트 소리 입니다.
    static AudioSource myAudio;

    public static SoundManager instance; // 

    private void Awake()
    {
        if (SoundManager.instance == null)
            SoundManager.instance = this;
    }

    void Start () {
        myAudio = GetComponent<AudioSource>();
	}

	public void PlaySound()
    {
		myAudio.PlayOneShot(SoundGun); //사운드 재생 함수를 만듭니다. 
	}

	public void PlaySound2()
	{
		myAudio.PlayOneShot(SoundExplosion); //사운드 재생 함수를 만듭니다. 
	}

   

	void Update () {
		
	}
}
