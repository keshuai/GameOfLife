using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOLHelperLine : ColorRender
{
    public Camera MainCamera;
    

    private void Update()
    {
        this.UpdateMesh();
    }

    public void UpdateMesh()
    {
        if (this.MainCamera.orthographicSize > 20)
        {
            this.ClearMesh();
            return;
        }

        this.ClearBuffer();

        float lineRadius = 0.005f * MainCamera.orthographicSize;
        
        var (xMin, xMax, yMin, yMax) = GetViewRect();

        for (float x = Mathf.Floor(xMin) - 0.5f; x < xMax; x += 1f)
        {
            _vects.Add(new Vector3(x - lineRadius, yMin, 0));
            _vects.Add(new Vector3(x + lineRadius, yMin, 0));
            _vects.Add(new Vector3(x + lineRadius, yMax, 0));
            _vects.Add(new Vector3(x - lineRadius, yMax, 0));
        }
        
        for (float y = Mathf.Floor(yMin) - 0.5f; y < yMax; y += 1f)
        {
            _vects.Add(new Vector3(xMin, y - lineRadius, 0));
            _vects.Add(new Vector3(xMax, y - lineRadius, 0));
            _vects.Add(new Vector3(xMax, y + lineRadius, 0));
            _vects.Add(new Vector3(xMin, y + lineRadius, 0));
        }
        
        this.FillVectToMesh();
    }

    (float xMin, float xMax, float yMin, float yMax) GetViewRect()
    {
        var center = this.MainCamera.transform.position;

        // 计算相机视野的一半
        var halfHeight = this.MainCamera.orthographicSize;
        var halfWidth = halfHeight * this.MainCamera.aspect;
       
        return (center.x - halfWidth, center.x + halfWidth, center.y - halfHeight, center.y + halfHeight);
    }
}
