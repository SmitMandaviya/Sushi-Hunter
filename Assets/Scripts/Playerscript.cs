using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
//using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

public class Playerscript : MonoBehaviour
{
   //player's rigib body var
    private Rigidbody2D rb;

    //player's movement var
    private float xinput;


    //for animation activate and deactivate
    private Animator anime;

    //for player movement speed
    [SerializeField] private float movespeed;

    //how much player can jump
    [SerializeField] public float jumpforce;

    


    [Header("Collisioncheck")]

    //created gizmo so player can only jump when its on the ground
    [SerializeField] private Transform gizmo;

    //for radius of gizmo
    [SerializeField] private float groundradious;


    [SerializeField] private LayerMask Surface;

    //to detect if player is touching ground or not 
    private bool detector;

    //to make player rotate if hes facing right ( later it switch to gun's position )
    private bool facingright = true;



    void Start()
    {
        //to get player rigidbody through script
        rb = GetComponent<Rigidbody2D>();

        //to get player's animator/animation mostly done so that script doesnt take too much space and make it look clean 
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if player is idle it will trigger idle animation 
        animationcontroller();

        
        collissioncheck();

        //if player (gun) changes direction player will rotate to that side 
        flipcontroller();

        //to make player move left and right / horizontally / default input system that uses A,D and left right arrow to make player move to respecting position
        xinput = Input.GetAxisRaw("Horizontal");

        //
        move();

        //to make player jump when space is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump();
        }

    }

    //if player (gun) facing in right or left make player rotate into respecting position 
    private void flipcontroller()
    {
        //to get mouse position if its right or left
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //if its right player will flip and face right 
        if(mousepos.x < transform.position.x && facingright == true)
        {
            flip();
        }

        //if its left player will flip again to face left 
        else if(mousepos.x > transform.position.x && !facingright)
        {
            flip();
        }
    }

    //to make player rotate 180 whenever mouse move to left or right side position
    private void flip()
    {
        facingright = !facingright;
        transform.Rotate(0,180,0);
    }

    //for player animationsd
    private void animationcontroller()
    {
        anime.SetFloat("xvelocity",rb.velocity.x);
        anime.SetFloat("yvelocity",rb.velocity.y);

        //this detector is different not related to the var created
        //to change the player animation from idle/move to jump/fall
        anime.SetBool("detector",detector);
    }

    //for playermovment speed
    private void move()
    {
        rb.velocity = new Vector2(xinput * movespeed, rb.velocity.y);
    }

    //to make player jump when conditions are met
    private void jump()
    {
        if (detector)
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
    }

    //to draw gizmo wiresphere
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(gizmo.position, groundradious);
    }

    //to check if gizmo is touched to the surface or not
    private void collissioncheck()
    {
        detector = Physics2D.OverlapCircle(gizmo.position, groundradious, Surface);
    }

}
