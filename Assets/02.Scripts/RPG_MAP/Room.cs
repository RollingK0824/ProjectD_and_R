using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType { Normal, Elite, Event, Shop, Reward, None }
public class Room : MonoBehaviour
{
    [SerializeField][Header("�̸�")] string _name = "";
    public string Name
    {
        get => _name;
        protected set => _name = value;
    }

    [SerializeField][Header("X ��ǥ")] int _xPos = 0;
    public int XPos
    {
        get => _xPos;
        protected set => _xPos = value;
    }

    [SerializeField][Header("Y ��ǥ")] int _yPos = 0;
    public int YPos
    {
        get => _yPos;
        protected set => _yPos = value;
    }

    [SerializeField][Header("�� Ÿ��")] RoomType _type = RoomType.None;
    public RoomType Type
    {
        get => _type;
        protected set => _type = value;
    }

    [SerializeField][Header("�̵��� �� �ִ� ��")] List<Room> _next = new List<Room>();

    //�׽�Ʈ��
    public Color _Color;
    public List<Room> Next
    {
        get => _next;
        protected set => _next = value;
    }

    public RoomTouchEvent _roomTouchEvent;

    /// <summary>
    /// �� ���� �ʱ�ȭ
    /// </summary>
    public void Init(RoomType type, int height, int width)
    {
        _type = type;
        _xPos = width;
        _yPos = height;
        _name = type.ToString() + "Room" + "(" + _yPos + "," + _xPos + ")";
        gameObject.name = _name;
        _Color = gameObject.GetComponent<Renderer>().material.color;
        RoomTouchEventConnect();
    }

    void RoomTouchEventConnect()
    {
        _roomTouchEvent = GetComponent<RoomTouchEvent>();
        if (_roomTouchEvent == null)
        {
            _roomTouchEvent = gameObject.AddComponent<RoomTouchEvent>();
        }

        _roomTouchEvent.Init(this);
    }
}

