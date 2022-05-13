using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleApp.Program.Student;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
			char _ch;
			User user;

			List<User> userList = new List<User>();

			//read from json file
			StreamReader srUserList = new StreamReader(@"C:\Users\HP i5\Desktop\MPU-ICT02\STUDENT\IR4STUDENT\ConsoleApp\tmp\UserList.json");
			string jsonObj = srUserList.ReadToEnd();

			userList = JsonConvert.DeserializeObject<List<User>>(jsonObj);
			srUserList.Close();
			if (userList == null)
				userList = new List<User>();

			//int num1, num2, num3, avg = 0;
			//string grade;

			Console.WriteLine("Welcome to IR 4.0 Session 2\n");

			Console.WriteLine("*****************************************************");
			Console.WriteLine("- - - Student marks calculation program in C# - - -\n");

			Console.WriteLine("Press 'x' to exit or any key to continue...\n");

			while ((_ch = Console.ReadKey(true).KeyChar) != 'x')
			{
				user = new User();

				Console.WriteLine("Full Name: ");
				user.Name = Console.ReadLine();

				Console.WriteLine("Class: ");
				user.Class = Console.ReadLine();

				Console.WriteLine("Student Course: ");
				user.student.Course = Console.ReadLine();

				Console.WriteLine("Student Current Semester: ");
				user.student.Semester = Console.ReadLine();

				Console.Write("Enter Physics mark: \n");
				user.student.num1 = Convert.ToInt32(Console.ReadLine());

				Console.WriteLine("Enter Computer Science mark: ");
				user.student.num2 = Convert.ToInt32(Console.ReadLine());

				Console.WriteLine("Enter English mark: ");
				user.student.num3 = Convert.ToInt32(Console.ReadLine());

				if (user.student.num1 > 100 || user.student.num2 > 100 || user.student.num3 > 100) user.student.grade = "Error";
				else if (user.student.num1 < 0 || user.student.num2 < 0 || user.student.num3 < 0) user.student.grade = "Error";
				else
				{
					user.student.avg = Convert.ToInt32((user.student.num1 + user.student.num2 + user.student.num3) / 3);

					if (user.student.avg > 100) user.student.grade = "Error";
					else if (user.student.avg < 0) user.student.grade = "Error";
					else if (user.student.avg > 90) user.student.grade = "A+";
					else if (user.student.avg > 70) user.student.grade = "B+";
					else if (user.student.avg > 50) user.student.grade = "C+";
					else if (user.student.avg > 30) user.student.grade = "C";
					else user.student.grade = "F";
				}

				System.Console.Write("\nThe average and grade: {0}\t{1}\t\n", user.student.avg, user.student.grade);
				Console.WriteLine("*****************************************************");

				user.student.EventHandler += OnNotification;
				userList.Add(user);

				Console.WriteLine("Press 'x' to exit or any key to continue...");
			}
		 
			foreach (User usr in userList)
			{
				Console.WriteLine("*********************************************************");
				Console.WriteLine($"Name: { usr.Name}");
				Console.WriteLine($"Class: { usr.Class}");
				Console.WriteLine($"Student Course Name: {usr.student.Course}");
				Console.WriteLine($"Student Current Semester: {usr.student.Semester}");
				Console.WriteLine($"Physics mark: {usr.student.num1}");
				Console.WriteLine($"Science Computer mark: {usr.student.num2}");
				Console.WriteLine($"English mark: {usr.student.num3}");
				Console.WriteLine($"Average mark: {usr.student.avg}");
				Console.WriteLine($"Full grade: {usr.student.grade}");
				Console.WriteLine("*********************************************************");

			}

			// Write to JSON file
			jsonObj = JsonConvert.SerializeObject(userList, Formatting.Indented);
			File.WriteAllText(@"C:\Users\HP i5\Desktop\MPU-ICT02\STUDENT\IR4STUDENT\ConsoleApp\tmp\UserList.json", jsonObj);

			jsonObj = JsonConvert.SerializeObject(userList[0], Formatting.Indented);
			File.WriteAllText("singleUser.json", jsonObj);

			Console.ReadKey();
		}

		public static void OnNotification(object sender, NotificationEventArgs e)
		{
			if (e.Category == "Notification")
			{

			}
			Console.WriteLine(e.MessageEvent);
		}

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

		public class Student
		{
            public string Course { get; set; }
			public string Semester	{ get; set; }
			public int num1 { get; set; }
			public int num2 { get; set; }
			public int num3 { get; set; }
			public int avg 
			{ 
				get { return _avg;  }
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

}
