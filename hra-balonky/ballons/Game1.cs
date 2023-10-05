using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System.Windows.Forms;

namespace balonky
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {    
        // obecn� data
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public Timer timer = new Timer(); // �asova�
        public static Random rand = new Random(); // randomiz�tor
        public byte stav = 1, level = 1;  // hodnoty levelu a stavu
        private MouseState mys;
        public static int sirkaOkna = 1366, vyskaOkna = 768; // rozli�en�
        public bool canshot = true, levelMustInit = true; // pomocn� bool prom�nn�
        private SpriteFont scorefont; // p�smo

        // textury (obr�zky)
        private Texture2D background, kurzor, pozadi2, compdesk, panel1; 
        public static Texture2D[] spriteballons = new Texture2D[5]; 

        // zvuky, muzika
        private Song hudba;
        public SoundEffect bum;

        // prom�nn� pot�ebn� k po��t�n� sk�re
        public int score = 0, score_pricist = 0;
        public byte sestreleno_zaraz = 0;

        // pole level� a bal�nk�
        public levels[] levely = new levels[10];
        public balonek[] ballony = new balonek[40];

        public Game1() // konstruktor
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected void TimerOnTick(object sender, EventArgs e) // jeden tick �asova�e
        {
            //Timer timer = (Timer)sender;
            if (levely[level - 1].secondsa == 0) 
            {
               levely[level - 1].secondsa = 60;
                levely[level - 1].minutesa -= 1;
            }
            levely[level - 1].secondsa -= 1;
        }
        protected override void Initialize()
        {    
            // nastavit okno
            graphics.PreferredBackBufferWidth = sirkaOkna;
            graphics.PreferredBackBufferHeight = vyskaOkna;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            // nastavit �asova�
            timer.Interval = 1000;
            timer.Tick += new EventHandler(TimerOnTick);

            // nastavit levely
            levely[0] = new levels(2, 30, 1000);
         
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // nahr�n� obr�zk� do prom�nn�ch
            spriteballons[0] = Content.Load<Texture2D>("balon1");
            spriteballons[1] = Content.Load<Texture2D>("balon2");
            spriteballons[2] = Content.Load<Texture2D>("balon3");
            spriteballons[3] = Content.Load<Texture2D>("balon4");
            spriteballons[4] = Content.Load<Texture2D>("balon5");
            compdesk = Content.Load<Texture2D>("compdesk"); 
            pozadi2 = Content.Load<Texture2D>("pozadi2");
            kurzor = Content.Load<Texture2D>("kurzor");
            background = Content.Load<Texture2D>("pozadi");
            panel1 = Content.Load<Texture2D>("panel1");

            // nahr�n� hudby a fontu
            hudba = Content.Load<Song>("can2");
            bum = Content.Load<SoundEffect>("bum");
            scorefont = Content.Load<SpriteFont>("SpriteFont1");

            // vytvo��me bal�nky
            for (int i = 0; i < 40; i++)
            {
                int color = rand.Next(4);
                ballony[i] = new balonek(spriteballons[color], color, 1 + rand.Next(5));
            }

            // nastavit p�ehr�va�
            MediaPlayer.Play(hudba);
            MediaPlayer.IsRepeating = true;
            
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape)) this.Exit(); // konec?
            
            if (stav == 1) // menu cyklus
            {
                mys = Mouse.GetState(); // jak je na tom my�?
                if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Enter)) // klikneme-li enter, p�epnout do hern� smy�ky
                {
                    stav = 2; // p�epnout na hern� cyklus
                    for (int i = 0; i < 40; i++) // vygenerovat nov� bal�nky
                    {
                        ballony[i].generateballon();
                    }
                }
                for (int i = 0; i < 40; i++) // poslat bal�nky nahoru
                {
                    ballony[i].flyup();
                }
            }

            if (stav == 2) // hern� cyklus
            {
                timer.Enabled = true; // spustit �as
                for (int i = 0; i < 40; i++)
                {
                    mys = Mouse.GetState(); // co my�?
                    if (mys.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed) // pokud stisknuto lev� tla��tko
                    {
                        if (canshot) // pokud p�edt�m nebylo dr�eno
                        {
                            // trefili jsme se?
                            if (mys.X > ballony[i].X && mys.X < ballony[i].X + 53 && mys.Y > ballony[i].Y && mys.Y < ballony[i].Y + 64)
                            {
                                ballony[i].aktivni = false;
                                sestreleno_zaraz += 1;
                                score_pricist += ballony[i].score_value;
                                bum.Play();
                                levely[level - 1].sestrelenocelk[ballony[i].color] += 1;
                               
                            }                                               
                        }
                    }

                    if (ballony[i].aktivni == true) //pokud nen� sest�elen
                    {
                        ballony[i].flyup(); // poslat ho nahoru
                    }
                    else // pokud je sest�elen
                    {
                        ballony[i].generateballon(); // vygenerovat nov�
                    }
                }

                if (sestreleno_zaraz > 1)
                {
                    score_pricist = score_pricist * sestreleno_zaraz * 5;
                }

                score += score_pricist; // p�i��st sk�re
                if (sestreleno_zaraz == 0 && mys.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && canshot == true) score -= 1; // netrefili jsme se? ode��st bod 

                // vynulovat pomocn� prom�nn�
                sestreleno_zaraz = 0;
                score_pricist = 0;

                if (levely[level - 1].target <= score) // vyhr�li jsme?
                {
                    levely[level - 1].win = true; // v�hra
                    stav = 3; // p�epnout do cyklu mezileveln�ch oken
                }

                canshot = false;
                mys = Mouse.GetState(); // stav my�i

                // pustili-li jsme my�, m��em sest�elit dal��
                if (mys.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released) canshot = true;

                base.Update(gameTime);
            }

            if (stav == 3) // mezileveln� cyklus
            {
                mys = Mouse.GetState(); // stav my�i
                if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Enter))
                {
                    stav = 2;
                    System.Threading.Thread.Sleep(3000);
                    for (int i = 0; i < 40; i++)
                    {
                        ballony[i].generateballon();
                    }
                }
                for (int i = 0; i < 40; i++) // poslat bal�ny nahoru
                {
                    ballony[i].flyup();
                }
            }
        }

        protected override void Draw(GameTime gameTime) // v�echno vykreslit (60x za sekundu)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue); // vy�istit, co tam bylo z posledn�ho kreslen�

            if (stav == 1) // menu kreslen�
            {
                spriteBatch.Begin();
                spriteBatch.Draw(background, Vector2.Zero, Color.White); // vykreslit pozad�
                for (int i = 0; i < 40; i++) // vykreslit v�echny bal�nky
                {
                    spriteBatch.Draw(ballony[i].ballon, new Vector2(ballony[i].X, ballony[i].Y), Color.White);
                }
                spriteBatch.Draw(pozadi2, new Vector2(sirkaOkna / 2 - 860 / 2, 5), Color.White); // vykreslit �vod
                spriteBatch.Draw(kurzor, new Vector2(mys.X - 22, mys.Y - 22), Color.White); // vykreslit kurzor
                spriteBatch.End();
            }

            if (stav == 2)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(background, Vector2.Zero, Color.White);
                for (int i = 0; i < 40; i++)
                {
                    spriteBatch.Draw(ballony[i].ballon, new Vector2(ballony[i].X, ballony[i].Y), Color.White);
                }
                spriteBatch.Draw(panel1, new Vector2(sirkaOkna - 739,-11), Color.White);
                drawTexts1();
                spriteBatch.Draw(kurzor, new Vector2(mys.X - 22, mys.Y - 22), Color.White);
                spriteBatch.End();
            }

            if (stav == 3)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(background, Vector2.Zero, Color.White);
                for (int i = 0; i < 40; i++)
                {
                    spriteBatch.Draw(ballony[i].ballon, new Vector2(ballony[i].X, ballony[i].Y), Color.White);
                }
                spriteBatch.Draw(compdesk, new Vector2(sirkaOkna / 2 - compdesk.Width / 2, vyskaOkna / 2 - compdesk.Height / 2), Color.White);
                drawTexts2();
                spriteBatch.Draw(kurzor, new Vector2(mys.X - 22, mys.Y - 22), Color.White);
                spriteBatch.End();
            }
    
            base.Draw(gameTime);
        }
        public void drawTexts1()
        {
          
           spriteBatch.DrawString(scorefont, score.ToString(), new Vector2(sirkaOkna - 345, 3), Color.White);
           spriteBatch.DrawString(scorefont, levely[level - 1].minutesa.ToString(), new Vector2(sirkaOkna - 570, 3), Color.White);
           spriteBatch.DrawString(scorefont, ":", new Vector2(sirkaOkna - 548, 3), Color.White);
           spriteBatch.DrawString(scorefont, levely[level - 1].secondsa.ToString(), new Vector2(sirkaOkna - 530, 3), Color.White);
           spriteBatch.DrawString(scorefont, levely[level - 1].target.ToString(), new Vector2(sirkaOkna - 90, 3), Color.White);
        }

        public void drawTexts2()
        {
            
            spriteBatch.DrawString(scorefont, score.ToString(), new Vector2(sirkaOkna - 140, 10), Color.White);
            spriteBatch.DrawString(scorefont, levely[level - 1].sestrelenocelk[0].ToString(), new Vector2((sirkaOkna / 2 - compdesk.Width / 2) + 90, (vyskaOkna / 2 - compdesk.Height / 2) + 80), Color.Black);
            spriteBatch.DrawString(scorefont, levely[level - 1].sestrelenocelk[1].ToString(), new Vector2((sirkaOkna / 2 - compdesk.Width / 2) + 90, (vyskaOkna / 2 - compdesk.Height / 2) + 132), Color.Black);
            spriteBatch.DrawString(scorefont, levely[level - 1].sestrelenocelk[2].ToString(), new Vector2((sirkaOkna / 2 - compdesk.Width / 2) + 90, (vyskaOkna / 2 - compdesk.Height / 2) + 184), Color.Black);
            spriteBatch.DrawString(scorefont, levely[level - 1].sestrelenocelk[3].ToString(), new Vector2((sirkaOkna / 2 - compdesk.Width / 2) + 90, (vyskaOkna / 2 - compdesk.Height / 2) + 235), Color.Black);
            spriteBatch.DrawString(scorefont, levely[level - 1].sestrelenocelk[4].ToString(), new Vector2((sirkaOkna / 2 - compdesk.Width / 2) + 90, (vyskaOkna / 2 - compdesk.Height / 2) + 287), Color.Black);
            spriteBatch.DrawString(scorefont, ":", new Vector2(sirkaOkna - 418, 10), Color.White);
            spriteBatch.DrawString(scorefont, levely[level - 1].secondsa.ToString(), new Vector2(sirkaOkna - 400, 10), Color.White);
        }
    }
}
