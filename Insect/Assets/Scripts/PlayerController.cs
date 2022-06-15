using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    float speed;
    [SerializeField]
    float dash_cooldown;
    [SerializeField]
    float dash_power;

    [Header("Health")]
    public int health = 3;
    public int maxHealth = 3;
    [Space]
    [SerializeField]
    float hit_cooldown = 0.1f;

    [Header("Inventory and Equipped Items")]
    public int chips = 0;

    [Header("Prefabs")]
    [SerializeField]
    GameObject death_particles;
    [SerializeField]
    GameObject hit_particles;
    [SerializeField]
    GameObject dash_particles;

    [Header("Sprites")]
    [SerializeField]
    Sprite health0;
    [SerializeField]
    Sprite health1;
    [SerializeField]
    Sprite health2;
    [SerializeField]
    Sprite health3;

    [Header("References")]
    [SerializeField]
    GameObject cam;
    [SerializeField]
    SpriteRenderer battery;

    //Timer variables
    float dash_timer;
    float hit_timer;

    private void Update()
    {
        PointAtMouse();
        Move();
        Dash();
        Battery();
    }

    public void Damage()
    {
        hit_timer = 0;

        health -= 1;

        cam.GetComponent<CameraController>().Shake(2, 0.25f, 40, 0.05f);

        GameObject p = Instantiate(hit_particles, transform);

        p.transform.parent = null;
        Destroy(p, 2);
    }

    void Battery()
    {
        hit_timer += Time.deltaTime;

        if (health == 0)
        {
            battery.sprite = health0;
        }
        else if (health == 1)
        {
            battery.sprite = health1;
        }
        else if (health == 2)
        {
            battery.sprite = health2;
        }
        else if (health == 3)
        {
            battery.sprite = health3;
        }
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        transform.position += new Vector3(x * speed * Time.deltaTime, y * speed * Time.deltaTime, 0);
    }

    void Dash()
    {
        dash_timer += Time.deltaTime;
        if (Input.GetMouseButtonDown(1) && dash_timer >= dash_cooldown)
        {
            cam.GetComponent<CameraController>().Shake(1, 0.25f, 20, 0);

            GameObject dash = Instantiate(dash_particles, transform);
            
            dash.transform.parent = null;
            Destroy(dash, 1);

            dash_timer = 0;
            transform.position += transform.up * dash_power;

            GameObject dash_arrive = Instantiate(dash_particles, transform);

            dash_arrive.transform.parent = null;
            Destroy(dash_arrive, 1);
        }
    }

    void PointAtMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5.23f;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle-90));
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            if (hit_timer >= hit_cooldown)
                Damage();
        }
    }
}
