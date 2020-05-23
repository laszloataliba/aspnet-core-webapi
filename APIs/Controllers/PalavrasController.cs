using APIs.DataBase;
using APIs.Models;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Get()
        {
            return Ok(_banco.Palavras);
        }

        [Route("{id}"),
         HttpGet]
        public IActionResult Get(int id)
        {
            return Ok(_banco.Palavras.Find(id));
        }

        [Route(""),
         HttpPost]
        public IActionResult Post(Palavra palavra)
        {
            _banco.Palavras.Add(palavra);

            return Ok();
        }

        [Route("{id}"),
         HttpPut]
        public IActionResult Put(int id, Palavra palavra)
        {
            palavra.Id = id;
            _banco.Palavras.Update(palavra);

            return Ok();
        }

        [Route("{id}"), 
         HttpDelete]
        public IActionResult Delete(int id)
        {
            _banco.Palavras.Remove(_banco.Palavras.Find(id));

            return Ok();
        }
    }
}
