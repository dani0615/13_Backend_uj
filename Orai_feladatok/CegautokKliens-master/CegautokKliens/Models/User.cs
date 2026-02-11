using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CegautokKliens.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string LoginName { get; set; } = null!;

        public bool Active { get; set; }

        public string Address { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Hash { get; set; } = null!;

        public string Salt { get; set; } = null!;

        public string? Phone { get; set; }

        public string Image { get; set; } = null!;

        public int Permission { get; set; }

    }
}
