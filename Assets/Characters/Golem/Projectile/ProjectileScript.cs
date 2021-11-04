using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Launch Parameters")]
    [SerializeField]
    [Range(0f,5f)]
    private float launchSpeed;

    private Rigidbody2D rb;
    private GameObject golem;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        golem = GameObject.FindGameObjectWithTag("Golem");
        GolemController golemController= golem.GetComponent<GolemController>();
        if (golemController.facingRight)
        {
            rb.velocity = new Vector2(launchSpeed, rb.velocity.y);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            rb.velocity = new Vector2(-launchSpeed, rb.velocity.y);
            gameObject.GetComponent<SpriteRenderer>().flipX=false;
        }
            
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("target"))
        {
            Debug.Log("Target reached");
            Destroy(this.gameObject);
        }
    }
}
