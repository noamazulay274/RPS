using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPS.Client
{
    public enum FightResult
    {
        FalledInTrap = -2,
        Loss = -1,
        Draw = 0,
        Win = 1,
        EndGame = 2
    }

    public enum Color
    {
        Red = 0,
        Blue = 1
    }

    public abstract class Soldier
    {
        protected Color color;
        protected bool visible;
        protected bool isAlive;

        public Soldier(Color color)
        {
            this.color = color;
            this.visible = false;
        }

        public Color Get_Color()
        {
            return this.color;
        }

        public void Turn_Visible()
        {
            this.visible = true;
        }

        public void Kill()
        {
            this.isAlive = false;
        }

        public virtual FightResult Fight(Soldier enemy)
        {
            this.Turn_Visible();
            enemy.Turn_Visible();
            return FightResult.Draw;
        }

        public abstract System.Drawing.Image Display();
    }
}
