using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public event Action<Transform> OnGridClicked;
    
    [SerializeField] private LayerMask _gridLayer;
    
    private Camera _mainCamera;
    private void Start()
    {
        _mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, _gridLayer);

            if (hit.collider != null)
            {
                OnGridClicked?.Invoke(hit.transform);
            }
        }
    }
}