using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneMGRScript : MonoBehaviour
{
    static SceneMGRScript instance;
      

    //properties
    public static SceneMGRScript Instance { get => instance; } 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }


    private void Start()
    {
        LoadNextScene();
    }


    public void LoadNextScene()
    {
        if(SceneManager.GetActiveScene().buildIndex +1 != 1)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
    }

    public void LoadGamePlayScene()
    {
        SceneManager.LoadScene(0);

    }
}
