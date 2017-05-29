using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "enemy")
        {
            Destroy(col.gameObject);
			//SoundManager.instance.PlaySound2();
        }
    }
}
