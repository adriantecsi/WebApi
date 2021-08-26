using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{

    public class Category
    {

        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Este campo é Obrigatório")]
        [MaxLength(60, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter no minimo 3 caracteres")]
        public string Title { get; set; }
    }

}


