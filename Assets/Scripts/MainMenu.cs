using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string mapScene;

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
    public void StartNewGame()
    {
        SceneManager.LoadScene(mapScene);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}