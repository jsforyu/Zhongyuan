using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartGame1 : MonoBehaviour
{
    public BagDataSO bag;
    // Start is called before the first frame update

    public void startGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Continue()
    {
        int temp = bag.sceneid;
        SceneManager.LoadScene(temp);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
