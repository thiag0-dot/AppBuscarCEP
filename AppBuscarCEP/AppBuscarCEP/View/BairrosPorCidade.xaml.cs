using AppBuscarCEP.Model;
using AppBuscarCEP.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppBuscarCEP.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BairrosPorCidade : ContentPage
    {

        ObservableCollection<Cidade> lista_cidades = new ObservableCollection<Cidade>();
        ObservableCollection<Bairro> lista_Bairros = new ObservableCollection<Bairro>();
        public BairrosPorCidade()
        {
            InitializeComponent();

            pck_cidade.ItemsSource = lista_cidades;
            lst_bairros.ItemsSource = lista_Bairros;
        }
       

        private async void pck_estado_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Picker disparador = sender as Picker;

                string estado = disparador.SelectedItem as string;

                List<Cidade> arr_cidades = await DataService.GetCidadeByUF(estado);

                Console.WriteLine(arr_cidades);

                lista_cidades.Clear();
                arr_cidades.ForEach(i => lista_cidades.Add(i));
                //arr_cidades.ForEach(i => Console.WriteLine(i.descricaoCidade));
            }
            catch (Exception ex) 
            {
                await DisplayAlert("Erro", ex.Message, "Ok");
            }
        }

        private async void pck_cidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Picker disparador = sender as Picker;
                carregando.IsRunning = true;
                Cidade cidade_selecionada = disparador.SelectedItem as Cidade;
                

                List<Bairro> arr_bairros = await DataService.GetBairrosByIdCidade(cidade_selecionada.id_cidade);
                lista_Bairros.Clear();

                arr_bairros.ForEach(item => lista_Bairros.Add(item));
            }
            catch(Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "Ok");
            }
            finally
            {
                carregando.IsRunning = false;
            }
        }
    }
}