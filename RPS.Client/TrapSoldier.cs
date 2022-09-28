using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPS.Client
{
    class TrapSoldier : Soldier
    {
        public TrapSoldier(Color color) : base(color) { }

        public override FightResult Fight(Soldier enemy)
        {
            return 0;
        }

        public override System.Drawing.Image Display()
        {
            if (this.color == Color.Red)
                return global::RPS.Client.Properties.Resources.MyTrap;
            else
                return global::RPS.Client.Properties.Resources.Enemy;
        }

    }
}
