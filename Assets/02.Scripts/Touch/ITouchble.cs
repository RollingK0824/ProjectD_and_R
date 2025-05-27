using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITouchble
{
    /// <summary>
    /// 현재 오브젝트 터치시
    /// </summary>
    void OnTouch();

    /// <summary>
    /// 빈 공간 터치시
    /// </summary>
    void OnEmptyTouch();

    /// <summary>
    /// 다른 오브젝트 터치시
    /// </summary>
    void OnOtherTouch();
}
