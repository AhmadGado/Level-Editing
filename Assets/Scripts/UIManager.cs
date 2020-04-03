using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    static UIManager instance;

    [SerializeField] GameObject gameOverPanel;

    //properties
    public static UIManager Instance { get => instance;}
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

    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel.SetActive(false);
    }
     

    public void ActivateGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

}
