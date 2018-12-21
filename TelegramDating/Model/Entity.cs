using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TelegramDating.Model
{
    public class Entity
    {
        [Key]
        [Column(Order = 0)]
        public long Id { get; set; }
    }
}