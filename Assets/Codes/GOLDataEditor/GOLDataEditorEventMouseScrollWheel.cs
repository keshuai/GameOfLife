using UnityEngine;

// 用于鼠标控制视图大小
public class GOLDataEditorEventMouseScrollWheel : MonoBehaviour
{
    public Camera MainCamera;
    public float MouseScrollWheelScale = 0.5f;
    

    // Update is called once per frame
    void Update()
    {
        this.CheckMouseScrollWheel();
    }

    void CheckMouseScrollWheel()
    {
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll == 0)
        {
            return;
        }

        var orthographicSize = this.MainCamera.orthographicSize;
        orthographicSize = Mathf.Clamp(orthographicSize + scroll * MouseScrollWheelScale * this.MainCamera.orthographicSize, 3f, 200f);
        this.MainCamera.orthographicSize = orthographicSize;
    }
}