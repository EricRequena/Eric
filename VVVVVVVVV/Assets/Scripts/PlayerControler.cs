using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public float velocidad;
    public float velocidadMax;

    private Rigidbody2D rPlayer;
    private float h;

    private bool facingRight = true;
    private bool isOnCeiling = false;
    private bool isGrounded = false;
    private Renderer playerRenderer;

    public GameObject canvasNegro;


    private bool IsTouch = false;
    private float tiempoInicio;

    void Start()
    {
        rPlayer = GetComponent<Rigidbody2D>();
        playerRenderer = GetComponent<Renderer>();

        if (canvasNegro != null)
        {
            canvasNegro.SetActive(false);
        }
        tiempoInicio = Time.time;
    }

    void Update()
    {
        float tiempoTranscurrido = Time.time - tiempoInicio;
        Flip(h);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            FlipGravity();
        }

        if ((!playerRenderer.isVisible && tiempoTranscurrido>= 2) || IsTouch)
        {
            canvasNegro.SetActive(true);
        }
        else if (playerRenderer.isVisible || !IsTouch)
        {
            canvasNegro.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        h = Input.GetAxis("Horizontal");
        rPlayer.AddForce(Vector2.right * velocidad * h);
    }

    public void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    public void FlipGravity()
    {
        isOnCeiling = !isOnCeiling; 
        rPlayer.gravityScale *= -1; 
        transform.Rotate(0f, 180f, 180f);
        
    }

   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            isGrounded = true;

        }

        if (collision.gameObject.CompareTag("Avion"))
        {
            IsTouch = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            isGrounded = false;
        }

        if (collision.gameObject.CompareTag("Avion"))
        {
            IsTouch = false;
        }
    }
}
