using UnityEngine;
using UnityEngine.SceneManagement;

public class BackScene : MonoBehaviour {
    private void Update() {
        if(Input.GetButtonDown("Cancel")) SceneManager.LoadScene(0);
    }
}
