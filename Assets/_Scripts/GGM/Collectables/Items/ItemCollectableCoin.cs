using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableCoin : ItemCollectableBase
{
    public Collider collider;
    public bool isCollected = false;
    public float lerp = 5f;
    public float minDistance = 0.5f;

    public void Start()
    {
        //CoinAnimationManager.Instance.RegisterCoin(this);
    }

    protected override void OnCollect()
    {
        base.OnCollect();
        collider.enabled = false;
        isCollected = true;
        //Debug.Log("OnCollect de itemCollectableCoin foi chamado");

        //PlayerController.Instance.Bounce();
        //ItemsManager.Instance.AddCoins();
    }

    protected override void Collect()
    {
        OnCollect();
        //Debug.Log("Collect de itemCollectableCoin foi chamado");
    }

    private void Update()
    {
        if (isCollected)
        {

            Vector3 playerPosition = PlayerController.Instance.transform.position;
            transform.position = Vector3.Lerp(transform.position, playerPosition, lerp * Time.deltaTime);
            //Debug.Log("Lerping towards player: " + transform.position);

            if(Vector3.Distance(transform.position, playerPosition) < minDistance)
            {
                //HideItens();
                Destroy(gameObject);
            }
        }
    }
}

