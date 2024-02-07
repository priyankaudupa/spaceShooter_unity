using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    
   
    private Player _player;
    private Animator _anim;

    private AudioSource _audioSource;
    private float _firerate = 3.0f;
    private float _canfire = -1;
    [SerializeField]
    private GameObject _laserPrefab;

    
    void Start()
    {
        transform.position = new Vector3(0,5.4f,0);
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();
       if(_player == null)
        {
            Debug.LogError("The Player is Null");
           
        }
         _anim = GetComponent<Animator>();

         if(_anim == null)
         {
            Debug.LogError("The Animator is Null");
         }
    }


    void Update()
    {
        calculateMovement();

        if(Time.time > _canfire)
        {
            _firerate = Random.Range(3f, 7f);
            _canfire = Time.time + _firerate;
            GameObject Enemy_laser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
           Laser[] lasers = Enemy_laser.GetComponentsInChildren<Laser>();

           for( int i=0; i<lasers.Length; i++)
           {
            lasers[i].AssignEnemyLaser();
           }
                  
      }
    }




    void calculateMovement()
    {
        transform.Translate(Vector3.down * _speed *Time.deltaTime);

        if(transform.position.y < -5f)
        {
            float randomX =Random.Range(-4f,5f);
            transform.position = new Vector3(randomX,5.4f,0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       
       if(other.tag == "Player")
        {
             
            Player player = other.transform.GetComponent<Player>();
        
            if (player!=null)
            {
                player.Damage();
            }

            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(this.gameObject,1.0f);
           _audioSource.Play();

        }

       if(other.tag == "laser")
       {

       
        Destroy(other.gameObject);
        if(_player != null)
        {
            _player.AddScore(10);
        }
        _anim.SetTrigger("OnEnemyDeath");
        _speed = 0;
        _audioSource.Play();
        Destroy(GetComponent<Collider2D>());
        Destroy(this.gameObject,1.0f);
        
       }
    }
    
}

