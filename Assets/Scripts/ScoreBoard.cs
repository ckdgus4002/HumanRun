// Copyright 2019. LCH. All rights reserved.

using UnityEngine;

namespace LCH
{
    public class ScoreBoard : MonoBehaviour
    {
        private Transform tr;

        // 예전위치를 기록하기위한 변수
        private Vector3 beforeTr = Vector3.zero; 

        private PlayerCtrl plyCtl;

        // 플레이어 점수
        private int score = 0;

        // 함수제어를 위한 타이머
        private int timer = 300;



        private void Start()
        {
            tr = GetComponent<Transform>();
            plyCtl = GetComponent<PlayerCtrl>();

            //원격 접속한 네트워크 유저일 경우 이 스크립트를 비활성화
            enabled = GetComponent<NetworkView>().isMine;
        }

        private void Update()
        {
            timer--;

            if (timer == 0)
            {
                timer = 300;
                beforeTr.x = tr.position.x;
                beforeTr.y = tr.position.y;
                beforeTr.z = tr.position.z;
            }

            if (plyCtl.isDie == false)
            {
                score++;
            }
            else
            {
                score = 0;
            }
        }


        private void OnGUI()
        {
            if (Network.peerType != NetworkPeerType.Disconnected)
            {
                GUI.Label(new Rect(1060, 20, 200, 20), "*달린 거리(점수): " + score + "점");
                GUI.Label(new Rect(1060, 45, 200, 20), "*현재 위치(x,y,z): " + (int)tr.position.x + ", " + (int)tr.position.y + ", " + (int)tr.position.z);
                GUI.Label(new Rect(1060, 70, 200, 20), "*이전 위치(x,y,z): " + (int)beforeTr.x + ", " + (int)beforeTr.y + ", " + (int)beforeTr.z + "  (" + timer + ")");
            }
        }
    }
}