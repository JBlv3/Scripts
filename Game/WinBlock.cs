using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinBlock : MonoBehaviour {

    public Text winText;
    private string level;

    /*Since there are only 3 levels I loop back to level 1 after level 3, it is intentional
      The levels will keep repeating until you go to the title through the pause menu*/

    void Start()
    {
        //Removes any constraints that might still be on the player
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        //Disables the text shown when a level is completed
        winText.enabled = false;
    }

    void Update()
    {
        //Only passes if the rigidbody is completely frozen
        if (gameObject.GetComponent<Rigidbody2D>().constraints == RigidbodyConstraints2D.FreezeAll)
        {
            //Only passes if the left mouse button is pressed down
            if (Input.GetMouseButton(0))
            {
                //Finds the name of the currently active scene (will only be one of the levels)
                level = SceneManager.GetActiveScene().name;
                //Checks the level variable
                switch (level)
                {
                    //If the above switch statement returns "Level 1"
                    case "Level 1":
                        //Loads the second level
                        SceneManager.LoadScene("Level 2");
                        break;

                    //If the above switch statement returns "Level 2"
                    case "Level 2":
                        //Loads the third level
                        SceneManager.LoadScene("Level 3");
                        break;

                    //If the above switch statement returns "Level 3"
                    case "Level 3":
                        //Loads the first level
                        SceneManager.LoadScene("Level 1");
                        break;
                }
            }
        }
    }

    /*Runs if the object with this script hits another object and assigns the object
      collided with to the variable other*/
	void OnTriggerEnter2D(Collider2D other)
    {
        //Passes if the name of the collided object is "WinBlock"
        if (other.name == "WinBlock")
        {
            //Turns on all constraints on the rigidbody
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            //Enables the win text
            winText.enabled = true;
        }
    }
}
