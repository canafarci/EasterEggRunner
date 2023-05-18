using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputReader : MonoBehaviour
{
    public float XChange { get; private set; } 
    [SerializeField] float _speed;
    bool _isDragging;
    float _oldX;

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (Input.GetMouseButtonDown(0))
        {
            _oldX = Camera.main.ScreenToViewportPoint(Input.mousePosition).x;
            _isDragging = true;
        }

        else if (Input.GetMouseButtonUp(0))
            _isDragging = false;
    }


    private void FixedUpdate()
    {
        if (_isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            float currentX = mousePos.x;

            XChange = (currentX - _oldX) * Time.fixedDeltaTime * _speed;
        }
        else
        {
            XChange = 0;
        }
    }
}
