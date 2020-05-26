using APIs.DataBase;
using APIs.Helpers;
using APIs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIs.Controllers
{
    [Route("api/Palavras")]
    public class PalavrasController : ControllerBase
    {
        private readonly APIContext _banco;

        public PalavrasController(APIContext banco)
        {
            _banco = banco;
        }

        [Route(""),
         HttpGet]
        public IActionResult Get([FromQuery] PalavraUrlQuery pQuery)
        {
            var item = _banco.Palavras.AsQueryable();

            if (pQuery.Data.HasValue)
            {
                item = item.Where(x => x.Criado >= pQuery.Data.Value || x.Atualizado > pQuery.Data.Value);
            }

            if (pQuery.PagNumero.HasValue)
            {
                var quantidadeTotalRegistros = item.Count();
                item = item.Skip((pQuery.PagNumero.Value - 1) * pQuery.PagRegistro.Value).Take(pQuery.PagRegistro.Value);

                var paginacao = new Paginacao();
                paginacao.NumeroPagina = pQuery.PagNumero.Value;
                paginacao.RegistroPorPagina = pQuery.PagRegistro.Value;
                paginacao.TotalRegistros = quantidadeTotalRegistros;
                paginacao.TotalPaginas = (int)Math.Ceiling((double)quantidadeTotalRegistros / pQuery.PagRegistro.Value);

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginacao));

                if (pQuery.PagNumero > paginacao.TotalPaginas)
                {
                    return NotFound();
                }
            }

            return Ok(item);
        }

        [Route("{id}"),
         HttpGet]
        public IActionResult Get(int id)
        {
            var palavra = _banco.Palavras.Find(id);

            if (palavra == null)
                return NotFound();

            return Ok(palavra);
        }

        [Route("GetById/{pId}"), 
         HttpGet]
        public IActionResult GetById(int pId)
        {
            var palavra = _banco.Palavras.Find(pId);

            if (palavra == null)
                return NotFound();

            return Ok(palavra);
        }

        [Route(""),
         HttpPost]
        public IActionResult Post([FromBody] Palavra palavra)
        {
            _banco.Palavras.Add(palavra);
            _banco.SaveChanges();

            return Created($"/api/Palavras/{palavra.Id}", palavra);
        }

        [Route("{id}"),
         HttpPut]
        public IActionResult Put(int id, [FromBody] Palavra palavra)
        {
            var word = _banco.Palavras.AsNoTracking().FirstOrDefault(x => x.Id == id);

            if (word == null)
                return NotFound();

            palavra.Id = word.Id;
            _banco.Palavras.Update(palavra);
            _banco.SaveChanges();

            return Ok();
        }

        [Route("{id}"), 
         HttpDelete]
        public IActionResult Delete(int id)
        {
            var palavra = _banco.Palavras.Find(id);

            if (palavra == null)
                return NotFound();

            palavra.Ativo = false;
            _banco.Palavras.Update(palavra);
            _banco.SaveChanges();

            return NoContent();
        }
    }
}
