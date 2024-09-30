using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    [System.Serializable]
    public struct CoinsObject
    {
        public GameObject prefab;
    }
    public CoinsObject[] coins;
    public float coinRate = 18f;
    public List<GameObject> coinsItem = new List<GameObject>();

    private void Start()
    {
        InvokeRepeating(nameof(SpawnCoin), coinRate, coinRate);
    }

    private void SpawnCoin()
    {
        int randomIndex = Random.Range(0, coins.Length);
        GameObject newCoin = Instantiate(coins[randomIndex].prefab, transform.position, Quaternion.identity);
        coinsItem.Add(newCoin.gameObject);
    }
}
