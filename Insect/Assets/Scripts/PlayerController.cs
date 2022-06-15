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

    [Header("Inventory and Equipped Items")]
    public int chips = 0;

    [Header("Prefabs")]
    [SerializeField]
    GameObject death_particles;
    [SerializeField]
    GameObject hit_particles;
    [SerializeField]
    GameObject dash_particles;

    [Header("References")]
    [SerializeField]
    GameObject cam;

    //Timer variables
    float dash_timer;

    private void Update()
    {
        PointAtMouse();
        Move();
        Dash();
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
            cam.GetComponent<CameraController>().Shake(1, 5f, 5, 0);

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
}
