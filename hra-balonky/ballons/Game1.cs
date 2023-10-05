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
        // obecná data
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public Timer timer = new Timer(); // èasovaè
        public static Random rand = new Random(); // randomizátor
        public byte stav = 1, level = 1;  // hodnoty levelu a stavu
        private MouseState mys;
        public static int sirkaOkna = 1366, vyskaOkna = 768; // rozlišení
        public bool canshot = true, levelMustInit = true; // pomocné bool promìnné
        private SpriteFont scorefont; // písmo

        // textury (obrázky)
        private Texture2D background, kurzor, pozadi2, compdesk, panel1; 
        public static Texture2D[] spriteballons = new Texture2D[5]; 

        // zvuky, muzika
        private Song hudba;
        public SoundEffect bum;

        // promìnné potøebné k poèítání skóre
        public int score = 0, score_pricist = 0;
        public byte sestreleno_zaraz = 0;

        // pole levelù a balónkù
        public levels[] levely = new levels[10];
        public balonek[] ballony = new balonek[40];

        public Game1() // konstruktor
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected void TimerOnTick(object sender, EventArgs e) // jeden tick èasovaèe
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

            // nastavit èasovaè
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

            // nahrání obrázkù do promìnných
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

            // nahrání hudby a fontu
            hudba = Content.Load<Song>("can2");
            bum = Content.Load<SoundEffect>("bum");
            scorefont = Content.Load<SpriteFont>("SpriteFont1");

            // vytvoøíme balónky
            for (int i = 0; i < 40; i++)
            {
                int color = rand.Next(4);
                ballony[i] = new balonek(spriteballons[color], color, 1 + rand.Next(5));
            }

            // nastavit pøehrávaè
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
                mys = Mouse.GetState(); // jak je na tom myš?
                if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Enter)) // klikneme-li enter, pøepnout do herní smyèky
                {
                    stav = 2; // pøepnout na herní cyklus
                    for (int i = 0; i < 40; i++) // vygenerovat nové balónky
                    {
                        ballony[i].generateballon();
                    }
                }
                for (int i = 0; i < 40; i++) // poslat balónky nahoru
                {
                    ballony[i].flyup();
                }
            }

            if (stav == 2) // herní cyklus
            {
                timer.Enabled = true; // spustit èas
                for (int i = 0; i < 40; i++)
                {
                    mys = Mouse.GetState(); // co myš?
                    if (mys.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed) // pokud stisknuto levé tlaèítko
                    {
                        if (canshot) // pokud pøedtím nebylo drženo
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

                    if (ballony[i].aktivni == true) //pokud není sestøelen
                    {
                        ballony[i].flyup(); // poslat ho nahoru
                    }
                    else // pokud je sestøelen
                    {
                        ballony[i].generateballon(); // vygenerovat nový
                    }
                }

                if (sestreleno_zaraz > 1)
                {
                    score_pricist = score_pricist * sestreleno_zaraz * 5;
                }

                score += score_pricist; // pøièíst skóre
                if (sestreleno_zaraz == 0 && mys.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && canshot == true) score -= 1; // netrefili jsme se? odeèíst bod 

                // vynulovat pomocné promìnné
                sestreleno_zaraz = 0;
                score_pricist = 0;

                if (levely[level - 1].target <= score) // vyhráli jsme?
                {
                    levely[level - 1].win = true; // výhra
                    stav = 3; // pøepnout do cyklu mezilevelních oken
                }

                canshot = false;
                mys = Mouse.GetState(); // stav myši

                // pustili-li jsme myš, mùžem sestøelit další
                if (mys.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released) canshot = true;

                base.Update(gameTime);
            }

            if (stav == 3) // mezilevelní cyklus
            {
                mys = Mouse.GetState(); // stav myši
                if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Enter))
                {
                    stav = 2;
                    System.Threading.Thread.Sleep(3000);
                    for (int i = 0; i < 40; i++)
                    {
                        ballony[i].generateballon();
                    }
                }
                for (int i = 0; i < 40; i++) // poslat balóny nahoru
                {
                    ballony[i].flyup();
                }
            }
        }

        protected override void Draw(GameTime gameTime) // všechno vykreslit (60x za sekundu)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue); // vyèistit, co tam bylo z posledního kreslení

            if (stav == 1) // menu kreslení
            {
                spriteBatch.Begin();
                spriteBatch.Draw(background, Vector2.Zero, Color.White); // vykreslit pozadí
                for (int i = 0; i < 40; i++) // vykreslit všechny balónky
                {
                    spriteBatch.Draw(ballony[i].ballon, new Vector2(ballony[i].X, ballony[i].Y), Color.White);
                }
                spriteBatch.Draw(pozadi2, new Vector2(sirkaOkna / 2 - 860 / 2, 5), Color.White); // vykreslit úvod
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
