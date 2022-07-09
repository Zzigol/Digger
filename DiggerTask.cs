using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Digger
{
    class Terrain : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }

        public int GetDrawingPriority()
        {
            return 0;
        }

        public string GetImageFileName()
        {
            return "Terrain.png";
        }
    }

    class Player : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            int deltaX=0;
            int deltaY=0;
            if (Game.KeyPressed == Keys.Right && x + 1 < Game.MapWidth)
                deltaX = 1;
            else if (Game.KeyPressed == Keys.Left && x - 1 >= 0)
                deltaX =-1;
            else if (Game.KeyPressed == Keys.Down && y + 1 < Game.MapHeight)
                deltaY = 1;
            else if (Game.KeyPressed == Keys.Up && y - 1 >= 0 )
                deltaY = -1;

            return new CreatureCommand() { DeltaX = deltaX, DeltaY = deltaY, TransformTo = null };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            
            return false;
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public string GetImageFileName()
        {
            return "Digger.png";

        }
    }

}