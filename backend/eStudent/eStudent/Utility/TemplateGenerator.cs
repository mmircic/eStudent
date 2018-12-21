using eStudent.Models;
using System.Text;

namespace eStudent.Utility
{
    public class TemplateGenerator
    {
        public static string GetHtmlString(UserCourse userCourse)
        {
            var sb = new StringBuilder();




            sb.AppendFormat(@"<html >
	                        <head>
                                <meta charset='utf-8'>
                            </head>
	                        <body>
                                <h2 align='center'>Izvješće o upisu</h2>
                                <div style='padding-top:30px; padding-left:100px;'>
                                    <h3>Student: </h3><br>
				                    <span><b>OIB: </b>{0}</span><br>
				                    <span><b>Ime: </b>{1}</span><br>
				                    <span><b>Prezime: </b>{2}</span><br>
				                    <span><b>Datum rođenja: </b>{3}</span><br><br>

			                        <span><b>Naziv studija: </b>{4}</span><br><br>
			                        <h3>Popis predmeta:</h3><br>
		
		                            <table>
                                        <tr>
                                            <th> Naziv predmeta </td>
                                            <th> ECTS bodovi </td>
                                        </tr> ", userCourse.User.OIB, userCourse.User.FirstName, userCourse.User.LastName, userCourse.User.BirthDate.ToString("dd.MM.yyyy "), userCourse.Course.Name);

            foreach (var subject in userCourse.Course.SubjectCourses)
            {
                sb.AppendFormat(@"<tr>
				                    <td>{0}</td>
				                    <td>{1}</td>
			                    </tr>", subject.Subject.Name, subject.Subject.ECTSPoints);
            }

            sb.Append(@"</table>
                    </div>
		        </body>
            </html>");

            return sb.ToString();

        }
    }
}
