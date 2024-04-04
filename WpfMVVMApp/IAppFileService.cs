using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace WpfMVVMApp
{
    public interface IAppFileService
    {
        IEnumerable<Employee> Open(string fileName);
        void Save(string fileName, IEnumerable<Employee> employees);
    }

    public class JsonAppFileService : IAppFileService
    {
        public IEnumerable<Employee> Open(string fileName)
        {
            List<Employee> employees = new List<Employee>();
            DataContractJsonSerializer jsonSerializer = 
                new DataContractJsonSerializer(typeof(List<Employee>));
            
            using(FileStream stream = new(fileName, FileMode.Open))
            {
                employees = (List<Employee>)jsonSerializer.ReadObject(stream);
            }
            return employees;
        }

        public void Save(string fileName, IEnumerable<Employee> employees)
        {
            DataContractJsonSerializer jsonSerializer =
                new DataContractJsonSerializer(typeof(List<Employee>));
            using(FileStream stream = new FileStream(fileName, FileMode.Create))
            {
                jsonSerializer.WriteObject(stream, employees);
            }
        }
    }
}
