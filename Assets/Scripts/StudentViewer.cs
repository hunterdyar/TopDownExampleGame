
	using UnityEngine;

	public class StudentViewer
	{
		private void AddStudents()
		{
			Student rose = new Student();
			rose.firstName = "Rose";
			Student malik = new Student();
			malik.firstName = "Malik";
		}
		
		
		public Student currentlyViewingStudent;

		public void DisplayDefaultStudent()
		{
			currentlyViewingStudent = new Student();
		}
		public void PrintNameToConsole()
		{
			Debug.Log(currentlyViewingStudent.lastName+", "+currentlyViewingStudent.firstName);
		}

		public bool CheckIfFirstYear()
		{
			return currentlyViewingStudent.year == 1;
		}
	}
