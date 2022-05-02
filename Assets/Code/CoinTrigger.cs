using UnityEngine;

public class CoinTrigger : MonoBehaviour
{
    GameController gameController;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
    }

    void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, 35 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            gameController.CoinSound();
            Destroy(gameObject);
        }    
    }
}
