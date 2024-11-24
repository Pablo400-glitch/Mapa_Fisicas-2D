using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    public float velocity = 5f;
    public float thrust = 5f;
    public AudioSource jumpUpSound;
    public AudioSource jumpDownSound;
    public AudioSource objectRecolectionSound;
    public AudioSource ambientSound;
    public AudioClip ambient;
    public AudioClip newAmbient;

    private bool isJumping = false;

    private Rigidbody2D rb2D;
    private SpriteRenderer spriteRenderer;

    public delegate void ItemCollected(int totalItemsCollected);
    public event ItemCollected onItemCollected;

    private int itemsCollected = 0;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        float moveH = Input.GetAxis("Horizontal");

        if (Input.GetButton("Jump") && !isJumping)
        {
            rb2D.AddForce(transform.up * thrust);
            jumpUpSound.Play();
            isJumping = true;
        }


        if (moveH > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveH < 0)
        {
            spriteRenderer.flipX = false;
        }

        Vector2 vtranslate = new Vector2(moveH * velocity * Time.deltaTime, 0);
        rb2D.MovePosition(rb2D.position + vtranslate);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor") || other.gameObject.CompareTag("Platform"))
        {
            jumpDownSound.Play();
            isJumping = false;
            rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
        }

        if (other.gameObject.CompareTag("Platform"))
        {
            transform.parent = other.transform;  // Hacer al jugador hijo de la plataforma
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("PlatInv"))
        {
            other.gameObject.GetComponent<TilemapRenderer>().enabled = true;
        }

        if (other.gameObject.CompareTag("Item"))
        {
            objectRecolectionSound.Play();
            AddItem(other);
        }
    }

    private void AddItem(Collision2D item)
    {
        itemsCollected++;
        if (onItemCollected != null)
        {
            onItemCollected(itemsCollected);
        }
        onItemCollected.Invoke(itemsCollected);
        Destroy(item.gameObject);
    }

    // Detectar cuando sale de la plataforma
    private void OnCollisionExit2D(Collision2D collision)
    {
        // Si sale de la plataforma, desasociar el jugador
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.parent = null;  // Desvincular al jugador de la plataforma
        }

        if (collision.gameObject.layer != LayerMask.NameToLayer("NoCollis"))
        {
            Debug.Log("Colisión detectada con un objeto en una capa válida.");
        }
        else
        {
            Debug.Log("Colisión ignorada con un objeto de la capa 'NoCollis'.");
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("PlatInv"))
        {
            collision.gameObject.GetComponent<TilemapRenderer>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NewAmbient"))
        {
            ambientSound.clip = newAmbient;
            ambientSound.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NewAmbient"))
        {
            ambientSound.clip = ambient;
            ambientSound.Play();
        }
    }
}
