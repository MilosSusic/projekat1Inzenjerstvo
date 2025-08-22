using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pr14_2022.MODELS
{   


    public enum UserRole { Visitor,Admin}

    [Serializable]
    public class User
    {    
        public string ime { get; set; }

        public string lozinka { get; set; }

        public UserRole role { get; set; }


        public User() { }
      

        public User(string ime,UserRole role,string lozinka)
        {
            this.ime = ime;
            this.role = role;
            this.lozinka = lozinka;
        }
    }
}
