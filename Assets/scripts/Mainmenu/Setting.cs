using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{
    private Player _player;
    public void Start()
    {
         _player = GameObject.Find("Player").GetComponent<Player>();
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void ResetHighscore()
    {
        _player.ResetBestScore();
    }
}
