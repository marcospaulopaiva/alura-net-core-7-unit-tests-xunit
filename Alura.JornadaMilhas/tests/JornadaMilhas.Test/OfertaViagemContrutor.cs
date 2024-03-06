using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test
{
    public class OfertaViagemContrutor
    {
        [Theory]
        [InlineData("",null,"2024-01-01", "2024-01-05", 0, false)]
        [InlineData("Fortaleza", "Gramado", "2024-01-01", "2024-01-05", 100, true)]
        [InlineData(null, "Sao Paulo", "2024-01-01", "2024-01-02", -1, false)]
        [InlineData("Fortaleza", "Sao Paulo", "2024-01-01", "2024-01-01", 0, false)]
        [InlineData("Fortaleza", "Rio de Janeiro", "2024-01-01", "2024-01-02", -500, false)]
        public void RetornaEnValidosDeAcordoComDadosDeEntrada(string origem, string destino, string dataIda, string dataVolta, double preco, bool validacao)
        {
            //Cen�rio - Arrange
            Rota rota = new Rota(origem, destino);
            Periodo periodo = new Periodo(DateTime.Parse(dataIda), DateTime.Parse(dataVolta));

            //A��o - Act
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            //Valida��o - Assert
            Assert.Equal(validacao, oferta.EhValido);
        }

        [Fact]
        public void RetornaMensagemDeErroDeRotaOuPeriodoInvalidoQuandoRotaNula()
        {
            //Cen�rio - Arrange
            Rota rota = null;
            Periodo periodo = new Periodo(new DateTime(2024, 2, 1), new DateTime(2024, 2, 5));
            double preco = 100;

            //A��o - Act
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            //Valida��o - Assert
            Assert.Contains("A oferta de viagem n�o possui rota ou per�odo v�lidos.", oferta.Erros.Sumario);
            Assert.False(oferta.EhValido);
        }

        [Fact]
        public void RetornaMensagemDeErroDeDataIdaNaoPodeSerMaiorQueDataVolta()
        {
            //Cen�rio - Arrange
            Rota rota = new Rota("OrigemTeste", "DestinoTeste");
            Periodo periodo = new Periodo(new DateTime(2024, 2, 5), new DateTime(2024, 2, 1));
            double preco = 100;

            //A��o - Act
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            //Valida��o - Assert
            Assert.Contains("Erro: Data de ida n�o pode ser maior que a data de volta.", oferta.Erros.Sumario);
            Assert.False(oferta.EhValido);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-250)]
        public void RetornaMensagemDeErroDePrecoInvalidoQuandoPrecoMenorOuIgualAZero(double preco)
        {
            //Cen�rio - Arrange
            Rota rota = new("OrigemTeste", "DestinoTeste");
            Periodo periodo = new(new DateTime(2024, 2, 1), new DateTime(2024, 2, 5));

            //A��o - Act
            OfertaViagem oferta = new(rota, periodo, preco);

            //Valida��o - Assert
            Assert.Contains("O pre�o da oferta de viagem deve ser maior que zero.", oferta.Erros.Sumario);
        }
    }
}