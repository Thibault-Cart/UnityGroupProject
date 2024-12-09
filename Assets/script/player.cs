using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    //PUBLIC
    [Header("Movement Settings")]
    public float speed = 10f;
    public ParticleSystem run;

    [Header("Jump Settings")]
    public float jumpForce = 10f;
    public float jumpBuffer = 1;
    public LayerMask groundLayer = 0;

    [Header("Movement Settings")]
    public levelManager manager = null;

    [Header("Misc")]
    public GameObject activeCheckpoint = null;
    public Material damageMat;

    // Movement for mobile app
    [HideInInspector]
    public float hor = 0;
    [HideInInspector]
    public bool jump = false;
    private bool lastJump = false;

    // PRIVATE
    private float damage = 0;
    private float grounded;
    private Rigidbody2D rb = null;
    private Camera cam = null;
    private SpriteRenderer sprite = null;
    private Vector3 respawnPoint;
    private GameObject checkpoint = null;
    private int mobileCheckTime = 1;
    private bool mobileCheck = false;




    void Start()
    {
        respawnPoint = transform.position;
        grounded = Time.time;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        cam = Camera.main;
        damageMat.SetFloat("_dark", 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print(name + " has hit " + other.name + " of type " + other.tag);
        // Si l'objet qui entre dans le trigger a un tag spécifique, on redémarre le niveau
        if (other.CompareTag("enemy"))
        {
            //Scene currentScene = SceneManager.GetActiveScene();

            // Recharge la scène actuelle
            //SceneManager.LoadScene(currentScene.name);
            transform.position = respawnPoint;
            damage = 0.8f;
        }
        if (other.CompareTag("checkpoint"))
        {
            respawnPoint = other.transform.position;
            if (checkpoint != null)
                Destroy(checkpoint);
            if (activeCheckpoint != null)
            {
                checkpoint = Instantiate(activeCheckpoint, respawnPoint + new Vector3(0, 0, -1), other.transform.rotation);
            }
        }
        if (other.CompareTag("win"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }




    private void Update()
    {
        if (mobileCheckTime > 0)
        {
            mobileCheckTime--;
            if (mobileCheckTime == 0 && (UnityEditor.EditorApplication.isRemoteConnected || Application.isMobilePlatform))
                mobileCheck = true;
        }
        Jump();
    }

    void Jump()
    {
        if (!mobileCheck)
            jump = Input.GetButton("Jump");
        if (jump
            && (grounded > Time.time))
        {
            grounded = 0;
            rb.velocityY = jumpForce;
        }
        if (!jump && lastJump && rb.velocityY > 0)
        {
            rb.velocityY *= 0.5f;
        }
        lastJump = jump;
    }



    private void FixedUpdate()
    {
        if (damage > 0)
        {
            damageMat.SetFloat("_dark", damage);
            damage -= 0.01f;
        }
        Movement();
        CameraPos();
    }

    void Movement()
    {
        if (!mobileCheck)
            hor = Input.GetAxis("Horizontal");
        if (hor < 0 == sprite.flipX)
            sprite.flipX = !sprite.flipX;

        if (hor != 0 && !run.isPlaying)
            run.Play();
        if (hor == 0 && run.isPlaying)
            run.Stop();

        rb.velocityX = hor * speed;
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
            if (contact.normal.y >= 0f)
            {
                grounded = Time.time + jumpBuffer;
            }
        }
    }
}
