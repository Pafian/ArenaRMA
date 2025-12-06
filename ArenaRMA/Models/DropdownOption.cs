using System.ComponentModel.DataAnnotations;

namespace ArenaRMA.Models
{
    public class DropdownOption
    {
        [Key]
        public int OptionID { get; set; }

        public int DropdownID { get; set; }
        public string OptionValue { get; set; }
        public bool Enabled { get; set; }
        public bool EditableByAdmin { get; set; }

        public Dropdown Dropdown { get; set; }
    }
}
