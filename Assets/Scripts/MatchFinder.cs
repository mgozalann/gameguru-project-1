using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MatchFinder : MonoBehaviour
{
    public event Action<Transform> OnGridMarked;
    public event Action OnMatched;

    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private GridManager _gridManager;

    private Dictionary<Transform, Vector2Int> _chosenGrid;
    private Dictionary<Transform, Vector2Int> _matchedGrid;

    private void Start()
    {
        _chosenGrid = new Dictionary<Transform, Vector2Int>();
        _matchedGrid = new Dictionary<Transform, Vector2Int>();

        _inputHandler.OnGridClicked += OnGridClicked;
        GameController.Instance.OnGridReseted+= OnRebuild;
    }

    private void OnRebuild()
    {
        _matchedGrid.Clear();
        _chosenGrid.Clear();
    }

  
    private void OnGridClicked(Transform pos)
    {
        if (_chosenGrid.ContainsKey(pos)) return;

        OnGridMarked?.Invoke(pos);

        _chosenGrid.Add(pos, _gridManager.GridObjects[pos]);

        foreach (var chosenGrid in _chosenGrid.Keys)
        {
            CheckForMatches(chosenGrid);
        }

        MatchProcess();
    }

    private void MatchProcess()
    {
        if (_matchedGrid.Keys.Count <= 0) return;

        ClearMatchedGrid();
        
        OnMatched?.Invoke();
    }
    
    private void CheckForMatches(Transform obj)
    {
        Vector2Int xMarkerCoords = _chosenGrid[obj];

        List<Vector2Int> directions = new List<Vector2Int>
        {
            new Vector2Int(0, -1), // Bottom
            new Vector2Int(0, 1), // Top
            new Vector2Int(-1, 0), // Left
            new Vector2Int(1, 0) // Right
        };

        Dictionary<Transform, Vector2Int> surroundingMarkers = new Dictionary<Transform, Vector2Int>();

        foreach (var direction in directions)
        {
            Vector2Int targetCoords = new Vector2Int(xMarkerCoords.x + direction.x, xMarkerCoords.y + direction.y);

            if (_chosenGrid.ContainsValue(targetCoords))
            {
                foreach (var item in _chosenGrid)
                {
                    if (item.Value == targetCoords)
                    {
                        surroundingMarkers.Add(item.Key, item.Value);
                        break;
                    }
                }
            }
        }

        if (surroundingMarkers.Keys.Count > 1) //That means its matching
        {
            foreach (var item in surroundingMarkers)
            {
                if (!_matchedGrid.ContainsKey(item.Key))
                {
                    _matchedGrid.Add(item.Key, item.Value);
                }
            }

            if (!_matchedGrid.ContainsKey(obj))
            {
                _matchedGrid.Add(obj, _gridManager.GridObjects[obj]);
            }
        }
    }

    
    private void ClearMatchedGrid()
    {
        foreach (var matchedKey in _matchedGrid.Keys.ToList())
        {
            if (_chosenGrid.ContainsKey(matchedKey))
            {
                _chosenGrid.Remove(matchedKey);
                Destroy(matchedKey.GetChild(0).gameObject); //zaman kalırsa pool a dönüştürülecek ve sistem değişecek.
            }
        }
        
        _matchedGrid.Clear();

    }
    private void OnDisable()
    {
        _inputHandler.OnGridClicked -= OnGridClicked;
        GameController.Instance.OnGridReseted-= OnRebuild;

    }
}