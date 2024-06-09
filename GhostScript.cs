using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GhostScript : MonoBehaviour
{
    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;
    public Sprite emb;
    public Transform player;
    public SpriteRenderer sr;
    public float moveSpeed = 0.1f;
    bool frozen = false;
    Transform currentTarget;
    Rigidbody2D rb;
    public float x = 500;
    
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        currentTarget = player;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.y - player.position.y) < Mathf.Abs(transform.position.x - player.position.x))
        {
            if (transform.position.x > player.position.x)
            {
                sr.sprite = left;
            }
            if (transform.position.x < player.position.x)
            {
                sr.sprite = right;
            }
        } else
        {
            if (transform.position.y > player.position.y)
            {
                sr.sprite = down;
            }
            if (transform.position.y < player.position.y)
            {
                sr.sprite = up;
            }
        }
        if (!frozen)
        {
            rb.AddForce((currentTarget.position - transform.position) / 10);
        } else
        {
            sr.sprite = emb;
            rb.velocity = Vector3.zero;
        }
        if (transform.position == currentTarget.position)
        {
            currentTarget = player;
        }
    }

    public void GoToQuadrant(GameObject q)
    {
        currentTarget = q.transform;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Flashlight")
        {
            //print(collision.gameObject.name);
            frozen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        frozen = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //print("g");
        if(collision.gameObject.layer != default)
        {collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-x, x), Random.Range(-x, x), 0));
            rb.AddForce(new Vector3(Random.Range(-x, x), Random.Range(-x, x), 0));
        }
    }
}
