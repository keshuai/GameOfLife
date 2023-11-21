using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOLRender : ColorRender
{
    public Camera MainCamera;
    
    public void UpdateWithPoints(SortedSet<long> points)
    {
        this.ClearBuffer();
        
        foreach (var point in points)
        {
            var (x, y) = GOL.Int2LongUtility.LongToInt2(point);
            
            var viewportPoint = this.MainCamera.WorldToViewportPoint(new Vector3(x, y));
      
            // 画布之外的点不用渲染
            if (viewportPoint.x < 0 || viewportPoint.x > 1 || viewportPoint.y < 0 || viewportPoint.y > 1)
            {
                continue;
            }

            const float pointRadius = 0.45f;
            
            _vects.Add(new Vector3(x - pointRadius, y - pointRadius, 0));
            _vects.Add(new Vector3(x + pointRadius, y - pointRadius, 0));
            _vects.Add(new Vector3(x + pointRadius, y + pointRadius, 0));
            _vects.Add(new Vector3(x - pointRadius, y + pointRadius, 0));
        }

        this.FillVectToMesh();
    }
}
