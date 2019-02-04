using UnityEngine;

public class CameraShaking : MonoBehaviour {
    private float decreaseFactor = 1.0f;

    public Transform camTransform;
    public float time = 5.0f;
    public float shakeAmount = 0.35f;

    public Vector3 originalPos;

    void Awake() {
        if (camTransform == null) camTransform = GetComponent<Transform>();
    }

    void OnEnable() {
        originalPos = camTransform.localPosition;
    }

    void Update() {
        if (time > 0) {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            time -= Time.deltaTime * decreaseFactor;
        }
        else {
            time = 0.0f;
            camTransform.localPosition = originalPos;
        }
    }
}