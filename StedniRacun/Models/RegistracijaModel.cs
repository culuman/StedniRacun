using System.ComponentModel.DataAnnotations;

public class RegistracijaModel
{
    [Required(ErrorMessage = "JMBG je obavezan.")]
    [StringLength(13, ErrorMessage = "JMBG ne sme biti duži od 13 brojeva.")]
    [RegularExpression(@"^[0-9]{13}$")]
    public string JMBG { get; set; }

    [Required(ErrorMessage = "Ime je obavezno.")]
    [StringLength(40, ErrorMessage = "Ime ne sme biti duže od 40 karaktera.")]
    public string Ime { get; set; }

    [Required(ErrorMessage = "Prezime je obavezno.")]
    [StringLength(40, ErrorMessage = "Prezime ne sme biti duže od 40 karaktera.")]
    public string Prezime { get; set; }

    [Required(ErrorMessage = "Email adresa je obavezna.")]
    [EmailAddress(ErrorMessage = "Neispravna email adresa.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Lozinka je obavezna.")]
    [DataType(DataType.Password)]
    public string Lozinka { get; set; }

    [Required(ErrorMessage = "Godine su obavezne.")]
    public int Godine { get; set; }

    [Required(ErrorMessage = "Tip korisnika je obavezan.")]
    [StringLength(40, ErrorMessage = "Tip korisnika ne sme biti duže od 40 karaktera.")]
    public string TipKorisnika { get; set; }
}
