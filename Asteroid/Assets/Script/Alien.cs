using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    public Sprite[] sprites;

    public float size = 1.0f;

    public float minSize = 1.0f;

    public float maxSize = 2.0f;

    public float speed = 30.0f;

    public float maxLifetime = 50.0f;

    public Vector2 myDirection;

    private SpriteRenderer _spriteRenderer;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

        //this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value = 360f);
        this.transform.localScale = Vector3.one * this.size;

        _rigidbody.mass = this.size * 2.0f;
    }

    public void setTrajectory(Vector2 direction)
    {
        _rigidbody.AddForce(direction * this.speed);
        myDirection = direction * this.speed;
        Destroy(this.gameObject, this.maxLifetime);
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Boundary")
        {
            Debug.Log("collide");
            //Store new direction
            Vector3 newDirection = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
            //Rotate bullet to new direction
            transform.rotation = Quaternion.LookRotation(newDirection);

            //add velocity to bullet based on new forward vector
            _rigidbody.velocity = transform.forward * speed;
        }


        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D mycollider)
    {
        Debug.Log("hello");
        GetComponent<BoxCollider2D>().enabled = true;
    }

    void OnBecameVisible()
    {
        StartCoroutine(makeCollidable());
    }

    IEnumerator makeCollidable()
    {
        yield return new WaitForSeconds[2];
    }
    
}
