using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour {

    public GameObject mainCanvas;
    public GameObject quitCanvas;

    private void Start()
    {
        //Enables the main canvas
        mainCanvas.SetActive(true);
        //Disables the canvas confirming quitting
        quitCanvas.GetComponent<Canvas>().enabled = false;
    }

    //Run via button
	public void Levels()
    {
        //Loads the level selection screen
        SceneManager.LoadScene("Levels");
    }

    //Run via button
    public void QuitAsk()
    {
        //Disables the main canvas
        mainCanvas.SetActive(false);
        //Enables the canvas confirming quitting
        quitCanvas.GetComponent<Canvas>().enabled = true;
    }

    //Run via button
    public void YesQuit()
    {
        //Ends the application process
        Application.Quit();
    }

    //Run via button
    public void NoQuit()
    {
        //Enables the main canvas
        mainCanvas.SetActive(true);
        //Disables the canvas confirming quitting
        quitCanvas.GetComponent<Canvas>().enabled = false;
    }
}
