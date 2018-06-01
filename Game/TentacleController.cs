using UnityEngine;
using UnityEngine.SceneManagement;

public class TentacleController : MonoBehaviour {

    public GameObject tentacleOrigin;
    public LineRenderer line;
    private RaycastHit2D hit;
    public float tentacleFrequency;
    public float swingSpeed;
    private SpringJoint2D tentacle;
    public Color tentacleColour;
    public float speed;
    [HideInInspector]
    public bool paused;

    void Update()
    {
        //Passes if the rigidbody is not frozen
        if (gameObject.GetComponent<Rigidbody2D>().constraints != RigidbodyConstraints2D.FreezeAll)
        {
            //Passes if the mouse button is down (0 is the left mouse button)
            if (Input.GetMouseButtonDown(0) && paused == false)
            {
                //Invokes the fire method
                fire();
            }

            //Passes if the right mouse button is pressed while the game isn't paused
            if (Input.GetMouseButtonDown(1) && paused == false)
            {
                //Invokes the breakTentacle method
                breakTentacle();
            }

            //Passes if the w key is being pressed down
            if (Input.GetKey("w"))
            {
                //Passes if the tentacle variable has a value assigned
                if (tentacle != null)
                {
                    //Invokes the rappelUp method
                    rappelUp();
                }
            }

            //Passes if the s key is being pressed down
            if (Input.GetKey("s"))
            {
                //Passes if the tentacle variable has a value assigned
                if (tentacle != null)
                {
                    //Invokes the rappelDown method
                    rappelDown();
                }
            }

            //Passes if the a key is being pressed down
            if (Input.GetKey("a"))
            {
                //Passes if the tentacle variable has a value assigned
                if (tentacle != null)
                {
                    //Invokes the swingLeft method
                    swingLeft();
                }
            }

            //Passes if the d key is being pressed down
            if (Input.GetKey("d"))
            {
                //Passes if the tentacle variable has a value assigned
                if (tentacle != null)
                {
                    //Invokes the swingRight method
                    swingRight();
                }
            }
        }

        //Passes if the rigidbody is frozen
        if (gameObject.GetComponent<Rigidbody2D>().constraints == RigidbodyConstraints2D.FreezeAll)
        {
            //Deletes the tentacle
            DestroyImmediate(tentacle);
        }
    }

    void LateUpdate()
    {
        //Runs if there is a tentacle active
        if (tentacle != null)
        {
            //Enables the lineRenderer component on the player
            line.enabled = true;
            //Sets the amount of connections the line will draw between
            line.positionCount = 2;
            //Sets the position of the first connection to the player object
            line.SetPosition(0, tentacleOrigin.transform.position);
            //Sets the position of the second connection to the spring joint
            line.SetPosition(1, tentacle.connectedBody.transform.localPosition);
            //Assigns the line a material
            line.material = new Material(Shader.Find("Particles/Additive"));
            //Sets the width at the start of the line
            line.startWidth = .05f;
            //Sets the width at the end of the line
            line.endWidth   = .05f;
            //Sets the colour of the line
            line.material.color = tentacleColour;
        }
        else
        {
            //Disables the lineRenderer component
            line.enabled = false;
        }
    }
    
    private void fire()
    {
        //Finds the position of the mouse on the screen and assigns it to a vector
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Finds the player object's position and assigns it to a vector
        Vector3 position = tentacleOrigin.transform.position;
        //Finds the direction to fire the raycast from as a vector 
        Vector3 direction = mousePos - position;

        //Sends out a raycast from the player towards the mouse with no limit to the length of the cast
        hit = Physics2D.Raycast(position, direction, 3f); 

        //Only runs if the raycast hit something
        if (hit.collider != null)
        {
            //Passes if the name of the object the raycast hit is "WinBlock"
            if (hit.transform.gameObject.name == "WinBlock")
            {
                //Returns to the line after the one invoking the current method
                return;
            }
            //Adds a SpringJoint component to the player and assigns it to a variable
            SpringJoint2D newTentacle = tentacleOrigin.AddComponent<SpringJoint2D>();
            //Disables the collision of the spring joint
            newTentacle.enableCollision = false;
            //Changes the frequency of the spring joint (essentially how springy it is)
            newTentacle.frequency = tentacleFrequency;
            //Sets the anchor point of the spring joint to the point that the raycast hit
            newTentacle.connectedBody = hit.transform.gameObject.GetComponent<Rigidbody2D>();
            //Turns off the automatic distance settings
            newTentacle.autoConfigureDistance = false;
            //Turns on the tentacle
            newTentacle.enabled = true;

            //Destroys the previous tentacle
            DestroyImmediate(tentacle);
            //Sets the new tentacle to the old spring joint
            tentacle = newTentacle;
        }
    }

    private void breakTentacle()
    {
        //Passes if there is a value assigned to tentacle
        if (tentacle != null)
        {
            //Deletes the tentacle
            DestroyImmediate(tentacle);
            //Turns off the line renderer used to visually show the tentacle in game
            line.enabled = false;
        }
    }

    private void rappelUp()
    {
        //Passes if the tentacle is more than .55 units long (the minimum distance before the player clips into the blocks)
        if (tentacle.distance > .55f)
        {
            //Shortens the length of the tentacle
            tentacle.distance -= speed;
        }
    }

    private void rappelDown()
    {
        //Passes if the tentacle is less than 3 units long (I chose this number because any longer would allow you to attatch to blocks that weren't on the screen)
        if (tentacle.distance < 3f)
        {
            //Lengthens the tentacle
            tentacle.distance += speed;
        }
    }

    private void swingLeft()
    {
        //Adds a force pushing left onto the player equal to the swingSpeed variable
        tentacleOrigin.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-swingSpeed, 0));
    }

    private void swingRight()
    {
        //Adds a force pushing right onto the player equal to the swingSpeed variable
        tentacleOrigin.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(swingSpeed, 0));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Passes if the collided object has the Obstacle tag
        if (other.tag == "Obstacle")
        {
            //Assigns the player's velocity to a variable
            Vector2 playerVelocity = tentacleOrigin.gameObject.GetComponent<Rigidbody2D>().velocity;
            //Adds a force backwards equal to 5.75 times more than the player's velocity
            //5.75 was worked out through trial and error, it still allows the gaining of velocity
            //but I couldn't find the amount that wouldn't gain or lose velocity and gaining it is better than losing it
            tentacleOrigin.gameObject.GetComponent<Rigidbody2D>().AddForce(-playerVelocity * 5.75f);
        }

        //Passes if the collided object has the Deathbox tag
        else if (other.tag == "Deathbox")
        {
            //Resets the scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}