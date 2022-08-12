using System.Net;
using Newtonsoft.Json;
using static Newtonsoft.Json.JsonConvert;

namespace FinanceKeeper.Data.Entities
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string? ErrorMessage { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
