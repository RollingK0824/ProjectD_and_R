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
                if (Touchble != null && Touchble != hit.collider.GetComponent<ITouchble>()) Touchble.OnOtherTouch();

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

            //���� Touchble�� ui �ִ��۾� �ʿ� 17���� ����

            Debug.Log("UI");
            return;
        }
    }


    /// <summary>
    /// Obj ��ġ �̺�Ʈ
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

    /// ����� ��ġ �̺�Ʈ
    /// </summary>
    void OnEmptySpaceTouch()
    {
        if (Touchble != null)
            Touchble.OnEmptyTouch();
    }
}

