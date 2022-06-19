using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CameraController : MonoBehaviour
{
    float lerpStrength = 1;

    [SerializeField]
    float zoomLerp = 20;

    Vector2 targetRes = new Vector2(480, 272);

    PixelPerfectCamera ppc;

    private void Start()
    {
        ppc = GetComponent<PixelPerfectCamera>();
    }

    public void Shake(int numShakes, float shakeStrength, float lerpValue, float interval)
    {
        StartCoroutine(Shaker(numShakes, shakeStrength, lerpValue, interval));
    }

    IEnumerator Shaker(int numShakes, float shakeStrength, float lerpValue, float interval)
    {
        lerpStrength = lerpValue;
        for (int i = 0; i < numShakes; i++)
        {
            transform.position += new Vector3(Random.Range(-shakeStrength * 100, shakeStrength * 100)/100, Random.Range(-shakeStrength * 100, shakeStrength * 100)/100, 0);
            yield return new WaitForSeconds(interval);
        }
    }

    public void ZoomOut()
    {
        targetRes = new Vector2(960, 544);
    }

    public void ZoomIn()
    {
        targetRes = new Vector2(480, 272);
    }

    Vector2 SnapPosition(Vector2 input, float factor = 1f)
    {
        if (factor <= 0f)
            throw new UnityException("factor argument must be above 0");

        float x = Mathf.Round(input.x / factor) * factor;
        float y = Mathf.Round(input.y / factor) * factor;

        return new Vector2(x, y);
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(0,0,-10), lerpStrength * Time.deltaTime);

        if (Vector3.Distance(transform.position, new Vector3(0,0,-10)) < 0.01f)
        {
            transform.position = new Vector3(0,0,-10);
        }

        Vector2 scrRes = SnapPosition(Vector2.Lerp(targetRes, new Vector2(ppc.refResolutionX, ppc.refResolutionY), zoomLerp * Time.deltaTime), 2f);

        ppc.refResolutionX = (int)scrRes.x;
        ppc.refResolutionY = (int)scrRes.y;
    }
}
