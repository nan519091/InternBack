using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternExample.Entity
{
    public class UserItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [ForeignKey(nameof(Users))]
        public int UserId { get; set; }
        
        [ForeignKey(nameof(Items))]
        public int ItemId { get; set; }

       
        public User Users { get; set; }
        public Item Items { get; set; }

    }
}
