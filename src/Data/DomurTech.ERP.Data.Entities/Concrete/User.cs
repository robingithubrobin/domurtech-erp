using System;
using System.Collections.Generic;
using DomurTech.Core.Abstract;

namespace DomurTech.ERP.Data.Entities.Concrete
{
    public class User : IEntity
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreateDate { get; set; }
        public Person Person { get; set; }
        public virtual Language Language { get; set; }
        public string FullName => Person.FirstName + " " + Person.LastName;
        public string DisplayName { get; set; }
        public string ConfirmPassword { get; set; }
        public string OldPassword { get; set; }

        public virtual IList<City> CitiesCreatedBy { get; set; }

        public virtual IList<CityLanguageLine> CityLanguageLinesCreatedBy { get; set; }

        public virtual IList<Country> CountriesCreatedBy { get; set; }

        public virtual IList<CountryLanguageLine> CountryLanguageLinesCreatedBy { get; set; }
        public virtual IList<Department> DepartmentsCreatedBy { get; set; }
        public virtual IList<District> DistrictsCreatedBy { get; set; }

        public virtual IList<DistrictLanguageLine> DistrictLanguageLinesCreatedBy { get; set; }
        public virtual IList<Organization> OrganizationsCreatedBy { get; set; }
        public virtual IList<Person> PersonsCreatedBy { get; set; }
        public virtual IList<Role> RolesCreatedBy { get; set; }

        public virtual IList<RoleLanguageLine> RoleLanguageLinesCreatedBy { get; set; }
        public virtual IList<RoleUserLine> RoleUserLines { get; set; }
        public virtual IList<RoleUserLine> RoleUserLinesCreatedBy { get; set; }
        public virtual IList<Session> SessionsCreatedBy { get; set; }
        public virtual IList<SessionHistory> SessionHistoriesCreatedBy { get; set; }
        public virtual IList<ApplicationSetting> ApplicationSettingsCreatedBy { get; set; }
        public virtual IList<User> UsersCreatedBy { get; set; }
       
    }
}
