using System.ComponentModel.DataAnnotations;

public class PrijavaModel
{
    [Required(ErrorMessage = "Unesite email.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Unesite lozinku.")]
    [DataType(DataType.Password)]
    public string Lozinka { get; set; }
}