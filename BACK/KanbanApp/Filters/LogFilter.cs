using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using KanbanApp.Model;

namespace KanbanApp.Filters
{
    public class LogFilter
    {
        private readonly ApiContext _context;
        private Card currentCard { get; set; }

        public LogFilter(ApiContext context)
        {
            _context = context;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //Buscar o card que será alterado/removido
            object obId;
            context.RouteData.Values.TryGetValue("Id", out obId);
            if (obId != null) //Id não está vazio
            {
                string id = obId.ToString();
                //buscar o card com o Id correspondente
                currentCard = _context.Cards.FirstOrDefault(s => s.Id.ToString() == id);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //Realizar o registro do log
            if (context.Result.GetType() == typeof(OkObjectResult) && currentCard != null)
            {
                HttpRequest req = context.HttpContext.Request;
                string methodType = req.Method == "PUT" ? "Alterar" : "Remover"; //checa o tipo de requisição
                //compor string segundo os requisitos
                string log_string = string.Format("{0} - Card {1} - {2} - {3}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), currentCard.Id, currentCard.Titulo, methodType);
                //escrever o log
                System.Diagnostics.Debug.WriteLine(log_string);
            }
        }
    }
}
