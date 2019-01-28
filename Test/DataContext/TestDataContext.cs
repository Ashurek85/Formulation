using System.Collections.Generic;

namespace Test.DataContext
{
    public class TestDataContext
    {

        private static TestDataContext instance;
        public static TestDataContext Instance
        {
            get
            {
                if (instance == null)
                    instance = new TestDataContext()
                    {
                        Department = "IT",
                        Boss = new Person()
                        {
                            Id = 1,
                            Name = "Wyatt",
                            FirstName = "Horton",
                            Email = "wyatt.horton@itcompany.com",
                            Phone = "655938124",
                            RemainingVacation = 8,
                            Salary = 75000D,
                            IsMarried = true,
                            InActive = true,
                        },
                        Subordinates = new Person[]
                        {
                            new Person()
                            {
                                Id = 2,
                                Name = "Craig",
                                FirstName = "McGrath",
                                Email = "craig.mcgrath@itcompany.com",
                                Phone = "659871141",
                                RemainingVacation = 15,
                                Salary = 42500D,
                                IsMarried = true,
                                InActive = true,                                    
                            },
                            new Person()
                            {
                                Id = 3,
                                Name = "Bennett",
                                FirstName = "Levesque",
                                Email = "bennett.levesque@itcompany.com",
                                Phone = "632398746",
                                RemainingVacation = 10,
                                Salary = 50100D,
                                IsMarried = false,
                                InActive = true,
                            },
                            new Person()
                            {
                                Id = 4,
                                Name = "Abraham",
                                FirstName = "Bragg",
                                Email = "abraham.bragg@itcompany.com",
                                Phone = "699857743",
                                RemainingVacation = 21,
                                Salary = 30000D,
                                IsMarried = true,
                                InActive = false,
                            },
                        },
                    };
                return instance;
            }
        }

        public string Department { get; set; }
        public Person Boss { get; set; }
        public IEnumerable<Person> Subordinates { get; set; }

    }
}
