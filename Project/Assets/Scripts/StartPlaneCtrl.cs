using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlaneCtrl : MonoBehaviour {
    private void OnTriggerEnter(Collider coll) {
        if (coll.gameObject.tag == "Player") {
            coll.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
            coll.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        }
    }
}
