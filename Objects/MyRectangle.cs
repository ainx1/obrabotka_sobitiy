using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Text;

namespace obrabotka_sobitiy.Objects
{
    class Goal : BaseObject // наследуем BaseObject
    {
        // создаем конструктор с тем же набором параметров что и в BaseObject
        // base(x, y, angle) - вызывает конструктор родительского класса

            public Goal(float x, float y) : base(x, y, 0)
            {
            }

            public override void Render(Graphics g)
            {
                // Рисуем зеленый кружок
                g.FillEllipse(new SolidBrush(Color.GreenYellow), -15, -15, 30, 30);
                g.DrawEllipse(new Pen(Color.Black, 2), -15, -15, 30, 30);
            }

            public override GraphicsPath GetGraphicsPath()
            {
                var path = base.GetGraphicsPath();
                path.AddEllipse(-15, -15, 30, 30);
                return path;
            }
        }
    }