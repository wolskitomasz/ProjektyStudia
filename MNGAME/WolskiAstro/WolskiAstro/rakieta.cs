using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace WolskiAstro
{
    class Rakieta
    {
       
        Texture2D texture;
        Texture2D pocisk2D;
        Vector2 position;

        int nrKlatki;

        Pocisk strzal;

        public Rakieta(Texture2D rakieta, Texture2D pocisk2D)
        {
            position = new Vector2(210, 480);
            this.texture = rakieta;
            this.pocisk2D = pocisk2D;
            nrKlatki = 0;

            strzal.wystrzelony = false;

        }
        public Vector2 GetPositionPocisk()
        {
            return strzal.position;
        }
        private struct Pocisk
        {
            public bool wystrzelony;
            public Vector2 position;
        }
        public void Wystrzel()
        {

            Vector2 korekta = new Vector2(31, 0);
            strzal.wystrzelony = true;
            strzal.position = position + korekta;

        }
        public void LotPocisku()
        {
            if (strzal.wystrzelony)
            {
                strzal.position.Y -= 10;
                if (strzal.position.Y < 0)
                    strzal.wystrzelony = false;

            }

        }
        public void UsunPocisk()
        {
            strzal.wystrzelony = false;
            strzal.position = new Vector2(200, -100);
        }
        public Vector2 GetPosition()
        {
            return position;
        }
        public void MoveL()
        {
            position.X -= 5;
            if (position.X < 0)
                position.X = 0;
        }
        public void MoveR()
        {
            position.X += 5;
            if (position.X > 400)
                position.X = 400;
        }
        public void MoveU()
        {
            position.Y -= 5;
            if (position.Y < 0)
                position.Y = 0;
        }
        public void MoveD()
        {
            position.Y += 5;
            if (position.X > 400)
                position.X = 400;
        }
        public Vector2 GetSize() 
        { 
            return new Vector2(texture.Width / 6, texture.Height);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            int szerokośćKlatki = texture.Width / 6;
            Rectangle klatka = new Rectangle(nrKlatki * szerokośćKlatki, 0, szerokośćKlatki, texture.Height);
            Rectangle rectGracza = new Rectangle((int)position.X, (int)position.Y,
                klatka.Width, klatka.Height);
            spriteBatch.Draw(pocisk2D, strzal.position, Color.White);
            spriteBatch.Draw(texture, rectGracza, klatka, Color.White);


            if (strzal.wystrzelony)
                nrKlatki++;

            if (nrKlatki == 6) 
                nrKlatki = 0;
        }
    }
}
