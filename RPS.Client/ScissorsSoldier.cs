using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPS.Client
{
    class ScissorsSoldier : Soldier
    {
        public ScissorsSoldier(Color color) : base(color) { }

        public override FightResult Fight(Soldier enemy)
        {
            base.Fight(enemy);
            if (typeof(PaperSoldier).IsInstanceOfType(enemy))
                return FightResult.Win;
            else if (typeof(ScissorsSoldier).IsInstanceOfType(enemy))
                return FightResult.Draw;
            else if (typeof(KingSoldier).IsInstanceOfType(enemy))
                return FightResult.EndGame;
            else if (typeof(TrapSoldier).IsInstanceOfType(enemy))
                return FightResult.FalledInTrap;
            else
                return FightResult.Loss;
        }

        public override System.Drawing.Image Display()
        {
            if (this.color == Color.Red)
            {
                if (this.visible)
                    return global::RPS.Client.Properties.Resources.VisibleScissors;
                else
                    return global::RPS.Client.Properties.Resources.ScissorsSoldier;

            }
            else
            {
                if (this.visible)
                    return global::RPS.Client.Properties.Resources.ScissorsEnemy;
                else
                    return global::RPS.Client.Properties.Resources.Enemy;
            }
        }
    }
}
