using System.ComponentModel.DataAnnotations;

namespace ArenaRMA.Models
{
    public class Dropdown
    {
        [Key]
        public int DropdownID { get; set; }

        public string DropdownName { get; set; }

        public List<DropdownOption> Options { get; set; }
    }
}
