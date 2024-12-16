﻿using MyJetWallet.Fireblocks.Client.Embedded.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MyJetWallet.Fireblocks.Client.Embedded
{
    public partial class FewAssetsClient : BaseClient, IFewAssetsClient
    {
        private string _baseUrl;

        private HttpClient _httpClient;
        private static Lazy<JsonSerializerSettings> _settings = new Lazy<JsonSerializerSettings>(CreateSerializerSettings, true);
        private JsonSerializerSettings _instanceSettings;

        public FewAssetsClient(ClientConfigurator clientConfigurator, HttpClient httpClient) : base(clientConfigurator)
        {
            BaseUrl = "https://api.fireblocks.io/v1";
            _httpClient = httpClient;
            Initialize();
        }

        public string BaseUrl
        {
            get { return _baseUrl; }
            set
            {
                _baseUrl = value;
                if (!string.IsNullOrEmpty(_baseUrl) && !_baseUrl.EndsWith("/"))
                    _baseUrl += '/';
            }
        }

        private static JsonSerializerSettings CreateSerializerSettings()
        {
            var settings = new JsonSerializerSettings();
            UpdateJsonSerializerSettings(settings);
            return settings;
        }

        protected JsonSerializerSettings JsonSerializerSettings { get { return _instanceSettings ?? _settings.Value; } }

        static partial void UpdateJsonSerializerSettings(JsonSerializerSettings settings);

        partial void Initialize();

        partial void PrepareRequest(HttpClient client, HttpRequestMessage request, string url);
        partial void PrepareRequest(HttpClient client, HttpRequestMessage request, System.Text.StringBuilder urlBuilder);
        partial void ProcessResponse(HttpClient client, HttpResponseMessage response);
        
        public async Task<Response<FewAssetsGetListResponse>> GetAssetsListAsync(FewAssetsGetListRequest request, CancellationToken cancellationToken = default)
        {
            if (request.WalletId == null)
                throw new ArgumentNullException("walletId");

            if (request.AccountId == null)
                throw new ArgumentNullException("accountId");

            var client_ = _httpClient;
            var disposeClient_ = false; 
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    var urlBuilder_ = new System.Text.StringBuilder();
                    if (!string.IsNullOrEmpty(_baseUrl)) urlBuilder_.Append(_baseUrl);
                    // Operation Path: "ncw/wallets/{walletId}/accounts/{accountId}/assets"
                    urlBuilder_.Append("ncw/wallets");
                    urlBuilder_.Append(Uri.EscapeDataString(ConvertToString(request.WalletId, System.Globalization.CultureInfo.InvariantCulture)));
                    urlBuilder_.Append("/accounts/");
                    urlBuilder_.Append(Uri.EscapeDataString(ConvertToString(request.AccountId, System.Globalization.CultureInfo.InvariantCulture)));
                    urlBuilder_.Append("/assets");
                    urlBuilder_.Append('?');

                    if (request.Order != null)
                    {
                        urlBuilder_.Append(Uri.EscapeDataString("order")).Append('=').Append(Uri.EscapeDataString(ConvertToString(request.Order, System.Globalization.CultureInfo.InvariantCulture))).Append('&');
                    }
                    if (request.Sort != null)
                    {
                        urlBuilder_.Append(Uri.EscapeDataString("sort")).Append('=').Append(value: System.Uri.EscapeDataString(ConvertToString(request.Sort, System.Globalization.CultureInfo.InvariantCulture))).Append('&');
                    }
                    if (request.PageSize >= 0)
                    {
                        urlBuilder_.Append(Uri.EscapeDataString("pageSize")).Append('=').Append(Uri.EscapeDataString(ConvertToString(request.PageSize, System.Globalization.CultureInfo.InvariantCulture))).Append('&');
                    }
                    if (request.PageCursor != null)
                    {
                        urlBuilder_.Append(Uri.EscapeDataString("pageCursor")).Append('=').Append(Uri.EscapeDataString(ConvertToString(request.PageCursor,    System.Globalization.CultureInfo.InvariantCulture))).Append('&');
                    }

                    urlBuilder_.Append(Uri.EscapeDataString("enabled")).Append('=').Append(Uri.EscapeDataString(ConvertToString(request.Enabled, System.Globalization.CultureInfo.InvariantCulture))).Append('&');
                    urlBuilder_.Length--;

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);

                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.IEnumerable<string>>();
                        foreach (var item_ in response_.Headers)
                            headers_[item_.Key] = item_.Value;
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<FewAssetsGetListResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return new Response<FewAssetsGetListResponse>(status_, headers_, objectResponse_.Object);
                        }
                        else
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ErrorSchema>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            throw new ApiException<ErrorSchema>("Error Response", status_, objectResponse_.Text, headers_, objectResponse_.Object, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        public async Task<Response<FewAsset>> GetAssetByIdAsync(FewAssetRequest request, CancellationToken cancellationToken = default)
        {
            if (request.WalletId == null)
                throw new ArgumentNullException("walletId");

            if (request.AccountId == null)
                throw new ArgumentNullException("accountId");

            if (request.AssetId == null)
                throw new ArgumentNullException("assetId");

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    var urlBuilder_ = new System.Text.StringBuilder();
                    if (!string.IsNullOrEmpty(_baseUrl)) urlBuilder_.Append(_baseUrl);
                    // Operation Path: "ncw/wallets/{walletId}/accounts/{accountId}/assets/{assetId}"
                    urlBuilder_.Append("ncw/wallets/");
                    urlBuilder_.Append(Uri.EscapeDataString(ConvertToString(request.WalletId, System.Globalization.CultureInfo.InvariantCulture)));
                    urlBuilder_.Append("/accounts/");
                    urlBuilder_.Append(Uri.EscapeDataString(ConvertToString(request.AccountId, System.Globalization.CultureInfo.InvariantCulture)));
                    urlBuilder_.Append("/assets/");
                    urlBuilder_.Append(Uri.EscapeDataString(ConvertToString(request.AssetId, System.Globalization.CultureInfo.InvariantCulture)));

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);

                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.IEnumerable<string>>();
                        foreach (var item_ in response_.Headers)
                            headers_[item_.Key] = item_.Value;
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<FewAsset>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return new Response<FewAsset>(status_, headers_, objectResponse_.Object);
                        }
                        else
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ErrorSchema>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            throw new ApiException<ErrorSchema>("Error Response", status_, objectResponse_.Text, headers_, objectResponse_.Object, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        public async Task<Response<FewAssetBalance>> GetAssetBalanceAsync(FewAssetRequest request, CancellationToken cancellationToken = default)
        {
            if (request.WalletId == null)
                throw new ArgumentNullException("walletId");

            if (request.AccountId == null)
                throw new ArgumentNullException("accountId");

            if (request.AssetId == null)
                throw new ArgumentNullException("assetId");

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    var urlBuilder_ = new System.Text.StringBuilder();
                    if (!string.IsNullOrEmpty(_baseUrl)) urlBuilder_.Append(_baseUrl);
                    // Operation Path: "ncw/wallets/{walletId}/accounts/{accountId}/assets/{assetId}/balance"
                    urlBuilder_.Append("ncw/wallets/");
                    urlBuilder_.Append(Uri.EscapeDataString(ConvertToString(request.WalletId, System.Globalization.CultureInfo.InvariantCulture)));
                    urlBuilder_.Append("/accounts/");
                    urlBuilder_.Append(Uri.EscapeDataString(ConvertToString(request.AccountId, System.Globalization.CultureInfo.InvariantCulture)));
                    urlBuilder_.Append("/assets/");
                    urlBuilder_.Append(Uri.EscapeDataString(ConvertToString(request.AssetId, System.Globalization.CultureInfo.InvariantCulture)));
                    urlBuilder_.Append("/balance");

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);

                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.IEnumerable<string>>();
                        foreach (var item_ in response_.Headers)
                            headers_[item_.Key] = item_.Value;
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<FewAssetBalance>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return new Response<FewAssetBalance>(status_, headers_, objectResponse_.Object);
                        }
                        else
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ErrorSchema>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            throw new ApiException<ErrorSchema>("Error Response", status_, objectResponse_.Text, headers_, objectResponse_.Object, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        public async Task<Response<FewAssetAddressesGetListResponse>> GetAssetAddressesListAsync(FewAssetAddressesGetListRequest request, CancellationToken cancellationToken = default)
        {
            if (request.WalletId == null)
                throw new ArgumentNullException("walletId");

            if (request.AccountId == null)
                throw new ArgumentNullException("accountId");

            if (request.AssetId == null)
                throw new ArgumentNullException("assetId");

            if (request.PageCursor == null)
                throw new ArgumentNullException("pageCursor");

            if (request.PageSize < 1)
                throw new ArgumentNullException("pageSize");

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    var urlBuilder_ = new System.Text.StringBuilder();
                    if (!string.IsNullOrEmpty(_baseUrl)) urlBuilder_.Append(_baseUrl);
                    // Operation Path: "ncwncw/wallets/{walletId}/accounts/{accountId}/assets/{assetId}/addresses"
                    urlBuilder_.Append("ncw/wallets");
                    urlBuilder_.Append(Uri.EscapeDataString(ConvertToString(request.WalletId, System.Globalization.CultureInfo.InvariantCulture)));
                    urlBuilder_.Append("/accounts/");
                    urlBuilder_.Append(Uri.EscapeDataString(ConvertToString(request.AccountId, System.Globalization.CultureInfo.InvariantCulture)));
                    urlBuilder_.Append("/assets/");
                    urlBuilder_.Append(Uri.EscapeDataString(ConvertToString(request.AssetId, System.Globalization.CultureInfo.InvariantCulture)));
                    urlBuilder_.Append("/addresses");
                    urlBuilder_.Append('?');

                    if (request.Order != null)
                    {
                        urlBuilder_.Append(Uri.EscapeDataString("order")).Append('=').Append(Uri.EscapeDataString(ConvertToString(request.Order, System.Globalization.CultureInfo.InvariantCulture))).Append('&');
                    }
                    if (request.Sort != null)
                    {
                        urlBuilder_.Append(Uri.EscapeDataString("sort")).Append('=').Append(value: System.Uri.EscapeDataString(ConvertToString(request.Sort, System.Globalization.CultureInfo.InvariantCulture))).Append('&');
                    }
                    if (request.PageSize >= 0)
                    {
                        urlBuilder_.Append(Uri.EscapeDataString("pageSize")).Append('=').Append(Uri.EscapeDataString(ConvertToString(request.PageSize, System.Globalization.CultureInfo.InvariantCulture))).Append('&');
                    }
                    if (request.PageCursor != null)
                    {
                        urlBuilder_.Append(Uri.EscapeDataString("pageCursor")).Append('=').Append(Uri.EscapeDataString(ConvertToString(request.PageCursor, System.Globalization.CultureInfo.InvariantCulture))).Append('&');
                    }

                    urlBuilder_.Append(Uri.EscapeDataString("enabled")).Append('=').Append(Uri.EscapeDataString(ConvertToString(request.Enabled, System.Globalization.CultureInfo.InvariantCulture))).Append('&');
                    urlBuilder_.Length--;

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);

                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.IEnumerable<string>>();
                        foreach (var item_ in response_.Headers)
                            headers_[item_.Key] = item_.Value;
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<FewAssetAddressesGetListResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return new Response<FewAssetAddressesGetListResponse>(status_, headers_, objectResponse_.Object);
                        }
                        else if (status_ == 400)
                        {
                            string responseText_ = (response_.Content == null) ? string.Empty : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("Query parameters were invalid", status_, responseText_, headers_, null);
                        }
                        else
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ErrorSchema>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            throw new ApiException<ErrorSchema>("Error Response", status_, objectResponse_.Text, headers_, objectResponse_.Object, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        public async Task<Response<FewAssetsGetListResponse>> GetSupportedAssetsListAsync(FewAssetGetSupportedAssetsListRequest request, CancellationToken cancellationToken = default)
        {
            if (request.PageSize < 1)
                throw new ArgumentNullException("pageSize");

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    var urlBuilder_ = new System.Text.StringBuilder();
                    if (!string.IsNullOrEmpty(_baseUrl)) urlBuilder_.Append(_baseUrl);
                    // Operation Path: "wallets/supported_assets"
                    urlBuilder_.Append("wallets/supported_assets");
                    urlBuilder_.Append('?');

                    if (request.PageSize >= 0)
                    {
                        urlBuilder_.Append(Uri.EscapeDataString("pageSize")).Append('=').Append(Uri.EscapeDataString(ConvertToString(request.PageSize, System.Globalization.CultureInfo.InvariantCulture))).Append('&');
                    }
                    if (request.PageCursor != null)
                    {
                        urlBuilder_.Append(Uri.EscapeDataString("pageCursor")).Append('=').Append(Uri.EscapeDataString(ConvertToString(request.PageCursor, System.Globalization.CultureInfo.InvariantCulture))).Append('&');
                    }

                    urlBuilder_.Append(Uri.EscapeDataString("onlyBaseAssets")).Append('=').Append(Uri.EscapeDataString(ConvertToString(request.OnlyBaseAssets, System.Globalization.CultureInfo.InvariantCulture))).Append('&');
                    urlBuilder_.Length--;

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);

                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.IEnumerable<string>>();
                        foreach (var item_ in response_.Headers)
                            headers_[item_.Key] = item_.Value;
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<FewAssetsGetListResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return new Response<FewAssetsGetListResponse>(status_, headers_, objectResponse_.Object);
                        }
                        else
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ErrorSchema>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            throw new ApiException<ErrorSchema>("Error Response", status_, objectResponse_.Text, headers_, objectResponse_.Object, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        public async Task<Response<FewAssetAddress>> AddAssetAsync(FewAssetRequest request, CancellationToken cancellationToken = default)
        {
            if (request.WalletId == null)
                throw new ArgumentNullException("walletId");

            if (request.AccountId == null)
                throw new ArgumentNullException("accountId");

            if (request.AssetId == null)
                throw new ArgumentNullException("assetId");

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("POST");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    var urlBuilder_ = new System.Text.StringBuilder();
                    if (!string.IsNullOrEmpty(_baseUrl)) urlBuilder_.Append(_baseUrl);
                    // Operation Path: "ncw/wallets/{walletId}/accounts/{accountId}/assets/{assetId}"
                    urlBuilder_.Append("ncw/wallets");
                    urlBuilder_.Append(Uri.EscapeDataString(ConvertToString(request.WalletId, System.Globalization.CultureInfo.InvariantCulture)));
                    urlBuilder_.Append("/accounts/");
                    urlBuilder_.Append(Uri.EscapeDataString(ConvertToString(request.AccountId, System.Globalization.CultureInfo.InvariantCulture)));
                    urlBuilder_.Append("/assets/");
                    urlBuilder_.Append(Uri.EscapeDataString(ConvertToString(request.AssetId, System.Globalization.CultureInfo.InvariantCulture)));

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.IEnumerable<string>>();
                        foreach (var item_ in response_.Headers)
                            headers_[item_.Key] = item_.Value;
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 201)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<FewAssetAddress>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return new Response<FewAssetAddress>(status_, headers_, objectResponse_.Object);
                        }
                        else
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ErrorSchema>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            throw new ApiException<ErrorSchema>("Error Response", status_, objectResponse_.Text, headers_, objectResponse_.Object, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        public async Task<Response<FewAssetBalance>> RefreshAssetBalanceAsync(FewAssetRequest request, CancellationToken cancellationToken = default)
        {
            if (request.WalletId == null)
                throw new ArgumentNullException("walletId");

            if (request.AccountId == null)
                throw new ArgumentNullException("accountId");

            if (request.AssetId == null)
                throw new ArgumentNullException("assetId");

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("PUT");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    var urlBuilder_ = new System.Text.StringBuilder();
                    if (!string.IsNullOrEmpty(_baseUrl)) urlBuilder_.Append(_baseUrl);
                    // Operation Path: "ncw/wallets/{walletId}/accounts/{accountId}/assets/{assetId}/balance"
                    urlBuilder_.Append("ncw/wallets/");
                    urlBuilder_.Append(Uri.EscapeDataString(ConvertToString(request.WalletId, System.Globalization.CultureInfo.InvariantCulture)));
                    urlBuilder_.Append("/accounts/");
                    urlBuilder_.Append(Uri.EscapeDataString(ConvertToString(request.AccountId, System.Globalization.CultureInfo.InvariantCulture)));
                    urlBuilder_.Append("/assets/");
                    urlBuilder_.Append(Uri.EscapeDataString(ConvertToString(request.AssetId, System.Globalization.CultureInfo.InvariantCulture)));
                    urlBuilder_.Append("/balance");

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);

                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.IEnumerable<string>>();
                        foreach (var item_ in response_.Headers)
                            headers_[item_.Key] = item_.Value;
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<FewAssetBalance>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return new Response<FewAssetBalance>(status_, headers_, objectResponse_.Object);
                        }
                        else
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ErrorSchema>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            throw new ApiException<ErrorSchema>("Error Response", status_, objectResponse_.Text, headers_, objectResponse_.Object, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        protected struct ObjectResponseResult<T>
        {
            public ObjectResponseResult(T responseObject, string responseText)
            {
                this.Object = responseObject;
                this.Text = responseText;
            }

            public T Object { get; }

            public string Text { get; }
        }

        public bool ReadResponseAsString { get; set; }

        protected virtual async Task<ObjectResponseResult<T>> ReadObjectResponseAsync<T>(HttpResponseMessage response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, CancellationToken cancellationToken)
        {
            if (response == null || response.Content == null)
            {
                return new ObjectResponseResult<T>(default(T), string.Empty);
            }

            if (ReadResponseAsString)
            {
                var responseText = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                try
                {
                    var typedBody = JsonConvert.DeserializeObject<T>(responseText, JsonSerializerSettings);
                    return new ObjectResponseResult<T>(typedBody, responseText);
                }
                catch (JsonException exception)
                {
                    var message = "Could not deserialize the response body string as " + typeof(T).FullName + ".";
                    throw new ApiException(message, (int)response.StatusCode, responseText, headers, exception);
                }
            }
            else
            {
                try
                {
                    using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    using (var streamReader = new System.IO.StreamReader(responseStream))
                    using (var jsonTextReader = new JsonTextReader(streamReader))
                    {
                        var serializer = JsonSerializer.Create(JsonSerializerSettings);
                        var typedBody = serializer.Deserialize<T>(jsonTextReader);
                        return new ObjectResponseResult<T>(typedBody, string.Empty);
                    }
                }
                catch (JsonException exception)
                {
                    var message = "Could not deserialize the response body stream as " + typeof(T).FullName + ".";
                    throw new ApiException(message, (int)response.StatusCode, string.Empty, headers, exception);
                }
            }
        }

        private string ConvertToString(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value == null)
            {
                return "";
            }

            if (value is Enum)
            {
                var name = Enum.GetName(value.GetType(), value);
                if (name != null)
                {
                    var field = System.Reflection.IntrospectionExtensions.GetTypeInfo(value.GetType()).GetDeclaredField(name);
                    if (field != null)
                    {
                        var attribute = System.Reflection.CustomAttributeExtensions.GetCustomAttribute(field, typeof(System.Runtime.Serialization.EnumMemberAttribute))
                            as System.Runtime.Serialization.EnumMemberAttribute;
                        if (attribute != null)
                        {
                            return attribute.Value != null ? attribute.Value : name;
                        }
                    }

                    var converted = Convert.ToString(Convert.ChangeType(value, Enum.GetUnderlyingType(value.GetType()), cultureInfo));
                    return converted == null ? string.Empty : converted;
                }
            }
            else if (value is bool)
            {
                return Convert.ToString((bool)value, cultureInfo).ToLowerInvariant();
            }
            else if (value is byte[])
            {
                return Convert.ToBase64String((byte[])value);
            }
            else if (value is string[])
            {
                return string.Join(",", (string[])value);
            }
            else if (value.GetType().IsArray)
            {
                var valueArray = (System.Array)value;
                var valueTextArray = new string[valueArray.Length];
                for (var i = 0; i < valueArray.Length; i++)
                {
                    valueTextArray[i] = ConvertToString(valueArray.GetValue(i), cultureInfo);
                }
                return string.Join(",", valueTextArray);
            }

            var result = Convert.ToString(value, cultureInfo);
            return result == null ? "" : result;
        }
    }
}
