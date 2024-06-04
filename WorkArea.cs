using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DiplomaProject
{
    public abstract class WorkArea : Window
    {
        private MainMenu _areaOwner;
        private Grid _grid;

        protected WorkArea() { }
        protected WorkArea(MainMenu owner)
        {
            AreaOwner = owner;
        }

        public MainMenu AreaOwner { get => _areaOwner; set => _areaOwner = value; }
        public Grid Grid { get => _grid; set => _grid = value; }
        public abstract Grid PlaceElements(); //розміщує елементи всередині гріда

    }
}
