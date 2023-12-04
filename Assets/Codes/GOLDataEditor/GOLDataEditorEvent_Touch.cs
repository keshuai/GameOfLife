using UnityEngine;

public class GOLDataEditorEvent_Touch : MonoBehaviour
{
    enum EventType
    {
        None,
        Move,
        Scale,
    }
    
    public Camera CameraMain;
    public GOLDataEditor DataEditor;
    public GOLDataEditorSelectPoint_Mouse SelectEvent;
    public GameObject[] MouseEvents;

    public float ZoomFactor = 0.001f;

    private EventType _eventType;
    private Vector2 _touch0BeginPos;
    private Vector2 _touch1BeginPos;
    
    private Vector3 _cameraStartPos;
    private float _lastDistance;

    void Update()
    {
        this.Check();
    }

    void Check()
    {
        if (_eventType == EventType.None)
        {
            if (Input.touchCount != 2)
            {
                return;
            }

            var touch0 = Input.GetTouch(0);
            var touch1 = Input.GetTouch(1);

            if (touch0.phase == TouchPhase.Ended || touch0.phase == TouchPhase.Canceled ||
                touch1.phase == TouchPhase.Ended || touch1.phase == TouchPhase.Canceled)
            {
                return;
            }

            if (touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began)
            {
                _touch0BeginPos = touch0.position;
                _touch1BeginPos = touch1.position;
                return;
            }
            
            if (touch0.phase == TouchPhase.Moved && touch1.phase == TouchPhase.Moved)
            {
                var delta0 = touch0.position - _touch0BeginPos;
                var delta1 = touch1.position - _touch1BeginPos;

                var angle = Vector2.Angle(delta0, delta1);
                if (angle < 30) // 同向
                {
                    _cameraStartPos = this.CameraMain.transform.localPosition;
                    _eventType = EventType.Move;
                    this.EventStart();
                }
                else if (angle > 100) // 反向
                {
                    _lastDistance = (Input.GetTouch(0).position - Input.GetTouch(1).position).magnitude;
                    _eventType = EventType.Scale;
                    this.EventStart();
                }
            }
        }
        else if (_eventType == EventType.Move)
        {
            if (Input.touchCount == 0)
            {
                this.EventReset2None();
                return;
            }

            if (Input.touchCount == 2 
                && Input.GetTouch(0).phase == TouchPhase.Moved
                && Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                this.DoMove();
            }
        }
        else if (_eventType == EventType.Scale)
        {
            if (Input.touchCount == 0)
            {
                this.EventReset2None();
                return;
            }
            
            if (Input.touchCount == 2
                && Input.GetTouch(0).phase == TouchPhase.Moved
                && Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                this.DoScale();
            }
        }
    }

    void EventStart()
    {
        this.SelectEvent.enabled = false;

        // 鼠标事件会与手势冲突，停用
        if (this.MouseEvents != null)
        {
            foreach (var mouseEvent in this.MouseEvents)
            {
                mouseEvent.SetActive(false);
            }

            this.MouseEvents = null;
        }
    }

    void EventReset2None()
    {
        _eventType = EventType.None;
        this.SelectEvent.enabled = true;
    }

    void DoMove()
    {
        var startPos = (_touch0BeginPos + _touch1BeginPos) / 2;
        var currentPos = (Input.GetTouch(0).position + Input.GetTouch(1).position) / 2;

        var delta = currentPos - startPos;
        delta /= this.CameraMain.pixelHeight / (2 * this.CameraMain.orthographicSize);

        this.CameraMain.transform.localPosition = 
            new Vector3(_cameraStartPos.x - delta.x, 
                _cameraStartPos.y - delta.y, 
                _cameraStartPos.z);
        
        this.DataEditor.OnViewChanged();
    }
    
    void DoScale()
    {
        var currentDistance = (Input.GetTouch(0).position - Input.GetTouch(1).position).magnitude;
        var delta = (currentDistance - _lastDistance) * ZoomFactor;
        _lastDistance = currentDistance;
        
        delta = -delta;

        var orthographicSize = this.CameraMain.orthographicSize;
        orthographicSize = Mathf.Clamp(orthographicSize + delta * this.CameraMain.orthographicSize, 3f, 200f);
        this.CameraMain.orthographicSize = orthographicSize;
        
        this.DataEditor.OnViewChanged();
    }
}