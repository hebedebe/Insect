using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField]
    GameObject centipede;
    [SerializeField]
    GameObject Boss1;

    [Header("Spawn Settings")]
    [SerializeField]
    bool spawn = true;
    [Space]
    [SerializeField]
    bool spawnCentipede = true;
    [SerializeField]
    float centipedeDelay = 60;
    public int centipedeLayer = 0;

    float centipedeTimer = 0;

    Transform spawnPos;

    private void Start()
    {
        centipedeTimer = centipedeDelay;
        spawnPos = transform.GetChild(0);
        StartCoroutine(Boss1Timer());
    }

    IEnumerator Boss1Timer()
    {
        yield return new WaitForSeconds(60*5);
        GameObject b = Instantiate(Boss1, new Vector3(0,4.75f,-2), Quaternion.identity);
        b.transform.localScale = new Vector3(2, 2, 2);
        spawn = false;
    }

    void FixedUpdate()
    {
        if (spawn)
        {
            if (spawnCentipede)
                Centipede();
        }
    }

    void Centipede()
    {
        centipedeTimer += Time.fixedDeltaTime;
        if (centipedeTimer >= centipedeDelay)
        {
            //transform.Rotate(0,0,Random.Range(-180f,180f));
            if (Random.Range(0,1) == 0)
                spawnPos.transform.position = new Vector3(-21, Random.Range(0, -5.4f), -1);
            else
                spawnPos.transform.position = new Vector3(17, Random.Range(0, -5.4f), -1);
            centipedeTimer = 0;
            GameObject c = Instantiate(centipede, spawnPos);
            c.transform.parent = transform;
            c.GetComponent<SpriteRenderer>().sortingOrder = centipedeLayer;
            centipedeLayer -= 1;
        }
    }
}
