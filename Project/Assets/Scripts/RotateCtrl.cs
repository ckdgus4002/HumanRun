using UnityEngine;

public class RotateCtrl : MonoBehaviour {
    private void OnTriggerEnter(Collider coll) {
        if (coll.gameObject.tag == "Player") coll.transform.rotation = Quaternion.Euler(0.0f, transform.eulerAngles.y, 0.0f);
    }
}