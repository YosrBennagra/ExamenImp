﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class Agent
    {
        public int AgentId { get; set; }
        public DateTime DateEmbauche { get; set; }
        public string Nom{ get; set; }
        public string Prenom { get; set; }

    }
}
