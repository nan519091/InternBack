using System.ComponentModel.DataAnnotations;// ใช้Annotations ในการระบุ กำหนดว่า ตัวนี้ๆๆต้องใส่ค่า ความยาวstring เท่าไหร่ ตัวไหนเป็น PK FK
using System.ComponentModel.DataAnnotations.Schema;//.ใช้กำหนดวิธีการmapping จากโค้ด->ฐานข้อมูล

namespace InternExample.Entity
{
    public class Item
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
