using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using vaultn_example_app.Models;

namespace vaultn_example_app
{
    public interface IVaultNApi
    {
        Task<string> Test();

        Task<string> PingPost();
        Task<string> PingGet();

        Task<BaseResponse> GetAgreements(int pageIndex,int pageSize,bool? onlyActive);

        Task<BaseResponse> GetSingleAgreement(string guid);

        Task<BaseResponse> CreateProduct(ProductRequest product);

        Task<BaseResponse> GetProducts(int pageIndex, int pageSize);

        Task<BaseResponse> UpdateProduct(string productGuid, ProductRequest product);

        Task<BaseResponse> GetSingleProduct(string productGuid);

        Task<BaseResponse> GetProductTypes();

        Task<BaseResponse> ImportTransaction(string productGuid, string agreementGuid, string clientReference,
            IList<string> serials);

        Task<BaseResponse> GetTransactions(int pageIndex, int pageSize);

        Task<BaseResponse> GetSingleTransaction(string transactionGuid);

        Task<BaseResponse> ExtractTransaction(string productGuid, string ipAddress, string clientReference,
            string agreementGuid);

        Task<BaseResponse> BlacklistTransaction(string productGuid, string keyCode, string clientReference);
    }
}
