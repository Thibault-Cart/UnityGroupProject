using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float speed = 10f;

    public float jump = 10f;
    [SerializeField]
    private bool grounded = true;
    public float jumpDuration = 0.2f;
    private float jumpEnd = 0;
    public bool wallJump = false;
    public bool doubleJump = false;
    private bool hasDoubleJump = false;

    public levelManager manager = null;
    private Rigidbody2D rb = null;
    private Camera cam = null;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    void Update()
    {
        Movement();
        CameraPos();
    }

    void Movement()
    {

        Vector2 newSpeed = rb.velocity;

        if (Input.GetAxis("Jump") == 0
            && jumpEnd > Time.time)
        {
            jumpEnd = Time.time;
        }
        if (Input.GetAxis("Jump") != 0
            && (grounded
            || hasDoubleJump))
        {
            if (grounded)
                grounded = false;
            else
                hasDoubleJump = false;
            jumpEnd = Time.time + jumpDuration;
            newSpeed.y = 0;
        }
        if (jumpEnd > Time.time)
        {
            newSpeed.y += jump * Time.fixedDeltaTime;
        }

        if (speed < newSpeed.x || newSpeed.x < speed)
            newSpeed.x += Input.GetAxis("Horizontal") * speed * Time.fixedDeltaTime;

        rb.velocity = newSpeed;
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
            if (contact.point.y < contact.normal.y
                //&& collision.transform.tag == "Ground"
                || (contact.normal.y == 0 && wallJump))
            {
                grounded = true;
                hasDoubleJump = false;
            }
        }
    }
}
