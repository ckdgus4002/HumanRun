// Copyright 2019. LCH. All rights reserved.

using UnityEngine;

namespace LCH
{
    public class MoveCtrl : MonoBehaviour
    {
        //중력, 이동 속도, 점프 속도 변수
        public float gravity = 3.0f;
        public float movSpeed = 10.0f;
        public float jumSpeed = 1.0f;

        //컴포넌트를 할당할 변수 선언
        private Transform tr;

        private CharacterController controller;

        //키보드 입력값 변수 선언
        private float h = 0.0f;

        //이동할 방향 벡터 변수
        private Vector3 movDir = Vector3.zero;
        


        private void Start()
        {
            tr = GetComponent<Transform>();
            controller = GetComponent<CharacterController>();
            
            //원격 접속한 네트워크 유저일 경우 이 스크립트를 비활성화
            enabled = GetComponent<NetworkView>().isMine;
        }

        private void Update()
        {
            //키보드 입력값을 받아옴
            h = Input.GetAxis("Horizontal");

            if (controller.isGrounded)
            {
                //이동 방향을 벡터의 덧셈 연산을 이용해 미리 계산
                movDir = tr.forward + (tr.right * h);

                if (Input.GetButton("Jump"))
                {
                    movDir.y = jumSpeed;
                }
            }

            //중력의 영향을 받아 밑으로 떨어지도록 y값 변경
            movDir.y -= gravity * Time.deltaTime;

            //플레이어를 이동
            controller.Move(movDir * movSpeed * Time.deltaTime);

            if (Input.GetButtonDown("Cancel"))
            {
                tr.position = new Vector3(0.0f, 0.0f, 0.0f);
                tr.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
            }
        }
    }
}