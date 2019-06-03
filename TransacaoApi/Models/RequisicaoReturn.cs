using TransacaoApi.Helpers;
using TransacaoApi.Serialization;

namespace TransacaoApi.Models
{
    public class RequisicaoReturn
    {
        public double ValorLiquido { get; private set; }

        public RequisicaoReturn()
        {
        }

        public void CalculaValorLiquido(string adquirente, string bandeira, string tipo, double valorBruto)
        {
            double taxa = 0;

            foreach (MerchantDiscount mer in Utils.mdr)
            {
                if (mer.Adquirente.ToLower().Equals(adquirente.ToLower()) && mer.Bandeira.ToLower().Equals(bandeira.ToLower()))
                {
                    if (tipo.ToLower().Equals("credito"))
                    {
                        taxa = mer.Credito;
                    }
                    else if (tipo.ToLower().Equals("debito"))
                    {
                        taxa = mer.Debito;
                    }
                }
            }

            this.ValorLiquido = valorBruto * ((100 - taxa) / 100);
        }
    }
}
