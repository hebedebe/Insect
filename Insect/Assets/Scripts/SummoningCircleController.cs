using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummoningCircleController : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;

    [SerializeField]
    Transform targeter;

    Transform player;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        yield return new WaitForSeconds(1.5f);

        Vector3 vectorToTarget = player.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;

        Quaternion q = Quaternion.AngleAxis(angle - 10, Vector3.forward);

        targeter.transform.rotation = q;

        GameObject b = Instantiate(bullet, targeter);
        b.transform.parent = null;


        yield return new WaitForSeconds(0.5f);


        q = Quaternion.AngleAxis(angle, Vector3.forward);

        targeter.transform.rotation = q;

        b = Instantiate(bullet, targeter);
        b.transform.parent = null;


        yield return new WaitForSeconds(0.5f);


        q = Quaternion.AngleAxis(angle + 10, Vector3.forward);

        targeter.transform.rotation = q;

        b = Instantiate(bullet, targeter);
        b.transform.parent = null;


        Destroy(gameObject, 1.5f);
    }
}
