using UnityEngine;

public class ScoreBoard : MonoBehaviour { 
    private Transform tr;
    private Vector3 beforeTr = Vector3.zero; // 예전위치를 기록하기위한 변수
    private PlayerCtrl plyCtl;
    private int score = 0; // 플레이어 점수


    private void Start() {
        tr = GetComponent<Transform>();
        plyCtl = GetComponent<PlayerCtrl>();
        //원격 접속한 네트워크 유저일 경우 이 스크립트를 비활성화
        this.enabled = GetComponent<NetworkView>().isMine;
    }


    private int timer = 300; // 함수제어를 위한 타이머
    void Update() {
        timer--;
        if (timer == 0) {
            timer = 300;
            beforeTr.x = tr.position.x;
            beforeTr.y = tr.position.y;
            beforeTr.z = tr.position.z;
        }

        if (plyCtl.isDie == false) score++;
        else score = 0;

    }

    
    private void OnGUI() {
        if (Network.peerType != NetworkPeerType.Disconnected) {
            GUI.Label(new Rect(1060, 20, 200, 20), "*달린 거리(점수): " + score + "점");
            GUI.Label(new Rect(1060, 45, 200, 20), "*현재 위치(x,y,z): " + (int)tr.position.x + ", " + (int)tr.position.y + ", " + (int)tr.position.z);
            GUI.Label(new Rect(1060, 70, 200, 20), "*이전 위치(x,y,z): " + (int)beforeTr.x + ", " + (int)beforeTr.y + ", " + (int)beforeTr.z + "  (" + timer + ")");
        }
    }
}
