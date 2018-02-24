using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkMgr : MonoBehaviour {
    private const string ip = "127.0.0.1"; //접속 IP
    private const int port = 30000; //접속 Port
    private bool _useNat = true; //NAT 기능의 사용 여부
    public GameObject player; //플레이어 프리팹
    public GameObject text1;

    private void OnGUI() {
        if (Network.peerType == NetworkPeerType.Disconnected) {
            if (GUI.Button(new Rect(480, 380, 320, 60), "서버 켜기")) Network.InitializeServer(20, port, _useNat);
            if (GUI.Button(new Rect(480, 470, 320, 60), "서버 들어가기")) Network.Connect(ip, port);
            if (GUI.Button(new Rect(480, 560, 320, 60), "후원정보")) SceneManager.LoadScene(1);
        } else {
            if (Network.peerType == NetworkPeerType.Server) {
                GUI.Label(new Rect(20, 20, 200, 25), "서버 활성화중");
                GUI.Label(new Rect(20, 50, 200, 25), "접속자 수: " + Network.connections.Length.ToString() + "명(최대 20명)");
            }
            if (Network.peerType == NetworkPeerType.Client) GUI.Label(new Rect(20, 20, 200, 25), "서버 접속중");
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
        Quaternion rot = Quaternion.identity;
        rot.eulerAngles = new Vector3(0.0f, 90.0f, 0.0f);
        Vector3 pos = new Vector3(0.0f, 0.0f, 0.0f);
        Network.Instantiate(player, pos, rot, 0);
    }

    //접속이 종료된 플레이어의 네트워크 객체를 모두 소멸 처리
    private void OnPlayerDisconnected(NetworkPlayer netPlayer) {
        Network.DestroyPlayerObjects(netPlayer); //네트워크 플레이어의 모든 네트워크 객체를 소멸 처리
    }
}




