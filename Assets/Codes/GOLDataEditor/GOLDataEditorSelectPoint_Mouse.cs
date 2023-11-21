using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 用于数据编辑模式下，鼠标的选择点的输入处理
public class GOLDataEditorSelectPoint_Mouse : MonoBehaviour
{
    public struct LastPoint
    {
        private int _x;
        private int _y;
        private bool _hasValue;

        public int X => _x;
        public int Y => _y;
        public bool HasValue => _hasValue;

        public void Reset()
        {
            _x = 0;
            _y = 0;
            _hasValue = false;
        }

        public void SetValue(int x, int y)
        {
            _x = x;
            _y = y;
            _hasValue = true;
        }

        public bool Equal(int x, int y) => _hasValue && _x == x && _y == y;
    }
    
    public Camera CameraMain;
    public GOLDataEditor DataEditor;
    
    private LastPoint _lastPoint;

    void Update()
    {
        this.CheckMouse();
    }

    void CheckMouse()
    {
        if (Input.GetMouseButtonUp(0))
        {
            _lastPoint.Reset();
        }
        else if (Input.GetMouseButton(0))
        {
            var (x, y) = this.GetCurrentPointOfMouse();
            
            var viewportPoint = this.CameraMain.WorldToViewportPoint(new Vector3(x, y));
      
            // 不选取画布之外的点
            if (viewportPoint.x < 0 || viewportPoint.x > 1 || viewportPoint.y < 0 || viewportPoint.y > 1)
            {
                return;
            }

            if (_lastPoint.Equal(x, y))
            {
                return;
            }
            
            _lastPoint.SetValue(x, y);
            this.DataEditor.SetPoint(x, y);
        }
    }

    (int x, int y) GetCurrentPointOfMouse()
    {
        var pos = this.CameraMain.ScreenToWorldPoint(Input.mousePosition);
        return (Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y));
    }
}
