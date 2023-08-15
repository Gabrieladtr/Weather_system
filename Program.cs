using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        //Temos 2 formas de acessar a API:
        //1º: Via comando do CMD. Dessa forma vamos usar o curl, que é um comando do DOS para acessar HTTP.
        //2º: Posso criar uma solicitação HTTP via o próprio C#.


        // Sua chave de API do OpenWeatherMap
        string apiKey = "72a2c82eea94bd3120a16e5c6a247aa5";

        // Cria a URL da API do OpenWeatherMap para obter os dados de São Paulo em unidades métricas
        string city = "Sao%20Paulo,BR";
        string apiUrl = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

        //1º forma de trazer as infos:
        Console.WriteLine("Call ChamandoAPIviaCMD():");
        ChamandoAPIviaCMD(apiUrl);




        Console.WriteLine("Call Chamando API via Http Client:");
        //2º forma de trazer as infos:
        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Retorno da call:");
                    Console.WriteLine(responseBody);
                }
                else
                {
                    Console.WriteLine($"Erro na solicitação. Código de status: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erro na solicitação: {ex.Message}");
            }
        }

    }







    public static void ChamandoAPIviaCMD(string apiUrl1)
    {
        // Configurações para iniciar o processo "curl"
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            // Nome do executável (no caso, "curl")
            FileName = "curl",

            // Argumentos para o comando curl (URL da API)
            Arguments = $"\"{apiUrl1}\"",

            // Redireciona a saída padrão do processo para que possamos capturá-la
            RedirectStandardOutput = true,

            // Não utiliza a shell para executar o processo
            UseShellExecute = false,

            // Não cria uma janela para o processo
            CreateNoWindow = true
        };

        // Cria e inicia o processo "curl"
        using (Process process = new Process { StartInfo = startInfo })
        {
            //Vai iniciar o processo
            process.Start();
            
            // Lê a saída padrão do processo (dados da API)
            string output = process.StandardOutput.ReadToEnd();

            // Aguarda até que o processo seja concluído
            process.WaitForExit();

            // Exibe a saída no console (dados da API)
            Console.WriteLine(output);
        }

    }

     
}