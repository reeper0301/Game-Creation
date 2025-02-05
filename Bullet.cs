using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 15;
    public float damage = 1;
    Vector2 direction;
    public Vector2 Direction
    {
        set
        {
            direction = value.normalized;
        }
    }
    void Start()
    {

    }

    void Update()
    {
        transform.Translate(direction*speed*Time.deltaTime);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Wall"|| collision.tag=="Enemy")
        {
            gameObject.SetActive(false);
        }
    }
}
