using System;
using DomurTech.ERP.Business.Entities.Models.BaseModels;
using DomurTech.ERP.Business.Managers.Abstract;
using DomurTech.ERP.Data.Access.Abstract;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.ERP.Business.Managers.Concrete
{
    public class LanguageManager : ILanguageManager
    {
        private readonly IRepository<Language> _repositoryLanguage;

        public LanguageManager(IRepository<Language> repositoryLanguage)
        {
            _repositoryLanguage = repositoryLanguage;
        }

        public AddModel<Language> Add()
        {
            throw new NotImplementedException();
        }

        public void Add(AddModel<Language> model)
        {
            throw new NotImplementedException();
        }

        public UpdateModel<Language> Update(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(UpdateModel<Language> model)
        {
            throw new NotImplementedException();
        }

        public DetailModel<Language> Detail(Guid id)
        {
            throw new NotImplementedException();
        }

        public ListModel<Language> GetList(ListModel<Language> model)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
