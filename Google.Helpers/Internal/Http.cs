/*
 * Licensed to the Apache Software Foundation (ASF) under one or more
 * contributor license agreements.  See the NOTICE file distributed with
 * this work for additional information regarding copyright ownership.
 * The ASF licenses this file to You under the Apache License, Version 2.0
 * (the "License"); you may not use this file except in compliance with
 * the License.  You may obtain a copy of the License at
 * 
 * http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Google.Helpers.Internal
{
	/// <summary>
	/// Provides an intuitive and simple HTTP client wrapper.
	/// </summary>
	public static class Http
	{
		public class HttpGetResponse
		{
			protected Uri RequestUri { get; set; }

			public HttpGetResponse(Uri uri)
			{
				RequestUri = uri;
			}

			protected virtual Task<StreamReader> GetStreamReaderAsync(Uri uri)
			{

                return  GetStreamReaderAsync(uri, GoogleSigned.SigningInstance);
			}
			protected virtual async Task<StreamReader> GetStreamReaderAsync(Uri uri, GoogleSigned signingInstance)
			{
				if (signingInstance != null)
				{
					uri = new Uri(signingInstance.GetSignedUri(uri));
				}
                var request = WebRequest.Create(uri);
                WebResponse response = await request.GetResponseAsync();

			    var stream =  response.GetResponseStream();
			    return new StreamReader(stream);
			}

			public virtual async Task<string> AsStringAsync()
			{
				var output = String.Empty;

				using (var reader = await GetStreamReaderAsync(this.RequestUri))
				{
					output = reader.ReadToEnd();
				}

				return output;
			}

			public virtual async Task<T> AsAsync<T>() where T : class
			{
				T output;
                using (var reader = await GetStreamReaderAsync(RequestUri))
				{
					var jsonReader = new JsonTextReader(reader);
					var serializer = new JsonSerializer();
					serializer.Converters.Add(new JsonEnumTypeConverter());
					serializer.Converters.Add(new JsonLocationConverter());
					output = serializer.Deserialize<T>(jsonReader);
				}
                return output;
			}
		}

		public static HttpGetResponse Get(Uri uri)
		{
			return Factory.CreateResponse(uri);
		}

		/// <summary>
		/// Gets or sets the factory that provides HttpGetResponse instances. Crude depency injection for the time being.
		/// </summary>
		public static HttpGetResponseFactory Factory = new HttpGetResponseFactory();

		/// <summary>
		/// A factory class for building HttpGetResponse instances.
		/// </summary>
		public class HttpGetResponseFactory
		{
			/// <summary>
			/// Builds a standard HttpGetResponse instance.
			/// </summary>
			/// <param name="uri"></param>
			/// <returns></returns>
			public virtual HttpGetResponse CreateResponse(Uri uri)
			{
				return new HttpGetResponse(uri);
			}
		}

	}
}
