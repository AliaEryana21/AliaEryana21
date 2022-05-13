using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentLibrary
{
	public class User
	{
		public User()
		{
			student = new Student();
			_name = "dummy name";
		}

		private string _name;
		public string Name
		{
			set { _name = value; }
			get { return _name; }
		}
		public string Class { get; set; }
		public Student student { get; set; }

	}

}
