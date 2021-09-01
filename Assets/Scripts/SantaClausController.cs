using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaClausController : MonoBehaviour
{

    public float velocityX = 5;
    public float jumpForce = 100;

    // Start is called before the first frame update
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;

    //properties

    private bool isIntangible = false;
    private float intangibleTime = 0f;

    // constantes
    private const int IDLE = 0;
    private const int RUN = 1;    
    private const int SLIDE = 2;
    private const int JUMP = 3;

    private const string TAG_ENEMY = "Enemy";
    private const string TAG_VIDA = "Vida";
    private const int LAYER_GROUND = 10;

    //private bool isJumping = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {   

        rb.velocity = new Vector2(0, rb.velocity.y);
        changeAnimation(IDLE);

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(velocityX , rb.velocity.y);
            sr.flipX = false;
            changeAnimation(RUN);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-velocityX, rb.velocity.y);
            sr.flipX = true;
            changeAnimation(RUN);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            changeAnimation(SLIDE);
        }

        if (Input.GetKeyDown(KeyCode.Space) /*&& !isJumping*/)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            changeAnimation(JUMP);
            //isJumping = true;
        }

        if (isIntangible && intangibleTime < 2f)
        {
            intangibleTime += Time.deltaTime;
            sr.enabled = !sr.enabled;

        }else if(isIntangible && intangibleTime >= 2f){

            isIntangible = false;
            sr.enabled = true;
            intangibleTime = 0f;
        }
               
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LAYER_GROUND)
        {
            Debug.Log("Collision: " + collision.gameObject.name);
        }

        if (collision.gameObject.CompareTag(TAG_ENEMY))
        {
            transform.localScale = new Vector3(0.6f, 0.6f, 1);
            isIntangible = true;
        }

        if (collision.gameObject.CompareTag(TAG_VIDA))
        {
            transform.localScale = new Vector3(1, 1, 1);
            Destroy(collision.gameObject);
        }
    }

    /*private void OnCollisionExit2d(Collision2D other)
    {

    }

    private void OnCollisionStay2d(Collision2D other)
    {


    }
      

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Suelo")
            isJumping = false;
    }*/


    private void changeAnimation(int animation)
    {
        animator.SetInteger("Estado", animation);
    }
}
