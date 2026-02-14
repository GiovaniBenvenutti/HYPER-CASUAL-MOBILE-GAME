using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using GGM.Singleton;

public class PlayerController : Singleton<PlayerController>
{
    //  public variables
    [Header("Lerp")]
    public Transform target;
    public float lerpSpeed = 0.1f;

    [Header("TextMeshPro")]
    public TextMeshPro uiTextPowerUp;

    [Header("Coin Collector SetUp")]
    public GameObject coinCollector;

    public float speed = 5f;

    public string tagToCheckEnemy = "Enemy";
    public string tagToCheckEndLine = "EndLine";

    public GameObject endScreen;

    public bool invencible = false;

    //  private variables
    private Vector3 _pos;
    private Vector3 _startPosition;
    private bool _canRun;
    private float _currentSpeed = 5f;

    void Start() 
    {
        _startPosition = transform.position;
        ResetSpeed();
        _currentSpeed = speed;
        _canRun = true;    
    }



    // public void startToRun()
    // {
    //     _canRun = true;
    // }

    // Update is called once per frame
    void Update()
    {
        if(!_canRun) return;

        _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);
        transform.Translate(transform.forward * Time.deltaTime * _currentSpeed);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.transform.CompareTag(tagToCheckEnemy))
        {
            if(!invencible)    
            {
                Debug.Log("Colidiu com inimigo!");
                EndGame();
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(tagToCheckEndLine))
        {
            if(!invencible)
            {
                Debug.Log("Chegou ao fim da pista!");
                EndGame();
            }
        }
    }

    public void EndGame()
    {
        _canRun = false;
        endScreen.SetActive(true);
    }


    #region PowerUp Methods

    public void SetPowerUpText (string powerUpName)
    {
       uiTextPowerUp.text = powerUpName;
    }

    public void PowerUpSpeedUp (float speedUp) 
    {
        _currentSpeed += speedUp;
    }

    public void ResetSpeed () 
    {
        _currentSpeed = speed;
    }

    public void SetInvencible (bool state = true) 
    {
        invencible = state;
    }

    public void ChangeHeight(float amountToHeight, float duration, float animationDuration, Ease ease)
    {
        transform.DOMoveY(_startPosition.y + amountToHeight, animationDuration).SetEase(ease);
        Invoke(nameof(ResetHeight), duration);
    }

    public void ResetHeight(float animationDuration, Ease ease)
    {
        transform.DOMoveY(_startPosition.y, animationDuration).SetEase(ease);
    }

    public void ChangeCoinCollectorSize(float amount)
    {
        coinCollector.transform.localScale = Vector3.one * amount;
    }



    #endregion

}
