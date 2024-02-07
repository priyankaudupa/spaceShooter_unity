using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _ScoreText;
    [SerializeField]
    private Text _bestScoreText;
    
    [SerializeField]
    private Sprite[] _liveSprite;
    [SerializeField]
    private Image _LivesImg;
    [SerializeField]
    private Text _gameOvertext;  
    [SerializeField]
    private Text _restartText;
    private Player _player;

    private GameManaging _gamemanager;
    
      // Start is called before the first frame update
    void Start()
    {
      
        _ScoreText.text = "SCORE :" + 0;
        
        _gameOvertext.gameObject.SetActive(false);
        _gamemanager = GameObject.Find("GameManager").GetComponent<GameManaging>();
        _player = GameObject.Find("Player").GetComponent<Player>();
       // _bestScoreText.text = "BEST :" + _player.bestScore;

        if(_gamemanager == null)
        {
            Debug.LogError("Game Manager is NULL");
        }
    }

    public void updateScore(int playerScore)
    {
        _ScoreText.text = "SCORE :" + playerScore.ToString();
       
    }

    //public void UpdateBestScore(int newBestScore)
   // {
   //     _bestScoreText.text = "BEST: " + newBestScore.ToString();
   // }

    public void UpdateLives(int currentLives)
    {
        _LivesImg.sprite = _liveSprite[currentLives];

        if(currentLives == 0)
        {
            GameOverSequence();
        }
    }

    void GameOverSequence()
    {
        _gamemanager.GameOver();
         _gameOvertext.gameObject.SetActive(true);
    
         _restartText.gameObject.SetActive(true);
            StartCoroutine(GameOverFlickerRoutine());
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while(true)
        {
            _gameOvertext.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOvertext.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void ResumePlay()
    {
        _gamemanager.Resumegame();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }


}

