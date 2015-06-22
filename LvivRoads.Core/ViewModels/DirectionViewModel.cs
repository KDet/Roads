using System;
using Cirrious.CrossCore.Core;
using Cirrious.MvvmCross.Plugins.Location;
using Cirrious.MvvmCross.ViewModels;
using LvivRoads.Core.Services;
using LvivRoads.Core.Services.Direction;

namespace LvivRoads.Core.ViewModels
{
    public class DirectionViewModel : LocationViewModel
    {
        /// <summary>
        /// Cервіс для визначення маршруту між двома позиціями на карті.
        /// </summary>
        private readonly IDirectionService _directionService;

        private Position _from = new Position("lviv");
        private Position _to = new Position("kiev");
        private DirectionRoute[] _routes;

        /// <summary>
        /// Створення нового екземпляру типу <see cref="LvivRoads.Core.ViewModels.DirectionViewModel"/>.
        /// </summary>
        /// <param name="locationWatcher">Сервіс визначення поточного місця перебування користувача.</param>
        /// <param name="directionService">Cервіс для визначення маршруту між двома позиціями на карті.</param>
        public DirectionViewModel(IMvxLocationWatcher locationWatcher, IDirectionService directionService)
            : base(locationWatcher)
        {
            _directionService = directionService;
            
        }

        public override void Start()
        {
            StartLocationWatchCommand.Execute();
            StopLocationWatchCommand.Execute();
        }

        /// <summary>
        /// Подія оновлення маршруту
        /// </summary>
        public event EventHandler<MvxValueEventArgs<DirectionRoute[]>> DirectionRoutesUpdated;

        /// <summary>
        /// Початкова точка маршруту
        /// </summary>
        public Position From
        {
            get { return _from; }
            set { _from = value; RaisePropertyChanged(() => From); }
        }
        /// <summary>
        /// Кінцева точка маршруту
        /// </summary>
        public Position To
        {
            get { return _to; }
            set { _to = value; RaisePropertyChanged(() => To); }
        }
        /// <summary>
        /// Маршрути
        /// </summary>
        public DirectionRoute[] Routes
        {
            get { return _routes; }
            set { _routes = value; RaisePropertyChanged(() => Routes); }
        }

        /// <summary>
        /// Команда прокласти маршрут між від позиції From до позиції To.
        /// </summary>
        public IMvxCommand PaveWayCommand
        {
            get { return new MvxCommand(async () =>
            {
                var request = ComputeDirectionRequest();
                var response = await _directionService.GetResponseAsync(request);
                Routes = response.Routes;
                OnDirectionRoutesUpdated(Routes);
            }); }
        }

        protected virtual void OnDirectionRoutesUpdated(DirectionRoute[] routes)
        {
            var handler = DirectionRoutesUpdated;
            if (handler != null) 
                handler(this, new MvxValueEventArgs<DirectionRoute[]>(routes));
        }
        /// <summary>
        /// Фукція створення запиту до сервісу <see cref="IDirectionService"/>.
        /// </summary>
        /// <returns></returns>
        protected virtual DirectionRequest ComputeDirectionRequest()
        {
            var request = new DirectionRequest();
            request.Origin = From;
            request.Destination = To;
            request.DepartureTime = DateTime.UtcNow;
            request.Sensor = false;
            request.Mode = TravelMode.Transit;
            request.Language = "UKRAINIAN";
            request.Alternatives = false;
            request.Units = Units.Metric;
            return request;
        }
        /// <summary>
        /// Вивільнення ресурсів.
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
            _directionService.DisposeIfDisposable();
            _from.DisposeIfDisposable();
            _to.DisposeIfDisposable();
            _routes.DisposeIfDisposable();
        }
    }
}