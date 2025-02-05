using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    Vector3 move;
    public float speed = 8;
    public GameObject bulletPrefab;
    public Material flashMaterial;
    public Material defaultMaterial;
    public AudioClip shotSound;
    public AudioClip hitSound;
    public AudioClip deathSound;
    void Start()
    {
    }
    void Update()
    {
        move = Vector3.zero;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            move += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            move += new Vector3(0, -1, 0);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            move += new Vector3(1, 0, 0);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            move += new Vector3(0, 1, 0);
        }
        move = move.normalized;
        if (move.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (move.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if (move.magnitude > 0)
        {
            GetComponent<Animator>().SetTrigger("Move");
        }
        else
        {
            GetComponent<Animator>().SetTrigger("Stop");
        }
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
    void Shoot()
    {
        GetComponent<AudioSource>().PlayOneShot(shotSound);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0;
        worldPosition -= (transform.position + new Vector3(0, -0.5f, 0));
        GameObject newBullet = GetComponent<ObjectPool>().Get();
        if(newBullet != null)
        {
            newBullet.transform.position = transform.position + new Vector3(0, -0.5f);
            newBullet.GetComponent<Bullet>().Direction = worldPosition;
        }
    }
    private void FixedUpdate()
    {
        transform.Translate(move * speed * Time.fixedDeltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy");
        {
            if (GetComponent<CharacterHP>().Hit(1)){
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
        GetComponent<Animator>().SetTrigger("Death");
        Invoke("AfterDying", 0.875f);
    }
    void AfterDying()
    {
        SceneManager.LoadScene("GameOverScene");
    }
}
