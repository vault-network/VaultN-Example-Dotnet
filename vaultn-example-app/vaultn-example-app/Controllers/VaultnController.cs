using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vaultn_example_app.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace vaultn_example_app.Controllers
{
    
    [Route("api/[controller]")]
    public class VaultnController : Controller
    {
        private readonly IVaultNApi _api;
        public VaultnController(IVaultNApi api) {
            _api = api;
        }
        // GET: api/values
        [HttpGet("agreements")]
        public async Task<BaseResponse> GetAgreements()
        {
            return await _api.GetAgreements();
        }

        [HttpGet("agreement/{agreementGuid}")]
        public async Task<BaseResponse> GetSingleAgreement(string agreementGuid)
        {
            if (string.IsNullOrEmpty(agreementGuid)) throw new ArgumentNullException();
            return await _api.GetSingleAgreement(agreementGuid);
        }

        [HttpGet("ping")]
        public async Task<string> PingGet()
        {
            return await _api.PingGet();
        }

        [HttpPost("ping")]
        public async Task<string> PingPost()
        {
            return await _api.PingGet();
        }

        [HttpPost("CreateProduct")]
        public async Task<BaseResponse> CreateProduct([FromBody]ProductRequest product)
        {
            return await _api.CreateProduct(product);
        }

        [HttpGet("Products")]
        public async Task<BaseResponse> GetProducts()
        {
            return await _api.GetProducts();
        }

        [HttpPost("Products/{productGuid}")]
        public async Task<BaseResponse> GetProducts(string productGuid,[FromBody]ProductRequest product)
        {
            return await _api.UpdateProduct(productGuid, product);
        }

        [HttpGet("Products/{productGuid}")]
        public async Task<BaseResponse> GetSingleProduct(string productGuid)
        {
            return await _api.GetSingleProduct(productGuid);
        }

        [HttpGet("producttypes")]
        public async Task<BaseResponse> GetProductTypes()
        {
            return await _api.GetProductTypes();
        }

        [HttpPost("transaction/import/{productGuid}/{agreementGuid}/{clientReference}")]
        public async Task<BaseResponse> TransactionImport(string productGuid, string agreementGuid, string clientReference, [FromBody] List<string> serials)
        {
            if (serials == null)
            {
                serials = new List<string>();
                for (int i = 0; i < 10; i++)
                {
                    serials.Add(Guid.NewGuid().ToString());
                }
            }

            if (serials.Count == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    serials.Add(Guid.NewGuid().ToString());
                }
            }

            return await _api.ImportTransaction(productGuid, agreementGuid, clientReference, serials);
        }

        [HttpGet("transactions/list/{pageIndex}/{pageSize}")]
        public async Task<BaseResponse> TransactionList(int pageIndex, int pageSize)
        {
            return await _api.GetTransactions(pageIndex, pageSize);
        }
        [HttpGet("transactions/{transactionGuid}")]
        public async Task<BaseResponse> SingleTransaction(string transactionGuid)
        {
            return await _api.GetSingleTransaction(transactionGuid);
        }

        [HttpGet("transactions/extract/{productGuid}/{ipAddress}/{clientReference}/{agreementGuid}")]
        public async Task<BaseResponse> TransactionExtract(string productGuid, string ipAddress, string clientReference, string agreementGuid)
        {
            return await _api.ExtractTransaction(productGuid, ipAddress, clientReference, agreementGuid);
        }

        [HttpGet("transactions/blacklist/{productGuid}/{keyCode}/{clientReference}/{agreementGuid}")]
        public async Task<BaseResponse> TransactionExtract(string productGuid, string keyCode, string clientReference)
        {
            return await _api.BlacklistTransaction(productGuid, keyCode, clientReference);
        }

        [HttpGet("token")]
        public async Task<string> GenerateToken()
        {
            return await _api.Test();
        }
    }
}
