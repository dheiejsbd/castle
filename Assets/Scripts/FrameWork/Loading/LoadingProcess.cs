using System;
using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace FrameWork.Loading
{
    public class LoadingProcess : MonoBehaviour
    {
        static string SceneName = "";
        [SerializeField] Image LoddingBar;
        public static void LoddingScene(string _SceneName)
        {
            SceneName = _SceneName;
            SceneManager.LoadScene("LoadScene");
        }

        void Start()
        {
            StartCoroutine(Loading());
        }

        IEnumerator Loading()
        {
            AsyncOperation op = SceneManager.LoadSceneAsync(SceneName);

            op.allowSceneActivation = false;
            float timer = 0;
            while (op.isDone)
            {
                yield return null;
                if (op.progress < 0.9f)
                {
                    LoddingBar.fillAmount = op.progress;
                }
                else
                {
                    timer += Time.unscaledDeltaTime;
                    LoddingBar.fillAmount = Mathf.Lerp(0.9f, 1, timer);
                    if (LoddingBar.fillAmount >= 1)
                    {
                        op.allowSceneActivation = true;
                        yield break;
                    }
                }
            }

        }
    }
}