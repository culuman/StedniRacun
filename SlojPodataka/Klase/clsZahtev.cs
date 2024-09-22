using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka
{
    // Class: Zahtev - Prati podatke o zahtevu za otvaranje računa.

    // Responsibility:
    // - Prati podatke o zahtevu (ID zahteva, status).
    // - Povezuje se sa korisnikom (JMBG korisnika).

    // Collaboration:
    // - Sa klasom Korisnik (veza sa JMBG korisnika).

    [Table("ZAHTEV")]
    public class clsZahtev
    {
        // Polja
        [Key]
        private int _idZahteva;

        [Required]
        private string _jmbgKorisnika;

        [Required]
        private string _status;

        // Property
        public int IdZahteva { get => _idZahteva; set => _idZahteva = value; }
        public string JMBGKorisnika { get => _jmbgKorisnika; set => _jmbgKorisnika = value; }
        public string Status { get => _status; set => _status = value; }
    }
}
