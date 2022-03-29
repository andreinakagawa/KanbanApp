using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using KanbanApp.Filters; //filter
using KanbanApp.Model; //modelos: 

namespace KanbanApp.Controllers
{
    [Route("[controller]/")]
    [Produces("[application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class CardController : Controller
    {
        private readonly ApiContext _context;

        public CardController(ApiContext context)
        {
            _context = context;
        }


        [Authorize]
        [HttpGet]
        public IEnumerable<Card> Get()
        {
            return _context.Cards;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Card card)
        {
            if (card.Id != Guid.Empty) //id não está vazio
                return BadRequest(); //retorna status 400


            card.Id = Guid.NewGuid(); //cria um novo id - único
            _context.Cards.Add(card); //adiciona um novo card
            _context.SaveChanges();
            return Ok(card); //retorna o novo card
        }

        [Authorize]
        [TypeFilter(typeof(LogFilter))]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Card card)
        {
            if (id != card.Id) //ids diferentes
                return BadRequest(); //retorna status 400  

            //procurar um card cujo id é igual ao da requisição
            Card retCard = _context.Cards.FirstOrDefault(s => s.Id == id);
            if (retCard == null) //card não encontrado
            {
                return NotFound(); //retorna status 404
            }

            _context.Entry<Card>(retCard).CurrentValues.SetValues(card); //atualiza o card
            _context.SaveChanges();
            return Ok(retCard); //retorna o card alterado
        }

        [Authorize]
        [TypeFilter(typeof(LogFilter))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            //procura card pelo id
            Card card = _context.Cards.FirstOrDefault(s => s.Id == id);
            if (card == null) //card não encontrado
            {
                return NotFound(); //retorna status 404
            }

            _context.Cards.Remove(card); //deletar card
            _context.SaveChanges();
            return Ok(_context.Cards); //retorna a lista de cards
        }
    }
}