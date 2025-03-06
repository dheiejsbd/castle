using System;
using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using FrameWork.UI;
using DG.Tweening;

namespace FrameWork.Loading
{
    public class LoadingProcess : MonoBehaviour
    {
        public static LoadingProcess instance;

        LoadingWindow loadingWindow;
        
        public void OnStart()
        {
            instance = this;
            loadingWindow = UIManager.instance.GetWindow(typeof(LoadingWindow)) as LoadingWindow;
        }
        public IEnumerator LoadScene(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            loadingWindow.SetPrograss(0);
            loadingWindow.ShowSequence.Play();

            yield return new WaitForSeconds(loadingWindow.FadeTime);


            AsyncOperation op = SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
            op.allowSceneActivation = false;
            float timer = 0;
            while (op.isDone)
            {
                yield return null;
                if (op.progress < 0.9f)
                {
                    loadingWindow.SetPrograss(op.progress);
                }
                else
                {
                    timer += Time.unscaledDeltaTime;
                    loadingWindow.SetPrograss(Mathf.Lerp(0.9f, 1, timer));
                    if (timer >= 1)
                    {
                        yield break;
                    }
                }
            }
            op.allowSceneActivation = true;
            loadingWindow.HideSequence.Play();
            yield return new WaitForSeconds(loadingWindow.FadeTime);
            yield return null;
        }

        public IEnumerator UnloadScene(string sceneName)
        {
            yield return SceneManager.UnloadScene(sceneName);
            yield return null;
        }
    }
}