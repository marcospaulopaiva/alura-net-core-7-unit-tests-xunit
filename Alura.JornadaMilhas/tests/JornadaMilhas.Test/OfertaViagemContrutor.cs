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
            //Cenário - Arrange
            Rota rota = new Rota(origem, destino);
            Periodo periodo = new Periodo(DateTime.Parse(dataIda), DateTime.Parse(dataVolta));

            //Ação - Act
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            //Validação - Assert
            Assert.Equal(validacao, oferta.EhValido);
        }

        [Fact]
        public void RetornaMensagemDeErroDeRotaOuPeriodoInvalidoQuandoRotaNula()
        {
            //Cenário - Arrange
            Rota rota = null;
            Periodo periodo = new Periodo(new DateTime(2024, 2, 1), new DateTime(2024, 2, 5));
            double preco = 100;

            //Ação - Act
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            //Validação - Assert
            Assert.Contains("A oferta de viagem não possui rota ou período válidos.", oferta.Erros.Sumario);
            Assert.False(oferta.EhValido);
        }

        [Fact]
        public void RetornaMensagemDeErroDeDataIdaNaoPodeSerMaiorQueDataVolta()
        {
            //Cenário - Arrange
            Rota rota = new Rota("OrigemTeste", "DestinoTeste");
            Periodo periodo = new Periodo(new DateTime(2024, 2, 5), new DateTime(2024, 2, 1));
            double preco = 100;

            //Ação - Act
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            //Validação - Assert
            Assert.Contains("Erro: Data de ida não pode ser maior que a data de volta.", oferta.Erros.Sumario);
            Assert.False(oferta.EhValido);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-250)]
        public void RetornaMensagemDeErroDePrecoInvalidoQuandoPrecoMenorOuIgualAZero(double preco)
        {
            //Cenário - Arrange
            Rota rota = new("OrigemTeste", "DestinoTeste");
            Periodo periodo = new(new DateTime(2024, 2, 1), new DateTime(2024, 2, 5));

            //Ação - Act
            OfertaViagem oferta = new(rota, periodo, preco);

            //Validação - Assert
            Assert.Contains("O preço da oferta de viagem deve ser maior que zero.", oferta.Erros.Sumario);
        }
    }
}