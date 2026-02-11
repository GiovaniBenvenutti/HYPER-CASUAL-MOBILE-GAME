using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //  public variables
    [Header("Lerp")]
    public Transform target;
    public float lerpSpeed = 0.1f;

    public float speed = 5f;

    public string tagToCheckEnemy = "Enemy";
    public string tagToCheckEndLine = "EndLine";

    public GameObject endScreen;

    //  private variables
    private Vector3 _pos;
    private bool _canRun = true;



    public void startToRun()
    {
        _canRun = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!_canRun) return;

        _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);
        transform.Translate(transform.forward * Time.deltaTime * speed);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.transform.CompareTag(tagToCheckEnemy))
        {
            Debug.Log("Colidiu com inimigo!");
            EndGame();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(tagToCheckEndLine))
        {
            Debug.Log("Chegou ao fim da pista!");
            EndGame();
        }
    }

    public void EndGame()
    {
        _canRun = false;
        endScreen.SetActive(true);
    }

}
