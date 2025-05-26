using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchManager : Singleton<TouchManager>
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnUiTouch();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                ITouchble Touchble = hit.collider.GetComponent<ITouchble>();
                OnObjTouch(Touchble);
            }
            else
            {
                OnEmptySpaceTouch();
            }
        }
    }

    /// <summary>
    /// UI 터치 이벤트
    /// </summary>
    void OnUiTouch()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("UI");
            return;
        }
    }

    void OnObjTouch(ITouchble touch)
    {
        if (touch != null)
        {
            touch.OnTouch();
        }
        else
        {
            Debug.Log("이벤트x");
        }
    }

    /// <summary>
    /// 빈공간 터치 이벤트
    /// </summary>
    void OnEmptySpaceTouch()
    {
        Debug.Log("빈 공간");
    }
}

