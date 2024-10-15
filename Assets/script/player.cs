using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    //PUBLIC
    [Header("Movement Settings")]
    public float speed = 10f;

    [Header("Jump Settings")]
    public float jump = 10f;
    public bool doubleJump = false;
    public LayerMask groundLayer = 0;
    //public bool wallJump = false;

    [Header("Movement Settings")]
    public levelManager manager = null;


    // PRIVATE
    private bool grounded = true;
    private bool hasDoubleJump = false;
    //private int walled = 0;

    private Rigidbody2D rb = null;
    private Camera cam = null;
    private SpriteRenderer sprite = null;




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        cam = Camera.main;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("danger"+other.name);
        print("danger"+other.tag);
        // Si l'objet qui entre dans le trigger a un tag spécifique, on redémarre le niveau
        if (other.CompareTag("enemy"))
        {
            Scene currentScene = SceneManager.GetActiveScene();

            // Recharge la scène actuelle
            SceneManager.LoadScene(currentScene.name);
        }
    }




    private void Update()
    {
        Jump();
        //WallJump ?
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump")
            && (grounded
            || hasDoubleJump))
        {
            if (grounded)
                grounded = false;
            else
                hasDoubleJump = false;
            rb.velocityY = jump;
        }
        if (Input.GetButtonUp("Jump") && rb.velocityY > 0)
        {
            rb.velocityY *= 0.5f;
        }
    }



    private void FixedUpdate()
    {
        Movement();
        CameraPos();
    }

    void Movement()
    {
        if (Input.GetAxis("Horizontal") < 0 == sprite.flipX)
            sprite.flipX = !sprite.flipX;


        rb.velocityX = Input.GetAxis("Horizontal") * speed;
    }

    void CameraPos()
    {
        if (manager == null)
            return;

        float vertExtent = cam.orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, horzExtent + manager.cameraBoundsX.x, manager.cameraBoundsX.y - horzExtent);
        pos.y = Mathf.Clamp(pos.y, vertExtent + manager.cameraBoundsY.x, manager.cameraBoundsY.y - vertExtent);
        pos.z = -10;
        cam.transform.position = pos;
    }



    private void OnCollisionStay2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            //Debug.DrawRay(contact.point, contact.normal * 10, Color.red);
            if ((contact.normal.y > 0.1f && rb.velocityY <= 0)
                /*&& collision.gameObject.layer == groundLayer*/)
            {
                grounded = true;
                hasDoubleJump = doubleJump;
            }
            //if ((contact.normal.y == 0 && wallJump)
            //    //&& collision.transform.tag == "Ground")
            //    )
            //{
            //    walled = contact.normal.x > 0 ? 1 : -1;
            //}
        }
    }
}
