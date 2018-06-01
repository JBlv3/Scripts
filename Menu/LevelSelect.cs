using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {

    public GameObject selector;
    public GameObject psyche;

    public void Start()
    {
        //Turns on the level selection buttons
        selector.SetActive(true);
        //Turns off the psyche canvas (entirely a joke, no useful functions)
        psyche.SetActive(false);
    }

    //Run via button
    public void LoadLevel1()
    {
        //Loads the first Level
        SceneManager.LoadScene("Level 1");   
    }

    //Run via button
    public void LoadLevel2()
    {
        //Loads the second Level
        SceneManager.LoadScene("Level 2");
    }

    //Run via button
    public void LoadLevel3()
    {
        //Loads the third Level
        SceneManager.LoadScene("Level 3");
    }

    //Run via button
    public void MainMenu()
    {
        //Loads the title menu
        SceneManager.LoadScene("Title Menu");
    }

    //Run via button
    public void BonusLevel()
    {
        //Turns off the levels selection buttons
        selector.SetActive(false);
        //Turns on the psyche canvas
        psyche.SetActive(true);
    }
}
