
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace MemoryArena
{
    public class GameForm : Form
    {
        private PictureBox backgroundLayer, deckLayer, liveDeckLayer;
        private const int TotalPairs = 15;
        private List<Card> cards = new List<Card>();
        private Card firstCard, secondCard;
        private int score = 0, streak = 0, lives = 3;
        private Label lblScore, lblCooldown;
        private FlowLayoutPanel livesPanel;
        private Timer cooldownTimer, revealTimer;
        private DateTime nextLifeTime;
        private Panel cardPanel;

        private int cheatRevealCount = 3;
        private int cheatFindCount = 3;

        private Timer cheatRevealTimer;
        private Timer cheatFindTimer;

        private double revealCooldownRemaining = 0;
        private double findCooldownRemaining = 0;

        private Label lblCheatRevealCooldown;
        private Label lblCheatFindCooldown;

        private Dictionary<string, ICheat> cheatMap;


        public GameForm()
        {
            this.DoubleBuffered = true;
            this.ClientSize = new Size(540, 960);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;

            InitBackground();
            InitCardDeck();
            InitCards();
            InitControls();
            InitLives();
            ShowAllCardsInitially();

            cheatMap = new Dictionary<string, ICheat>
            {
                { "RevealAll", new RevealAllCheat() },
                { "FindPair", new FindPairCheat() }
            };

        }

        private void InitBackground()
        {
            backgroundLayer = new PictureBox
            {
                Image = Image.FromFile(Path.Combine("Assets", "background.png")),
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            this.Controls.Add(backgroundLayer);
            backgroundLayer.SendToBack();
        }

        private void InitCardDeck()
        {
            deckLayer = new PictureBox
            {
                Image = Image.FromFile(Path.Combine("Assets", "card-deck.png")),
                Location = new Point(55, 70),
                Size = new Size((int)(480 * 0.9), (int)(680 * 0.9)),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.FromArgb(87, 191, 237)
            };
            this.Controls.Add(deckLayer);
            deckLayer.BringToFront();
        }

        private void InitCards()
        {
            cardPanel = new Panel
            {
                Location = new Point(68, 90),
                Size = new Size((int)(448 * 0.9), (int)(635 * 0.9)),
                BackColor = Color.White
            };
            for (int i = 1; i <= TotalPairs; i++)
            {
                string path = Path.Combine("Assets", $"card-{i}.png");
                cards.Add(new Card(i, path, 0.9));
                cards.Add(new Card(i, path, 0.9));
            }
            var rnd = new Random();
            cards = cards.OrderBy(c => rnd.Next()).ToList();

            int x = 0, y = 0, col = 5;
            int spacingX = 5, spacingY = 5;
            foreach (var card in cards)
            {
                card.Location = new Point(x * (card.Width + spacingX), y * (card.Height + spacingY));
                card.Click += Card_Click;
                cardPanel.Controls.Add(card);
                x++;
                if (x >= col) { x = 0; y++; }
            }
            this.Controls.Add(cardPanel);
            cardPanel.BringToFront();
        }

        private void InitControls()
        {
            lblScore = new Label() //label untuk skor
            {
                Text = "0",
                Font = new Font("Raleway Black", 35),
                ForeColor = Color.Black,
                Location = new Point(430, -5),
                AutoSize = true,
                BackColor = Color.FromArgb(87, 191, 237)
            };
            this.Controls.Add(lblScore);
            lblScore.BringToFront();

            lblCooldown = new Label() //label untuk cooldown nyawa
            {
                Text = "",
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.White,
                Size = new Size(215, 30),
                Location = new Point(245, 690),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(87, 191, 237)
            };
            this.Controls.Add(lblCooldown);
            lblCooldown.BringToFront();

            PictureBox btnBack = CreateIcon("back-home.png", new Point(10, 10), new Size(50, 50), () => this.Close());
            btnBack.BackColor = Color.FromArgb(87, 191, 237);
            this.Controls.Add(btnBack);
            btnBack.BringToFront();

            PictureBox btnSettings = CreateIcon("settings.png", new Point(70, 10), new Size(50, 50), () => MessageBox.Show("Settings"));
            btnSettings.BackColor = Color.FromArgb(87, 191, 237);
            this.Controls.Add(btnSettings);
            btnSettings.BringToFront();

            Button btnCheatReveal = CreateButton("card-all-flip.png", new Point(60, 708), new Size(70, 70), () => UseCheatReveal());
            btnCheatReveal.BackColor = Color.FromArgb(87, 191, 237);
            this.Controls.Add(btnCheatReveal);
            btnCheatReveal.BringToFront();

            lblCheatRevealCooldown = new Label() //label untuk cooldown cheat reveal
            {
                Text = "",
                Font = new Font("Arial", 10, FontStyle.Bold),
                ForeColor = Color.White,
                Size = new Size(80, 20),
                Location = new Point(60, 688),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(87, 191, 237)
            };
            this.Controls.Add(lblCheatRevealCooldown);
            lblCheatRevealCooldown.BringToFront();


            Button btnCheatFind = CreateButton("card-find.png", new Point(170, 708), new Size(70, 70), () => UseCheatFind());
            btnCheatFind.BackColor = Color.FromArgb(87, 191, 237);
            this.Controls.Add(btnCheatFind);
            btnCheatFind.BringToFront();


            lblCheatFindCooldown = new Label() //label untuk cooldown cheat find
            {
                Text = "",
                Font = new Font("Arial", 10, FontStyle.Bold),
                ForeColor = Color.White,
                Size = new Size(80, 20),
                Location = new Point(170, 688),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(87, 191, 237)
            };
            this.Controls.Add(lblCheatFindCooldown);
            lblCheatFindCooldown.BringToFront();  
        }

        private void InitLives()
        {
            liveDeckLayer = new PictureBox //deck untuk nyawa
            {
                Image = Image.FromFile(Path.Combine("Assets", "live-deck.png")),
                Size = new Size(174, 53),
                Location = new Point(325, 715),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.FromArgb(87, 191, 237)
            };
            this.Controls.Add(liveDeckLayer);
            liveDeckLayer.BringToFront();

            livesPanel = new FlowLayoutPanel //panel untuk menampilkan nyawa
            {
                Location = new Point(345, 721),
                Size = new Size(140, 40),
                BackColor = Color.White
            };
            this.Controls.Add(livesPanel);
            livesPanel.BringToFront();
            DrawLives();
        }

        private void DrawLives() //looping untuk add nyawa
        {
            livesPanel.Controls.Clear();
            for (int i = 0; i < lives; i++)
            {
                livesPanel.Controls.Add(new PictureBox
                {
                    Image = Image.FromFile(Path.Combine("Assets", "live.png")),
                    Size = new Size(35, 35),
                    SizeMode = PictureBoxSizeMode.StretchImage
                });
            }
        }


        private double cooldownSecondsRemaining = 0;
        private double cooldownProgress = 0;
        private void StartLifeCooldown()
        {
            cooldownSecondsRemaining += 300; // cooldown nyawa 5 menit

            if (cooldownTimer == null)
            {
                cooldownTimer = new Timer { Interval = 1000 };
                cooldownTimer.Tick += (s, e) =>
                {
                    if (cooldownSecondsRemaining > 0)
                    {
                        cooldownSecondsRemaining--;
                        cooldownProgress++;
                    }

                    if (cooldownProgress >= 300 && lives < 3)
                    {
                        lives++;
                        DrawLives();
                        cooldownProgress -= 300;
                    }

                    if (lives >= 3)
                    {
                        cooldownTimer.Stop();
                        cooldownTimer = null;
                        cooldownSecondsRemaining = 0;
                        cooldownProgress = 0;
                        lblCooldown.Text = "";
                        return;
                    }

                    TimeSpan span = TimeSpan.FromSeconds(cooldownSecondsRemaining);
                    lblCooldown.Text = $" {span.Minutes:D2}:{span.Seconds:D2}";
                };
                cooldownTimer.Start();
            }
        }

        private void ShowAllCardsInitially() //membuka kartu diawal permainan
        {
            foreach (var c in cards) c.Flip();
            revealTimer = new Timer { Interval = 8000 }; //8 detik
            revealTimer.Tick += (s, e) =>
            {
                revealTimer.Stop();
                foreach (var c in cards) if (!c.IsMatched) c.HideCard();
            };
            revealTimer.Start();
        }

        private void Card_Click(object sender, EventArgs e) //event handler untuk klik kartu
        {
            var clicked = sender as Card;
            if (clicked.IsFlipped || clicked.IsMatched) return;
            clicked.Flip();
            if (firstCard == null) firstCard = clicked;
            else
            {
                secondCard = clicked;
                this.Enabled = false;
                Timer t = new Timer { Interval = 800 };
                t.Tick += (s, ev) => { t.Stop(); CheckMatch(); this.Enabled = true; };
                t.Start();
            }
        }

        private void CheckMatch() // method untuk cek apakah kartu cocok
        {
            if (firstCard.ID == secondCard.ID)
            {
                firstCard.Match(); secondCard.Match();
                streak++; score += 25 + (streak * 10);
            }
            else
            {
                firstCard.HideCard(); secondCard.HideCard();
                streak = 0;
                lives--;
                PlayerData.Lives--;
                if (PlayerData.Lives < 3 && PlayerData.NextLifeTime == null)
                    PlayerData.NextLifeTime = DateTime.Now.AddMinutes(5);
                DrawLives();

                if (lives <= 0)
                {
                    cooldownTimer?.Stop();
                    MessageBox.Show("Game Over");
                    this.Close();
                    return;
                }

                if (lives < 3)
                    StartLifeCooldown();
            }

            if (CheckWin())
            {
                PlayerData.Score += score;
                MessageBox.Show("Selamat! Game selesai!");
                this.Close();
                return;
            }

            lblScore.Text = $"{score}";
            firstCard = secondCard = null;
        }

        private Button CreateButton(string asset, Point location, Size size, Action action)
        {
            var b = new Button
            {
                BackgroundImage = Image.FromFile(Path.Combine("Assets", asset)),
                BackgroundImageLayout = ImageLayout.Stretch,
                Size = size,
                Location = location,
                FlatStyle = FlatStyle.Flat
            };
            b.FlatAppearance.BorderSize = 0;
            b.Click += (s, e) => action();
            return b;
        }

        private PictureBox CreateIcon(string asset, Point location, Size size, Action action)
        {
            var p = new PictureBox
            {
                Image = Image.FromFile(Path.Combine("Assets", asset)),
                Size = size,
                Location = location,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Cursor = Cursors.Hand,
                BackColor = Color.Transparent
            };
            p.Click += (s, e) => action();
            return p;
        }

        private async void UseCheatReveal()
        {
            if (cheatRevealCount > 0)
            {
                cheatRevealCount--;
                await cheatMap["RevealAll"].Execute(cards);
                StartCheatRevealCooldown();
            }
        }

        private void StartCheatRevealCooldown()
        {
            revealCooldownRemaining += 180;

            if (cheatRevealTimer == null)
            {
                cheatRevealTimer = new Timer { Interval = 1000 };
                cheatRevealTimer.Tick += (s, e) =>
                {
                    if (revealCooldownRemaining > 0)
                    {
                        revealCooldownRemaining--;
                        TimeSpan span = TimeSpan.FromSeconds(revealCooldownRemaining);
                        lblCheatRevealCooldown.Text = $"CD: {span.Minutes:D2}:{span.Seconds:D2}";
                    }

                    if (revealCooldownRemaining <= 0)
                    {
                        cheatRevealCount++;
                        if (cheatRevealCount < 3)
                        {
                            revealCooldownRemaining += 180;
                        }
                        else
                        {
                            cheatRevealTimer.Stop();
                            cheatRevealTimer = null;
                            lblCheatRevealCooldown.Text = "";
                        }
                    }
                };
                cheatRevealTimer.Start();
            }
        }

        private async void UseCheatFind()
        {
            if (cheatFindCount > 0)
            {
                cheatFindCount--;
                await cheatMap["FindPair"].Execute(cards);
                StartCheatFindCooldown();
            }
        }

        private void StartCheatFindCooldown()
        {
            findCooldownRemaining += 180;

            if (cheatFindTimer == null)
            {
                cheatFindTimer = new Timer { Interval = 1000 };
                cheatFindTimer.Tick += (s, e) =>
                {
                    if (findCooldownRemaining > 0)
                    {
                        findCooldownRemaining--;
                        TimeSpan span = TimeSpan.FromSeconds(findCooldownRemaining);
                        lblCheatFindCooldown.Text = $"CD: {span.Minutes:D2}:{span.Seconds:D2}";
                    }

                    if (findCooldownRemaining <= 0)
                    {
                        cheatFindCount++;
                        if (cheatFindCount < 3)
                        {
                            findCooldownRemaining += 180;
                        }
                        else
                        {
                            cheatFindTimer.Stop();
                            cheatFindTimer = null;
                            lblCheatFindCooldown.Text = "";
                        }
                    }
                };
                cheatFindTimer.Start();
            }
        }

        private bool CheckWin()
        {
            return cards.All(c => c.IsMatched);
        }
    }

    public class Card : PictureBox
    {
        public int ID { get; private set; }
        private string facePath;
        public bool IsFlipped { get; private set; }
        public bool IsMatched { get; private set; }
        private string backPath = Path.Combine("Assets", "card-back.png");

        public Card(int id, string facePath, double scale = 1.0)
        {
            ID = id;
            this.facePath = facePath;
            this.Image = Image.FromFile(backPath);
            int width = (int)(85 * scale);
            int height = (int)(100 * scale);
            this.Size = new Size(width, height);
            this.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public async void Flip()
        {
            if (IsFlipped) return;
            await AnimateFlip(() => this.Image = Image.FromFile(facePath));
            IsFlipped = true;
        }

        public async void HideCard()
        {
            if (!IsFlipped) return;
            await AnimateFlip(() => this.Image = Image.FromFile(backPath));
            IsFlipped = false;
        }

        public void Match()
        {
            IsMatched = true;
        }

        public async void FlipTemporarily()
        {
            this.Image = Image.FromFile(facePath);
            await Task.Delay(1500);
            if (!IsMatched) HideCard();
        }


        private async Task AnimateFlip(Action midAction) // Animasi flip kartu
        {
            int originalWidth = this.Width;
            int centerX = this.Left + this.Width / 2;

            // Shrink
            for (int i = originalWidth; i >= 10; i -= 10)
            {
                await Task.Delay(10);
                this.Invoke((MethodInvoker)(() => {
                    this.Width = i;
                    this.Left = centerX - this.Width / 2;
                }));
            }

            // Change image
            midAction();

            // Expand
            for (int i = 10; i <= originalWidth; i += 10)
            {
                await Task.Delay(10);
                this.Invoke((MethodInvoker)(() => {
                    this.Width = i;
                    this.Left = centerX - this.Width / 2;
                }));
            }
        }

    }
}
