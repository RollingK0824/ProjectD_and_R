using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType { Normal, Elite, Event, Shop, Reward, None }
public class Room : MonoBehaviour
{
    [SerializeField][Header("ÀÌ¸§")] string _name = "";
    public string Name
    {
        get => _name;
        protected set => _name = value;
    }

    [SerializeField][Header("X ÁÂÇ¥")] int _xPos = 0;
    public int XPos
    {
        get => _xPos;
        protected set => _xPos = value;
    }

    [SerializeField][Header("Y ÁÂÇ¥")] int _yPos = 0;
    public int YPos
    {
        get => _yPos;
        protected set => _yPos = value;
    }

    [SerializeField][Header("¹æ Å¸ÀÔ")] RoomType _type = RoomType.None;
    public RoomType Type
    {
        get => _type;
        protected set => _type = value;
    }

    public void Init(RoomType type, int height, int width)
    {
        _type = type;
        _xPos = width;
        _yPos = height;
        _name = type.ToString() + "Room" + "(" + YPos + "," + XPos + ")";
    }
}

