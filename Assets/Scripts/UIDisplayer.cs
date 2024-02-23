using TMPro;
using UnityEngine;

public class UIDisplayer : MonoBehaviour
{
    [SerializeField] private MatchFinder _matchFinder;
    [SerializeField] private int _matchCount = 0;
    [SerializeField] private TextMeshProUGUI _matchCountText;
    private void Start()
    {
        UpdateMatchCountText();

        _matchFinder.OnMatched+=OnMatch;
        GameController.Instance.OnGridReseted+=OnGridReseted;
    }

    private void OnGridReseted()
    {
        _matchCount = 0;
        UpdateMatchCountText();
    }

    private void OnMatch()
    {
        _matchCount++;
        UpdateMatchCountText();
    }

    private void UpdateMatchCountText()
    {
        _matchCountText.text = "Match Count:" + _matchCount;
    }
    private void OnDisable()
    {
        _matchFinder.OnMatched-=OnMatch;

    }
}