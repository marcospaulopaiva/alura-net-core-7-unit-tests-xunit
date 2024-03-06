using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test
{
    public class OfertaViagemDesconto
    {
        [Fact]
        public void RetornaPrecoAtualizadoQuandoAplicadoDesconto()
        {
            //Cenário - Arrange
            Rota rota = new("Fortalza", "Gramado");
            Periodo periodo = new(DateTime.Parse("2024-05-01"), DateTime.Parse("2024-05-31"));
            double precoOriginal = 10000.00;
            double desconto = 2000.00;
            double precoComDesconto = precoOriginal - desconto;
            OfertaViagem oferta = new(rota, periodo, precoOriginal);

            //Ação - Act
            oferta.Desconto = desconto;

            //Validação - Assert
            Assert.Equal(precoComDesconto, oferta.Preco);
        }

        [Fact]
        public void RetornaDescontoMaximoQuandoValorDescontoMaiorQuePreco()
        {
            //Cenário - Arrange
            Rota rota = new("Fortalza", "Gramado");
            Periodo periodo = new(DateTime.Parse("2024-05-01"), DateTime.Parse("2024-05-31"));
            double precoOriginal = 100.00;
            double desconto = 120.00;
            double precoComDesconto = 30.00;
            OfertaViagem oferta = new(rota, periodo, precoOriginal);

            //Ação - Act
            oferta.Desconto = desconto;

            //Validação - Assert
            Assert.Equal(precoComDesconto, oferta.Preco, 0.001);
        }

        [Fact]
        public void RetornaPrecoOriginalQuandoValorDescontoNegativo()
        {
            //arrange
            Rota rota = new Rota("Fortalza", "Rio de Janeiro");
            Periodo periodo = new Periodo(new DateTime(2024, 05, 01), new DateTime(2024, 05, 10));
            double precoOriginal = 100.00;
            double desconto = -20.00;
            double precoComDesconto = precoOriginal;
            OfertaViagem oferta = new OfertaViagem(rota, periodo, precoOriginal);

            //act
            oferta.Desconto = desconto;

            //assert
            Assert.Equal(precoComDesconto, oferta.Preco, 0.001);
        }

    }
}
