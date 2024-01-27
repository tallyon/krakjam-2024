using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

namespace DefaultNamespace
{
    public class LoadBootstrap : MonoBehaviour
    {
        private void Awake()
        {
            StartCoroutine(LoadCoroutine());
        }

        private IEnumerator LoadCoroutine()
        {
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene(0);
        }
    }
}