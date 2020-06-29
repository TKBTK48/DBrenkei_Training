using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMSystem_CUI.Dtos
{
	public class EmployeeDto
    {
		/** 社員ID */
		public int IdEmployee { get; set; }
		/** 社員名 */
		public string NmEmployee { get; set; }
		/** 社員名カナ */
		public string KnEmployee { get; set; }
		/** メールアドレス */
		public string MailAddress { get; set; }
		/** パスワード */
		public string Password { get; set; }
		/** 役職ID */
		public int IdDepartment { get; set; }
	}
}
