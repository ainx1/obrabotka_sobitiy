using obrabotka_sobitiy.Objects;

namespace obrabotka_sobitiy
{
    public partial class Form1 : Form
    {
        List<BaseObject> objects = new();
        Player player;
        Marker marker;
        public Form1()
        {
            InitializeComponent();

            player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0);
            marker = new Marker(pbMain.Width / 2 + 50, pbMain.Height / 2 + 50, 0);

            objects.Add(marker);

            objects.Add(player);
            objects.Add(new MyRectangle(50, 50, 0));
            objects.Add(new MyRectangle(100, 100, 45));

        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            g.Clear(Color.White);

            foreach (var obj in objects)
            {
                if (obj != player && player.Overlaps(obj, g))
                {
                    // и если было вывожу информацию на форму
                    txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] Игрок пересекся с {obj}\n" + txtLog.Text;
                }
                g.Transform = obj.GetTransform();
                obj.Render(g);
            }
            //var matrix = g.Transform;
            //matrix.Translate(myRect.X, myRect.Y); // смещаем ее в пространстве
            //matrix.Rotate(myRect.Angle);
            //g.Transform = myRect.GetTransform(); // устанавливаем новую матрицу

            //myRect.Render(g); // теперь так рисуем
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //вектор между игроком и маркером
            float dx = marker.X - player.X;
            float dy = marker.Y - player.Y;

            float length = MathF.Sqrt(dx * dx + dy * dy);
            dx /= length; // нормализуем координаты
            dy /= length; // нормализуем координаты

            //пересчитываем координаты игрока
            player.X += dx * 2; // 2 - это скорость
            player.Y += dy * 2;

            pbMain.Invalidate(); // перерисовываем картинку
        }

        private void pbMain_MouseClick(object sender, MouseEventArgs e)
        {
            marker.X = e.X;
            marker.Y = e.Y;
        }
    }
}
