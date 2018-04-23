using System;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Reflection;
using DevExpress.Xpf.Grid;
using System.ComponentModel.DataAnnotations;

namespace Model {
    public class ViewModel {
        public List<int> Ids { get; private set; }
        public List<string> Cities { get; private set; }
        public List<string> Titles { get; private set; }
        public IList<Employee> Source { get; private set; }
        public ViewModel() {
            Source = EmployeesData.DataSource;
            List<string> cities = new List<string>();
            foreach(Employee employee in Source) {
                if(!cities.Contains(employee.City))
                    cities.Add(employee.City);
            }
            Cities = cities;
            Titles = new List<string>() { "Mr.", "Ms." };
            List<int> ids = new List<int>();
            foreach(Employee employee in Source) {
                if(!ids.Contains(employee.Id))
                    ids.Add(employee.Id);
            }
            Ids = ids;
        }
    }

    [XmlRoot("Employees")]
    public class EmployeesData : List<Employee> {
        public static IList<Employee> DataSource {
            get {

                XmlSerializer s = new XmlSerializer(typeof(EmployeesData));
#if SILVERLIGHT
                return (List<Employee>)s.Deserialize(Assembly.GetExecutingAssembly().GetManifestResourceStream("SLColumnTemplateSelectorExample.EmployeesWithPhoto.xml"));
#else
                return (List<Employee>)s.Deserialize(Assembly.GetExecutingAssembly().GetManifestResourceStream("ColumnTemplateSelectorExample.EmployeesWithPhoto.xml"));
#endif
            }
        }
    }

    public class Employee {
        [ColumnGeneratorTemplateNameAttribute("IdTemplate")]
        public int Id { get; set; }
        public string FirstName { get; set; }
        [ColumnGeneratorTemplateNameAttribute("CityTemplate")]
        public string City { get; set; }
        public DateTime BirthDate { get; set; }
        [ColumnGeneratorTemplateNameAttribute("TitleTemplate")]
        public string Title { get; set; }
        [ColumnGeneratorTemplateNameAttribute("ImageTemplate"), Display(Name = "Photo")]
        public byte[] ImageData { get; set; }
    }
}
