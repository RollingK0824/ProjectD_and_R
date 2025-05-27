using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITouchble
{
    /// <summary>
    /// ���� ������Ʈ ��ġ��
    /// </summary>
    void OnTouch();

    /// <summary>
    /// �� ���� ��ġ��
    /// </summary>
    void OnEmptyTouch();

    /// <summary>
    /// �ٸ� ������Ʈ ��ġ��
    /// </summary>
    void OnOtherTouch();
}
