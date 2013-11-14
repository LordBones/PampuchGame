using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenArt.Core.Classes;

namespace test_app_hledani_cesty
{
    public class WeightSearchShortPath
    {


        public enum MazeTypeObject { Empty = 0, Wall = 1 }

        /// <summary>
        /// value from 0 - 255 
        /// higher value means, path cross this field is longer.
        /// </summary>
        private Array2D<byte> _areaWeights;
        private Array2D<byte> _area;
        private Array2D<int> _areaSumWeights;



        public WeightSearchShortPath(int width, int height, Array2D<byte> area)
        {
            if (width != area.Width || height != area.Height) throw new Exception();

            _areaWeights = new Array2D<byte>(width,height);
            _area = new Array2D<byte>(width, height);
            _areaSumWeights = new Array2D<int>(width, height);

            for (int i = 0; i < _area.Length; i++)
            {
                byte tmp = area.Data[i];
                if (tmp == (int)MazeTypeObject.Empty) _area.Data[i] = (int)MazeTypeObject.Empty;
                else if (tmp == (int)MazeTypeObject.Wall) _area.Data[i] = (int)MazeTypeObject.Wall;
                else _area.Data[i] = (int)MazeTypeObject.Empty;
            }
        }


        public void FindShortestPath(Point start, Point end)
        {
            ComputeSumWeight(start, end);
        }

        private void ComputeSumWeight(Point start, Point end)
        {
            _areaSumWeights.FillByValue(-1);
            _areaSumWeights[start.X, start.Y] = 0;

            Queue<Point> queueForSearch = new Queue<Point>();
            queueForSearch.Enqueue(start);
        }
    }
}
