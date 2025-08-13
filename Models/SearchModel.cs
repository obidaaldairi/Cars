using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class SearchModel
    {
        [Required]
        public int MakeId { get; set; }
        public List<CarMake> Makes { get; set; }
        public List<int> Years { get; set; }
        [Required]
        public int YearID { get; set; }
    }
}
