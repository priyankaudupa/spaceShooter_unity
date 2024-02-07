using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManaging : MonoBehaviour
{

    //public bool isCoopMode = false;
    private bool _isGameOver;
    [SerializeField]
    private GameObject _pauseMenuPanel;
    private Animator _pauseAnimator;

    private void Start()
    {
     _pauseAnimator = GameObject.Find("Pause_Panel").GetComponent<Animator>();
     _pauseAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
    } 

public void Update()
{
    if(Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
    {
        SceneManager.LoadScene(1); //current game scene
    }

    if(Input.GetKeyDown(KeyCode.Escape))
    {
        SceneManager.LoadScene(0);
    }

    if(Input.GetKeyDown(KeyCode.P))
    {
        _pauseMenuPanel.SetActive(true);
        _pauseAnimator.SetBool("isPanel",true);
        Time.timeScale = 0;
    }
    
}
    public void GameOver()
    {
        
        _isGameOver = true;
    }

    public void Resumegame()
    {
        _pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}


