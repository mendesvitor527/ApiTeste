using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

class Program
{
    public static async Task Main(string[] args)
    {
        // Captura o nome e o email do usuário via console
        Console.Write("Informe o nome do contato: ");
        string name = Console.ReadLine();

        Console.Write("Informe o email do contato: ");
        string email = Console.ReadLine();

        // Cria o contato na API Huggy
        await CreateContact(name, email);
    }

    public static async Task CreateContact(string name, string email)
    {
        // URL da API Huggy
        string url = "https://api.huggy.app/v3/contacts";

        // Access Token (Bearer)
        string accessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImp0aSI6IjkxZTg0OGI0NDNkNzdmODM3YmM2NTE2NTk3ZWRmYjViYTkxMWU3MmM3ZjVjMWFhMmM0NjdlZjU5ZjAxYzUzODRjOWYzNGVhMzRhYTg3MDRhIn0.eyJhdWQiOiJBUFAtYmViNzU1YmItZTEyMi00YTRmLWFiM2QtNDEzN2IxN2YzMWIwIiwianRpIjoiOTFlODQ4YjQ0M2Q3N2Y4MzdiYzY1MTY1OTdlZGZiNWJhOTExZTcyYzdmNWMxYWEyYzQ2N2VmNTlmMDFjNTM4NGM5ZjM0ZWEzNGFhODcwNGEiLCJpYXQiOjE3Mjk2MzQ4MTEsIm5iZiI6MTcyOTYzNDgxMSwiZXhwIjoxNzYxMTcwODExLCJzdWIiOiIxNTQxMjIiLCJzY29wZXMiOlsiaW5zdGFsbF9hcHAiLCJyZWFkX2FnZW50X3Byb2ZpbGUiXX0.wMAOZaiidfKPF3to2qUkYxbIWQMOooB0s0og_xOElPHIBzjGRVSGH2cEm28dvR9ikx4LxBXmE9ie9WnMCK7UVm6GhiiendpCs3D2YCoQ2x-4k3R9YP8HFGFwiHp2ZU1WyDG4WG92WAS5750blrSe_EnWEhdGKxIHScu2IZDgnlQ";

        using (HttpClient client = new HttpClient())
        {
            // Adiciona o cabeçalho de autorização (Bearer Token)
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            // Adiciona o cabeçalho de Cookie (opcional, depende da necessidade)
            client.DefaultRequestHeaders.Add("Cookie", "app_powerzap=useus0iqvo8uf72809n3b9bib1");

            // Cria o objeto para enviar no corpo da requisição
            var contactData = new
            {
                name = name,
                email = email
            };

            // Serializa o objeto para JSON
            string jsonData = JsonConvert.SerializeObject(contactData);

            // Converte os dados para StringContent com o Content-Type correto
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            try
            {
                // Faz a requisição POST para criar o contato
                HttpResponseMessage response = await client.PostAsync(url, content);

                // Lê o conteúdo da resposta
                string responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Contato criado com sucesso:");
                    Console.WriteLine(responseBody);
                }
                else
                {
                    Console.WriteLine($"Falha ao criar contato. Status Code: {response.StatusCode}");
                    Console.WriteLine(responseBody);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exceção capturada: {ex.Message}");
            }
        }
    }
}