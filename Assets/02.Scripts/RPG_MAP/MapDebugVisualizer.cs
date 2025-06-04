using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDebugVisualizer : MonoBehaviour
{
  //디버그 라인그려줄 테스트용 클래스 삭제예정
    private const float GRID_TO_WORLD_SCALE = 1.5f;

    public static void DrawConnection(Vector2Int worldPosA, Vector2Int worldPosB, Color color, float duration = 100f)
    {
#if UNITY_EDITOR
        Vector3 start = new Vector3(worldPosA.x * GRID_TO_WORLD_SCALE, 0.1f, worldPosA.y * GRID_TO_WORLD_SCALE);
        Vector3 end = new Vector3(worldPosB.x * GRID_TO_WORLD_SCALE, 0.1f, worldPosB.y * GRID_TO_WORLD_SCALE);
        Debug.DrawLine(start, end, color, duration);
#endif
    }

    public static void DrawZoneBoundary(Vector2Int zoneOffset, int zoneId, int zoneWidth, int zoneHeight, Color[] zoneColors, bool enabled = true)
    {
#if UNITY_EDITOR
        if (!enabled) return;

        // 방 프리팹이 중앙에 놓이고 크기가 1x1x1 이라고 가정하면, 각 방의 시각적 경계는 중심에서 +/- (GRID_TO_WORLD_SCALE / 2)
        // GRID_TO_WORLD_SCALE이 2f이면, 방 하나의 크기가 2x2가 되고, 중심에서 +/- 1f
        float halfCellSize = GRID_TO_WORLD_SCALE / 2f;

        float xMinVis = zoneOffset.x * GRID_TO_WORLD_SCALE - halfCellSize;
        float xMaxVis = (zoneOffset.x + zoneWidth - 1) * GRID_TO_WORLD_SCALE + halfCellSize;
        float zMinVis = zoneOffset.y * GRID_TO_WORLD_SCALE - halfCellSize;
        float zMaxVis = (zoneOffset.y + zoneHeight - 1) * GRID_TO_WORLD_SCALE + halfCellSize;

        float lineYPos = 0.5f; // 방보다 살짝 위에

        Vector3 BL = new Vector3(xMinVis, lineYPos, zMinVis);
        Vector3 BR = new Vector3(xMaxVis, lineYPos, zMinVis);
        Vector3 TL = new Vector3(xMinVis, lineYPos, zMaxVis);
        Vector3 TR = new Vector3(xMaxVis, lineYPos, zMaxVis);

        Color lineColor = Color.black;
        if (zoneId >= 0 && zoneId < zoneColors.Length)
        {
            lineColor = new Color(zoneColors[zoneId].r * 0.7f, zoneColors[zoneId].g * 0.7f, zoneColors[zoneId].b * 0.7f, 1f);
        }

        Debug.DrawLine(BL, BR, lineColor, 120f);
        Debug.DrawLine(BR, TR, lineColor, 120f);
        Debug.DrawLine(TR, TL, lineColor, 120f);
        Debug.DrawLine(TL, BL, lineColor, 120f);
#endif
    }
}
