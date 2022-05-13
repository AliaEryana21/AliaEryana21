using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentLibrary
{
	public class Student
	{
		public string Course { get; set; }
		public string Semester { get; set; }
		public int num1 { get; set; }
		public int num2 { get; set; }
		public int num3 { get; set; }
		public int avg
		{
			get { return _avg; }
			set { _avg = value; }
		}
		public string grade
		{
			get
			{
				MessageNotification();
				return _grade;
			}
			set
			{
				this._grade = value;
			}
		}

		private int _avg { get; set; }
		private string _grade { get; set; }

		//************************************************************
		//Create event

		public event EventHandler<NotificationEventArgs> EventHandler;

		private void MessageNotification()
		{
			if (avg > 90)
			{
				NotificationEventArgs args = new NotificationEventArgs();
				Console.WriteLine("Congrats for having A+! You deserved a present!");
				args.Category = "Notification";
				OnNotification(args);
			}
			else if (avg <= 30)
			{
				NotificationEventArgs args = new NotificationEventArgs();
				Console.WriteLine("Very bad! Need to do a lot of assessment from now on...");
				args.Category = "Notification";
				OnNotification(args);
			}
		}

		protected virtual void OnNotification(NotificationEventArgs NotificationArg)
		{
			EventHandler<NotificationEventArgs> handle = EventHandler;

			if (handle != null)
			{
				handle(this, NotificationArg);
			}
		}

		public class NotificationEventArgs
		{
			public string MessageEvent { get; set; }
			public string Category { get; set; }
		}

	}
}
