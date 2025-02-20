﻿using TaskFinanceAPI.Models;

namespace TaskFinanceAPI.DTOs
{
    public class TarefaDTO
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public int IdUsuario { get; set; }
        public StatusTarefa Status { get; set; }
    }
}
