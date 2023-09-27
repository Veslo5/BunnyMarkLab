using System.Numerics;
using Raylib_cs;

public class Bunny
{
    public float PositionX { get; set; }
    public float PositionY { get; set; }

    public float SpeedX { get; set; }
    public float SpeedY { get; set; }

    public Raylib_cs.Color Color { get; set; }
}
public class Program
{
    public static void Main()
    {
        Raylib.InitWindow(800, 600, "Bunny");

        List<Bunny> bunnies = new List<Bunny>();

        Texture2D bunnyTexture = Raylib.LoadTexture("wabbit_alpha.png");
        int bunniesCount = 0;
        int screenWidth = Raylib.GetScreenWidth();
        int screenHeight = Raylib.GetScreenHeight();

        // Raylib.SetTargetFPS(60);

        while (!Raylib.WindowShouldClose())
        {

            if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT))
            {
                for (int i = 0; i < 100; i++)
                {
                    var pos = Raylib.GetMousePosition();
                    var bunny = new Bunny()
                    {
                        PositionX = pos.X,
                        PositionY = pos.Y,
                        SpeedX = (float)Raylib.GetRandomValue(-250, 250) / 60.0f,
                        SpeedY = (float)Raylib.GetRandomValue(-250, 250) / 60.0f,
                        Color = new Color(Raylib.GetRandomValue(50, 240), Raylib.GetRandomValue(80, 240), Raylib.GetRandomValue(100, 240), 255)
                    };
                    bunnies.Add(bunny);

                    bunniesCount++;
                }
            }

            // foreach (var bunny in bunnies)
            for (int i = 0; i < bunniesCount; i++)
            {
                var bunny = bunnies[i];
                bunny.PositionX += bunny.SpeedX;
                bunny.PositionY += bunny.SpeedY;


                if ((bunny.PositionX + bunnyTexture.width / 2) > screenWidth ||
                (bunny.PositionX + bunnyTexture.width / 2) < 0)
                    bunny.SpeedX *= -1;

                if ((bunny.PositionY + bunnyTexture.height / 2) > screenHeight ||
                (bunny.PositionY + bunnyTexture.height / 2) < 0)
                    bunny.SpeedY *= -1;
            }

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.WHITE);

            for (int i = 0; i < bunniesCount; i++)
            {
                var bunny = bunnies[i];
                Raylib.DrawTexture(bunnyTexture, (int)bunny.PositionX, (int)bunny.PositionY, bunny.Color);
            }

            Raylib.DrawFPS(0, 0);
            Raylib.DrawText(bunniesCount.ToString(), 0, 20, 20, Color.BLACK);

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();

    }
}
