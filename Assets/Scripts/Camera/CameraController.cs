using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    new Camera camera;

    [Header("Zoom")]
    [SerializeField] Vector3 TargetPos;
    [SerializeField] float Size;
    [SerializeField] float Time;
    Vector3 originPos;

    public void OnStart()
    {
        camera = GetComponent<Camera>();
        originPos = camera.transform.position;
    }
    public void Shake(float force, float time, float cycle)
    {
        Coroutine.instance.StartCor(ShakeCor(force, time / cycle, cycle));
    }

    public void ZoomIn()
    {
        DOTween.To(() => camera.transform.position, x => camera.transform.position = x, TargetPos, Time).SetEase(Ease.OutBack);
        DOTween.To(() => camera.orthographicSize, x => camera.orthographicSize = x, Size, Time).SetEase(Ease.OutBack);
    }

    IEnumerator ShakeCor(float force, float delay, float cycle)
    {
        for (int i = 0; i < cycle; i++)
        {
            camera.transform.DOMove(originPos + Vector3.right * Random.Range(-1, 1f) * force, 0.01f);
            yield return new WaitForSeconds(0.01f);
            camera.transform.DOMove(originPos, delay);
            yield return new WaitForSeconds(delay);
        }
    }
}
