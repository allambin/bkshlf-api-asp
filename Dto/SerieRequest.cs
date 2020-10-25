using System.ComponentModel.DataAnnotations;

namespace BKSHLF.Dto
{
    public class SerieRequest
    {
        [Required]
        public string Name { get; set; }
    }
}