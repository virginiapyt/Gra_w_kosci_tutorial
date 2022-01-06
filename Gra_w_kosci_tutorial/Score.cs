using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gra_w_kosci_tutorial
{
    public class Score : NotifyPropertyChanged
    {
        private int points;
        private bool done;
        private string name;

        public string Name
        {
            get => name; set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        public int Points
        {
            get => points; set
            {
                points = value;
                OnPropertyChanged();
            }
        }

        public bool Done
        {
            get => done; set
            {
                done = value;
                OnPropertyChanged();
            }
        }

        public Score(string name)
        {
            Name = name;
            Points = 0;
            Done = false;
        }
    }
}
