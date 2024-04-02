using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class Reservation
    {
        public int LocataireId { get; set; }
        public int VehiculeId { get; set; }
        public DateTime DateReservation { get; set; }
        [Range(1, 30)]
        public int DureeJours { get; set; }

        public Locataire Locataire { get; set; }
        public Vehicule Vehicule { get; set; }
    }
}
