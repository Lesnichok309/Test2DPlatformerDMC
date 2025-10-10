using UnityEngine;

public class CoinContact : MonoBehaviour
{
    [SerializeField] int count;
    [SerializeField] GameObject CoinSoundPrefab;
    
    void Start()
    {
        if (count == 0)
        {   
            count = Random.Range(1, 6);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CoinManager.AddCoin(count);
            if (CoinSoundPrefab != null)
            {
               GameObject coin =  Instantiate(CoinSoundPrefab, transform.position, Quaternion.identity);
                Destroy(coin, 1); 
            } 
            Destroy(gameObject);
        }
    }
}
