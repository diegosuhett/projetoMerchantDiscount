using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TransacaoApi.Models;
using System.IO;
using TransacaoApi.Helpers;
using TransacaoApi.Serialization;
using System;

namespace TransacaoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequisicaoController : ControllerBase
    {
        public class Requisicao
        {
            public double Valor { get; set; }
            public string Adquirente { get; set; }
            public string Bandeira { get; set; }
            public string Tipo { get; set; }

            public bool EhValida()
            {
                if (this.Valor <= 0)
                {
                    return false;
                }
                else if (string.IsNullOrEmpty(this.Adquirente))
                {
                    return false;
                }
                else if (string.IsNullOrEmpty(this.Bandeira))
                {
                    return false;
                }
                else if (string.IsNullOrEmpty(this.Tipo))
                {
                    return false;
                }
                else { return true; }
            }
        }

        [HttpGet]
        public string IndexGet()
        {
            return "Informe os dados";
        }

        // GET
        [HttpGet("{adquirente}")]
        public string mdr(string adquirente)
        {
            List<MerchantDiscountReturn> listaAdq = new List<MerchantDiscountReturn>();
            List<Taxa> taxas = new List<Taxa>();

            if(adquirente != null)
            {
                foreach (MerchantDiscount mer in Utils.mdr)
                {
                    if (mer.Adquirente.ToLower().Equals(adquirente.ToLower()))
                    {
                        taxas.Add(new Taxa() { Bandeira = mer.Bandeira, Credito = mer.Credito, Debito = mer.Debito });
                    }
                }

                if (taxas.Count > 0)
                {
                    listaAdq.Add(new MerchantDiscountReturn() { Adquirente = "Adquirente " + adquirente.ToUpper(), Taxas = taxas });
                }
            }

            var retorno = Newtonsoft.Json.JsonConvert.SerializeObject(listaAdq);

            return retorno;
        }
        
        // POST
        [HttpPost]
        public string transaction([FromBody] Requisicao requisicao)
        {            
            if (requisicao != null && !requisicao.EhValida())
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject("Parametros inválidos");
            }
            else
            {
                RequisicaoReturn reqReturn = new RequisicaoReturn();
                reqReturn.CalculaValorLiquido(requisicao.Adquirente, requisicao.Bandeira, requisicao.Tipo, requisicao.Valor);

                var retorno = Newtonsoft.Json.JsonConvert.SerializeObject(reqReturn);

                return retorno;
            }
        }
    }
}