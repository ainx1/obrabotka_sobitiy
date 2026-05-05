using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Text;

namespace obrabotka_sobitiy.Objects
{
    class Goal : BaseObject // наследуем BaseObject
    {
        public float Size = 30; 
        public Action<Goal> OnSizeZero; 

        public int Counter = 100; 
        public Action<Goal> OnTimeout; 

        // создаем конструктор с тем же набором параметров что и в BaseObject
        // base(x, y, angle) - вызывает конструктор родительского класса

        public Goal(float x, float y) : base(x, y, 0)
            {
            }

        public override void Update()
        {
            if (Size > 0)
            {
                Size -= 0.2f;
            }

            Counter--;

            if (Size <= 0)
            {
                Size = 0;
                OnSizeZero?.Invoke(this); // говорим форме об удалении
            }

            if (Counter <= 0)
            {
                OnTimeout?.Invoke(this); // говорим форме об удалении
            }
        }

        public override void Render(Graphics g)
            {
            // рисуем кружок, размер которого зависит от переменной Size
            // -Size/2, чтобы центр круга оставался в точке X, Y
            g.FillEllipse(new SolidBrush(Color.GreenYellow), -Size / 2, -Size / 2, Size, Size);
            g.DrawEllipse(new Pen(Color.Black, 2), -Size / 2, -Size / 2, Size, Size);

            // Задание 3 Чтобы отрендерить текст надо написать как-то так
            g.DrawString(
                Counter.ToString(),
                new Font("Verdana", 8), // шрифт и размер
                new SolidBrush(Color.Green), // цвет шрифта
                10, 10 // точка в которой нарисовать текст
            );
        }

            public override GraphicsPath GetGraphicsPath()
            {
                var path = base.GetGraphicsPath();
                path.AddEllipse(-Size / 2, -Size / 2, Size, Size);
                return path;
            }
        }
    }