using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    //to take multiple target picture
    [SerializeField] private Sprite[] targetsprite;
    //to make target spawn from there
    [SerializeField] private BoxCollider2D cd;
    //taking prefab of the target
    [SerializeField] private GameObject prefab;
    //interval between spawn
    [SerializeField] private float cooldown;
    //to set the time
    private float timer;
    //
    private int sushicreated;
    //when 10 sushi gets hit the speed of spawn gets faster
    private int sushimilestone = 10;
    void Update()
    {
        timer -= Time.deltaTime;

        //to make sushi spawn in specific interval
        if (timer < 0)
        {
            timer = cooldown;
            sushicreated++;
            //decrease sushi spawn time require
            if (sushicreated > sushimilestone && cooldown > .5f)
            {
                sushimilestone += 10;
                cooldown -= .3f;
            }
            
            GameObject newtarget = Instantiate(prefab);
            //spawn at random position in bettwen given cords 
            float randomX = Random.Range(cd.bounds.min.x, cd.bounds.max.x);
            newtarget.transform.position = new Vector2(randomX, transform.position.y);

            
            int randomIndex = Random.Range(0, targetsprite.Length);
            newtarget.GetComponent<SpriteRenderer>().sprite = targetsprite[randomIndex];
        }

    }
}
