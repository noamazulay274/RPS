using SuperSimpleTcp;
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RPS.Client
{
    public partial class Game : Form
    {
        Random rnd = new Random();
        bool KingChosen = false;
        bool TrapChosen = false;
        bool RocksChosen = false;
        bool PapersChosen = false;
        bool ScissorsChosen = false;
        Button[,] Squares = new Button[7, 7];
        Soldier[,] Players = new Soldier[7, 7];
        (int, int) ChosenCoordinates = (-1, -1);
        (int, int) KingCoordinates = (-1, -1);
        (int, int) enemyKingCoordinates = (-1, -1);
        int Seconds_Left_To_Choose_King = 31;
        int Seconds_Left_To_Choose_Trap = 31;
        SimpleTcpClient client;
        bool myturn = false;
        string  Mydraw = null;
        string fightplace;
        public Game(SimpleTcpClient client)
        {
            InitializeComponent();
            this.client = client;
        }
        private void Game_Load(object sender, EventArgs e)
        {
            turnlbl.Location = new Point(turnlbl.Parent.ClientSize.Width - 240, 100);
            turnlbl.Visible = false;
             //FormBorderStyle = FormBorderStyle.None;
             WindowState = FormWindowState.Maximized;
            BoardBackground.Location = new Point((BoardBackground.Parent.ClientSize.Width / 2) - (BoardBackground.Width / 2), (BoardBackground.Parent.ClientSize.Height / 2) - (BoardBackground.Height / 2));
            Board.Location = new Point((Board.Parent.ClientSize.Width / 2) - (Board.Width / 2), (Board.Parent.ClientSize.Height / 2) - (Board.Height / 2));
            ExitGame.Location = new Point(Board.Parent.ClientSize.Width - ExitGame.Width, 0);
            MadeWithLove.Parent = Board.Parent;
            MadeWithLove.Location = new Point((MadeWithLove.Parent.ClientSize.Width / 2) - (MadeWithLove.Width / 2), (MadeWithLove.Parent.ClientSize.Height) - (MadeWithLove.Height / 2));
            ThreeTwoOne.Parent = Board;
            ThreeTwoOne.Location = new Point((ThreeTwoOne.Parent.ClientSize.Width / 2) - (ThreeTwoOne.Width / 2) + (ThreeTwoOne.Width / 18), (ThreeTwoOne.Parent.ClientSize.Height / 2) - (ThreeTwoOne.Height / 2));
            UpArrow.Parent = Board;
            RightArrow.Parent = Board;
            DownArrow.Parent = Board;
            LeftArrow.Parent = Board;
            DisableArrows();
            LeftFight.Parent = Board;
            RightFight.Parent = Board;
            UpFight.Parent = Board;
            DownFight.Parent = Board;
            paperpic.Parent = Board;
            rockpic.Parent = Board;
            scissorspic.Parent = Board;
            paperpic.Location = new Point( -20,220);
            paperpic.BackgroundImageLayout = ImageLayout.Stretch;
            rockpic.Location = new Point(230, 220);
            rockpic.BackgroundImageLayout = ImageLayout.Stretch;
            scissorspic.Location = new Point(494, 220);
            scissorspic.BackgroundImageLayout = ImageLayout.Stretch;
            drawLbl.Parent = Board;


            TimeLeftToChoose.Visible = false;
            TimeLeftToChoose.Location = new Point((TimeLeftToChoose.Parent.ClientSize.Width / 2) - 67, Board.Location.Y - 110);
            TimeLeftToChoose.BringToFront();


            client.Events.DataReceived += Events_ClientDataReceived1;

            Squares[0, 0] = Square_0_0;
            Squares[0, 1] = Square_0_1;
            Squares[0, 2] = Square_0_2;
            Squares[0, 3] = Square_0_3;
            Squares[0, 4] = Square_0_4;
            Squares[0, 5] = Square_0_5;
            Squares[0, 6] = Square_0_6;
            Squares[1, 0] = Square_1_0;
            Squares[1, 1] = Square_1_1;
            Squares[1, 2] = Square_1_2;
            Squares[1, 3] = Square_1_3;
            Squares[1, 4] = Square_1_4;
            Squares[1, 5] = Square_1_5;
            Squares[1, 6] = Square_1_6;
            Squares[2, 0] = Square_2_0;
            Squares[2, 1] = Square_2_1;
            Squares[2, 2] = Square_2_2;
            Squares[2, 3] = Square_2_3;
            Squares[2, 4] = Square_2_4;
            Squares[2, 5] = Square_2_5;
            Squares[2, 6] = Square_2_6;
            Squares[3, 0] = Square_3_0;
            Squares[3, 1] = Square_3_1;
            Squares[3, 2] = Square_3_2;
            Squares[3, 3] = Square_3_3;
            Squares[3, 4] = Square_3_4;
            Squares[3, 5] = Square_3_5;
            Squares[3, 6] = Square_3_6;
            Squares[4, 0] = Square_4_0;
            Squares[4, 1] = Square_4_1;
            Squares[4, 2] = Square_4_2;
            Squares[4, 3] = Square_4_3;
            Squares[4, 4] = Square_4_4;
            Squares[4, 5] = Square_4_5;
            Squares[4, 6] = Square_4_6;
            Squares[5, 0] = Square_5_0;
            Squares[5, 1] = Square_5_1;
            Squares[5, 2] = Square_5_2;
            Squares[5, 3] = Square_5_3;
            Squares[5, 4] = Square_5_4;
            Squares[5, 5] = Square_5_5;
            Squares[5, 6] = Square_5_6;
            Squares[6, 0] = Square_6_0;
            Squares[6, 1] = Square_6_1;
            Squares[6, 2] = Square_6_2;
            Squares[6, 3] = Square_6_3;
            Squares[6, 4] = Square_6_4;
            Squares[6, 5] = Square_6_5;
            Squares[6, 6] = Square_6_6;

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Squares[i, j].Parent = Board;
                    if (i > 1 && i < 5)
                    {
                        Squares[i, j].Image = null;
                        Squares[i, j].Visible = false;
                    }
                    Squares[i, j].Location = new Point(110 * j, 110 * i);
                }
            }

            Choose.Parent = Board;
            Choose.Location = new Point(0, 330);
            Choose.Visible = false;
            ThreeTwoOne.Visible = true;
            Counting.Start();

        }

        public void Events_ClientDataReceived1(object sender, DataReceivedEventArgs e)
        {
            if (e is null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            this.Invoke((MethodInvoker)delegate
            {
                int place = -1;
                string command = Encoding.UTF8.GetString(e.Data);
                if (command.Contains("Playerlst"))
                {
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 7; j++)
                        {
                            if (command[command.Length+place] =='1')
                            {
                                Players[i, j] = new KingSoldier(Color.Blue);
                                enemyKingCoordinates = (i, j);
                            }
                            else if (command[command.Length + place] == '2')
                            {
                                Players[i, j] = new TrapSoldier(Color.Blue);
                            }
                            else if (command[command.Length + place] == '3')
                            {
                                Players[i, j] = new PaperSoldier(Color.Blue);
                            }
                            else if (command[command.Length + place] == '4')
                            {
                                Players[i, j] = new RockSoldier(Color.Blue);
                            }
                            else if (command[command.Length + place] == '5')
                            {
                                Players[i, j] = new ScissorsSoldier(Color.Blue);
                            }
                            place--;
                            //Players[i, j].Turn_Visible();
                            //Squares[i,j].Image = Players[i, j].Display();

                        }
                    }
                }
                else if (command.Contains("letdr"))
                {
                    if (Mydraw != null)
                    {
                        int x = int.Parse(Mydraw[Mydraw.Length - 1].ToString());
                        int y = int.Parse(Mydraw[Mydraw.Length - 2].ToString());
                        ChosenCoordinates = (y, x);
                        if (Mydraw.Contains("Up"))
                        {
                            UpFight_Click(sender, e);
                        }
                        if (Mydraw.Contains("Down"))
                        {
                            DownFight_Click(sender, e);

                        }
                        if (Mydraw.Contains("Left"))
                        {
                           FightPicture_Click(sender, e);

                        }
                        if (Mydraw.Contains("Right"))
                        {
                            RightFight_Click(sender, e);

                        }
                    }
                }

                else if (command.Contains("Move"))
                {
                    turnlbl.Text = "Your Turn!";
                    Mydraw = null;
                    int x = int.Parse(command[command.Length - 1].ToString());
                    int y = int.Parse(command[command.Length - 2].ToString());
                    x = 6 - x;
                    y = 6 - y;
                    if (command.Contains("Up"))
                    {
                        Players[y + 1, x] = Players[y,x];
                        Squares[y + 1, x].Image = Players[y,x].Display();
                        Squares[y + 1, x].Visible = true;
                        Players[y, x] = null;
                        Squares[y,x].Image = null;
                    }
                    if (command.Contains("Down"))
                    {
                        Players[y - 1, x] = Players[y, x];
                        Squares[y - 1, x].Image = Players[y, x].Display();
                        Squares[y - 1, x].Visible = true;
                        Players[y, x] = null;
                        Squares[y, x].Image = null;
                    }
                    if (command.Contains("Left"))
                    {
                        Players[y, x + 1] = Players[y, x];
                        Squares[y, x + 1].Image = Players[y, x].Display();
                        Squares[y, x + 1].Visible = true;
                        Players[y, x]= null;
                        Squares[y, x].Image = null;
                    }
                    if (command.Contains("Right"))
                    {
                        Players[y, x-1] = Players[y, x];
                        Squares[y , x-1].Image = Players[y, x].Display();
                        Squares[y , x-1].Visible = true;
                        Players[y, x] = null;
                        Squares[y, x].Image = null;
                    
                    }
                    myturn = true;
                    turnlbl.Text = "Your Turn!";
                }
                else if (command.Contains("turn"))
                {
                    turnlbl.Visible = true;
                    turnlbl.Text = "Your Turn!";
                    myturn = true;
                }
                else if (command.Contains("nottrn"))
                {
                    turnlbl.Visible = true;
                    turnlbl.Text = "Enemy Turn!";
                    myturn = false;
                }



                else if (command.Contains("Fight"))
                {
                    Mydraw = null;
                    myturn = true;
                    turnlbl.Text = "Your Turn!";
                    int x = int.Parse(command[command.Length - 1].ToString());
                    int y = int.Parse(command[command.Length - 2].ToString());
                    x = 6 - x;
                    y = 6 - y;   
                    if (command.Contains("Up"))
                    {
                        fightplace = "Down";
                        Players[y + 1, x].Turn_Visible();
                        Players[y, x].Turn_Visible();
                        DisableArrows();
                        if (Players[y, x].Fight(Players[y + 1, x]) == FightResult.Win)
                        {
                            Players[y+ 1, x] = Players[y, x];
                            Squares[y + 1, x].Image = Players[y,x].Display();
                            Players[y, x] = null;
                            Squares[y, x].Image = null;
                        }
                        else if (Players[y, x].Fight(Players[y + 1, x]) == FightResult.Loss)
                        {
                            Players[y, x] = null;
                            Squares[y, x].Image = null;
                        }
                        else if (Players[y, x].Fight(Players[y + 1, x]) == FightResult.FalledInTrap)
                        {
                            Players[y, x] = null;
                            Squares[y, x].Image = null;
                            //falling func
                        }
                        else if (Players[y, x].Fight(Players[y + 1, x]) == FightResult.EndGame)
                        {
                            myturn = false;
                            turnlbl.Text = "Enemy won!";
                            Squares[enemyKingCoordinates.Item1, enemyKingCoordinates.Item2].Image = global::RPS.Client.Properties.Resources.EnemyWon;

                            //win func
                        }
                        else if (Players[y, x].Fight(Players[y + 1, x]) == FightResult.Draw)
                        {
                            ChosenCoordinates = (y + 1, x);
                            myturn = false ;
                            turnlbl.Text = "Enemy Turn!";
                            paperpic.Visible = true;
                            scissorspic.Visible = true;
                            rockpic.Visible = true;
                            drawLbl.Visible = true;

                        }
                        Squares[y + 1, x].Image = Players[y + 1, x].Display();


                    }
                    else if (command.Contains("Down"))
                    {
                        fightplace = "Up";

                        Players[y - 1, x].Turn_Visible();
                        Players[y, x].Turn_Visible();
                        DisableArrows();
                        if (Players[y, x].Fight(Players[y - 1, x]) == FightResult.Win)
                        {
                            Players[y - 1, x] = Players[y, x];
                            Squares[y - 1, x].Image = Players[y, x].Display();
                            Players[y, x] = null;
                            Squares[y, x].Image = null;
                        }
                        else if (Players[y, x].Fight(Players[y - 1, x]) == FightResult.Loss)
                        {
                            Players[y, x] = null;
                            Squares[y, x].Image = null;
                        }
                        else if (Players[y, x].Fight(Players[y - 1, x]) == FightResult.FalledInTrap)
                        {
                            Players[y, x] = null;
                            Squares[y, x].Image = null;
                            //falling func
                        }
                        else if (Players[y, x].Fight(Players[y - 1, x]) == FightResult.EndGame)
                        {
                            myturn = false;
                            turnlbl.Text = "Enemy won!";
                            Squares[enemyKingCoordinates.Item1, enemyKingCoordinates.Item2].Image = global::RPS.Client.Properties.Resources.EnemyWon;
                            //win func
                        }
                        else if (Players[y, x].Fight(Players[y - 1, x]) == FightResult.Draw)
                        {
                            ChosenCoordinates = (y - 1, x);
                            myturn = false;
                            turnlbl.Text = "Enemy Turn!";
                            paperpic.Visible = true;
                            scissorspic.Visible = true;
                            rockpic.Visible = true;
                            drawLbl.Visible = true;


                        }
                        Squares[y-1, x].Image = Players[y-1, x ].Display();

                    }
                    else if (command.Contains("Right"))
                    {
                        fightplace = "Left";

                        Players[y , x- 1].Turn_Visible();
                        Players[y, x].Turn_Visible();
                        DisableArrows();
                        if (Players[y, x].Fight(Players[y , x- 1]) == FightResult.Win)
                        {
                            Players[y, x - 1] = Players[y, x];
                            Squares[y, x - 1].Image = Players[y, x].Display();
                            Players[y, x] = null;
                            Squares[y, x].Image = null;
                        }
                        else if (Players[y, x].Fight(Players[y , x- 1]) == FightResult.Loss)
                        {
                            Players[y, x] = null;
                            Squares[y, x].Image = null;
                        }
                        else if (Players[y, x].Fight(Players[y, x - 1]) == FightResult.FalledInTrap)
                        {
                            Players[y, x] = null;
                            Squares[y, x].Image = null;
                            //falling func
                        }
                        else if (Players[y, x].Fight(Players[y , x- 1]) == FightResult.EndGame)
                        {
                            myturn = false;
                            turnlbl.Text = "Enemy won!";
                            Squares[enemyKingCoordinates.Item1, enemyKingCoordinates.Item2].Image = global::RPS.Client.Properties.Resources.EnemyWon;
                            //win func
                        }
                        else if (Players[y, x].Fight(Players[y , x- 1]) == FightResult.Draw)
                        {
                            myturn = false;
                            turnlbl.Text = "Enemy Turn!";
                            ChosenCoordinates = (y , x- 1);
                            paperpic.Visible = true;
                            scissorspic.Visible = true;
                            rockpic.Visible = true;
                            drawLbl.Visible = true;

                        }
                        Squares[y, x -1].Image = Players[y, x - 1].Display();

                    }
                    else if (command.Contains("Left"))
                    {
                        fightplace = "Right";

                        Players[y, x + 1].Turn_Visible();
                        Players[y, x].Turn_Visible();
                        DisableArrows();
                        if (Players[y, x].Fight(Players[y, x + 1]) == FightResult.Win)
                        {
                            Players[y, x + 1] = Players[y, x];
                            Squares[y, x + 1].Image = Players[y, x].Display();
                            Players[y, x] = null;
                            Squares[y, x].Image = null;
                        }
                        else if (Players[y, x].Fight(Players[y, x + 1]) == FightResult.Loss)
                        {
                            Players[y, x] = null;
                            Squares[y, x].Image = null;
                        }
                        else if (Players[y, x].Fight(Players[y, x + 1]) == FightResult.FalledInTrap)
                        {
                            Players[y, x] = null;
                            Squares[y, x].Image = null;
                            //falling func
                        }
                        else if (Players[y, x].Fight(Players[y, x + 1]) == FightResult.EndGame)
                        {
                            myturn = false;
                            turnlbl.Text = "Enemy won!";
                            Squares[enemyKingCoordinates.Item1, enemyKingCoordinates.Item2].Image = global::RPS.Client.Properties.Resources.EnemyWon;
                            //win func
                        }
                        else if (Players[y, x].Fight(Players[y, x + 1]) == FightResult.Draw)
                        {
                            ChosenCoordinates = (y, x+1);
                            myturn = false;
                            turnlbl.Text = "Enemy Turn!";
                            paperpic.Visible = true;
                            scissorspic.Visible = true;
                            rockpic.Visible = true;
                            drawLbl.Visible = true;

                        }
                        Squares[y, x + 1].Image = Players[y, x + 1].Display();

                    }


                }
                else if (command.Contains("Draw"))
                {
                    int x = int.Parse(command[command.Length - 1].ToString());
                    int y = int.Parse(command[command.Length - 2].ToString());
                    x = 6 - x;
                    y = 6 - y;
                    if (command.Contains("Paper"))
                    {
                        Players[y, x] = new PaperSoldier(Color.Blue);
                        Squares[y, x].Image = Players[y, x].Display();
                    }
                    if (command.Contains("Rock"))
                    {
                        Players[y, x] = new RockSoldier(Color.Blue);
                        Squares[y, x].Image = Players[y, x].Display();
                    }
                    if (command.Contains("Scissors"))
                    {
                        Players[y, x] = new ScissorsSoldier(Color.Blue);
                        Squares[y, x].Image = Players[y, x].Display();
                    }
                }



            });
        }
        private void ChooseTimer_Tick(object state)
        {
            if (!KingChosen)
            {
                Seconds_Left_To_Choose_King--;
                TimeLeftToChoose.Text = Seconds_Left_To_Choose_King.ToString();
                TimeLeftToChoose.Visible = true;
                if (Seconds_Left_To_Choose_King < 10)
                    TimeLeftToChoose.Location = new Point((TimeLeftToChoose.Parent.ClientSize.Width / 2) - 40, Board.Location.Y - 110);
                if (Seconds_Left_To_Choose_King == 0)
                {
                    TimeLeftToChoose.Visible = false;
                    int line = rnd.Next(5, 7);
                    int indexInLine = rnd.Next(7);
                    Choose_King(line, indexInLine);
                    Counting.Interval = 1;
                }
                Counting.Start();
            }
        }
        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void Choose_King(int line, int indexInLine)
        {
            if (!Counting.Enabled)
            {
                Players[line, indexInLine] = new KingSoldier(Color.Red);
                Squares[line, indexInLine].Image = Players[line, indexInLine].Display();
                KingChosen = true;
                KingCoordinates = (line, indexInLine);

                Choose.Text = "Choose Your Trap";
            }
        }

        public void Choose_Trap(int line, int indexInLine)
        {
            if (!Counting.Enabled && Players[line, indexInLine] == null)
            {
                Players[line, indexInLine] = new TrapSoldier(Color.Red);
                Squares[line, indexInLine].Image = Players[line, indexInLine].Display();
                TrapChosen = true;
                Choose.Text = "Generating Players...";
                this.Choose.Padding = new System.Windows.Forms.Padding(0, 8, 220, 14);
                Counting.Start();
            }
        }

        public void Choose_Rocks()
        {
            for (int i = 0; i < 4; i++)
            {
                int line = rnd.Next(5, 7);
                int indexInLine = rnd.Next(7);
                while (Players[line, indexInLine] != null)
                {
                    line = rnd.Next(5, 7);
                    indexInLine = rnd.Next(7);
                }
                Players[line, indexInLine] = new RockSoldier(Color.Red);
                Squares[line, indexInLine].Image = Players[line, indexInLine].Display();
            }
        }
        public void Choose_Papers()
        {
            for (int i = 0; i < 4; i++)
            {
                int line = rnd.Next(5, 7);
                int indexInLine = rnd.Next(7);
                while (Players[line, indexInLine] != null)
                {
                    line = rnd.Next(5, 7);
                    indexInLine = rnd.Next(7);
                }
                Players[line, indexInLine] = new PaperSoldier(Color.Red);
                Squares[line, indexInLine].Image = Players[line, indexInLine].Display();
            }
        }
        public void Choose_Scissors()
        {
            for (int i = 0; i < 4; i++)
            {
                int line = rnd.Next(5, 7);
                int indexInLine = rnd.Next(7);
                while (Players[line, indexInLine] != null)
                {
                    line = rnd.Next(5, 7);
                    indexInLine = rnd.Next(7);
                }
                Players[line, indexInLine] = new ScissorsSoldier(Color.Red);
                Squares[line, indexInLine].Image = Players[line, indexInLine].Display();
            }
        }

        private void Counting_Tick(object sender, EventArgs e)
        {
            if (!KingChosen && !TrapChosen)
            {
                if (ThreeTwoOne.Text == "3")
                    ThreeTwoOne.Text = "2";
                else if (ThreeTwoOne.Text == "2")
                    ThreeTwoOne.Text = "1";
                else
                {
                    ThreeTwoOne.Visible = false;
                    ThreeTwoOne.Text = "done";
                    Counting.Stop();
                    Choose.Visible = true;
                    if (!KingChosen)
                    {
                        Seconds_Left_To_Choose_King--;
                        TimeLeftToChoose.Text = Seconds_Left_To_Choose_King.ToString();
                        TimeLeftToChoose.Visible = true;
                        if (Seconds_Left_To_Choose_King < 10)
                            TimeLeftToChoose.Location = new Point((TimeLeftToChoose.Parent.ClientSize.Width / 2) - 40, Board.Location.Y - 110);
                        if (Seconds_Left_To_Choose_King == 0)
                        {
                            TimeLeftToChoose.Visible = false;
                            int line = rnd.Next(5, 7);
                            int indexInLine = rnd.Next(7);
                            Choose_King(line, indexInLine);
                            Counting.Interval = 1;
                        }
                        Counting.Start();
                    }
                }
            }
            else
            {
                Counting.Interval = 1000;
                if (KingChosen && !TrapChosen)
                {
                    Counting.Stop();
                    TimeLeftToChoose.Location = new Point((TimeLeftToChoose.Parent.ClientSize.Width / 2) - 67, Board.Location.Y - 110);
                    Seconds_Left_To_Choose_Trap--;
                    TimeLeftToChoose.Text = Seconds_Left_To_Choose_Trap.ToString();
                    TimeLeftToChoose.Visible = true;
                    if (Seconds_Left_To_Choose_Trap < 10)
                        TimeLeftToChoose.Location = new Point((TimeLeftToChoose.Parent.ClientSize.Width / 2) - 40, Board.Location.Y - 110);
                    if (Seconds_Left_To_Choose_Trap == 0)
                    {
                        TimeLeftToChoose.Visible = false;
                        int line = rnd.Next(5, 7);
                        int indexInLine = rnd.Next(7);
                        while (Players[line, indexInLine] != null)
                        {
                            line = rnd.Next(5, 7);
                            indexInLine = rnd.Next(7);
                        }
                        Choose_Trap(line, indexInLine);
                    }
                    Counting.Start();
                }
                else if (!RocksChosen)
                {
                    Counting.Stop();
                    Choose_Rocks();
                    RocksChosen = true;
                    Counting.Start();
                }
                else if (!PapersChosen)
                {
                    Counting.Stop();
                    Choose_Papers();
                    PapersChosen = true;
                    Counting.Start();
                }
                else if (!ScissorsChosen)
                {
                    Counting.Stop();
                    Choose_Scissors();
                    ScissorsChosen = true;
                    Counting.Start();
                }
                else if (Choose.Text != "Let's go!")
                {
                    Counting.Stop();
                    for (int i = 0; i < 7; i++)
                    {
                        Squares[0, i].Image = global::RPS.Client.Properties.Resources.Enemy;
                        Squares[1, i].Image = global::RPS.Client.Properties.Resources.Enemy;
                    }
                    Choose.Text = "Let's go!";
                    Choose.Padding = new System.Windows.Forms.Padding(225, 8, 220, 14);
                    Counting.Interval = 1150;
                    Counting.Start();
                }
                else
                {
                    string playerslst="Playerlst_";
                    
                    for (int i=5; i < 7; i++)
                    {
                        for (int j = 0; j < 7; j++)
                        {
                            if (typeof(KingSoldier).IsInstanceOfType(Players[i, j]))
                            {
                                playerslst += "1";
                            }
                            else if (typeof(TrapSoldier).IsInstanceOfType(Players[i, j]))
                            {
                                playerslst += "2";
                            }
                            else if (typeof(PaperSoldier).IsInstanceOfType(Players[i, j]))
                            {
                                playerslst += "3";
                            }
                            else if (typeof(RockSoldier).IsInstanceOfType(Players[i, j]))
                            {
                                playerslst += "4";
                            }
                            else if (typeof(ScissorsSoldier).IsInstanceOfType(Players[i, j]))
                            {
                                playerslst += "5";
                            }
                        }
                        
                    }
                    client.Send(playerslst);
                    Counting.Stop();
                    Choose.Visible = false;
                    
                }

            }
        }

        private void ShowArrows(int line, int indexInLine)
        {
            if (!myturn)
            {
                return;
            }
            DisableArrows();
            if (Players[line, indexInLine] != null && Players[line, indexInLine].Get_Color() == Color.Red && !typeof(KingSoldier).IsInstanceOfType(Players[line, indexInLine]) && !typeof(TrapSoldier).IsInstanceOfType(Players[line, indexInLine]) && Counting.Enabled == false)
            {
                ChosenCoordinates = (line, indexInLine);
                if (line > 0 && ((Players[line - 1, indexInLine] != null && Players[line - 1, indexInLine].Get_Color() == Color.Blue) || Players[line - 1, indexInLine] == null)) //UpArrow
                {
                    if (Players[line - 1, indexInLine] == null )
                    {
                        Squares[line - 1, indexInLine].Visible = true;
                        UpArrow.Location = new Point(indexInLine * 110, (line - 1) * 110);
                        UpArrow.Visible = true;
                    }
                    else
                    {
                        UpFight.Location = new Point(indexInLine * 110, (line - 1) * 110);
                        UpFight.Visible = true;
                        UpFight.Image = Players[line - 1, indexInLine].Display();
                        UpFight.BringToFront();
                    }
                }
                if (line < 6 && ((Players[line + 1, indexInLine] != null && Players[line + 1, indexInLine].Get_Color() == Color.Blue) || Players[line + 1, indexInLine] == null)) //DownArrow
                {
                    if (Players[line + 1, indexInLine] == null)
                    {
                        Squares[line + 1, indexInLine].Visible = true;
                        DownArrow.Location = new Point(indexInLine * 110, (line + 1) * 110);
                        DownArrow.Visible = true;
                    }
                    else
                    {
                        DownFight.Location = new Point(indexInLine * 110, (line + 1) * 110);
                        DownFight.Visible = true;
                        DownFight.Image = Players[line + 1, indexInLine].Display();
                        DownFight.BringToFront();
                    }
                }
                if (indexInLine < 6 && ((Players[line, indexInLine + 1] != null && Players[line, indexInLine + 1].Get_Color() == Color.Blue) || Players[line, indexInLine + 1] == null)) //RightArrow
                {
                    if (Players[line, indexInLine + 1] == null)
                    {
                        Squares[line, indexInLine + 1].Visible = true;
                        RightArrow.Location = new Point((indexInLine + 1) * 110, line * 110);
                        RightArrow.Visible = true;
                    }
                    else
                    {
                        RightFight.Location = new Point((indexInLine + 1) * 110, line * 110);
                        RightFight.Visible = true;
                        RightFight.Image = Players[line, indexInLine + 1].Display();
                        RightFight.BringToFront();
                    }
                }
                if (indexInLine > 0 && ((Players[line, indexInLine - 1] != null && Players[line, indexInLine - 1].Get_Color() == Color.Blue) || Players[line, indexInLine - 1] == null)) //LeftArrow
                {
                    if (Players[line, indexInLine - 1] == null)
                    {
                        Squares[line, indexInLine - 1].Visible = true;
                        LeftArrow.Location = new Point((indexInLine - 1) * 110, line * 110);
                        LeftArrow.Visible = true;
                    }
                    else
                    {
                        LeftFight.Location = new Point((indexInLine - 1) * 110, line * 110);
                        LeftFight.Visible = true;
                        LeftFight.Image = Players[line, indexInLine - 1].Display();
                        LeftFight.BringToFront();
                    }
                }
            }
        }

        private void DisableArrows()
        {
            UpArrow.Visible = false;
            DownArrow.Visible = false;
            RightArrow.Visible = false;
            LeftArrow.Visible = false;

            LeftFight.Visible = false;
            UpFight.Visible = false;
            RightFight.Visible = false;
            DownFight.Visible = false;

            paperpic.Visible = false;
            rockpic.Visible = false;
            scissorspic.Visible = false;
            drawLbl.Visible = false;

        }

        private void MovePlayer(int DestinationX, int DestinationY)
        {
            if (Players[DestinationX, DestinationY] == null)
            {
                Players[DestinationX, DestinationY] = Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2];
                Squares[DestinationX, DestinationY].Image = Players[DestinationX, DestinationY].Display();
                Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2] = null;
                Squares[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Visible = false;
            }
        }

        private void Square_0_0_Click(object sender, EventArgs e)
        {
            ShowArrows(0, 0);
        }

        private void Square_0_1_Click(object sender, EventArgs e)
        {
            ShowArrows(0, 1);
        }

        private void Square_0_2_Click(object sender, EventArgs e)
        {
            ShowArrows(0, 2);
        }

        private void Square_0_3_Click(object sender, EventArgs e)
        {
            ShowArrows(0, 3);
        }

        private void Square_0_4_Click(object sender, EventArgs e)
        {
            ShowArrows(0, 4);
        }

        private void Square_0_5_Click(object sender, EventArgs e)
        {
            ShowArrows(0, 5);
        }

        private void Square_0_6_Click(object sender, EventArgs e)
        {
            ShowArrows(0, 6);
        }

        private void Square_1_0_Click(object sender, EventArgs e)
        {
            ShowArrows(1, 0);
        }

        private void Square_1_1_Click(object sender, EventArgs e)
        {
            ShowArrows(1, 1);
        }

        private void Square_1_2_Click(object sender, EventArgs e)
        {
            ShowArrows(1, 2);
        }

        private void Square_1_3_Click(object sender, EventArgs e)
        {
            ShowArrows(1, 3);
        }

        private void Square_1_4_Click(object sender, EventArgs e)
        {
            ShowArrows(1, 4);
        }

        private void Square_1_5_Click(object sender, EventArgs e)
        {
            ShowArrows(1, 5);
        }

        private void Square_1_6_Click(object sender, EventArgs e)
        {
            ShowArrows(1, 6);
        }

        private void Square_2_0_Click(object sender, EventArgs e)
        {
            ShowArrows(2, 0);
        }

        private void Square_2_1_Click(object sender, EventArgs e)
        {
            ShowArrows(2, 1);
        }

        private void Square_2_2_Click(object sender, EventArgs e)
        {
            ShowArrows(2, 2);
        }

        private void Square_2_3_Click(object sender, EventArgs e)
        {
            ShowArrows(2, 3);
        }

        private void Square_2_4_Click(object sender, EventArgs e)
        {
            ShowArrows(2, 4);
        }

        private void Square_2_5_Click(object sender, EventArgs e)
        {
            ShowArrows(2, 5);
        }

        private void Square_2_6_Click(object sender, EventArgs e)
        {
            ShowArrows(2, 6);
        }

        private void Square_3_0_Click(object sender, EventArgs e)
        {
            ShowArrows(3, 0);
        }

        private void Square_3_1_Click(object sender, EventArgs e)
        {
            ShowArrows(3, 1);
        }

        private void Square_3_2_Click(object sender, EventArgs e)
        {
            ShowArrows(3, 2);
        }

        private void Square_3_3_Click(object sender, EventArgs e)
        {
            ShowArrows(3, 3);
        }

        private void Square_3_4_Click(object sender, EventArgs e)
        {
            ShowArrows(3, 4);
        }

        private void Square_3_5_Click(object sender, EventArgs e)
        {
            ShowArrows(3, 5);
        }

        private void Square_3_6_Click(object sender, EventArgs e)
        {
            ShowArrows(3, 6);
        }

        private void Square_4_0_Click(object sender, EventArgs e)
        {
            ShowArrows(4, 0);
        }

        private void Square_4_1_Click(object sender, EventArgs e)
        {
            ShowArrows(4, 1);
        }

        private void Square_4_2_Click(object sender, EventArgs e)
        {
            ShowArrows(4, 2);
        }

        private void Square_4_3_Click(object sender, EventArgs e)
        {
            ShowArrows(4, 3);
        }

        private void Square_4_4_Click(object sender, EventArgs e)
        {
            ShowArrows(4, 4);
        }

        private void Square_4_5_Click(object sender, EventArgs e)
        {
            ShowArrows(4, 5);
        }

        private void Square_4_6_Click(object sender, EventArgs e)
        {
            ShowArrows(4, 6);
        }

        private void Square_5_0_Click(object sender, EventArgs e)
        {
            if (!KingChosen)
            {
                if (ThreeTwoOne.Text == "done")
                {
                    Counting.Stop();
                    TimeLeftToChoose.Visible = false;
                    Choose_King(5, 0);
                    Counting.Interval = 1;
                    Counting.Start();
                }
                else
                    Choose_King(5, 0);
            }
            else if (!TrapChosen)
            {
                Counting.Stop();
                Counting.Interval = 1000;
                Choose_Trap(5, 0);
                TimeLeftToChoose.Visible = false;
                Counting.Start();
            }
            else
                ShowArrows(5, 0);
        }

        private void Square_5_1_Click(object sender, EventArgs e)
        {
            if (!KingChosen)
            {
                if (ThreeTwoOne.Text == "done")
                {
                    Counting.Stop();
                    TimeLeftToChoose.Visible = false;
                    Choose_King(5, 1);
                    Counting.Interval = 1;
                    Counting.Start();
                }
                else
                    Choose_King(5, 1);
            }
            else if (!TrapChosen)
            {
                Counting.Stop();
                Counting.Interval = 1000;
                Choose_Trap(5, 1);
                TimeLeftToChoose.Visible = false;
                Counting.Start();
            }
            else
                ShowArrows(5, 1);
        }

        private void Square_5_2_Click(object sender, EventArgs e)
        {
            if (!KingChosen)
            {
                if (ThreeTwoOne.Text == "done")
                {
                    Counting.Stop();
                    TimeLeftToChoose.Visible = false;
                    Choose_King(5, 2);
                    Counting.Interval = 1;
                    Counting.Start();
                }
                else
                    Choose_King(5, 2);
            }
            else if (!TrapChosen && !typeof(KingSoldier).IsInstanceOfType(Players[5, 2]))
            {
                Counting.Stop();
                Counting.Interval = 1000;
                Choose_Trap(5, 2);
                TimeLeftToChoose.Visible = false;
                Counting.Start();
            }
            else
                ShowArrows(5, 2);
        }

        private void Square_5_3_Click(object sender, EventArgs e)
        {
            if (!KingChosen)
            {
                if (ThreeTwoOne.Text == "done")
                {
                    Counting.Stop();
                    TimeLeftToChoose.Visible = false;
                    Choose_King(5, 3);
                    Counting.Interval = 1;
                    Counting.Start();
                }
                else
                    Choose_King(5, 3);
            }
            else if (!TrapChosen && !typeof(KingSoldier).IsInstanceOfType(Players[5, 3]))
            {
                Counting.Stop();
                Counting.Interval = 1000;
                Choose_Trap(5, 3);
                TimeLeftToChoose.Visible = false;
                Counting.Start();
            }
            else
                ShowArrows(5, 3);
        }

        private void Square_5_4_Click(object sender, EventArgs e)
        {
            if (!KingChosen)
            {
                if (ThreeTwoOne.Text == "done")
                {
                    Counting.Stop();
                    TimeLeftToChoose.Visible = false;
                    Choose_King(5, 4);
                    Counting.Interval = 1;
                    Counting.Start();
                }
                else
                    Choose_King(5, 4);
            }
            else if (!TrapChosen && !typeof(KingSoldier).IsInstanceOfType(Players[5, 4]))
            {
                Counting.Stop();
                Counting.Interval = 1000;
                Choose_Trap(5, 4);
                TimeLeftToChoose.Visible = false;
                Counting.Start();
            }
            else
                ShowArrows(5, 4);
        }

        private void Square_5_5_Click(object sender, EventArgs e)
        {
            if (!KingChosen)
            {
                if (ThreeTwoOne.Text == "done")
                {
                    Counting.Stop();
                    TimeLeftToChoose.Visible = false;
                    Choose_King(5, 5);
                    Counting.Interval = 1;
                    Counting.Start();
                }
                else
                    Choose_King(5, 5);
            }
            else if (!TrapChosen && !typeof(KingSoldier).IsInstanceOfType(Players[5, 5]))
            {
                Counting.Stop();
                Counting.Interval = 1000;
                Choose_Trap(5, 5);
                TimeLeftToChoose.Visible = false;
                Counting.Start();
            }
            else
                ShowArrows(5, 5);
        }

        private void Square_5_6_Click(object sender, EventArgs e)
        {
            if (!KingChosen)
            {
                if (ThreeTwoOne.Text == "done")
                {
                    Counting.Stop();
                    TimeLeftToChoose.Visible = false;
                    Choose_King(5, 6);
                    Counting.Interval = 1;
                    Counting.Start();
                }
                else
                    Choose_King(5, 6);
            }
            else if (!TrapChosen && !typeof(KingSoldier).IsInstanceOfType(Players[5, 6]))
            {
                Counting.Stop();
                Counting.Interval = 1000;
                Choose_Trap(5, 6);
                TimeLeftToChoose.Visible = false;
                Counting.Start();
            }
            else
                ShowArrows(5, 6);
        }

        private void Square_6_0_Click(object sender, EventArgs e)
        {
            if (!KingChosen)
            {
                if (ThreeTwoOne.Text == "done")
                {
                    Counting.Stop();
                    TimeLeftToChoose.Visible = false;
                    Choose_King(6, 0);
                    Counting.Interval = 1;
                    Counting.Start();
                }
                else
                    Choose_King(6, 0);
            }
            else if (!TrapChosen && !typeof(KingSoldier).IsInstanceOfType(Players[6, 0]))
            {
                Counting.Stop();
                Counting.Interval = 1000;
                Choose_Trap(6, 0);
                TimeLeftToChoose.Visible = false;
                Counting.Start();
            }
            else
                ShowArrows(6, 0);
        }

        private void Square_6_1_Click(object sender, EventArgs e)
        {
            if (!KingChosen)
            {
                if (ThreeTwoOne.Text == "done")
                {
                    Counting.Stop();
                    TimeLeftToChoose.Visible = false;
                    Choose_King(6, 1);
                    Counting.Interval = 1;
                    Counting.Start();
                }
                else
                    Choose_King(6, 1);
            }
            else if (!TrapChosen && !typeof(KingSoldier).IsInstanceOfType(Players[6, 1]))
            {
                Counting.Stop();
                Counting.Interval = 1000;
                Choose_Trap(6, 1);
                TimeLeftToChoose.Visible = false;
                Counting.Start();
            }
            else
                ShowArrows(6, 1);
        }

        private void Square_6_2_Click(object sender, EventArgs e)
        {
            if (!KingChosen)
            {
                if (ThreeTwoOne.Text == "done")
                {
                    Counting.Stop();
                    TimeLeftToChoose.Visible = false;
                    Choose_King(6, 2);
                    Counting.Interval = 1;
                    Counting.Start();
                }
                else
                    Choose_King(6, 2);
            }
            else if (!TrapChosen && !typeof(KingSoldier).IsInstanceOfType(Players[6, 2]))
            {
                Counting.Stop();
                Counting.Interval = 1000;
                Choose_Trap(6, 2);
                TimeLeftToChoose.Visible = false;
                Counting.Start();
            }
            else
                ShowArrows(6, 2);
        }

        private void Square_6_3_Click(object sender, EventArgs e)
        {
            if (!KingChosen)
            {
                if (ThreeTwoOne.Text == "done")
                {
                    Counting.Stop();
                    TimeLeftToChoose.Visible = false;
                    Choose_King(6, 3);
                    Counting.Interval = 1;
                    Counting.Start();
                }
                else
                    Choose_King(6, 3);
            }
            else if (!TrapChosen && !typeof(KingSoldier).IsInstanceOfType(Players[6, 3]))
            {
                Counting.Stop();
                Counting.Interval = 1000;
                Choose_Trap(6, 3);
                TimeLeftToChoose.Visible = false;
                Counting.Start();
            }
            else
                ShowArrows(6, 3);
        }

        private void Square_6_4_Click(object sender, EventArgs e)
        {
            if (!KingChosen)
            {
                if (ThreeTwoOne.Text == "done")
                {
                    Counting.Stop();
                    TimeLeftToChoose.Visible = false;
                    Choose_King(6, 4);
                    Counting.Interval = 1;
                    Counting.Start();
                }
                else
                    Choose_King(6, 4);
            }
            else if (!TrapChosen && !typeof(KingSoldier).IsInstanceOfType(Players[6, 4]))
            {
                Counting.Stop();
                Counting.Interval = 1000;
                Choose_Trap(6, 4);
                TimeLeftToChoose.Visible = false;
                Counting.Start();
            }
            else
                ShowArrows(6, 4);
        }

        private void Square_6_5_Click(object sender, EventArgs e)
        {
            if (!KingChosen)
            {
                if (ThreeTwoOne.Text == "done")
                {
                    Counting.Stop();
                    TimeLeftToChoose.Visible = false;
                    Choose_King(6, 5);
                    Counting.Interval = 1;
                    Counting.Start();
                }
                else
                    Choose_King(6, 5);
            }
            else if (!TrapChosen && !typeof(KingSoldier).IsInstanceOfType(Players[6, 5]))
            {
                Counting.Stop();
                Counting.Interval = 1000;
                Choose_Trap(6, 5);
                TimeLeftToChoose.Visible = false;
                Counting.Start();
            }
            else
                ShowArrows(6, 5);
        }

        private void Square_6_6_Click(object sender, EventArgs e)
        {
            if (!KingChosen)
            {
                if (ThreeTwoOne.Text == "done")
                {
                    Counting.Stop();
                    TimeLeftToChoose.Visible = false;
                    Choose_King(6, 6);
                    Counting.Interval = 1;
                    Counting.Start();
                }
                else
                    Choose_King(6, 6);
            }
            else if (!TrapChosen && !typeof(KingSoldier).IsInstanceOfType(Players[6, 6]))
            {
                Counting.Stop();
                Counting.Interval = 1000;
                Choose_Trap(6, 6);
                TimeLeftToChoose.Visible = false;
                Counting.Start();
            }
            else
                ShowArrows(6, 6);
        }

        private void RightArrow_Click(object sender, EventArgs e)
        {
            Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2 + 1] = Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2];
            Squares[ChosenCoordinates.Item1, ChosenCoordinates.Item2 + 1].Image = Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Display();
            Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2] = null;
            Squares[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Image = null;
            DisableArrows();
            client.Send("Move Right " + ChosenCoordinates.Item1.ToString() + ChosenCoordinates.Item2.ToString());
            myturn = false;
            turnlbl.Text = "Enemy Turn!";
        }

        private void UpArrow_Click(object sender, EventArgs e)
        {
            Players[ChosenCoordinates.Item1 - 1, ChosenCoordinates.Item2] = Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2];
            Squares[ChosenCoordinates.Item1 - 1, ChosenCoordinates.Item2].Image = Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Display();
            Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2] = null;
            Squares[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Image = null;
            DisableArrows();
            client.Send("Move Up "+ ChosenCoordinates.Item1.ToString()+ ChosenCoordinates.Item2.ToString());
            myturn = false;
            turnlbl.Text = "Enemy Turn!";
        }

        private void LeftArrow_Click(object sender, EventArgs e)
        {
            Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2 - 1] = Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2];
            Squares[ChosenCoordinates.Item1, ChosenCoordinates.Item2 - 1].Image = Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Display();
            Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2] = null;
            Squares[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Image = null;
            DisableArrows();
            client.Send("Move Left " + ChosenCoordinates.Item1.ToString() + ChosenCoordinates.Item2.ToString());
            myturn = false;
            turnlbl.Text = "Enemy Turn!";


        }

        private void DownArrow_Click(object sender, EventArgs e)
        {
            Players[ChosenCoordinates.Item1 + 1, ChosenCoordinates.Item2] = Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2];
            Squares[ChosenCoordinates.Item1 + 1, ChosenCoordinates.Item2].Image = Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Display();
            Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2] = null;
            Squares[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Image = null;
            DisableArrows();
            client.Send("Move Down " + ChosenCoordinates.Item1.ToString() + ChosenCoordinates.Item2.ToString());
            myturn = false;
            turnlbl.Text = "Enemy Turn!";
        }

        private void FightPicture_Click(object sender, EventArgs e)
        {
            fightplace = "Left";
            myturn = false;
            turnlbl.Text = "Enemy Turn!";
            Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2 - 1].Turn_Visible();
            Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Turn_Visible();
            DisableArrows();
            if (Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Fight(Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2 - 1]) == FightResult.Win)
            {
                Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2 - 1] = Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2];
                Squares[ChosenCoordinates.Item1, ChosenCoordinates.Item2 - 1].Image = Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Display();
                Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2] = null;
                Squares[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Image = null;
            }
            else if (Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Fight(Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2 - 1]) == FightResult.Loss)
            {
                Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2] = null;
                Squares[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Image = null;
            }
            else if (Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Fight(Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2 - 1]) == FightResult.FalledInTrap)
            {
                Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2] = null;
                Squares[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Image = null;
                //falling func
            }
            else if (Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Fight(Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2 - 1]) == FightResult.EndGame)
            {
                myturn = false;
                turnlbl.Text = "You won!";
                Squares[KingCoordinates.Item1, KingCoordinates.Item2].Image = global::RPS.Client.Properties.Resources.MyKingWon;
                //win func
            }
            else if (Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Fight(Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2 - 1]) == FightResult.Draw)
            {
                Mydraw = "Left" + ChosenCoordinates.Item1.ToString() + ChosenCoordinates.Item2.ToString();
                paperpic.Visible = true;
                rockpic.Visible = true;
                scissorspic.Visible = true;
                drawLbl.Visible = true;

            }
            Squares[ChosenCoordinates.Item1, ChosenCoordinates.Item2 - 1].Image = Players[ChosenCoordinates.Item1 , ChosenCoordinates.Item2- 1].Display();
            client.Send("Fight Left " + ChosenCoordinates.Item1.ToString() + ChosenCoordinates.Item2.ToString());
        }

        private void UpFight_Click(object sender, EventArgs e)
        {
            fightplace = "Up";

            myturn = false;
            turnlbl.Text = "Enemy Turn!";
            Players[ChosenCoordinates.Item1 - 1, ChosenCoordinates.Item2].Turn_Visible();
            Players[ChosenCoordinates.Item1 , ChosenCoordinates.Item2].Turn_Visible();
            DisableArrows();
            if (Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Fight(Players[ChosenCoordinates.Item1 - 1, ChosenCoordinates.Item2]) == FightResult.Win)
            {
                Players[ChosenCoordinates.Item1 - 1, ChosenCoordinates.Item2] = Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2];
                Squares[ChosenCoordinates.Item1 - 1, ChosenCoordinates.Item2].Image = Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Display();
                Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2] = null;
                Squares[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Image = null;
            }
            else if(Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Fight(Players[ChosenCoordinates.Item1 - 1, ChosenCoordinates.Item2]) == FightResult.Loss)
            {
                Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2] = null;
                Squares[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Image = null;
            }
            else if (Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Fight(Players[ChosenCoordinates.Item1 - 1, ChosenCoordinates.Item2]) == FightResult.FalledInTrap)
            {
                Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2] = null;
                Squares[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Image = null;
                //falling func
            }
            else if (Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Fight(Players[ChosenCoordinates.Item1 - 1, ChosenCoordinates.Item2]) == FightResult.EndGame)
            {
                myturn = false;
                turnlbl.Text = "You won!";
                Squares[KingCoordinates.Item1, KingCoordinates.Item2].Image = global::RPS.Client.Properties.Resources.MyKingWon;
                //win func
            }
            else if (Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Fight(Players[ChosenCoordinates.Item1 - 1, ChosenCoordinates.Item2]) == FightResult.Draw)
            {
                Mydraw = "Up" + ChosenCoordinates.Item1.ToString() + ChosenCoordinates.Item2.ToString();
                myturn = true;
                turnlbl.Text = "Your Turn!";
                paperpic.Visible = true;
                rockpic.Visible = true;
                scissorspic.Visible = true;
                drawLbl.Visible = true;

            }
            Squares[ChosenCoordinates.Item1 - 1, ChosenCoordinates.Item2].Image = Players[ChosenCoordinates.Item1 - 1, ChosenCoordinates.Item2].Display();
            client.Send("Fight Up " + ChosenCoordinates.Item1.ToString() + ChosenCoordinates.Item2.ToString());
        }

        private void DownFight_Click(object sender, EventArgs e)
        {
            fightplace = "Down";
            myturn = false;
            turnlbl.Text = "Enemy Turn!";
            Players[ChosenCoordinates.Item1 + 1, ChosenCoordinates.Item2].Turn_Visible();
            Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Turn_Visible();
            DisableArrows();
            if (Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Fight(Players[ChosenCoordinates.Item1 + 1, ChosenCoordinates.Item2]) == FightResult.Win)
            {
                Players[ChosenCoordinates.Item1 + 1, ChosenCoordinates.Item2] = Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2];
                Squares[ChosenCoordinates.Item1 + 1, ChosenCoordinates.Item2].Image = Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Display();
                Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2] = null;
                Squares[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Image = null;
            }
            else if (Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Fight(Players[ChosenCoordinates.Item1 + 1, ChosenCoordinates.Item2]) == FightResult.Loss)
            {
                Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2] = null;
                Squares[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Image = null;
            }
            else if (Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Fight(Players[ChosenCoordinates.Item1 + 1, ChosenCoordinates.Item2]) == FightResult.FalledInTrap)
            {
                Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2] = null;
                Squares[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Image = null;
                //falling func
            }
            else if (Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Fight(Players[ChosenCoordinates.Item1 + 1, ChosenCoordinates.Item2]) == FightResult.EndGame)
            {
                myturn = false;
                turnlbl.Text = "You won!";
                Squares[KingCoordinates.Item1, KingCoordinates.Item2].Image = global::RPS.Client.Properties.Resources.MyKingWon;
                //win func
            }
            else if (Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Fight(Players[ChosenCoordinates.Item1 + 1, ChosenCoordinates.Item2]) == FightResult.Draw)
            {
                Mydraw = "Down" + ChosenCoordinates.Item1.ToString() + ChosenCoordinates.Item2.ToString();

                paperpic.Visible = true;
                rockpic.Visible = true;
                scissorspic.Visible = true;
                drawLbl.Visible = true;
            }
            Squares[ChosenCoordinates.Item1 + 1, ChosenCoordinates.Item2].Image = Players[ChosenCoordinates.Item1+ 1, ChosenCoordinates.Item2 ].Display();
            client.Send("Fight Down " + ChosenCoordinates.Item1.ToString() + ChosenCoordinates.Item2.ToString());
        }

        private void RightFight_Click(object sender, EventArgs e)
        {
            fightplace = "Right";
            myturn = false;
            turnlbl.Text = "Enemy Turn!";
            Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2 + 1].Turn_Visible();
            Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Turn_Visible();
            DisableArrows();
            if (Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Fight(Players[ChosenCoordinates.Item1 , ChosenCoordinates.Item2+ 1]) == FightResult.Win)
            {
                Players[ChosenCoordinates.Item1 , ChosenCoordinates.Item2+ 1] = Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2];
                Squares[ChosenCoordinates.Item1 , ChosenCoordinates.Item2+ 1].Image = Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Display();
                Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2] = null;
                Squares[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Image = null;
            }
            else if (Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Fight(Players[ChosenCoordinates.Item1 , ChosenCoordinates.Item2+ 1]) == FightResult.Loss)
            {
                Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2] = null;
                Squares[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Image = null;
            }
            else if (Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Fight(Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2 + 1]) == FightResult.FalledInTrap)
            {
                Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2] = null;
                Squares[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Image = null;
                //falling func
            }
            else if (Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Fight(Players[ChosenCoordinates.Item1 , ChosenCoordinates.Item2+ 1]) == FightResult.EndGame)
            {
                myturn = false;
                turnlbl.Text = "You won!";
                Squares[KingCoordinates.Item1, KingCoordinates.Item2].Image = global::RPS.Client.Properties.Resources.MyKingWon;
                //wingame func
            }
            else if (Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Fight(Players[ChosenCoordinates.Item1 , ChosenCoordinates.Item2+ 1]) == FightResult.Draw)
            {
                Mydraw = "Right" + ChosenCoordinates.Item1.ToString() + ChosenCoordinates.Item2.ToString();
                paperpic.Visible = true;
                rockpic.Visible = true;
                scissorspic.Visible = true;
                drawLbl.Visible = true;

                //win func
            }
            Squares[ChosenCoordinates.Item1, ChosenCoordinates.Item2 + 1].Image = Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2 + 1].Display();
            client.Send("Fight Right " + ChosenCoordinates.Item1.ToString() + ChosenCoordinates.Item2.ToString());
        }

        private void paperpic_Click(object sender, EventArgs e)
        {
            DisableArrows();
            Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2] = new PaperSoldier(Color.Red);
            Squares[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Image = Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Display();
            client.Send("Paper Draw" + fightplace+ ChosenCoordinates.Item1.ToString() + ChosenCoordinates.Item2.ToString());
/*           if (getother)
            {
                if (myturn)
                {


                    if (fightplace == "Up")
                    {
                        UpFight_Click(sender, e);
                    }
                    if (fightplace == "Down")
                    {
                        DownFight_Click(sender, e);
                    }
                    if (fightplace == "Right")
                    {
                        RightFight_Click(sender, e);
                    }
                    if (fightplace == "Left")
                    {
                        FightPicture_Click(sender, e);
                    }

                }
                else
                {
                    client.Send("getother");
                }
            }*/

        }

        private void rockpic_Click(object sender, EventArgs e)
        {   
            Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2] = new RockSoldier(Color.Red);
            Squares[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Image = Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Display();
            if (myturn)
            {
                client.Send("Rock Draw" + fightplace + ChosenCoordinates.Item1.ToString() + ChosenCoordinates.Item2.ToString());
            }
            else
            {
                client.Send("Rock Draw" + fightplace + ChosenCoordinates.Item1.ToString() + ChosenCoordinates.Item2.ToString());
            }
/*            if (getother)
            {
                if (myturn)
                {


                    if (fightplace == "Up")
                    {
                        UpFight_Click(sender, e);
                    }
                    else if (fightplace == "Down")
                    {
                        DownFight_Click(sender, e);
                    }
                    else if (fightplace == "Right")
                    {
                        RightFight_Click(sender, e);
                    }
                    else if (fightplace == "Left")
                    {
                        FightPicture_Click(sender, e);
                    }

                }
                else
                {
                    client.Send("getother");
                }
            }
*/
            DisableArrows();
        }

        private void scissorspic_Click(object sender, EventArgs e)
        {   
            
            Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2] = new ScissorsSoldier(Color.Red);
            Squares[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Image = Players[ChosenCoordinates.Item1, ChosenCoordinates.Item2].Display();
            client.Send("Scissors Draw" + fightplace + ChosenCoordinates.Item1.ToString() + ChosenCoordinates.Item2.ToString());
/*            if (getother)
            {
                if (myturn)
                {
                    if (fightplace == "Up")
                    {
                        UpFight_Click(sender, e);
                    }
                    if (fightplace == "Down")
                    {
                        DownFight_Click(sender, e);
                    }
                    if (fightplace == "Right")
                    {
                        RightFight_Click(sender, e);
                    }
                    if (fightplace == "Left")
                    {
                        FightPicture_Click(sender, e);
                    }
                }
                else
                {
                    client.Send("getother");
                }        
                getother = false;

            }*/

            DisableArrows();
        }
    }
}
