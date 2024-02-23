using UnityEngine;

public class MarkerPlacer : MonoBehaviour
{
    [SerializeField] private MatchFinder _matchFinder;
    [SerializeField] private GameObject _xMarker;
    private void Start()
    {
        _matchFinder.OnGridMarked+=OnGridClicked;
    }

    private void OnGridClicked(Transform pos)
    {
        PlaceXMarker(pos);
    }

    private void PlaceXMarker(Transform pos)
    {
        GameObject xMarker = Instantiate(_xMarker, pos.position, Quaternion.identity);
        xMarker.transform.parent = pos;
        xMarker.transform.localScale = Vector3.one;
    }
    
    private void OnDisable()
    {
        _matchFinder.OnGridMarked-=OnGridClicked;
    }
}