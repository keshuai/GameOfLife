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
}
