using UnityEngine;

public class MarkerPlacer : MonoBehaviour
{
    [SerializeField] private MarkerObjectPool _markerOP;
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
        _xMarker = _markerOP.GetObject();
        
        _xMarker.transform.position = pos.position;
        _xMarker.transform.rotation = Quaternion.identity;
        _xMarker.transform.parent = pos;
        _xMarker.transform.localScale = Vector3.one;
    }
    
    private void OnDisable()
    {
        _matchFinder.OnGridMarked-=OnGridClicked;
    }
}