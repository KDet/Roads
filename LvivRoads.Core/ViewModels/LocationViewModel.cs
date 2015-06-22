using System;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Core;
using Cirrious.MvvmCross.Plugins.Location;
using Cirrious.MvvmCross.ViewModels;
using LvivRoads.Core.Services;

namespace LvivRoads.Core.ViewModels
{
    public class LocationViewModel
        : BaseViewModel
    {
        /// <summary>
        /// Період оновлення позиції на карті
        /// </summary>
        private readonly TimeSpan _locationUpdateTimeSpan = TimeSpan.FromSeconds(2);
        /// <summary>
        /// Сервіс визначення поточного місця перебування користувача
        /// </summary>
        private readonly IMvxLocationWatcher _locationWatcher;
        
        /// <summary>
        /// Сервіс визначення місця перебування громадського транспорту
        /// </summary>
        private LatitudeLongitude _location;
        
        private void StartLocationWatch()
        {

            if (!_locationWatcher.Started)
                _locationWatcher.Start(new MvxLocationOptions {TimeBetweenUpdates = _locationUpdateTimeSpan},
                    goeLocation =>
                    {
                        Location = new LatitudeLongitude(goeLocation.Coordinates.Latitude, goeLocation.Coordinates.Longitude);
                        LocationUpdated.Raise(this, Location);
                    },
                    error => Mvx.Error("Помилка локатора {0}", error.Code));
        }

        /// <summary>
        /// Створення нового об'єкту типу <see cref="LvivRoads.Core.ViewModels.LocationViewModel"/>.
        /// </summary>
        /// <param name="locationWatcher">Сервіс визначення поточного місця перебування користувача</param>
        public LocationViewModel(IMvxLocationWatcher locationWatcher)
        {
            if (locationWatcher == null)
                throw new ArgumentNullException("locationWatcher", "LocationWatcher can't be null");
            _locationWatcher = locationWatcher;
        }
        public override void Start()
        {
            base.Start();
            StartLocationWatch();
        }

        /// <summary>
        /// Подія зміни поточного місця перебування користувача
        /// </summary>
        public event EventHandler<MvxValueEventArgs<LatitudeLongitude>> LocationUpdated;

        /// <summary>
        /// Команда запуску GSP-локатора  визначення поточного місця перебування користувача
        /// </summary>
        public IMvxCommand StartLocationWatchCommand
        {
            get { return new MvxCommand(StartLocationWatch); }
        }
        /// <summary>
        /// Команда зупинки GSP-локатора  визначення поточного місця перебування користувача
        /// </summary>
        public IMvxCommand StopLocationWatchCommand
        {
            get { return new MvxCommand(() => _locationWatcher.Stop()); }
        }
        /// <summary>
        /// Поточне місце перебування користувача
        /// </summary>
        
        public LatitudeLongitude Location
        {
            get { return _location; }
            set { _location = value; RaisePropertyChanged(() => Location); }
        }

        /// <summary>
        /// Функція вивільнення ресурсів
        /// </summary>
        public override void  Dispose()
        {
            if (_locationWatcher.Started)
                _locationWatcher.Stop();
            _locationWatcher.DisposeIfDisposable();
            _location.DisposeIfDisposable();
            if(LocationUpdated!=null)
                LocationUpdated.DisposeIfDisposable();
        }
    }
}