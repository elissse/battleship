using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static battleship_.MapVM;
using System.Windows;

namespace battleship_
{
    enum DirectionShip { horisontal, vertical }
    internal class ShipVM : ViewModelBase
    {
        int rang = 1;
        (int x, int y) pos;
        DirectionShip direction = DirectionShip.horisontal;

        public ShipVM() { }
        public ShipVM(MapVM.Ship ship)
        {
            pos = (ship.X, ship.Y);
            rang = ship.Rang;
            direction=ship.Direction;
        }

        public DirectionShip Direct
        {
            get => direction;
            set => Set(ref direction, value, "Angle");
        }

        public int Rang
        {
            get => rang;
            set => Set(ref rang, value, "RangView");
        }
        public int RangView => Rang * App.cellSize - 5;
        public int Angle => direction == DirectionShip.horisontal ? 0 : 90;

        public (int, int) Pos
        {
            get => pos;
            set
            {
                Set(ref pos, value, "X", "Y");
            }
        }
        public int X => pos.x * App.cellSize + 3;
        public int Y => pos.y * App.cellSize + 3;
        

    }
}
