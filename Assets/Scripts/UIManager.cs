using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    static UIManager instance;

    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject messagePanel;
    [SerializeField] TextMeshProUGUI msgTxt;

    //properties
    public static UIManager Instance { get => instance;}
    public GameObject MessagePanel { get => messagePanel; }

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
        MessagePanel.SetActive(true);
        StartCoroutine("HideMessagePanel");
    }
     

    public void ActivateGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }


    public IEnumerator HideMessagePanel(float t = 7)
    {
        yield return new WaitForSeconds(t);
        messagePanel.SetActive(false);
    }

    public void SetMessage(string msg)
    {
        msgTxt.text = msg;
        StartCoroutine("HideMessagePanel", 5);
    }
}
