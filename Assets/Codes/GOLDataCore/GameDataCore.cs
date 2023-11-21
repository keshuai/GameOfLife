using System.Collections.Generic;

namespace GOL
{
    public class GameDataCore
    {
        private readonly Points _points = new Points();

        public SortedSet<long> GetPoints() => _points.GetPoints();

        public void AddPoint(int x, int y) => _points.AddPoint(x, y);

        public void SetInitialPoints(SortedSet<long> initialPoints) => _points.SetInitialPoints(initialPoints);

        public void Tick()
        {
            _points.Tick();
        }
    }

    public static class Int2LongUtility
    {
        // 将两个int合成一个long
        public static long Int2Long(int v1, int v2)
        {
            return ((long)v1 << 32) | (uint)v2;
        }
        
        // 将两个long还原成两个int
        public static (int v1, int v2) LongToInt2(long v0)
        {
            return ((int)(v0 >> 32), (int)v0);
        }
    }

    public class Points
    {
        private readonly SortedSet<long> _livePoints = new SortedSet<long>();
        private readonly SortedSet<long> _deadPoints = new SortedSet<long>();
        private readonly List<long> _bornPoints = new List<long>();
        private readonly List<long> _willDeadTempPoints = new List<long>();

        private static readonly long[] _tempArrayNeighbor8Points = new long[8];

        public SortedSet<long> GetPoints() => _livePoints;
        
        public void SetInitialPoints(SortedSet<long> initialPoints)
        {
            _livePoints.Clear();
            foreach(var p in initialPoints)
            {
                _livePoints.Add(p);
            }
        }
        
        public void AddPoint(int x, int y)
        {
            var point = Int2LongUtility.Int2Long(x, y);
            _livePoints.Add(point);
        }

        public void Tick()
        {
            this.GetAllDeadPoints();
            this.CheckAllDeadPoints2BornLivePoint();
            this.CheckAllLivePoints();
            this.AddBorn2LivePoints();
        }
        
        static long[] GetNeighbor8Points(long point)
        {
            var (x, y) = Int2LongUtility.LongToInt2(point);
            
            var result = _tempArrayNeighbor8Points;
            
            // 上三
            result[0] = Int2LongUtility.Int2Long(x - 1, y + 1);
            result[1] = Int2LongUtility.Int2Long(x + 0, y + 1);
            result[2] = Int2LongUtility.Int2Long(x + 1, y + 1);
                
            // 中二
            result[3] = Int2LongUtility.Int2Long(x - 1, y + 0);
            result[4] = Int2LongUtility.Int2Long(x + 1, y + 0);
                
            // 下三
            result[5] = Int2LongUtility.Int2Long(x - 1, y - 1);
            result[6] = Int2LongUtility.Int2Long(x + 0, y - 1);
            result[7] = Int2LongUtility.Int2Long(x + 1, y - 1);

            return result;
        }

        // 获取邻居8个点的存活数目
        private int GetNeighbor8PointsLiveNum(long point)
        {
            var liveNum = 0;
            
            var neighbor8Point = GetNeighbor8Points(point);
            foreach (var p in neighbor8Point)
            {
                if (_livePoints.Contains(p))
                {
                    ++liveNum;
                }
            }

            return liveNum;
        }

        private void GetAllDeadPoints()
        {
            void Add2DeadPoints(long point)
            {
                if (_livePoints.Contains(point) || _deadPoints.Contains(point))
                {
                    return;
                }

                _deadPoints.Add(point);
            }

            void CheckPoint(long point)
            {
                var neighbor8Point = GetNeighbor8Points(point);
                foreach (var p in neighbor8Point)
                {
                    Add2DeadPoints(p);
                }
            }
            
            _deadPoints.Clear();
            
            foreach (var p in _livePoints)
            {
                CheckPoint(p);
            }
        }

        // 检查所有记录的死亡的点, 产生新的点
        private void CheckAllDeadPoints2BornLivePoint()
        {
            // 康威生命游戏 死亡细胞的规则：
            // 当前细胞为死亡状态时，当周围有3个存活细胞时，该细胞变成存活状态。
            foreach (var point in _deadPoints)
            {
                var liveNum = GetNeighbor8PointsLiveNum(point);
                if (liveNum == 3)
                {
                    _bornPoints.Add(point);
                }
            }
        }

        private void CheckAllLivePoints()
        {
            // 康威生命游戏 存活细胞的规则：
            // 当前细胞为存活状态时，当周围的存活细胞低于2个时（不包含2个），该细胞变成死亡状态。（模拟生命数量稀少）
            // 当前细胞为存活状态时，当周围有2个或3个存活细胞时，该细胞保持原样。
            // 当前细胞为存活状态时，当周围有超过3个存活细胞时，该细胞变成死亡状态。（模拟生命数量过多）
            foreach (var point in _livePoints)
            {
                var liveNum = GetNeighbor8PointsLiveNum(point);
                if (liveNum < 2 || liveNum > 3)
                {
                    _willDeadTempPoints.Add(point);
                }
            }

            foreach (var point in _willDeadTempPoints)
            {
                _livePoints.Remove(point);
            }
            
            _willDeadTempPoints.Clear();
        }

        private void AddBorn2LivePoints()
        {
            foreach (var point in _bornPoints)
            {
                _livePoints.Add(point);
            }
            
            _bornPoints.Clear();
        }
    }
}
