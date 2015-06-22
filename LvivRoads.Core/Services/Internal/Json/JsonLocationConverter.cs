using System;
using Newtonsoft.Json.Linq;

namespace LvivRoads.Core.Services.Internal.Json
{
    public class JsonLocationConverter : JsonCreationConverter<Position>
    {
        protected override Position Create(Type objectType, JObject jsonObject)
        {
            return new LatitudeLongitude(jsonObject.Value<double>("lat"), jsonObject.Value<double>("lng"));
        }
    }
}