using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka
{
    // Class: Uplata - Prati podatke o uplati na račun.

    // Responsibility:
    // - Prati podatke o uplati (ID uplate, iznos, status, datum).
    // - Povezuje se sa korisnikom (JMBG korisnika).
    // - Povezuje se sa računom (ID računa).

    // Collaboration:
    // - Sa klasom Korisnik (veza sa JMBG korisnika).
    // - Sa klasom Racun (veza sa ID računa).

    [Table("UPLATA")]
    public class Uplata
    {
        // Polja
        [Key]
        private int _idUplate;

        [Required]
        private string _jmbgKorisnika;

        [Required]
        private int _idRacuna;

        [Required]
        private decimal _iznos;

        [Required]
        private string _status;

        [Required]
        private DateTime _datum;

        // Property
        public int IdUplate { get => _idUplate; set => _idUplate = value; }
        public string JMBGKorisnika { get => _jmbgKorisnika; set => _jmbgKorisnika = value; }
        public int IDRacuna { get => _idRacuna; set => _idRacuna = value; }
        public decimal Iznos { get => _iznos; set => _iznos = value; }
        public string Status { get => _status; set => _status = value; }
        public DateTime Datum { get => _datum; set => _datum = value; }
    }
}
