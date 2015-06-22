using System;
using System.Collections.Generic;

namespace LvivRoads.Core.Services
{
    public abstract class BaseSigningService
    {
        private static readonly Dictionary<Type, GoogleSigned> SigningInstances = new Dictionary<Type, GoogleSigned>();

        internal static void AssignSigningInstance(BaseSigningService serviceInstance, GoogleSigned signingInstance)
        {
            SigningInstances[serviceInstance.GetType()] = signingInstance;
        }

        protected GoogleSigned GetSigningInstance()
        {
            GoogleSigned instance;
            return SigningInstances.TryGetValue(GetType(), out instance) == false ? null : instance;
        }
    }
}
