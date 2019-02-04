using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resolution : MonoBehaviour {
    public int width = 1280;
    public int height = 720;

    void Start() {
        Screen.SetResolution(width, height, false);
    }
}
