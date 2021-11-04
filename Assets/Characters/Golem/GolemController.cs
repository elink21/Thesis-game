using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemController : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Movement Parameters")]
    [SerializeField]
    [Range(0f, 10f)]
    private float movementSpeed;
    [SerializeField]
    [Range(1, 3)]
    private int speedMultiplier;

    [Header("Projectile Parameters")]
    
    public Transform spawnPoint;
    
    public GameObject bulletPrefab;


    //Internal components
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    private Transform tr;


    //Directional flags
    public bool facingRight = true;


    //Cooldown times
    private float nextShootingTime=0;
    private float shootingCooldown = 0.8f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        tr = GetComponent<Transform>();
    }

    void cancelMovement()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
    }


    void moveRight()
    {
        sr.flipX = false;
        facingRight = true;
        spawnPoint.localPosition = new Vector2(1.173f, spawnPoint.localPosition.y);
        rb.velocity = new Vector2(movementSpeed * speedMultiplier, rb.velocity.y);
    }

    void moveLeft()
    {
        sr.flipX = true;
        facingRight = false;
        spawnPoint.localPosition = new Vector2(-1.173f, spawnPoint.localPosition.y);
        rb.velocity = new Vector2(-1* movementSpeed * speedMultiplier, rb.velocity.y);
    }

    IEnumerator attack()
    {
        anim.SetTrigger("attacked");

        yield return new WaitForSeconds(0.5f);
        Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
    }
    // Update is called once per frame
    void Update()
    {

       if(Input.GetKeyDown(KeyCode.C))
        {
            if(Time.time> nextShootingTime)
            {
                StartCoroutine(attack());
                nextShootingTime = Time.time + shootingCooldown;
            }
        }

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            moveLeft();
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            moveRight();
        }



        else
        {
            cancelMovement();
        }
    }
}
