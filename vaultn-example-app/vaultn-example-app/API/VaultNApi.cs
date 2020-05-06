using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using vaultn_example_app.Models;

namespace vaultn_example_app
{
    public class VaultNApi: IVaultNApi
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUtilities _utilities;
        public VaultNApi(IHttpClientFactory httpClientFactory,IUtilities utilities)
        {
            _httpClientFactory = httpClientFactory;
            _utilities = utilities;
        }

        private HttpClient CreateClient()
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _utilities.GenerateToken());
            return client;
        }
        public async Task<string> Test()
        {
           return await Task.FromResult(_utilities.GenerateToken());
        }

        public async Task<string> PingPost()
        {
            var client = CreateClient();

            var result = 
                await client.PostAsync(ApiEndPointPrefix.Ping, new StringContent(""));
            if (result.IsSuccessStatusCode)
            {
                return "Test Operation Success";
            }
            else
            {
                return "Test Operation Failed due " + result.StatusCode.ToString() + " " + ((HttpStatusCode)result.StatusCode).ToString();
            }
        }

        public async Task<string> PingGet()
        {
            var client = CreateClient();

            var result = 
                await client.GetAsync(ApiEndPointPrefix.Ping);
            if (result.IsSuccessStatusCode)
            {
                return "Test Operation Success";
            }
            else
            {
                return "Test Operation Failed due " + result.StatusCode.ToString() + " " + ((HttpStatusCode)result.StatusCode).ToString();
            }
        }

        public async Task<BaseResponse> GetAgreements(int pageIndex = 0, int pageSize = 10,bool? onlyActive = null)
        {
            var client = CreateClient();


            var uri = $"{ApiEndPointPrefix.Agreement}?pageIndex={pageIndex}&pageSize={pageSize}";
            var result = 
                await client.GetAsync(uri);
            if (result.IsSuccessStatusCode)
            {
                return new ApiResponse<IList<AgreementResponse>>
                {
                    IsSuccess =  true,
                    Result = JsonConvert.DeserializeObject<IList<AgreementResponse>>(await result.Content.ReadAsStringAsync())
                };
            }
            else
            {

                var error = JsonConvert.DeserializeObject<ErrorResponse>(await result.Content.ReadAsStringAsync());
                if (error == null) {
                    error = new ErrorResponse();
                    error.Message = result.StatusCode.ToString();
                }
                error.IsSuccess = false;

                return error;
            }
        }

        public async Task<BaseResponse> GetSingleAgreement(string guid)
        {
            var client = CreateClient();

            
            var result = 
                await client.GetAsync($"{ApiEndPointPrefix.Agreement}?agreementGUID={guid}");
            if (result.IsSuccessStatusCode)
            {
                return new ApiResponse<AgreementResponse>
                {
                    IsSuccess =  true,
                    Result = JsonConvert.DeserializeObject<AgreementResponse>(await result.Content.ReadAsStringAsync())
                };
            }
            else
            {
                var error = JsonConvert.DeserializeObject<ErrorResponse>(await result.Content.ReadAsStringAsync());
                if (error == null)
                {
                    error = new ErrorResponse();
                    error.Message = result.StatusCode.ToString();
                }
                error.IsSuccess = false;
                return error;
            }
        }

        public async Task<BaseResponse> CreateProduct(ProductRequest product)
        {
            var client = CreateClient();
            
            var result = 
                await client.PostAsync($"{ApiEndPointPrefix.Product}", 
                    new StringContent(JsonConvert.SerializeObject(product)));
            if (result.IsSuccessStatusCode)
            {
                return new ApiResponse<ProductResponse>
                {
                    IsSuccess =  true,
                    Result = JsonConvert.DeserializeObject<ProductResponse>(await result.Content.ReadAsStringAsync())
                };
            }
            else
            {
                var error = JsonConvert.DeserializeObject<ErrorResponse>(await result.Content.ReadAsStringAsync());
                if (error == null)
                {
                    error = new ErrorResponse();
                    error.Message = result.StatusCode.ToString();
                }
                error.IsSuccess = false;
                return error;
            }
        }

        public async Task<BaseResponse> GetProducts(int pageIndex = 0, int pageSize = 0)
        {
            var client = CreateClient();

            var result =
                await client.GetAsync($"{ApiEndPointPrefix.Product}" +
                                      $"?pageIndex={pageIndex}" +
                                      $"&pageSize={pageSize}");
            if (result.IsSuccessStatusCode)
            {
                return new ApiResponse<IList<ProductResponse>>
                {
                    IsSuccess =  true,
                    Result = JsonConvert.DeserializeObject<IList<ProductResponse>>(await result.Content.ReadAsStringAsync())
                };
            }
            else
            {
                var error = JsonConvert.DeserializeObject<ErrorResponse>(await result.Content.ReadAsStringAsync());
                if (error == null)
                {
                    error = new ErrorResponse();
                    error.Message = result.StatusCode.ToString();
                }
                error.IsSuccess = false;
                return error;
            }
        }

        public async Task<BaseResponse> UpdateProduct(string productGuid, ProductRequest product)
        {
            var client = CreateClient();
            
            var result = 
                await client.PostAsync($"{ApiEndPointPrefix.Product}/{productGuid}", 
                    new StringContent(JsonConvert.SerializeObject(product)));
            if (result.IsSuccessStatusCode)
            {
                return new ApiResponse<ProductResponse>
                {
                    IsSuccess =  true,
                    Result = JsonConvert.DeserializeObject<ProductResponse>(await result.Content.ReadAsStringAsync())
                };
            }
            else
            {
                var error = JsonConvert.DeserializeObject<ErrorResponse>(await result.Content.ReadAsStringAsync());
                if (error == null)
                {
                    error = new ErrorResponse();
                    error.Message = result.StatusCode.ToString();
                }
                error.IsSuccess = false;
                return error;
            }
        }

        public async Task<BaseResponse> GetSingleProduct(string productGuid)
        {
            var client = CreateClient();
            
            var result = 
                await client.GetAsync($"{ApiEndPointPrefix.Product}/{productGuid}");
            if (result.IsSuccessStatusCode)
            {
                return new ApiResponse<ProductResponse>
                {
                    IsSuccess =  true,
                    Result = JsonConvert.DeserializeObject<ProductResponse>(await result.Content.ReadAsStringAsync())
                };
            }
            else
            {
                var error = JsonConvert.DeserializeObject<ErrorResponse>(await result.Content.ReadAsStringAsync());
                if (error == null)
                {
                    error = new ErrorResponse();
                    error.Message = result.StatusCode.ToString();
                }
                error.IsSuccess = false;
                return error;
            }
        }

        public async Task<BaseResponse> GetProductTypes()
        {
            var client = CreateClient();
            
            var result = 
                await client.GetAsync($"{ApiEndPointPrefix.ProductType}");
            if (result.IsSuccessStatusCode)
            {
                return new ApiResponse<IList<ProductTypeResponse>>
                {
                    IsSuccess =  true,
                    Result = JsonConvert.DeserializeObject<IList<ProductTypeResponse>>(await result.Content.ReadAsStringAsync())
                };
            }
            else
            {
                var error = JsonConvert.DeserializeObject<ErrorResponse>(await result.Content.ReadAsStringAsync());
                if (error == null)
                {
                    error = new ErrorResponse();
                    error.Message = result.StatusCode.ToString();
                }
                error.IsSuccess = false;
                return error;
            }
        }

        public async Task<BaseResponse> ImportTransaction(string productGuid, string agreementGuid, string clientReference, IList<string> serials)
        {
            var client = CreateClient();

            var result =
                await client.PostAsync(
                    $"{ApiEndPointPrefix.Transaction}" +
                    $"?productGUID={productGuid}" +
                    $"&agreementGUID={agreementGuid}" +
                    $"&clientReference={clientReference}",
                    new StringContent(JsonConvert.SerializeObject(serials)));
            if (result.IsSuccessStatusCode)
            {
                return new ApiResponse<TransactionResponse>
                {
                    IsSuccess =  true,
                    Result = JsonConvert.DeserializeObject<TransactionResponse>(await result.Content.ReadAsStringAsync())
                };
            }
            else
            {
                var error = JsonConvert.DeserializeObject<ErrorResponse>(await result.Content.ReadAsStringAsync());
                if (error == null)
                {
                    error = new ErrorResponse();
                    error.Message = result.StatusCode.ToString();
                }
                error.IsSuccess = false;
                return error;
            }
        }

        public async Task<BaseResponse> GetTransactions(int pageIndex, int pageSize)
        {
            var client = CreateClient();

            var result =
                await client.GetAsync(
                    $"{ApiEndPointPrefix.Transaction}" +
                    $"?pageIndex={pageIndex}" +
                    $"&pageSize={pageSize}");
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                Console.WriteLine(content);
                return new ApiResponse<IList<TransactionResponse>>
                {
                    IsSuccess =  true,
                    Result = JsonConvert.DeserializeObject<IList<TransactionResponse>>(content)
                };
            }
            else
            {
                var error = JsonConvert.DeserializeObject<ErrorResponse>(await result.Content.ReadAsStringAsync());
                if (error == null)
                {
                    error = new ErrorResponse();
                    error.Message = result.StatusCode.ToString();
                }
                error.IsSuccess = false;
                return error;
            }
        }

        public async Task<BaseResponse> GetSingleTransaction(string transactionGuid)
        {
            var client = CreateClient();

            var result =
                await client.GetAsync(
                    $"{ApiEndPointPrefix.Transaction}/{transactionGuid}");
            if (result.IsSuccessStatusCode)
            {
                return new ApiResponse<TransactionResponse>
                {
                    IsSuccess =  true,
                    Result = JsonConvert.DeserializeObject<TransactionResponse>(await result.Content.ReadAsStringAsync())
                };
            }
            else
            {
                var error = JsonConvert.DeserializeObject<ErrorResponse>(await result.Content.ReadAsStringAsync());
                if (error == null)
                {
                    error = new ErrorResponse();
                    error.Message = result.StatusCode.ToString();
                }
                error.IsSuccess = false;
                return error;
            }
        }

        public async Task<BaseResponse> ExtractTransaction(string productGuid, string ipAddress, string clientReference, string agreementGuid)
        {
            var client = CreateClient();

            var result =
                await client.GetAsync(
                    $"{ApiEndPointPrefix.Transaction}/extract" +
                    $"?productGUID={productGuid}" +
                    $"&ipAddress={ipAddress}" +
                    $"&clientReference={clientReference}" +
                    $"&agreementGUID={agreementGuid}");
            if (result.IsSuccessStatusCode)
            {
                return new ApiResponse<TransactionResponse>
                {
                    IsSuccess =  true,
                    Result = JsonConvert.DeserializeObject<TransactionResponse>(await result.Content.ReadAsStringAsync())
                };
            }
            else
            {
                var error = JsonConvert.DeserializeObject<ErrorResponse>(await result.Content.ReadAsStringAsync());
                if (error == null)
                {
                    error = new ErrorResponse();
                    error.Message = result.StatusCode.ToString();
                }
                error.IsSuccess = false;
                return error;
            }
        }

        public async Task<BaseResponse> BlacklistTransaction(string productGuid, string keyCode, string clientReference)
        {
            var client = CreateClient();

            var result =
                await client.GetAsync(
                    $"{ApiEndPointPrefix.Transaction}/blacklist" +
                    $"?productGUID={productGuid}" +
                    $"&keyCode={keyCode}" +
                    $"&clientReference={clientReference}");
                
            if (result.IsSuccessStatusCode)
            {
                return new ApiResponse<TransactionResponse>
                {
                    IsSuccess =  true,
                    Result = JsonConvert.DeserializeObject<TransactionResponse>(await result.Content.ReadAsStringAsync())
                };
            }
            else
            {
                var error = JsonConvert.DeserializeObject<ErrorResponse>(await result.Content.ReadAsStringAsync());
                if (error == null)
                {
                    error = new ErrorResponse();
                    error.Message = result.StatusCode.ToString();
                }
                error.IsSuccess = false;
                return error;
            }
        }
    }
}
