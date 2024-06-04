using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DiplomaProject
{
    class AddPersonArea : WorkArea
    {
        public AddPersonArea(){ }
        public AddPersonArea(MainMenu owner):base(owner)
        {
            Grid grid = new Grid();

        }
        public override Grid PlaceElements()
        {
            throw new NotImplementedException();
        }
    }
}
