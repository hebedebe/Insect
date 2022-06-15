using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float lerpStrength = 1;

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

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(0,0,-10), lerpStrength * Time.deltaTime);

        if (Vector3.Distance(transform.position, new Vector3(0,0,-10)) < 0.01f)
        {
            transform.position = new Vector3(0,0,-10);
        }
    }
}
