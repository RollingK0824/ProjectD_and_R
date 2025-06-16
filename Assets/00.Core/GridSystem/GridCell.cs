using ProjectD_and_R.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell
{
    public int X { get; set; }
    public int Y { get; set; } 
    public GameObject PlacedObject { get; set; }
    public GridCellType CellType { get; set; }
}
