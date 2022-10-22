using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace battleship_
{
    internal class MapVM : ViewModelBase
    {
        static Random random = new Random();
        CellVM[,] map;


        public ObservableCollection<ShipVM> Ships { get; } = new ObservableCollection<ShipVM>();
        public CellVM this[int x, int y] { get { return map[y, x]; } }

        public IReadOnlyCollection<IReadOnlyCollection<CellVM>> Map
        {
            get
            {
                var viewMap = new List<List<CellVM>>();
                for (int y = 0; y < App.fieldSize; y++)
                {
                    viewMap.Add(new List<CellVM>());
                    for (int x = 0; x < App.fieldSize; x++)
                    {
                        viewMap[y].Add(this[x, y]);
                    }
                }
                return viewMap;
            }
        }

        internal void SetShips(params ShipVM[] ships)
        {
            foreach (var ship in ships)
            {
                Ships.Add(ship);
                var (x, y) = ship.Pos;
                var rang = ship.Rang;
                var direction = ship.Direct;
                if (direction == DirectionShip.horisontal)
                    for (int j = x; j < x + rang; j++)
                        this[j, y].ToShip();
                else
                    for (int i = y; i < y + rang; i++)
                        this[x, i].ToShip();
            }
        }

        public MapVM(string s) : this()
        {
            var mp = s.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < App.fieldSize; i++)
            {
                for (int j = 0; j < App.fieldSize; j++)
                {
                    if (mp[i][j] == 'X')
                        map[i, j].ToShip();
                }
            }
        }

        public MapVM()
        {
            map = new CellVM[App.fieldSize, App.fieldSize];
            for (int i = 0; i < App.fieldSize; i++)
            {
                for (int j = 0; j < App.fieldSize; j++)
                {
                    map[i, j] = new CellVM();
                }
            }
        }

        public void FillMap(params int[] navy)
        {
            List<Ship> ships = null;
            while (ships == null)
            {
                ships = fillMap(new List<Ship>(), navy);
            }
            foreach (var ship in ships)
            {
                if (ship.Direction == DirectionShip.horisontal)
                {
                    for (int x = ship.X; x < ship.X + ship.Rang; x++)
                    {
                        this[x, ship.Y].ToShip();
                    }
                }
                else
                {
                    for (int y = ship.Y; y < ship.Y + ship.Rang; y++)
                    {
                        this[ship.X, y].ToShip();
                    }
                }
            }
            Ships.Clear();
            foreach (var ship in ships)
            {
                Ships.Add(new ShipVM(ship));
            }
        }

        private List<Ship> fillMap(List<Ship> ships, params int[] navy)
        {
            var p = navy.Length - 1;

            while (p > 0 && navy[p] == 0) p--;

            if (p < 1)
            {
                return ships;
            }
            else
            {
                var ship = new Ship();
                ship.Rang = p;
                navy[p]--;
                int k = 0;
                while (k < 20)
                {
                    if (random.Next(2) == 0)
                    {
                        ship.Direction = DirectionShip.horisontal;
                        ship.X = random.Next(10 - p);
                        ship.Y = random.Next(10);
                    }
                    else
                    {
                        ship.Direction = DirectionShip.vertical;
                        ship.X = random.Next(10);
                        ship.Y = random.Next(10 - p);
                    }

                    if (ships.All(other => !ship.Cross(ref other)))
                    {
                        ships.Add(ship);
                        var result = fillMap(ships, navy);
                        if (result != null)
                            return result;
                        ships.RemoveAt(ships.Count - 1);
                    }
                    k++;
                }
                navy[p]++;
            }
            return null;
        }

        internal struct Ship
        {
            public int X, Y, Rang;
            public DirectionShip Direction;

            public Ship(int x, int y, int rang, DirectionShip direction)
            {
                X = x; Y = y; Rang = rang; Direction = direction;
            }

            public bool Cross(ref Ship other)
            {
                int x = X - 1, y = Y - 1, xx, yy;
                if (Direction == DirectionShip.horisontal)
                {
                    xx = x + Rang + 1 ;
                    yy = y + 2 ;
                }
                else
                {
                    yy = y + Rang + 1;
                    xx = x + 2;
                }
                int ox = other.X, oy = other.Y, oxx = ox, oyy = oy;
                if (Direction == DirectionShip.horisontal)
                {
                    oxx += other.Rang - 2;
                }
                else
                {
                    oyy += other.Rang - 2;
                }
                return x <= ox && ox <= xx && y <= oy && oy <= yy ||
                       x <= oxx && oxx <= xx && y <= oyy && oyy <= yy;
            }
            public override string ToString()
            {
                return $"x:{X} y:{Y} R:{Rang}{(Direction == DirectionShip.horisontal ? '-' : '|')}";
            }

        }
        
    }
}
