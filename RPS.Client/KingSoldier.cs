using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPS.Client
{
    class KingSoldier : Soldier
    {
        public KingSoldier(Color color) : base(color) { }

        public override FightResult Fight(Soldier enemy)
        {
            return 0;
        }

        public override System.Drawing.Image Display()
        {
            if (this.color == Color.Red)
                return global::RPS.Client.Properties.Resources.MyKing;
            else
            {
                if (visible)
                {
                    return global::RPS.Client.Properties.Resources.EnemyLoss;

                }
                else
                {
                return global::RPS.Client.Properties.Resources.Enemy;
                }

            }
        }

    }
}
