using Elasticsearch.Net;
using Nest;
using System;

namespace elastic_con
{
    class Program
    {
        static void Main(string[] args)
        {
            var node = new Uri("http://localhost:9200"); //http://localhost:9200/my-application/person/3
            var settings = new ConnectionSettings(node).DefaultIndex("my-application");
            var client = new ElasticClient(settings);


            var employee1 = new Person { Id = "1", Firstname = "Martin", Lastname = "Lacos", Date = DateTime.Now };
            var employee_index1 = client.Index(employee1);
            var employee2 = new Person { Id = "2", Firstname = "Lukas", Lastname = "Skamene", Date = DateTime.Now };
            var employee2_index = client.Index(employee2);
            var employee3 = new Person { Id = "3", Firstname = "Jiri", Lastname = "Zizka", Date = DateTime.Today };
            var employee3_index = client.Index(employee3);

            //
            var searchResults = client.Search<Person>(s => s
                .From(0)
                .Size(10)
                .Query(q => q
                .Term(p => p.Id, "2")
                )
            );
            var searchResults2 = client.Search<Person>(s => s.AllIndices());
            //

            //
            if (searchResults.IsValid)
                foreach (var p in searchResults.Documents)
                    Console.WriteLine("Id: " + p.Id + " | Name: " + p.Firstname + " | Surname: " + p.Lastname + " | Datetime: " + p.Date);
            Console.WriteLine("\n");
            if (searchResults2.IsValid)
                foreach (var x in searchResults2.Documents)
                    Console.WriteLine("Id: " + x.Id + " | Name: " + x.Firstname + " | Surname: " + x.Lastname + " | Datetime: " + x.Date);
            //
            Console.ReadKey();
        }
    }

    public class Person
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Date { get; set; }
    }
}
