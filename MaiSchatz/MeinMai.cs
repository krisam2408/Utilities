using Newtonsoft.Json;
using System.Text;

namespace MaiSchatz;

public sealed class MeinMai
{
    private readonly string m_endpointCall;
    private readonly bool m_enabled;

    public MeinMai(MaiSchatzSettings settings)
    {
        m_enabled = settings.IsEnabled;

        m_endpointCall = settings
            .Endpoint
            .Replace("{API_KEY}", settings.Key);
    }

    public async Task<T?> CallAsync<T>() where T : class
    {
        if (!m_enabled)
            return null;

        using(HttpClient client = new())
        using(MemoryStream ms = new())
        {
            HttpResponseMessage response = await client.GetAsync(m_endpointCall);
            await response.Content.CopyToAsync(ms);
            byte[] dataBuffer = ms.ToArray();
            string data = Encoding.UTF8.GetString(dataBuffer);
            T? result = JsonConvert.DeserializeObject<T>(data);

            if(result == null)
                throw new FormatException(data);

            return result;
        }
    }
}
