using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField]
    GameObject centipede;

    [Header("Spawn Settings")]
    [SerializeField]
    bool spawn = true;
    [Space]
    bool spawnCentipede = true;
    float centipedeDelay = 60;
    public int centipedeLayer = 0;

    float centipedeTimer = 0;

    Transform spawnPos;

    private void Start()
    {
        centipedeTimer = centipedeDelay;
        spawnPos = transform.GetChild(0);
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
            transform.Rotate(0,0,Random.Range(-180f,180f));
            centipedeTimer = 0;
            GameObject c = Instantiate(centipede, spawnPos);
            c.transform.parent = transform;
            c.GetComponent<SpriteRenderer>().sortingOrder = centipedeLayer;
            centipedeLayer -= 1;
        }
    }
}
