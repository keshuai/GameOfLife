using System;
using System.Collections.Generic;
using GOL;
using UnityEngine;

public class GOLView : MonoBehaviour
{
    public GOLRender Render;
    public GOLDataEditor DataEditor;
    public float TickIntervalSeconds = 0.2f;
    
    private GOL.GameDataCore _gameDataCore = new GameDataCore();
    private bool _ticking = false;
    private float _currentTickTime = 0;

    private void Update()
    {
        this.Tick();
    }

    private void Tick()
    {
        if (_ticking)
        {
            _currentTickTime += Time.deltaTime;
            if (_currentTickTime >= TickIntervalSeconds)
            {
                _currentTickTime -= TickIntervalSeconds;
                _gameDataCore.Tick();
                this.Render.UpdateWithPoints(_gameDataCore.GetPoints());
            }
        }
    }

    public void StartWithInitialPoints(SortedSet<long> initialPoints)
    {
        if (_ticking)
        {
            throw new Exception("ticking...");
        }

        this._gameDataCore.SetInitialPoints(initialPoints);
        _ticking = true;
    }

    public void EventClear()
    {
        this.DataEditor.ClearPoints();
        this.Render.ClearMesh();
    }
    
    public void EventPlay()
    {
        if (_ticking)
        {
            throw new Exception("do event play when ticking");
        }
        
        this.StartWithInitialPoints(this.DataEditor.Points);
    }
    
    public void EventReset2Edit()
    {
        _ticking = false;
        this.Render.UpdateWithPoints(this.DataEditor.Points);
    }
    
    public void EventStop()
    {
        _ticking = false;
    }
    
    public void EventRunInterval(float runInterval)
    {
        if (runInterval < 0.000001f)
        {
            throw new Exception($"error runInterval: {runInterval}");
        }

        this.TickIntervalSeconds = runInterval;
    }
}
