using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka
{
    // Class: Racun - Prati podatke o računu korisnika.

    // Responsibility:
    // - Prati podatke o računu (ID računa, stanje).
    // - Povezuje se sa korisnikom (JMBG korisnika).

    // Collaboration:
    // - Sa klasom Korisnik (veza sa JMBG korisnika).

    [Table("RACUN")]
    public class clsRacun
    {
        // Polja
        [Key]
        private int _idRacuna;

        [Required]
        [StringLength(13)]
        private string _jmbgKorisnika;

        [Required]
        private decimal _stanje;

        // Property
        public int IdRacuna { get => _idRacuna; set => _idRacuna = value; }
        public string JMBGKorisnika { get => _jmbgKorisnika; set => _jmbgKorisnika = value; }
        public decimal Stanje { get => _stanje; set => _stanje = value; }
    }
}
