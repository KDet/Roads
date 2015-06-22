using Cirrious.MvvmCross.ViewModels;
using LvivRoads.Core.Services;

namespace LvivRoads.Core.Model
{
    public class Zombie
        : MvxNotifyPropertyChanged
    {
        private string _name;
        public string Name 
        {   
            get { return _name; }
            set { _name = value; RaisePropertyChanged(() => Name); }
        }

        private Position _position;
        public Position Position 
        {   
            get { return _position; }
            set { _position = value; RaisePropertyChanged(() => Position); }
        }

        private bool _isMale;
        public bool IsMale 
        {   
            get { return _isMale; }
            set { _isMale = value; RaisePropertyChanged(() => IsMale); }
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Name, Position);
        }
    }
}