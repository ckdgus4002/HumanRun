// Copyright 2019. LCH. All rights reserved.

using UnityEngine;
using System.Collections;
using UnityStandardAssets.Utility;

namespace LCH
{
    public class PlayerCtrl : MonoBehaviour
    {
        public GameObject canvas2Create;
        public GameObject canvas2Destory;


        // 사망 여부를 나타내는 변수
        public bool isDie = false;

        // CharacterController 컴포넌트를 할당할 변수
        private CharacterController controller;

        // NetworkView 컴포넌트를 할당할 변수
        private NetworkView _networkView;

        // 위치 정보를 송수신할 때 사용할 변수 선언 및 초기값 설정
        private Vector3 currPos = Vector3.zero;

        // 부활 시간
        private float respawnTime = 3.0f;

        // Transform 컴포넌트를 할당할 변수
        private Transform tr;

        private Quaternion currRot = Quaternion.identity;



        private void Awake()
        {
            tr = GetComponent<Transform>();
            controller = GetComponent<CharacterController>();
            _networkView = GetComponent<NetworkView>();

            if (_networkView.isMine)
            {
                Camera.main.GetComponent<SmoothFollow>().target = tr;
            }
        }

        private void Update()
        {
            // 원격 플레이어일 때 수행
            //현재 좌표와 전송받은 새로운 좌표 간의 거리차가 너무 크다면 바로 이동
            if (!_networkView.isMine)
            {
                if (Vector3.Distance(tr.position, currPos) >= 50.0f)
                {
                    tr.position = currPos;
                    tr.rotation = currRot;
                }
                else
                {
                    tr.position = Vector3.Lerp(tr.position, currPos, Time.deltaTime * 10.0f);
                    tr.rotation = Quaternion.Slerp(tr.rotation, currRot, Time.deltaTime * 10.0f);
                }
            }
            if (_networkView.isMine) canvas2Destory = GameObject.Find("Canvas2 (GameOver)(Clone)");
        }


        private void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
        {
            if (stream.isWriting)
            {
                Vector3 pos = tr.position;
                Quaternion rot = tr.rotation;

                stream.Serialize(ref pos); // 위치값 전송
                stream.Serialize(ref rot); // 회전값 전송
            }
            else
            {
                Vector3 revPos = Vector3.zero;
                Quaternion revRot = Quaternion.identity;

                stream.Serialize(ref revPos);
                stream.Serialize(ref revRot);

                currPos = revPos;
                currRot = revRot;
            }
        }

        private void OnTriggerEnter(Collider coll)
        {
            if (coll.gameObject.tag == "DeathObstacle")
            {
                // 부딪혔을때 사망 및 Respawn 코루틴 함수 호출
                StartCoroutine(RespawnPlayer(respawnTime));

                if (_networkView.isMine)
                {
                    Instantiate(canvas2Create, Vector3.zero, Quaternion.identity);
                }
            }
        }



        //사망 처리 및 Respawn 처리
        private IEnumerator RespawnPlayer(float waitTime)
        {
            isDie = true;

            // 플레이어의 Mesh Renderer를 비활성화하는 코루틴 함수 호출
            StartCoroutine(PlayerVisible(false, 0.0f));

            // Respawn시간까지 기다림
            yield return new WaitForSeconds(waitTime);

            tr.position = new Vector3(0.0f, 0.0f, 0.0f);
            tr.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);

            // 플레이어를 컨트롤할 수 있게 변수 설정
            isDie = false; 

            Destroy(canvas2Destory);

            // 플레이어의 Mesh Renderer 활성화
            StartCoroutine(PlayerVisible(true, 0.5f)); 
        }

        // 플레이어의 Mesh Renderer와 Character Controller의 활성/비활성 처리
        private IEnumerator PlayerVisible(bool visibled, float delayTime)
        {
            yield return new WaitForSeconds(delayTime);

            // 플레이어 바디의 Skinned Mesh Renderer 활성/비활성화
            // 키보드 움직임에 반응하지 않게 MoveCtrl과 Charactor Controller를 활성/비활성화
            GetComponentInChildren<SkinnedMeshRenderer>().enabled = visibled; 

            if (_networkView.isMine)
            {
                GetComponent<MoveCtrl>().enabled = visibled;
                controller.enabled = visibled;
            }
        }
    }
}