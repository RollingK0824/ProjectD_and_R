using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType { Normal, Elite, Event, Shop, Reward, None }
public class Room : MonoBehaviour
{
    [SerializeField][Header("이름")] string _name = "";
    public string Name
    {
        get => _name;
        protected set => _name = value;
    }

    [SerializeField][Header("X 좌표")] int _xPos = 0;
    public int XPos
    {
        get => _xPos;
        protected set => _xPos = value;
    }

    [SerializeField][Header("Y 좌표")] int _yPos = 0;
    public int YPos
    {
        get => _yPos;
        protected set => _yPos = value;
    }

    [SerializeField][Header("방 타입")] RoomType _type = RoomType.None;
    public RoomType Type
    {
        get => _type;
        protected set => _type = value;
    }

    [SerializeField][Header("이동할 수 있는 방")] List<Room> _next = new List<Room>();
    public List<Room> Next
    {
        get => _next;
        protected set => _next = value;
    }

    RoomTouchEvent _roomTouchEvent;

    public void Init(RoomType type, int height, int width)
    {
        _type = type;
        _xPos = width;
        _yPos = height;
        _name = type.ToString() + "Room" + "(" + _yPos + "," + _xPos + ")";
        gameObject.name = _name;

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

