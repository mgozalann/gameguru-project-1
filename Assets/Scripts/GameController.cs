using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public event Action OnGridReseted;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void OnGridReset()
    {
        OnGridReseted?.Invoke();
    }
}