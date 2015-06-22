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
        /// ����� ��������� ������� �� ����
        /// </summary>
        private readonly TimeSpan _locationUpdateTimeSpan = TimeSpan.FromSeconds(2);
        /// <summary>
        /// ����� ���������� ��������� ���� ����������� �����������
        /// </summary>
        private readonly IMvxLocationWatcher _locationWatcher;
        
        /// <summary>
        /// ����� ���������� ���� ����������� ������������ ����������
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
                    error => Mvx.Error("������� �������� {0}", error.Code));
        }

        /// <summary>
        /// ��������� ������ ��'���� ���� <see cref="LvivRoads.Core.ViewModels.LocationViewModel"/>.
        /// </summary>
        /// <param name="locationWatcher">����� ���������� ��������� ���� ����������� �����������</param>
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
        /// ���� ���� ��������� ���� ����������� �����������
        /// </summary>
        public event EventHandler<MvxValueEventArgs<LatitudeLongitude>> LocationUpdated;

        /// <summary>
        /// ������� ������� GSP-��������  ���������� ��������� ���� ����������� �����������
        /// </summary>
        public IMvxCommand StartLocationWatchCommand
        {
            get { return new MvxCommand(StartLocationWatch); }
        }
        /// <summary>
        /// ������� ������� GSP-��������  ���������� ��������� ���� ����������� �����������
        /// </summary>
        public IMvxCommand StopLocationWatchCommand
        {
            get { return new MvxCommand(() => _locationWatcher.Stop()); }
        }
        /// <summary>
        /// ������� ���� ����������� �����������
        /// </summary>
        
        public LatitudeLongitude Location
        {
            get { return _location; }
            set { _location = value; RaisePropertyChanged(() => Location); }
        }

        /// <summary>
        /// ������� ���������� �������
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