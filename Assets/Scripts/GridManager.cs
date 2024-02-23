using System;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GameObject _gridObject;
    [SerializeField] private float _gridAreaSize; // Max area size of grids
    [SerializeField] private int _gridSize;

    private Dictionary<Transform, Vector2Int> _gridObjects;
    public Dictionary<Transform, Vector2Int> GridObjects => _gridObjects;
    private void Start()
    {
        GenerateGrid();

        GameController.Instance.OnGridReseted+=OnRebuild;
    }

    private void OnRebuild()
    {
        DestroyGrids();
        GenerateGrid();
    }

    private void DestroyGrids()
    {
        foreach (var gridObjectKey in GridObjects.Keys)
        {
            Destroy(gridObjectKey.gameObject);
        }
    }
    void GenerateGrid()
    {
        float cellSize = _gridAreaSize / _gridSize; // size of each grid
        _gridObjects = new Dictionary<Transform, Vector2Int>();

        for (int x = 0; x < _gridSize; x++)
        {
            for (int y = 0; y < _gridSize; y++)
            {
                Vector3 gridPosition = new Vector3((x + 0.5f) * cellSize - _gridAreaSize / 2f, (y + 0.5f) * cellSize - _gridAreaSize / 2f, 0f);

                GameObject grid = Instantiate(_gridObject, this.transform);
                grid.transform.localScale *= cellSize;
                grid.transform.localPosition = gridPosition;
                grid.transform.name = x + "," + y;
                
                _gridObjects.Add(grid.transform,new Vector2Int(x,y));
            }
        }
    }

    public void ChangeGridSize(string gridSize)
    {
        _gridSize = int.Parse(gridSize);
    }

    private void OnDisable()
    {
        GameController.Instance.OnGridReseted-=OnRebuild;
    }
}
