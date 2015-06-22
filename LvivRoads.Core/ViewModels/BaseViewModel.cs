using System;
using Cirrious.MvvmCross.ViewModels;

namespace LvivRoads.Core.ViewModels
{
    public class BaseViewModel : MvxViewModel, IDisposable
    {
        public virtual void Dispose() { }
    }
}