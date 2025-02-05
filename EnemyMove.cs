using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnemyMove : MonoBehaviour
{
    enum State
    {
        Spawn,
        Move,
        Die
    }
    public float speed = 3;
    public Material flashMaterial;
    public Material defaultMaterial;
    public AudioClip deathSound;
    public AudioClip hitSound;
    GameObject target;
    State state;
    void Start()
    {

    }
    public void Spawn(GameObject target)
    {
        this.target = target;
        state = State.Spawn;
        GetComponent<CharacterHP>().Initialize();
        GetComponent<Animator>().SetTrigger("Spawn");
        Invoke("StartMoving", 1);
        GetComponent<Collider2D>().enabled = false;
    }
    void StartMoving()
    {
        GetComponent<Collider2D>().enabled = true;
        state = State.Move;
    }
    private void FixedUpdate()
    {
        if (state == State.Move)
        {

            Vector2 direction = target.transform.position - transform.position;
            transform.Translate(direction.normalized * speed * Time.fixedDeltaTime);
            if (direction.x < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            if (direction.x > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            float d = collision.gameObject.GetComponent<Bullet>().damage;
            if (GetComponent<CharacterHP>().Hit(d))
            {
                GetComponent<AudioSource>().PlayOneShot(hitSound);
                Flash();
            }
            else
            {
                GetComponent<AudioSource>().PlayOneShot(deathSound);
                Death();
            }
        }
    }
    void Flash()
    {
        GetComponent<SpriteRenderer>().material = flashMaterial;
        Invoke("AfterFlash", 0.5f);
    }
    void AfterFlash()
    {
        GetComponent<SpriteRenderer>().material = defaultMaterial;
    }
    void Death()
    {
        state = State.Die;
        GetComponent<Animator>().SetTrigger("Death");
        Invoke("AfterDying", 1.4f);
    }
    void AfterDying()
    {
        gameObject.SetActive(false);
    }
}
