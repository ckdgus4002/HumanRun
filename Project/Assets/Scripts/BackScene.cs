using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
//using UnityStandardAssets.Utility;
//using Vuforia;

namespace LCHCompany.LCH
{
    public class BackScene : MonoBehaviour
    {
        void Start()
        {
            // 코루틴화 된 업데이트 문 시작.
            StartCoroutine(CoroutineUpdate());
        }

        IEnumerator CoroutineUpdate()
        {
            while (true)
            {
                if (Input.GetButtonDown("Cancel"))
                {
                    SceneManager.LoadScene(0);
                }
                yield return null;
            }
        }
    }
}