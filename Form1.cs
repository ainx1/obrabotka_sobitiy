using obrabotka_sobitiy.Objects;

namespace obrabotka_sobitiy
{
    public partial class Form1 : Form
    {
        List<BaseObject> objects = new();
        Player player;
        Marker marker;

        int score = 0;
        Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();

            player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0);

            // добавляю реакцию на пересечение
            player.OnOverlap += (p, obj) =>
            {
                txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] Игрок пересекся с {obj}\n" + txtLog.Text;
            };

            player.OnOverlap += (p, obj) =>
            {
                // если объект зеленый круг (Goal)
                if (obj is Goal g)
                {
                    
                    g.X = rnd.Next(30, pbMain.Width - 30);
                    g.Y = rnd.Next(30, pbMain.Height - 30);

                    
                    score++;
                    
                    lblScore.Text = $"Очки: {score}";
                }
            };

            // добавил реакцию на пересечение с маркером
            player.OnMarkerOverlap += (m) =>
            {
                objects.Remove(m);
                marker = null;
            };

            marker = new Marker(pbMain.Width / 2 + 50, pbMain.Height / 2 + 50, 0);

            objects.Add(marker);
            objects.Add(player);
            
            objects.Add(new Goal(rnd.Next(100, 400), rnd.Next(100, 300)));
            objects.Add(new Goal(rnd.Next(100, 400), rnd.Next(100, 300)));

        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            g.Clear(Color.White);

            // objects на objects.ToList()
            // это будет создавать копию списка
            // и позволит модифицировать ориг objects прямо из цикла foreach
            updatePlayer(); // сюда, теперь сначала вызываем пересчет игрока
            // пересчитываем пересечения
            foreach (var obj in objects.ToList())
            {
                if (obj != player && player.Overlaps(obj, g))
                {
                    player.Overlap(obj); // то есть игрок пересекся с объектом
                    obj.Overlap(player); // и объект пересекся с игроком
                }
            }

            // рендерим объекты
            foreach (var obj in objects)
            {
                g.Transform = obj.GetTransform();
                obj.Render(g);
            }
                       
        }

        private void updatePlayer()
        {
            if (marker != null)
            {
                float dx = marker.X - player.X;
                float dy = marker.Y - player.Y;

                float length = MathF.Sqrt(dx * dx + dy * dy);
                dx /= length;
                dy /= length;

                // по сути мы теперь используем вектор dx, dy
                // как вектор ускорения, точнее даже вектор притяжения
                // который притягивает игрока к маркеру
                // 0.5 просто коэффициент который подобрал на глаз
                // и который дает естественное ощущение движения
                player.vX += dx * 0.75f;
                player.vY += dy * 0.75f;

                // расчитываем угол поворота игрока 
                player.Angle = 90 - MathF.Atan2(player.vX, player.vY) * 180 / MathF.PI;
            }
            // тормозящий момент,
            // нужен чтобы, когда игрок достигнет маркера произошло постепенное замедление
            player.vX += -player.vX * 0.1f;
            player.vY += -player.vY * 0.1f;

            // пересчет позиция игрока с помощью вектора скорости
            player.X += player.vX;
            player.Y += player.vY;
            // запрашиваем обновление pbMain
            // это вызовет метод pbMain_Paint по новой
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            // это вызовет метод pbMain_Paint по новой
            pbMain.Invalidate();
        }

        private void pbMain_MouseClick(object sender, MouseEventArgs e)
        {
            // тут добавил создание маркера по клику если он еще не создан
            if (marker == null)
            {
                marker = new Marker(0, 0, 0);
                objects.Add(marker); // и главное не забыть пололжить в objects
            }

            // а это так и остается
            marker.X = e.X;
            marker.Y = e.Y;
        }
    }
}
