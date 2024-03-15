using System.ComponentModel.DataAnnotations;

namespace EFCrud.Model
{
    public class TvShow
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public double ImdbRating { get; set; }

        public TvShow()
        {
        }
    }
}
