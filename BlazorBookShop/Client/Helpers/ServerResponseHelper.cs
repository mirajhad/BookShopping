using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBookShop.Client.Helpers
{
    public class ServerResponseHelper<T>
    {
        public ServerResponseHelper(bool isSuccess,
            T serverResponse,
            HttpResponseMessage responseMessage)
        {
            IsSuccess = isSuccess;
            ServerResponse = serverResponse;
            ResponseMessage = responseMessage;
        }

        public bool IsSuccess { get; set; }
        public T ServerResponse { get; set; }
        public HttpResponseMessage ResponseMessage { get; set; }

        public async Task<string> GetBodyPart()
        {
            return await ResponseMessage.Content.ReadAsStringAsync();
        }


    }
}
