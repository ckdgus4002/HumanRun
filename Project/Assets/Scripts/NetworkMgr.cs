using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NetworkMgr : MonoBehaviour {
    //접속 IP
    private const string ip = "210.178.183.39"; // 내 방 IP
    //private const string ip = "127.0.0.1"; // 테스트용 IP
    private const int port = 30000; // 접속 Port
    private bool _useNat = false; // NAT 기능의 사용 여부
    public GameObject player; // 플레이어 프리팹
    public GameObject text1; // 휴먼런 로고
    public GameObject text2; // 서버끊김 로고

    private void OnGUI() {
        if (Time.timeScale != 0.0f) {
            if (Network.peerType == NetworkPeerType.Disconnected) {
                if (GUI.Button(new Rect(480, 380, 320, 60), "서버 켜기")) Network.InitializeServer(3, port, _useNat);
                if (GUI.Button(new Rect(480, 470, 320, 60), "서버 접속")) Network.Connect(ip, port);
                if (GUI.Button(new Rect(480, 560, 320, 60), "후원정보")) SceneManager.LoadScene(1);
                GUI.Label(new Rect(1060, 20, 200, 25), "*서버 IP: " + ip);
            }
            else {
                int users = Network.connections.Length + 1;
                GUI.Label(new Rect(20, 20, 200, 20), "*서버 활성화(접속)되었음");
                GUI.Label(new Rect(20, 45, 200, 20), "*접속자 수: " + users + "명(최대 4명)");
                GUI.Label(new Rect(20, 70, 200, 20), "*A,D,←,→: 좌우 이동");
                GUI.Label(new Rect(20, 95, 200, 20), "*SPACE: 점프");
                GUI.Label(new Rect(20, 120, 200, 20), "*ESC: 출발위치로 복귀");
            }
        }
    }


    private void OnServerInitialized() {
        text1.SetActive(false);
        CreatePlayer();
    }


    private void OnConnectedToServer() {
        text1.SetActive(false);
        CreatePlayer();
    }


    void CreatePlayer() {
        Quaternion rot = Quaternion.Euler(0.0f, 90.0f, 0.0f);
        Vector3 pos = new Vector3(0.0f, 0.0f, 0.0f);
        Network.Instantiate(player, pos, rot, 0);
    }


    //접속이 종료된 플레이어의 네트워크 객체를 모두 소멸 처리
    private void OnPlayerDisconnected(NetworkPlayer netPlayer) {
        Network.DestroyPlayerObjects(netPlayer); //네트워크 플레이어의 모든 네트워크 객체를 소멸 처리
    }


    private void OnDisconnectedFromServer(NetworkDisconnection info) {
        text2.SetActive(true);
        Time.timeScale = 0.0f;
    }
}




