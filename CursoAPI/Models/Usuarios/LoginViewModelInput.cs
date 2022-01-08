using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace curso.api.Models.Usuarios
{
    public class LoginViewModelInput
    {
        [Required(ErrorMessage = "O Ligin é obrigatório")]
        public string login { get; set; }

        [Required(ErrorMessage = "A senha é obrigatório")]
        public string senha { get; set; }
    }
}
