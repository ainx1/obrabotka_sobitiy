using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace obrabotka_sobitiy.Objects
{
    class BaseObject
    {
        public float X;
        public float Y;
        public float Angle;

        public BaseObject(float x, float y, float angle)
        {
            /* ... */
        }
        public Matrix GetTransform() { 
            var matrix = new Matrix(); 
            matrix.Translate(X, Y);
            matrix.Rotate(Angle);
            return matrix;
        
        }
        // добавил виртуальный метод для отрисовки
        public virtual void Render(Graphics g)
        {
        } 
    }
}
