using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinManager : MonoBehaviour
{
    public static int coinsCount;
    public static UnityEvent<int> OnAddCoin = new UnityEvent<int>();
    public static void AddCoin(int count)
    {
        coinsCount += count;
        OnAddCoin.Invoke(count);
    }


}
