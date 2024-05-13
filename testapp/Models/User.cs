using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace testapp.Models
{
	public class User
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[DisplayName("USER NAME")]
		public string UserName { get; set; }
		[Required]
        [DisplayName("USER NAME")]
        public string Gender { get; set; }
		[Required]
		[DisplayName("DATE OF BIRTH")]
		public string BirthDay { get; set; }
		[Required]
        [DisplayName("STATUS")]
        public bool Status { get; set; }
		[Required]
        [DisplayName("ADDRESS")]
        public string Address { get; set; }
        [DisplayName("CREATE DATE")]
        public DateTime CreatDateTime { get; set; } = DateTime.Now;
	}
}
