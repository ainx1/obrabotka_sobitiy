using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Text;

namespace obrabotka_sobitiy.Objects
{
    class Goal : BaseObject // наследуем BaseObject
    {
        public int Counter = 150; // Текущее значение счетчика
        public Action<Goal> OnTimeout; // Событие конца отсчета

        // создаем конструктор с тем же набором параметров что и в BaseObject
        // base(x, y, angle) - вызывает конструктор родительского класса

        public Goal(float x, float y) : base(x, y, 0)
            {
            }

        public override void Update()
        {
            Counter--; // Уменьшаем счетчик каждый такт
            if (Counter <= 0)
            {
                Counter = 150; // Сбрасываем для следующего цикла
                if (OnTimeout != null)
                {
                    OnTimeout(this); // Генерируем событие
                }
            }
        }

        public override void Render(Graphics g)
            {
                // Рисуем зеленый кружок
                g.FillEllipse(new SolidBrush(Color.GreenYellow), -15, -15, 30, 30);
                g.DrawEllipse(new Pen(Color.Black, 2), -15, -15, 30, 30);

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
                path.AddEllipse(-15, -15, 30, 30);
                return path;
            }
        }
    }