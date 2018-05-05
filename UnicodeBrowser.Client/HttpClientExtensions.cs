﻿using Microsoft.AspNetCore.Blazor;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace UnicodeBrowser.Client
{
	public static class HttpClientExtensions
    {
		private const string ApplicationJsonMediaType = "application/json";

		public static async Task<(long count, T[] items)> GetItemsAsync<T>(this HttpClient httpClient, string uri, int index, int count)
		{
			using (var response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, uri) { Headers = { Accept = { new MediaTypeWithQualityHeaderValue(ApplicationJsonMediaType) }, Range = new RangeHeaderValue(index, index + count - 1) { Unit = "items" } } }).ConfigureAwait(false))
			{
				if (!response.IsSuccessStatusCode) throw new InvalidDataException("The server did not respond successfully.");

				if (!string.Equals(response.Content.Headers.ContentType?.MediaType, ApplicationJsonMediaType, StringComparison.OrdinalIgnoreCase)) throw new InvalidDataException("The server responded with an unexpected Content-Type");

				var contentRange = response.Content.Headers.ContentRange;

				if (contentRange == null || !string.Equals(contentRange.Unit, "items")) throw new InvalidDataException();

				long itemCount = (contentRange.To - index + 1) ?? count;

				return (itemCount, JsonUtil.Deserialize<T[]>(await response.Content.ReadAsStringAsync().ConfigureAwait(false)));
			}
		}
    }
}
