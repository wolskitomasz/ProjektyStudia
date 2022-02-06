using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
//using Windows.UI.Xaml.Controls;


namespace WolskiAstro
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        //nowe
        Rakieta gracz;
        Meteor wrog;
        Meteor wrogDrugi;
        Texture2D rakieta, control, niebo, meteor, pocisk;
        SoundEffectInstance wybuchraz;
        DateTime GameOver;
        SoundEffect wybuch;
        bool isthegameOver = false;
        SpriteFont font, font2;
       public string koniec;
 
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 480;
            _graphics.PreferredBackBufferHeight = 800;
            _graphics.ApplyChanges();
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            rakieta = this.Content.Load<Texture2D>("AnimRakiety");
            control = this.Content.Load<Texture2D>("control");
            niebo = this.Content.Load<Texture2D>("niebo");
            meteor = this.Content.Load<Texture2D>("meteor");
            pocisk = this.Content.Load<Texture2D>("pocisk2D");
            gracz = new Rakieta(rakieta, pocisk);
            wrog = new Meteor(meteor);
            wrogDrugi = new Meteor(meteor);

            wybuch = this.Content.Load<SoundEffect>("wybuch");
            wybuchraz = wybuch.CreateInstance();



            font = Content.Load<SpriteFont>("File");
            font2 = Content.Load<SpriteFont>("File");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                gracz.MoveU();
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                gracz.MoveD();
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                gracz.MoveL();
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                gracz.MoveR();

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                gracz.Wystrzel();

            wrog.Update();
            wrogDrugi.Update();
            gracz.LotPocisku();

            if (wrog.Kolizja(gracz) || wrogDrugi.Kolizja(gracz))
            {
                wybuchraz.Play();
                isthegameOver = true;
                GameOver = DateTime.Now;
               /* ContentDialog przegrana = new ContentDialog
                {
                    Title = "Game Over!",
                    Content = "Koniec gry.",
                    CloseButtonText = "OK"

                }; */
            }

            if (isthegameOver == true)
            {
                if ((DateTime.Now - GameOver).TotalSeconds >= 0.5f)
                {

                    Exit();
                }

            }
           

            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            _spriteBatch.Draw(niebo, new Vector2(0, 0), Color.Red);
            wrog.Draw(_spriteBatch);
            wrogDrugi.Draw(_spriteBatch);
            _spriteBatch.Draw(control, new Vector2(0, 583), Color.White);
            gracz.Draw(_spriteBatch);

            if (isthegameOver == true)
            {
                _spriteBatch.DrawString(font, koniec = "Game Over!!!", new Vector2(110, 110), Color.White);
 
            }
            _spriteBatch.DrawString(font2, "Wynik= " + (wrog.Punkty() + wrogDrugi.Punkty()),
                new Vector2(380, 770), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
