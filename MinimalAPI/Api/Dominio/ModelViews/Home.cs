using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinimalAPI.Dominio.ModelViews
{
    public struct Home
    {
        public string Mensagem { get => "API de gerenciamento de veículos. Use o endpoint /login para autenticação."; }
        public string Documentacao { get => "/swagger"; }
    }
}