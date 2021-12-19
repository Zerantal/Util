using System.Runtime.InteropServices;
using System.Drawing;

namespace Util
{
    // add a reference to system.drawing.dll
    // Note: backing fields were added because structs don't automatically supply them.
    [StructLayout(LayoutKind.Sequential)]
    internal readonly struct Rect
    {
        private readonly int _left;
        private readonly int _top;
        private readonly int _right;
        private readonly int _bottom;

        public Rect(Rectangle rectangle)
            : this(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom)
        {
        }
        public Rect(int left, int top, int right, int bottom)
        {
            _left = left;
            _top = top;
            _right = right;
            _bottom = bottom;
        }

        internal Rectangle ToRectangle()
        {
            return new Rectangle(_left, _top, _right - _left, _bottom - _top);
        }
        /*         
                internal static RECT FromRectangle(Rectangle Rectangle)
                {
                    return new RECT(Rectangle.Left, Rectangle.Top, Rectangle.Right, Rectangle.Bottom);
                }
                */
        public override string ToString() => "{Left: " + _left + "; " + "Top: " + _top + "; Right: " + _right + "; Bottom: " + _bottom + "}";

        internal bool Equals(Rect rectangle)
        {
            return rectangle._left == _left && rectangle._top == _top 
                && rectangle._right == _right && rectangle._bottom == _bottom;
        }

        public override bool Equals(object @object)
        {
            switch (@object)
            {
                case Rect rect:
                    return Equals(rect);
                case Rectangle rectangle:
                    return Equals(new Rect(rectangle));
                default:
                    return false;
            }
        }

        public override int GetHashCode()
        {
            return _left.GetHashCode() ^ _right.GetHashCode() ^ _top.GetHashCode() ^ _bottom.GetHashCode();
        }
    }
}