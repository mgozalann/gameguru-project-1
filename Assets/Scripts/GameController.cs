using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public event Action OnGridReseted;

    // private GameDataSet _gameDataSet;
    // public GameDataSet GameDataSet => _gameDataSet;
    
    
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

        //_gameDataSet = GetComponent<GameDataSet>();
    }


    public void OnGridReset()
    {
        OnGridReseted?.Invoke();
    }
}