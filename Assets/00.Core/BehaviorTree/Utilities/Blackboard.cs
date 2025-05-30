using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackboard
{
    private Dictionary<string, object> _data = new Dictionary<string, object>();

    public void SetData(string key, object value)
    {
        _data[key] = value;
    }

    public T GetData<T>(string key)
    {
        if(_data.ContainsKey(key) && _data[key] is T)
        {
            return (T) _data[key];
        }
        return default(T);  // 해당 키가 없거나 일치하지 않으면 기본값 반환
    }

    public bool HasData(string key)
    {
        return _data.ContainsKey(key);
    }

    public void RemoveData(string key)
    {
        _data.Remove(key);
    }
}
 