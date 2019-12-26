using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using KatlaSport.Services;
using KatlaSport.Services.OrderManagement;
using KatlaSport.WebApi.CustomFilters;
using Microsoft.ApplicationInsights;
using Microsoft.Web.Http;
using Swashbuckle.Swagger.Annotations;

namespace KatlaSport.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [RoutePrefix("api/transactions")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [CustomExceptionFilter]
    [SwaggerResponseRemoveDefaults]
    public class TransactionsController : ApiController
    {
        private readonly IRepository<Transaction> _transactionRepositoryService;
        private TelemetryClient TelemetryClient = new TelemetryClient();

        public TransactionsController(IRepository<Transaction> transactionRepositoryService)
        {
            _transactionRepositoryService = transactionRepositoryService ?? throw new ArgumentNullException(nameof(transactionRepositoryService));
            TelemetryClient.TrackEvent("Twitter-like API");
        }

        [HttpGet]
        [Route("getAll")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Returns a list of transactions.", Type = typeof(TransactionListItem[]))]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> GetTransactionsAsync()
        {
            TelemetryClient.TrackEvent("Get all Transactions");

            var transactions = await _transactionRepositoryService.GetAllAsync();
            return Ok(transactions);
        }

        [HttpGet]
        [Route("getOne/{transactionId:int:min(1)}")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Returns a transaction.", Type = typeof(Transaction))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> GetTransactionAsync(int transactionId)
        {
            var transaction = await _transactionRepositoryService.GetAsync(transactionId);
            return Ok(transaction);
        }

        [HttpPost]
        [Route("create")]
        [SwaggerResponse(HttpStatusCode.Created, Description = "Creates a new transaction.")]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.Conflict)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> AddTransaction([FromBody] Transaction createRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _transactionRepositoryService.AddAsync(createRequest);
            var transactions = await _transactionRepositoryService.GetAllAsync();
            var transaction = transactions.Last();
            var location = string.Format("/api/transactions/getOne/{0}", transaction.TransactionId);
            return Created<Transaction>(location, transaction);
        }

        [HttpPost]
        [Route("update/{id:int:min(1)}")]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Updates an existed transaction.")]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.Conflict)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> UpdateTransaction([FromUri] int id, [FromBody] Transaction updateRequest)
        {
            TelemetryClient.TrackEvent($"update transaction with {id} id");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            updateRequest.TransactionId = id;
            await _transactionRepositoryService.UpdateAsync(updateRequest);
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NoContent));
        }

        [HttpPost]
        [Route("delete/{id:int:min(1)}")]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Deletes an existed transacction.")]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.Conflict)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> DeleteTransaction([FromUri] int id)
        {
            TelemetryClient.TrackEvent($"delete transaction with {id} id");

            await _transactionRepositoryService.RemoveAsync(new Transaction() { TransactionId = id });
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NoContent));
        }

        [HttpPost]
        [Route("setStatus/{transactionId:int:min(1)}/status/{deletedStatus:bool}")]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Sets deleted status for an existed transaction.")]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> SetStatus([FromUri] int transactionId, [FromUri] bool deletedStatus)
        {
            await _transactionRepositoryService.SetStatusAsync(transactionId, deletedStatus);
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NoContent));
        }
    }
}
