using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

public class GPT4
{
    private readonly string apiKey;
    private readonly string apiEndpoint;

    public GPT4(IConfiguration configuration)
    {
        this.apiKey = configuration["OpenAI:ApiKey"];
        this.apiEndpoint = configuration["OpenAI:ApiEndpoint"];
    }

    public async Task<string> EnviarMensaje(string mensaje)
    {
        using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {this.apiKey}");

            var requestBody = new
            {
                prompt = mensaje,
                max_tokens = 150
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(this.apiEndpoint, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error en la solicitud: {response.ReasonPhrase}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            dynamic responseObject = JsonConvert.DeserializeObject(responseContent);

            return responseObject.choices[0].text.ToString();
        }
    }
}
