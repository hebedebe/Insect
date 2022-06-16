using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentipedeController : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField]
    GameObject bodyPrefab;
    [SerializeField]
    GameObject deathParticles;
    [SerializeField]
    GameObject hitParticles;
    [SerializeField]
    GameObject slime;

    [Header("Sprites")]
    [SerializeField]
    Sprite head;
    [SerializeField]
    Sprite segment;

    [Header("Movement")]
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float rotSpeed;
    [SerializeField]
    float headRotSpeed;
    [SerializeField]
    float minDist;

    [Header("Health")]
    [SerializeField]
    float health = 3;
    [SerializeField]
    float hitDelay;

    [Header("Setup")]
    [SerializeField]
    int numSegments;
    [SerializeField]
    bool isHead;
    [SerializeField]
    bool isSetup = true;
    [SerializeField]
    float slimeDelay = 1f;
    [SerializeField]
    bool slimeTrail = false;

    //private variables
    Transform player;
    Transform target;
    SpriteRenderer sp;
    EnemySpawner es;
    float timer;
    float slimeTimer;

    void Start()
    {
        sp = GetComponent<SpriteRenderer>();

        es = transform.parent.GetComponent<EnemySpawner>();

        if (isHead)
        {
            player = GameObject.Find("Player").transform;
            SegmentSetup();
        }
    }

    void SegmentSetup()
    {
        float offset = 0.25f;
        Transform prevSegment = transform;
        for (int i = 0; i < numSegments; i++)
        {
            GameObject obj = Instantiate(bodyPrefab, new Vector3(transform.position.x + offset, transform.position.y, 0), transform.rotation);

            CentipedeController mg = obj.GetComponent<CentipedeController>();
            mg.target = prevSegment;
            offset += 0.25f;
            prevSegment = obj.transform;

            obj.transform.parent = transform.parent;
            obj.GetComponent<SpriteRenderer>().sortingOrder = es.centipedeLayer;
            es.centipedeLayer -= 1;
        }
        isSetup = true;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        slimeTimer += Time.deltaTime;
    }

    void FixedUpdate()
    {
        if (slimeTimer > slimeDelay && slimeTrail)
        {
            GameObject slimeObj = Instantiate(slime, transform);
            slimeObj.transform.parent = null;
            slimeObj.transform.position = new Vector3(transform.position.x, transform.position.y, 1);
            Destroy(slimeObj, 5);
            slimeTimer = 0;
        }
        if (isHead)
        {


            if (isSetup)
            {
                sp.sprite = head;

                Vector3 vectorToTarget = player.position - transform.position;
                float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;

                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.fixedDeltaTime * headRotSpeed);
                transform.position += transform.right * moveSpeed * Time.fixedDeltaTime;

            }
        }
        else
        {
            if (player == null)
            {
                player = target.GetComponent<CentipedeController>().player;
            }
            if (target != null)
            {
                sp.sprite = segment;
                Vector3 vectorToTarget = target.position - transform.position;
                float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;

                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.fixedDeltaTime * rotSpeed);


                if (Vector3.Distance(transform.position, target.transform.position) + moveSpeed * Time.fixedDeltaTime > minDist)
                    transform.position += transform.right * moveSpeed * Time.fixedDeltaTime;
            }
            else
            {
                isHead = true;
            }

        }
        sp.flipX = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Sword") && timer < 0)
        {
            timer = hitDelay;
            health -= 1;
            if (health < 1)
            {
                Die();
            }
            Hit();
        }
    }

    void Hit()
    {
        GameObject particle = Instantiate(hitParticles, transform.position, Quaternion.identity);
        particle.transform.parent = null;
        Destroy(particle, 5);
    }

    public void Die()
    {
        GameObject particle = Instantiate(deathParticles, transform.position,

       Quaternion.identity);

        particle.transform.parent = null;
        Destroy(particle, 5);
        Destroy(gameObject);
    }

}