﻿using Microsoft.AspNetCore.Blazor;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using UnicodeBrowser.Client.Models;

namespace UnicodeBrowser.Client.Repositories
{
	internal sealed class SearchRepository : RepositoryBase
	{
		public SearchRepository(ApplicationState applicationState, HttpClient httpClient)
			: base(applicationState, httpClient)
		{
		}

		public async Task<CodePoint[]> SearchAsync(string query, CancellationToken cancellationToken)
		{
			BeginAsyncOperation();
			try
			{
				var codePoints = await HttpClient.GetJsonAsync<CodePoint[]>("/api/search?q=" + Uri.EscapeDataString(query) + "&limit=100");
				cancellationToken.ThrowIfCancellationRequested();
				return codePoints;
			}
			finally
			{
				EndAsyncOperation();
			}
		}
	}
}
