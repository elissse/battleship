using System;
using System.Windows;

namespace battleship_
{
    internal class CellVM : ViewModelBase
    {
        static Random random=new Random();

        public int Angle { get; } = random.Next(-5, 5);
        public int AngleX { get; } = random.Next(-5, 5);
        public int AngleY { get; } = random.Next(-5, 5);
        public double ShiftX { get; } = random.Next(-20, 20)/10.0;
        public double ShiftY { get; } = random.Next(-20, 20) / 10.0;
        public double ScaleX { get; } = 1 + random.Next(-10, 3) / 100.0;
        public double ScaleY { get; } = 1 + random.Next(-10, 3) / 100.0;



        bool ship,shot;

        public CellVM(char state = '*')
        {
            ship = state == 'X';
        }

        public Visibility Miss => 
            shot && !ship? Visibility.Visible : Visibility.Collapsed;

        public Visibility Shot =>
            shot && ship? Visibility.Visible : Visibility.Collapsed;
        public Visibility IsEnemyShip =>
             ship ? Visibility.Visible : Visibility.Collapsed;
        public void ToShoot()
        {
            shot = true;
            Notify("Miss", "Shot");
        }
        public void ToShip()
        {
            ship = true;
        }
        public override string ToString()
        {
            if (ship && shot) return "X";
            if (ship && !shot) return "#";
            if (!ship && shot) return "*";
            if (!ship && !shot) return " ";
            return "";  
        }
    }
 
}
