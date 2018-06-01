using UnityEngine;

public class BlockMover : MonoBehaviour {

    /* I made this to work entirely on varibles allowing movement,
     along either the x or the y axis and between any two co-ordinates,
     this means that it is entirely configurable and until I decide that
     I want it to change directions or move diagonally then I can use this
     for all of my blocks.
     
     As a side note, this doesn't alternate between moving to the points specified,
     but instead moves along the specified axis until it hits the point defined,
     where it is told to go the opposite way*/

    public bool moveInX;
    public bool moveforwards;
    public float max;
    public float min;
    public float speed;

	void Update () {
        //Returns if the block is moving along the x axis
        switch (moveInX)
        {
            //Passes if the above switch statement returns true
            case true:
                //Returns if the block is moving forwards (right)
                switch (moveforwards)
                {
                    //Passes if the above switch statement returns true
                    case true:
                        //Returns true if the x coordinate of the block is less than the designated maximum coordinate
                        if (transform.position.x < max)
                        {
                            //Moves the block right by the amount specified by the variable speed
                            transform.position = new Vector2(transform.position.x + speed, transform.position.y);
                        }
                        //Returns true if the x coordinate of the block is more than or equal to the designated maximum coordinate
                        else if (transform.position.x >= max)
                        {
                            //Tells the block to move backwards
                            moveforwards = false;
                        }
                        //Exits the most recently run switch statement
                        break;

                    //Passes if the above switch statement returns false
                    case false:
                        if (transform.position.x > min)
                        {
                            //Moves the block left by the amount specified by the variable speed
                            transform.position = new Vector2(transform.position.x - speed, transform.position.y);
                        }
                        else if (transform.position.x <= min)
                        {
                            //Tells the block to move forwards
                            moveforwards = true;
                        }
                        break;
                }
                break;

            //Passes if the above swith statement returns false
            case false:
                //Returns if the block is moving forwards (up)
                switch (moveforwards)
                {
                    //Passes if the above switch statement returns true
                    case true:
                        if (transform.position.y < max)
                        {
                            //Moves the block up by the amount specified by the variable speed
                            transform.position = new Vector2(transform.position.x, transform.position.y + speed);
                        }
                        else if (transform.position.y >= max)
                        {
                            //Tells the block to move downwards
                            moveforwards = false;
                        }
                        break;

                    //Passes if the above switch statement returns false
                    case false:
                        if (transform.position.y > min)
                        {
                            //Moves the block down by the amount specified by the variable speed
                            transform.position = new Vector2(transform.position.x, transform.position.y - speed);
                        }
                        else if (transform.position.y <= min)
                        {
                            //Tells the block to move upwards
                            moveforwards = true;
                        }
                        break;
                }
                break;
        }
	}
}
