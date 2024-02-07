using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    public float speed = 10f;
    [SerializeField]
    private float _speedMultiplier = 2;

   [SerializeField]
    private GameObject _laserPrefab;


    [SerializeField]
    private GameObject _tripleShotPrefab;
    
    
    [SerializeField]
    private float _firerate = 0.5f;

    [SerializeField]
    private float _canfire = -1f;

   [SerializeField]
    private int _lives = 3;

   [SerializeField]
    private GameObject _leftEngine,_rightEngine;
    

    private SpawnManager _spawnManager;

    private bool  _isSpeedBoostActive = false;

   [SerializeField]
    private bool _isTripleShotActive = false;
    [SerializeField]
    private bool _isShieldsActive = false;
    [SerializeField]
    private GameObject _shieldVisualizer;
   [SerializeField]
    private int _score;
    public int bestScore;
    
 public Text bestText;
    private UIManager _uimanager;
    [SerializeField]
    private AudioClip _laserSoundClip;
    [SerializeField]
    private AudioSource _audioSource;
    //private GameManaging _gameManage;

   
    void Start()
    {
        transform.position = new Vector3(0,0,0);
        //_gameManage = GameObject.Find("GameManaging").GetComponent<GameManaging>();
        _spawnManager = GameObject.Find("Spawn_manager").GetComponent<SpawnManager>();
        _uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestText.text = "BEST : " + bestScore;

        if(_spawnManager ==  null)
        {
            Debug.LogError("The apwn Manager is NUll");
        }
         //if(_gameManage.isCoopMode == true)
       // {
            
        //}

        if(_uimanager == null)
        {
            Debug.LogError("The UI Manager is NUll");
        }

        if(_audioSource == null)
        {
            Debug.LogError("AudioSource on the player is NUll");
        }
        else{
            _audioSource.clip = _laserSoundClip;
        }
       
    }

    
    void Update()
    {
            calculateMovement();

            if(Input.GetKeyDown(KeyCode.Space) && Time.time > _canfire)
            {
               FireLaser();
            }
    }



    void calculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput,verticalInput,0);
        

        if(_isSpeedBoostActive == false)
        {
        transform.Translate(direction * speed* Time.deltaTime);
        }
        else{
            transform.Translate(direction *( speed*_speedMultiplier)* Time.deltaTime);
        }
        
    
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f,0),0);

        if(transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y,0);
        
        }
        else if(transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    
    }


         void FireLaser()
             {
                _canfire = Time.time + _firerate;  

                if (_isTripleShotActive == true)
                {
                    Instantiate(_tripleShotPrefab, transform.position + new Vector3(0.25f, 0.39f, 0), Quaternion.identity);
                }   
                else
                {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
                }

                _audioSource.Play();
             }

    public void Damage()
    {

        if(_isShieldsActive == true)
        {
            _isShieldsActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }

    
        _lives--;

        if(_lives ==2 )
        {
            _leftEngine.SetActive(true);
        }
        else if(_lives == 1)
        {
            _rightEngine.SetActive(true);
        }

        _uimanager.UpdateLives(_lives);
       
        if(_lives < 1)
        {
            
            _spawnManager.OnPlayerDeath();
            CheckForBestScore();
            Destroy(this.gameObject);
            
        }
    }

    public void TripleShotActive()
    {
         _isTripleShotActive = true; 
         StartCoroutine(TripleShotPowerDownRoutine()); 

    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }


    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
       
        StartCoroutine(SpeedBoostPowerupDownRoutine());
    }

    IEnumerator SpeedBoostPowerupDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
        
    }

    public void ShieldsActive()
    {
        _isShieldsActive = true;
        _shieldVisualizer.SetActive(true);
    }

    public void AddScore(int points)
    {
        _score += points;
        _uimanager.updateScore(_score);
    }

    public void CheckForBestScore()
    {
      if (_score > bestScore)
        {
            bestScore = _score;
            bestText.text = "BEST : " + bestScore;

            // Save the best score to PlayerPrefs
            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.Save(); // Make sure to call Save to persist the changes
        }
    }

    public void ResetBestScore()
    {
        bestScore = 0;
        bestText.text = "BEST : " + bestScore;

        // Clear the best score from PlayerPrefs
        PlayerPrefs.DeleteKey("BestScore");
        PlayerPrefs.Save(); // Make sure to call Save to persist the changes
    }


}
