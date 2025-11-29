using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    //to set up a gun animation
    [SerializeField] private Animator gunanime;
    //for gun transform(position,rotation,scale)
    [SerializeField] private Transform gun;
    //for distance between player and the gun 
    [SerializeField] private float gundist = 1.5f;

    //for gun's rotation
    private bool facingright=true;

    [Header("Bullet")]
    //to set up bullet's prefab to make it spawn whenever user give command
    [SerializeField] private GameObject bulletprefab;
    //to set up bullet travel speed
    [SerializeField] private float bulletspeed;
    //specific time when bullet gets destroy to free up that memory 
    [SerializeField] private float bulletdestroy;
    //amount of ammo gun left
    public int currentbullet;
    //total amount of bullet has
    public int maxbullet;

    private void Start()
    {
        //reload gun once when the game starts
        reload();

    }
    void Update()
    {
        //to reload the gun
        if (Input.GetKeyDown(KeyCode.R))
        {
            reload();
        }

        //getting mouse position 
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //to shot at where mouse/hand is pointed
        Vector3 direction = mousepos - transform.position;

        //to make gun rotate into scare with math formula
        gun.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));

        //to set gun position with math formula
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gun.position = transform.position + Quaternion.Euler(0, 0, angle) * new Vector3(gundist, 0, 0);


        //setting up mouse left click to shoot bullet
        if (Input.GetKeyDown(KeyCode.Mouse0) && havebullet())
        {
            //to spawn a bullet and shot it into the mouse position
            Shoot(direction);
        }
        //change gun's rotation
        gunflipcontroller(mousepos);
    }


    //to change gun's roation according to which side mouse is pointing 
    private void gunflipcontroller(Vector3 mousepos)
    {
        if (mousepos.x < gun.position.x && facingright)
        {
            //for gun's rotation
            gunflip();

        }
        else if (mousepos.x > gun.position.x && !facingright)
        {
            gunflip();
        }
    }

    //rotate gun
    private void gunflip()
    {
        facingright = !facingright;
        gun.localScale = new Vector3(gun.localScale.x, gun.localScale.y * -1, gun.localScale.z);
    }

    //to shoot bullet
    public void Shoot(Vector3 direction)
    {
        //gun's animation when left click is pressed
        gunanime.SetTrigger("shoot");

        //to spawn a new bullet
        GameObject newbullet = Instantiate(bulletprefab, gun.position, Quaternion.identity);
        //to set up a speed for bullet
        newbullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * bulletspeed;

        //destroy bullet 
        Destroy(newbullet,bulletdestroy);

    }
    //to reload ammo
    private void reload()
    {
        currentbullet = maxbullet;
    }
    
    //if gun doesnt have ammo it wont spawn any bullet
    public bool havebullet()
    {
        if (currentbullet <= 0)
        {
            return false;
        }

        currentbullet--;
        return true;
    }
}
