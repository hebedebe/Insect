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

    private void Start()
    {
        centipedeTimer = centipedeDelay;
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
            centipedeTimer = 0;
            Vector3 pos = new Vector3(0, 0, 0);
            GameObject c = Instantiate(centipede, transform);
            c.transform.position = pos;
            c.GetComponent<SpriteRenderer>().sortingOrder = centipedeLayer;
            centipedeLayer -= 1;
        }
    }
}
