using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchManager : Singleton<TouchManager>
{
    [SerializeField] ITouchble currentTouchble;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsPointerOverUIObject())
            {
                OnUiTouch();
                return;
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                ITouchble Touchble = hit.collider.GetComponent<ITouchble>();
                if (currentTouchble != null && currentTouchble != Touchble)
                {
                    currentTouchble.OnOtherTouch();
                }
                currentTouchble = Touchble;
                OnObjTouch(currentTouchble);
            }
            else
            {
                if (currentTouchble != null)
                {
                    currentTouchble.OnEmptyTouch();
                    currentTouchble = null;
                }

            }
        }
    }

    /// <summary>
    /// ui터치 확인
    /// </summary>
    bool IsPointerOverUIObject()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return true;
        return false;
    }
    /// <summary>
    /// UI 터치 이벤트
    /// </summary>
    void OnUiTouch()
    {
        Debug.Log("UI");
    }


    /// <summary>
    /// Obj 터치 이벤트
    /// </summary>

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
    /// 빈공간 터치
    /// </summary>
    void OnEmptySpaceTouch()
    {
        if (currentTouchble != null)
            currentTouchble.OnEmptyTouch();
    }
}

