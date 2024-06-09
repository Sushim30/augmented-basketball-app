using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    public void start()
    {
        SceneManager.LoadScene(1);
    }

  public  void quit()
    {
        Application.Quit();
    }
   
    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
