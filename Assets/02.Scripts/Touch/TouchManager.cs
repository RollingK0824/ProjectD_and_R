using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchManager : Singleton<TouchManager>
{
    [SerializeField] ITouchble Touchble;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnUiTouch();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (Touchble != null) Touchble.OnOtherTouch();

                Touchble = hit.collider.GetComponent<ITouchble>();
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
            //추후 Touchble에 ui 넣는작업 필요 17라인 참조
            Debug.Log("UI");
            return;
        }
    }

    /// <summary>
    /// Obj 터치 이벤트
    /// </summary>
    /// <param name="touch"></param>
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
        if (Touchble != null)
            Touchble.OnEmptyTouch();
        Debug.Log("빈 공간");
    }
}

