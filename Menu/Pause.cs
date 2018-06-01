using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

    private GameObject panel;
    private GameObject tint;
    private GameObject tentCont;
    public Canvas quitAsk;
    private bool paused = false;

	private void Start () {
        //Sets the variable panel to the second child of the gameobject this script is run from (in this case the panel that shows the buttons)
        panel = gameObject.transform.GetChild(1).gameObject;
        //Turns off the panel
        panel.SetActive(false);
        //Sets the variable panel to the first child of the gameobject this script is run from (in this case the panel that has the tinting animation)
        tint = gameObject.transform.GetChild(0).gameObject;
        //Turns off the panel
        tint.SetActive(false);
        //Finds the gameobject called "Cthulhu" and assigns it to a variable
        tentCont = GameObject.Find("Cthulhu"); 
	}

    //Runs at the end of each frame
    private void LateUpdate()
    {
        //Returns true if the escape key is pressed while the game is not paused
        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            //Turns on the pause menu buttons
            panel.SetActive(true);
            //Turns on the tint panel as well as it's effects
            tint.SetActive(true);
            //Sets the boolean pause to true
            paused = true;
            //Sets the scale of time to 0 (for every 1 real world second 0 in game seconds pass)
            Time.timeScale = 0f;
            //Sets to true the paused boolean of the TentacleController script in the object contained in the tentCont variable
            tentCont.GetComponent<TentacleController>().paused = true;
        } 
        //Returns true if the escape key is pressed while the game is paused
        else if (Input.GetKeyDown(KeyCode.Escape) && paused)
        {
            //Runs the part of the script that unpauses
            Resume();
        }
    }

    //Run via button
    public void Resume()
    {
        //Undoes all everything to do with pausing
        panel.SetActive(false);
        tint.SetActive(false);
        paused = false;
        Time.timeScale = 1f;
        tentCont.GetComponent<TentacleController>().paused = false;
    }

    //Run via button
    public void GoToMenu()
    {
        //Runs the part of the script that unpauses
        Resume();
        //Loads the title menu
        SceneManager.LoadScene("Title Menu");
    }

    //Run via button
    public void RestartLevel()
    {
        //Runs the part of the script that unpauses
        Resume();
        //Resets the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Run via button
    public void QuitAsk()
    {
        //Enables the canvas confirming quitting the game
        quitAsk.enabled = true;
        //Turns off the buttons of the pause menu
        panel.SetActive(false);
    }

    //Run via button
    public void QuitYes()
    {
        //Ends the application Process
        Application.Quit();
    }

    //Run via button
    public void QuitNo()
    {
        //Goes back to the regular pause menu
        quitAsk.enabled = false;
        panel.SetActive(true);
    }
}

