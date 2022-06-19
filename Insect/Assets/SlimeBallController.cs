using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBallController : MonoBehaviour
{
    [SerializeField]
    GameObject slime;
    [SerializeField]
    float speed;

    private void Start()
    {
        transform.GetChild(0).rotation = Quaternion.Euler(0,0,0);
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    private void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }

    private void OnDestroy()
    {
        for (int i = 0; i < 16; i++)
        {
            GameObject obj = Instantiate(slime, new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f), 0), Quaternion.identity);
            Destroy(obj, Random.Range(0.5f,1.5f));
            obj.transform.parent = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.GetComponent<PlayerController>().Damage();
            Destroy(gameObject);
        }
    }
}
