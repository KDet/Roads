using System;
using System.Collections.Generic;
using Cirrious.CrossCore.Core;
using Cirrious.MvvmCross.Plugins.Location;
using LvivRoads.Core.Services;
using LvivRoads.Core.Services.Direction;
using LvivRoads.Core.Services.Transit;

namespace LvivRoads.Core.ViewModels
{
    public class TransitPositionUpdatedEventArgs : EventArgs
    {
        private readonly DirectionStep _directionStep;
        private readonly IEnumerable<LatitudeLongitude> _points;

        public TransitPositionUpdatedEventArgs(DirectionStep directionStep, IEnumerable<LatitudeLongitude> points)
        {
            _directionStep = directionStep;
            _points = points;
        }

        public DirectionStep DirectionStep
        {
            get { return _directionStep; }
        }
        public IEnumerable<LatitudeLongitude> Points
        {
            get { return _points; }
        }
    }

    public class TransitViewModel : DirectionViewModel
    {
        /// <summary>
        /// Сервіс визначення місця перебування громадсього транспорту
        /// </summary>
        private readonly ITransitService _transitService;
        
        private Timer _timer;

        /// <summary>
        /// Створення нового об'єкту типу <see cref="TransitViewModel"/>.
        /// </summary>
        /// <param name="locationWatcher">Сервіс визначення поточного місця перебування користувача.</param>
        /// <param name="directionService">Cервіс для визначення маршруту між двома позиціями на карті.</param>
        /// <param name="transitService">Сервіс визначення місця перебування громадсього транспорту.</param>
        public TransitViewModel(IMvxLocationWatcher locationWatcher, IDirectionService directionService, ITransitService transitService) : base(locationWatcher, directionService)
        {
            _transitService = transitService;  
        }

        /// <summary>
        /// Подія оновлення маршруту
        /// </summary>
        public event EventHandler<TransitPositionUpdatedEventArgs> TransitPositionUpdated;

        protected virtual void OnTransitPositionUpdated(DirectionStep directionStep, IEnumerable<LatitudeLongitude> transitPositions)
        {
            var handler = TransitPositionUpdated;
            if (handler != null)
                handler(this, new TransitPositionUpdatedEventArgs(directionStep, transitPositions));
        }

        private void OnTick(object param)
        {
            InvokeOnMainThread(() =>
            {
                var routes = param as DirectionRoute[];
                if (routes != null)
                    foreach (var route in routes)
                        foreach (var leg in route.Legs)
                            foreach (var step in leg.Steps)
                                if (step.TravelMode == TravelMode.Transit)
                                    OnTransitPositionUpdated(step, _transitService.GetTransportPosition(step));
            });
        }

        protected override void OnDirectionRoutesUpdated(DirectionRoute[] routes)
        {
            base.OnDirectionRoutesUpdated(routes);
            OnTick(routes);
            _timer = new Timer(OnTick, routes, 1000, 1000);
        }

        public override void Dispose()
        {
            base.Dispose();
            _transitService.DisposeIfDisposable();
            _timer.DisposeIfDisposable();
        }
    }
}