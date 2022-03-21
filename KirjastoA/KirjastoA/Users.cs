using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KirjastoA
{
    public class User
    {
        /// <summary>
        /// Tietokannassa käyttäjällä on uniikki ID-numero.
        /// </summary>
        public int Id { get; set; } = 0;

        public string Username { get; set; } = "";

        public string UserEmail { get; set; } = "";

        public string UserPassword { get; set; } = "";

    }
}
