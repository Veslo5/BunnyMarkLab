using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_NET7;

public class Bunny
{
    public float PositionX { get; set; }
    public float PositionY { get; set; }

    public float SpeedX { get; set; }
    public float SpeedY { get; set; }

    public Color Color { get; set; }
}

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private List<Bunny> _bunnies;

    private int _bunniesCount;
    private int _screenWidth, _screenHeight;

    private Texture2D _bunnyTexture;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _bunnies = new List<Bunny>();
        _bunniesCount = 0;
    }

    protected override void Initialize()
    {

        _graphics.PreferredBackBufferWidth = 800;
        _graphics.PreferredBackBufferHeight = 600;
        _graphics.SynchronizeWithVerticalRetrace = false;
        IsFixedTimeStep = false;
        _graphics.ApplyChanges();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _screenWidth = 800;
        _screenHeight = 600;

        _bunnyTexture = Texture2D.FromFile(_graphics.GraphicsDevice, "wabbit_alpha.png");


        // TargetElapsedTime = TimeSpan.FromSeconds(1d / 60d);

    }

    protected override void Update(GameTime gameTime)
    {

        var mouseState = Mouse.GetState();

        if (mouseState.LeftButton == ButtonState.Pressed)
        {
            for (int i = 0; i < 100; i++)
            {
                var bunny = new Bunny()
                {
                    PositionX = mouseState.X,
                    PositionY = mouseState.Y,
                    SpeedX = (float)Random.Shared.Next(-250, 250) / 60.0f,
                    SpeedY = (float)Random.Shared.Next(-250, 250) / 60.0f,
                    Color = new Color(Random.Shared.Next(50, 240), Random.Shared.Next(80, 240), Random.Shared.Next(100, 240), 255)
                };
                _bunnies.Add(bunny);

                _bunniesCount++;
            }
        }

        for (int i = 0; i < _bunniesCount; i++)
        {
            var bunny = _bunnies[i];
            bunny.PositionX += bunny.SpeedX;
            bunny.PositionY += bunny.SpeedY;


            if ((bunny.PositionX + _bunnyTexture.Width / 2) > _screenWidth ||
            (bunny.PositionX + _bunnyTexture.Width / 2) < 0)
                bunny.SpeedX *= -1;

            if ((bunny.PositionY + _bunnyTexture.Height / 2) > _screenHeight ||
            (bunny.PositionY + _bunnyTexture.Height / 2) < 0)
                bunny.SpeedY *= -1;
        }


        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);

        _spriteBatch.Begin();
        for (int i = 0; i < _bunniesCount; i++)
        {
            var bunny = _bunnies[i];
            _spriteBatch.Draw(_bunnyTexture, new Vector2(bunny.PositionX, bunny.PositionY), bunny.Color);
        }
        _spriteBatch.End();


        this.Window.Title = 1 / gameTime.ElapsedGameTime.TotalSeconds + "_" + _bunniesCount;

        base.Draw(gameTime);
    }
}
