using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenArt.Core.Classes
{
    public class Array2D
    {
        public byte [] Data;
        protected int _width;
        protected int _height;


        public Array2D(int width, int height) :
            this(width, height, 1)
        {
        }

        protected Array2D(int width, int height, int stride)
        {
            this.Data = new byte[width * height * stride];
            this._width = width;
            this._height = height;
        }

        public int Width
        {
            get { return _width; }
        }

        public int Height
        {
            get { return _height; }
        }

        public int Length
        {
            get { return this.Data.Length; }
        }

    }

    public class Array2D<T>
    {
        public T [] Data;
        protected int _width;
        protected int _height;


        public Array2D(int width, int height) :
            this(width, height, 1)
        {
        }

        protected Array2D(int width, int height, int stride)
        {
            this.Data = new T[width * height * stride];
            this._width = width;
            this._height = height;
        }

        public void FillByValue(T value)
        {
            for (int i = 0; i < Data.Length; i++)
                Data[i] = value;
        }

        public int Width
        {
            get { return _width; }
        }

        public int Height
        {
            get { return _height; }
        }

        public int Length
        {
            get { return this.Data.Length; }
        }

        public T this[int x, int y]
        {
            get { int index = y * _width + x; return Data[index]; }
            set { int index = y * _width + x; Data[index] =value; }
        }

    }
}
