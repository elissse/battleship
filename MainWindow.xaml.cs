using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace battleship_
{
    public partial class MainWindow : Window
    {
        BattleShiVM battleship = new BattleShiVM();
        Random random = new Random();
        public MainWindow()
        {
            DataContext = battleship;
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var border = sender as Border;
            var CellVM = border.DataContext as CellVM;
            CellVM.ToShoot();
            ;

            var x = random.Next(App.fieldSize);
            var y = random.Next(App.fieldSize);
            battleship.ShotOurMap(x, y);
        }

    }

}
