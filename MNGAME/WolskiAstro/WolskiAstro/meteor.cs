using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//
using Microsoft.Xna.Framework.Audio;


namespace WolskiAstro
{
    class Meteor
    {
       
        Texture2D texture;
        Vector2 position, predkosc;
        int nrKlatki;
        int nrCyklu;
        Random generujLL = new Random();
        int punkty = 0;



        
        public Meteor(Texture2D meteor)
        {
            position = new Vector2(generujLL.Next(100, 300), 0);
            predkosc = new Vector2(x: generujLL.Next(-13, 13), y: generujLL.Next(3, 10));
            texture = meteor;
            nrKlatki = 0;
            nrCyklu = 0;


        }
        public Vector2 GetPosition()
        {
            return position;
        }
        public void Startuj()
        {
            position = new Vector2(generujLL.Next(100, 300), 0);
            predkosc = new Vector2(x: generujLL.Next(-13, 13), y: generujLL.Next(3, 10));
        }
        public Vector2 GetSize()
        {
            return new Vector2(texture.Width / 3, texture.Height);
        }
        public bool Kolizja(Rakieta gracz)
        {
            Rectangle graczRectangle = new Rectangle((int)gracz.GetPosition().X, (int)gracz.GetPosition().Y, (int)gracz.GetSize().X, (int)gracz.GetSize().Y);
            Rectangle wrogRectangle = new Rectangle((int)position.X, (int)position.Y, (int)GetSize().X, (int)GetSize().Y);

            Rectangle pociskRectangle = new Rectangle((int)gracz.GetPositionPocisk().X, (int)gracz.GetPositionPocisk().Y, 0,0);

            var intersectWrogPocisk = Rectangle.Intersect(pociskRectangle, wrogRectangle);
            var intersectGraczWrog = Rectangle.Intersect(graczRectangle, wrogRectangle);


            if (!intersectWrogPocisk.IsEmpty)
            {
                punkty++;
                Startuj();
                gracz.UsunPocisk();
                
                
            }

            if (intersectGraczWrog.IsEmpty)
            {
                return false;
            }
            else
                return true;


        

    }

        public void Update()
        {
            nrCyklu++;
            if (nrCyklu==8)
            {
                nrKlatki++;
                nrCyklu = 0;
            }
            position += predkosc;
            if (position.X>450 || position.Y>580 || position.Y<0)
            {
                Startuj();
            }

        }
        public int Punkty()
        {
            return punkty;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            int szerokośćKlatki = texture.Width / 3;
            Rectangle klatka = new Rectangle(nrKlatki * szerokośćKlatki, 0, szerokośćKlatki, texture.Height);
            Rectangle rectMeteor = new Rectangle((int)position.X, (int)position.Y,
                klatka.Width, klatka.Height);
  
            spriteBatch.Draw(texture, rectMeteor, klatka, Color.White);
            if (nrKlatki == 3)
                nrKlatki = 0;
            
        }
    }
}
