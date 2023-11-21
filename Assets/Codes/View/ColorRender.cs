using System;
using System.Collections.Generic;
using UnityEngine;

public class ColorRender : MonoBehaviour
{
    public Material Material;
    
    protected Mesh _mesh;
    protected List<Vector3> _vects = new List<Vector3>();
    protected List<int> _vectIndexList = new List<int>();
    
    private void OnDestroy()
    {
        if (_mesh != null)
        {
            if (Application.isPlaying)
            {
                GameObject.Destroy(_mesh);
            }
            else
            {
                GameObject.DestroyImmediate(_mesh);  
            }
            _mesh = null;
        }
    }

    void LateUpdate()
    {
        if (_mesh != null && this.Material != null)
            Graphics.DrawMesh(_mesh, this.transform.localToWorldMatrix, this.Material, this.gameObject.layer);
    }

    protected void ClearBuffer()
    {
        _vects.Clear();
        _vectIndexList.Clear();
    }

    private void AutoVectIndex()
    {
        if (_vects.Count % 4 != 0)
        {
            throw new Exception($"vect count != 4x: {_vects.Count}");
        }

        var vectCount = _vects.Count / 4;
        for (int i = 0; i < vectCount; ++i)
        {
            var vectIndexOffset = i * 4;
            _vectIndexList.Add(vectIndexOffset + 0);
            _vectIndexList.Add(vectIndexOffset + 3);
            _vectIndexList.Add(vectIndexOffset + 2);
            
            _vectIndexList.Add(vectIndexOffset + 0);
            _vectIndexList.Add(vectIndexOffset + 2);
            _vectIndexList.Add(vectIndexOffset + 1);
        }
    }

    protected void FillVectToMesh()
    {
        this.AutoVectIndex();
        
        if (_mesh == null) _mesh = new Mesh();
        else _mesh.Clear();

        _mesh.SetVertices(_vects);
        _mesh.SetTriangles(_vectIndexList, 0);
    }

    public void ClearMesh()
    {
        _mesh.Clear();
    }
}