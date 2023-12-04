using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOLDataEditor : MonoBehaviour
{
    public class EditorData
    {
        private readonly SortedSet<long> _points = new SortedSet<long>();

        public SortedSet<long> Points => _points;

        public void ClearPoints() => _points.Clear();
        
        public void SetPoint(long point)
        {
            if (_points.Contains(point))
            {
                _points.Remove(point);
            }
            else
            {
                _points.Add(point);
            }
        }
        
        public void SetPoint(int x, int y) => this.SetPoint(GOL.Int2LongUtility.Int2Long(x, y));
    }

    public GOLView GOLView;
    
    private readonly EditorData _editorData = new EditorData();
    
    public SortedSet<long> Points => _editorData.Points;

    public void ClearPoints() => _editorData.ClearPoints();

    public void SetPoint(int x, int y)
    {
        _editorData.SetPoint(x, y);
        this.GOLView.Render.UpdateWithPoints(_editorData.Points);
    }

    public void OnViewChanged()
    {
        if (!this.GOLView.Playing)
        {
            this.GOLView.Render.UpdateWithPoints(_editorData.Points);
        }
    }

    private void Start()
    {
        this.InitPulsar();
        this.GOLView.Render.UpdateWithPoints(_editorData.Points);
    }

    // 脉冲星
    private void InitPulsar()
    {
        _editorData.ClearPoints();
        {
            _editorData.SetPoint(1, 2);
            _editorData.SetPoint(1, 3);
            _editorData.SetPoint(1, 4);
            
            _editorData.SetPoint(2, 1);
            _editorData.SetPoint(2, 3);
            _editorData.SetPoint(2, 5);
            
            _editorData.SetPoint(3, 1);
            _editorData.SetPoint(3, 2);
            _editorData.SetPoint(3, 4);
            _editorData.SetPoint(3, 5);
            _editorData.SetPoint(3, 6);
            
            _editorData.SetPoint(4, 1);
            _editorData.SetPoint(4, 3);
            _editorData.SetPoint(4, 6);
            
            _editorData.SetPoint(5, 2);
            _editorData.SetPoint(5, 3);
            
            _editorData.SetPoint(6, 3);
            _editorData.SetPoint(6, 4);
        }
        
        {
            _editorData.SetPoint(-1, 2);
            _editorData.SetPoint(-1, 3);
            _editorData.SetPoint(-1, 4);
            
            _editorData.SetPoint(-2, 1);
            _editorData.SetPoint(-2, 3);
            _editorData.SetPoint(-2, 5);
            
            _editorData.SetPoint(-3, 1);
            _editorData.SetPoint(-3, 2);
            _editorData.SetPoint(-3, 4);
            _editorData.SetPoint(-3, 5);
            _editorData.SetPoint(-3, 6);
            
            _editorData.SetPoint(-4, 1);
            _editorData.SetPoint(-4, 3);
            _editorData.SetPoint(-4, 6);
            
            _editorData.SetPoint(-5, 2);
            _editorData.SetPoint(-5, 3);
            
            _editorData.SetPoint(-6, 3);
            _editorData.SetPoint(-6, 4);
        }

        {
            _editorData.SetPoint(1, -2);
            _editorData.SetPoint(1, -3);
            _editorData.SetPoint(1, -4);
            
            _editorData.SetPoint(2, -1);
            _editorData.SetPoint(2, -3);
            _editorData.SetPoint(2, -5);
            
            _editorData.SetPoint(3, -1);
            _editorData.SetPoint(3, -2);
            _editorData.SetPoint(3, -4);
            _editorData.SetPoint(3, -5);
            _editorData.SetPoint(3, -6);
            
            _editorData.SetPoint(4, -1);
            _editorData.SetPoint(4, -3);
            _editorData.SetPoint(4, -6);
            
            _editorData.SetPoint(5, -2);
            _editorData.SetPoint(5, -3);
            
            _editorData.SetPoint(6, -3);
            _editorData.SetPoint(6, -4);
        }
        
        {
            _editorData.SetPoint(-1, -2);
            _editorData.SetPoint(-1, -3);
            _editorData.SetPoint(-1, -4);
            
            _editorData.SetPoint(-2, -1);
            _editorData.SetPoint(-2, -3);
            _editorData.SetPoint(-2, -5);
            
            _editorData.SetPoint(-3, -1);
            _editorData.SetPoint(-3, -2);
            _editorData.SetPoint(-3, -4);
            _editorData.SetPoint(-3, -5);
            _editorData.SetPoint(-3, -6);
            
            _editorData.SetPoint(-4, -1);
            _editorData.SetPoint(-4, -3);
            _editorData.SetPoint(-4, -6);
            
            _editorData.SetPoint(-5, -2);
            _editorData.SetPoint(-5, -3);
            
            _editorData.SetPoint(-6, -3);
            _editorData.SetPoint(-6, -4);
        }
    }
}
