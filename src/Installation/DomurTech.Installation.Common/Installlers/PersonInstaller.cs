using System.Linq;
using DomurTech.ERP.Data.Access.Abstract;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.Installation.Common.Installlers
{
    public class PersonInstaller
    {
        private readonly IRepository<Person> _repositoryPerson;
        private readonly IRepository<PersonHistory> _repositoryPersonHistory;

        public PersonInstaller(IRepository<Person> repositoryPerson, IRepository<PersonHistory> repositoryPersonHistory)
        {
            _repositoryPerson = repositoryPerson;
            _repositoryPersonHistory = repositoryPersonHistory;
        }
    
        public Person GetFirst()
        {
            return _repositoryPerson.Get().FirstOrDefault(x => x.DisplayOrder == 1);
        }
        public Person Add(Person person)
        {
            var result = _repositoryPerson.Add(person);
            _repositoryPerson.SaveChanges();
            return result;
        }

        public void Add(PersonHistory person)
        {
            _repositoryPersonHistory.Add(person);
            _repositoryPersonHistory.SaveChanges();
        }

        public bool Exists()
        {
            return _repositoryPerson.Get().Any();
        }
    }
}