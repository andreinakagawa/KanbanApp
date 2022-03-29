using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KanbanApp.Model
{
    public class UsuarioToken
    {
        public string Token { get; set; } //token
        public DateTime ExpirationDate { get; set; } //data de expiração
    }
}
