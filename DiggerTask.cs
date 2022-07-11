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
            if (Game.KeyPressed == Keys.Right && x + 1 < Game.MapWidth && !(Game.Map[x+1, y ] is Digger.Sack))
                deltaX = 1;
            else if (Game.KeyPressed == Keys.Left && x - 1 >= 0 && !(Game.Map[x - 1, y] is Digger.Sack))
                deltaX =-1;
            else if (Game.KeyPressed == Keys.Down && y + 1 < Game.MapHeight && !(Game.Map[x, y+1] is Digger.Sack))
                deltaY = 1;
            else if (Game.KeyPressed == Keys.Up && y - 1 >= 0 && !(Game.Map[x, y-1] is Digger.Sack))
                deltaY = -1;

            return new CreatureCommand() { DeltaX = deltaX, DeltaY = deltaY, TransformTo = null };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Gold) Game.Scores+= 10;
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

    class Sack : ICreature
    {
        int number = 0;
        public CreatureCommand Act(int x, int y)
        {            
             var result=new CreatureCommand();

             if (y < Game.MapHeight - 1)
             {
                 if (Game.Map[x, y + 1] != null && !((Game.Map[x, y + 1] is Player)&& number >= 1))
                 {
                     result.DeltaY= 0;
                   
                     if (number > 1)
                     {
                         result.TransformTo = new Gold();
                         number = 0;
                     }
                     number = 0;                    
                 }
                 else if (Game.Map[x, y + 1] == null || Game.Map[x, y + 1] is Player)
                 {
                    number++;
                    result.DeltaY = 1;
                    if (Game.Map[x, y + 1] is Player)
                        Game.Map[x, y + 1] = null;
                 }                
             }
             else if (number > 1) result.TransformTo = new Gold();

             return result; 
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
            return "Sack.png";
        }
    }

    class Gold : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Sack) return false;
            return true;
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public string GetImageFileName()
        {
            return "Gold.png";
        }
    }
}