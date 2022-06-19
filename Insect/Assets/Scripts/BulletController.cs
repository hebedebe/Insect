using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    GameObject hitParticles;

    private void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }

    private void OnDestroy()
    {
        GameObject p = Instantiate(hitParticles, transform);
        p.transform.parent = null;
        Destroy(p, 5);
    }
}
