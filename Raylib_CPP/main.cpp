#include "raylib.h"
#include "raylib-cpp.hpp"
#include <vector>

struct Bunny
{
    float position_x;
    float position_y;

    float speed_x;
    float speed_y;

    raylib::Color color;
};

int main(int argc, char const *argv[])
{
    auto window = raylib::Window(800, 600, "Bunny cpp");

    // I know, I know ... I should have used RAII and smart pointers yaddayadda...  but for testing purposes its OK :D (pls don't kill me)
    auto bunnies = std::vector<Bunny *>();

    auto bunnyTexture = raylib::Texture2D("wabbit_alpha.png");
    int bunniesCount = 0;
    int screenWidth = GetScreenWidth();
    int screenHeight = GetScreenHeight();

    while (!window.ShouldClose())
    {

        if (IsMouseButtonDown(MOUSE_BUTTON_LEFT))
        {
            for (size_t i = 0; i < 100; i++)
            {
                auto pos = GetMousePosition();
                Bunny *bunny = new Bunny();
                bunny->position_x = pos.x;
                bunny->position_y = pos.y;
                bunny->speed_x = GetRandomValue(-250, 250) / 60.0f;
                bunny->speed_y = GetRandomValue(-250, 250) / 60.0f;
                bunny->color = raylib::Color(GetRandomValue(50, 240), GetRandomValue(80, 240), GetRandomValue(100, 240), 255);

                bunnies.push_back(bunny);

                bunniesCount++;
            }
        }

        for (size_t i = 0; i < bunniesCount; i++)
        {
            Bunny *bunny = bunnies[i];
            bunny->position_x += bunny->speed_x;
            bunny->position_y += bunny->speed_y;

            if ((bunny->position_x + bunnyTexture.width / 2) > screenWidth ||
                (bunny->position_x + bunnyTexture.width / 2) < 0)
                bunny->speed_x *= -1;

            if ((bunny->position_y + bunnyTexture.height / 2) > screenHeight ||
                (bunny->position_y + bunnyTexture.height / 2) < 0)
                bunny->speed_y *= -1;
        }

        BeginDrawing();
        window.ClearBackground(WHITE);

        for (size_t i = 0; i < bunniesCount; i++)
        {
            Bunny *bunny = bunnies[i];
            bunnyTexture.Draw(bunny->position_x, bunny->position_y, bunny->color);
        }

        DrawFPS(0, 0);
        raylib::DrawText(std::to_string(bunniesCount), 0, 20, 20, BLACK);

        EndDrawing();
    }

    for (size_t i = 0; i < bunniesCount; i++)
    {
        delete bunnies[i];
    }

    CloseWindow();

    return 0;
}
