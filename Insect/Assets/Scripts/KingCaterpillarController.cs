using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KingCaterpillarController : MonoBehaviour
{
    [Header("Health")]
    public float health = 100;

    [Header("VFX")]
    [SerializeField]
    float spitScreenshake = 0.2f;
    [SerializeField]
    float laserScreenShake = 0.3f;

    [Header("Timing")]
    [SerializeField]
    float spitDelay = 4;
    float spitTimer;
    [SerializeField]
    float laserDelay;
    float laserTimer;
    [SerializeField]
    float bigLaserDuration;
    float bigLaserTimer;
    [SerializeField]
    float smallLaserDuration;
    float smallLaserTimer;
    [SerializeField]
    float summoningCircleDelay = 5;
    float summoningCircleTimer;
    [SerializeField]
    float bigSummoningCircleDelay = 15;
    float bigSummoningCircleTimer;

    [Header("Speed")]
    [SerializeField]
    float bigLaserSpeed;
    [SerializeField]
    float bigLaserSpeedPhase3;
    [SerializeField]
    float smallLaserSpeed;

    [Header("Tracking")]
    [SerializeField]
    int phase = 1;
    [SerializeField]
    bool spawnedPhase1;
    [SerializeField]
    bool rechargingLasers;

    [Header("Prefabs")]
    [SerializeField]
    GameObject centipede;
    [SerializeField]
    GameObject slimeBall;
    [SerializeField]
    GameObject bigLaser;
    [SerializeField]
    GameObject smallLaser;
    [SerializeField]
    GameObject summoningCircle;

    [Header("References")]
    [SerializeField]
    CameraController cam;
    [SerializeField]
    Transform player;
    [SerializeField]
    Transform leftSpawnPoint;
    [SerializeField]
    Transform rightSpawnPoint;
    Transform es;
    [SerializeField]
    Transform targeter;
    [SerializeField]
    Transform eye1;
    [SerializeField]
    Transform eye2;
    [SerializeField]
    Transform eye3;
    [SerializeField]
    Transform eye4;
    [SerializeField]
    Transform leftCirclePos;
    [SerializeField]
    Transform rightCirclePos;
    [SerializeField]
    Transform leftBigCirclePos;
    [SerializeField]
    Transform rightBigCirclePos;
    [SerializeField]
    Slider healthBar;

    private void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<CameraController>();
        player = GameObject.Find("Player").transform;
        es = GameObject.Find("EnemySpawner").transform;

        healthBar = GameObject.Find("BossBar").GetComponent<Slider>();
    }

    private void FixedUpdate()
    {
        healthBar.value = health;

        CheckPhase();

        if (phase == 1)
        {
            Phase1();
        } else if (phase == 2)
        {
            Phase2();
        } else
        {
            Phase3();
        }
    }

    void Phase1()
    {
        spitTimer += Time.deltaTime;

        if (!spawnedPhase1)
        {
            StartCoroutine(SpawnLeft());
            StartCoroutine(SpawnRight());
            spawnedPhase1 = true;
        }

        if (spitTimer >= spitDelay)
        {
            Spit();
            spitTimer = 0;
        }
    }

    void Spit()
    {
        Vector3 vectorToTarget = player.position - targeter.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;

        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

        targeter.rotation = q;

        GameObject projectile = Instantiate(slimeBall, targeter);
        projectile.transform.parent = transform;
        Destroy(projectile, 10);

        cam.Shake(1, spitScreenshake, 20, 0);
    }

    void Phase2()
    {
        spitTimer += Time.deltaTime;

        if (rechargingLasers)
            laserTimer += Time.deltaTime;

        if (spitTimer >= spitDelay)
        {
            Spit();
            spitTimer = 0;
        }

        Laser();
    }

    void Laser()
    {
        if (laserTimer > laserDelay)
        {
            Debug.Log("Shooting Lasers");

            laserTimer = 0;
            rechargingLasers = false;
            bigLaserTimer = 0;
            smallLaserTimer = 0;

            eye1.gameObject.SetActive(true);
            eye2.gameObject.SetActive(true);
            eye3.gameObject.SetActive(true);
            eye4.gameObject.SetActive(true);

            eye1.transform.rotation = Quaternion.Euler(0,0,-90);
            eye2.transform.rotation = Quaternion.Euler(0, 0, 90);

            eye3.transform.rotation = Quaternion.Euler(0, 0, -120);
            eye4.transform.rotation = Quaternion.Euler(0, 0, 120);

            Destroy(Instantiate(bigLaser, eye1), bigLaserDuration);
            Destroy(Instantiate(bigLaser, eye2), bigLaserDuration);

            Destroy(Instantiate(smallLaser, eye3), smallLaserDuration);
            Destroy(Instantiate(smallLaser, eye4), smallLaserDuration);
        }

        if (!rechargingLasers)
        {
            if (phase == 2)
            {
                eye1.transform.Rotate(0, 0, bigLaserSpeed * Time.deltaTime);
                eye2.transform.Rotate(0, 0, -bigLaserSpeed * Time.deltaTime);
            } else
            {
                eye1.transform.Rotate(0, 0, bigLaserSpeedPhase3 * Time.deltaTime);
                eye2.transform.Rotate(0, 0, -bigLaserSpeedPhase3 * Time.deltaTime);
            }

            eye3.transform.Rotate(0, 0, smallLaserSpeed * Time.deltaTime);
            eye4.transform.Rotate(0, 0, -smallLaserSpeed * Time.deltaTime);


            if (bigLaserTimer >= bigLaserDuration)
            {
                eye1.gameObject.SetActive(false);
                eye2.gameObject.SetActive(false);
            }

            if (smallLaserTimer >= smallLaserDuration)
            {
                eye3.gameObject.SetActive(false);
                eye4.gameObject.SetActive(false);
            }

            smallLaserTimer += Time.deltaTime;
            bigLaserTimer += Time.deltaTime;

            cam.Shake(1, laserScreenShake, 20, 0);
        }

        if (!rechargingLasers && smallLaserTimer >= smallLaserDuration && bigLaserTimer >= bigLaserDuration)
        {
            rechargingLasers = true;
        }
    }

    void Phase3()
    {
        summoningCircleTimer += Time.deltaTime;
        bigSummoningCircleTimer += Time.deltaTime;

        if (rechargingLasers)
            laserTimer += Time.deltaTime;

        Laser();

        Summon();
        BigSummon();
    }

    void Summon()
    {
        if (summoningCircleTimer >= summoningCircleDelay)
        {
            summoningCircleTimer = 0;

            GameObject l = Instantiate(summoningCircle, leftCirclePos);
            l.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            GameObject r = Instantiate(summoningCircle, rightCirclePos);
            r.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }

    void BigSummon()
    {
        if (bigSummoningCircleTimer >= bigSummoningCircleDelay)
        {
            bigSummoningCircleTimer = 0;

            GameObject l = Instantiate(summoningCircle, leftBigCirclePos);
            GameObject r = Instantiate(summoningCircle, rightBigCirclePos);
        }
    }

    IEnumerator SpawnLeft()
    {
        GameObject c = Instantiate(centipede, leftSpawnPoint);
        c.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        c.transform.parent = es;

        yield return new WaitForSeconds(3);

        GameObject c2 = Instantiate(centipede, new Vector3(leftSpawnPoint.position.x, leftSpawnPoint.position.y-5, 0), Quaternion.identity);
        c2.transform.parent = es;

        yield return new WaitForSeconds(3);

        GameObject c3 = Instantiate(centipede, new Vector3(leftSpawnPoint.position.x, leftSpawnPoint.position.y + 5, 0), Quaternion.identity);
        c3.transform.parent = es;
    }

    IEnumerator SpawnRight()
    {
        GameObject c = Instantiate(centipede, rightSpawnPoint);
        c.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        c.transform.parent = es;

        yield return new WaitForSeconds(3);

        GameObject c2 = Instantiate(centipede, new Vector3(rightSpawnPoint.position.x, rightSpawnPoint.position.y - 5, 0), Quaternion.identity);
        c2.transform.parent = es;

        yield return new WaitForSeconds(3);

        GameObject c3 = Instantiate(centipede, new Vector3(rightSpawnPoint.position.x, rightSpawnPoint.position.y + 5, 0), Quaternion.identity);
        c3.transform.parent = es;
    }

    void CheckPhase()
    {
        if (health >= 75)
        {
            phase = 1;
        } else if (health >= 50) 
        {
            phase = 2;
        } else
        {
            phase = 3;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Sword"))
        {
            health -= 0.25f;
        }
    }
}
