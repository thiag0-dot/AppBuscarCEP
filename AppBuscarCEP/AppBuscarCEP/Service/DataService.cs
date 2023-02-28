using AppBuscarCEP.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Runtime.InteropServices.ComTypes;

namespace AppBuscarCEP.Service
{
    public class DataService
    {
        public static async Task<Endereco> GetEnderecoByCep(string cep)
        {
            Endereco end;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://cep.metoda.com.br/endereco/by-cep?cep=172105580" + cep);

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;

                    end = JsonConvert.DeserializeObject<Endereco>(json);
                }
                else
                {
                    throw new Exception(response.RequestMessage.Content.ToString());
                }
            }
            return end;

        }

        public static async Task<List<Bairro>> GetBairrosByIdCidade(int id_cidade)
        {
            List<Bairro> arr_bairros = new List<Bairro>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://cep.metoda.com.br/bairro/by-cidade?id=4874" + arr_bairros);

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;

                    arr_bairros = JsonConvert.DeserializeObject<List<Bairro>>(json);
                }
                else
                {
                    throw new Exception(response.RequestMessage.Content.ToString());
                }
            }
            return arr_bairros;
        }

        public static async Task<List<Logradouro>> GetLogradouroByBairroAndIdCidade(string bairro, int id_cidade)
        {
            List<Logradouro> arr_log = new List<Logradouro>(); 
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://cep.metoda.com.br/logradouro/by-bairro?id_cidade=" + id_cidade + "&bairro=" + bairro);
                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content?.ReadAsStringAsync().Result;

                    arr_log = JsonConvert.DeserializeObject<List<Logradouro>>(json);
                }
                else
                {
                    throw new Exception(response.RequestMessage.Content.ToString());
                }
            }
            return arr_log;
        }

        public static async Task<List<Logradouro>> GetCepByLogradouro(int CEP)
        {
            List<Logradouro> arr_cep = new List<Logradouro>();
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://cep.metoda.com.br/cep/by-logradouro?logradouro=Rua" + arr_cep);
                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content?.ReadAsStringAsync().Result;
                    arr_cep = JsonConvert.DeserializeObject<List<Logradouro>>(json);
                }
                else
                {
                    throw new Exception(response.RequestMessage.Content.ToString());
                }
            }
            return arr_cep;
        }

        

    }
}
