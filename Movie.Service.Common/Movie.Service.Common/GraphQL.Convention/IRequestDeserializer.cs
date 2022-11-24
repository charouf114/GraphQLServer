using System.Collections.Generic;

namespace Movies.Service.Common
{
    public interface IRequestDeserializer
    {
        QueryInput GetQueryFromRequestBody(string requestBody);

        Dictionary<string, object> ConvertJsonToDictionary(string json);
    }
}
