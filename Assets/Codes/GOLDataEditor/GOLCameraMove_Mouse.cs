using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 用于鼠标拖动视图
public class GOLCameraMove_Mouse : MonoBehaviour
{
    public Camera CameraMain;

    private Vector3 _cameraStartPos;
    private Vector3 _mouseStartPos;
    
    void Update()
    {
        this.CheckMouse();
    }

    void CheckMouse()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _cameraStartPos = this.CameraMain.transform.localPosition;
            _mouseStartPos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(1) || Input.GetMouseButton(1))
        {
            var delta = Input.mousePosition - _mouseStartPos;
            delta /= this.CameraMain.pixelHeight / (2 * this.CameraMain.orthographicSize);
            this.CameraMain.transform.localPosition = _cameraStartPos - delta;
        }
    }
}
