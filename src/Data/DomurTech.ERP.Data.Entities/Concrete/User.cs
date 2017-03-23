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

        public virtual ICollection<City> CitiesCreatedBy { get; set; }

        public virtual ICollection<CityLanguageLine> CityLanguageLinesCreatedBy { get; set; }

        public virtual ICollection<Country> CountriesCreatedBy { get; set; }

        public virtual ICollection<CountryLanguageLine> CountryLanguageLinesCreatedBy { get; set; }
        public virtual ICollection<Department> DepartmentsCreatedBy { get; set; }
        public virtual ICollection<District> DistrictsCreatedBy { get; set; }

        public virtual ICollection<DistrictLanguageLine> DistrictLanguageLinesCreatedBy { get; set; }
        public virtual ICollection<Organization> OrganizationsCreatedBy { get; set; }
        public virtual ICollection<Person> PersonsCreatedBy { get; set; }
        public virtual ICollection<Role> RolesCreatedBy { get; set; }

        public virtual ICollection<RoleLanguageLine> RoleLanguageLinesCreatedBy { get; set; }
        public virtual ICollection<RoleUserLine> RoleUserLines { get; set; }
        public virtual ICollection<RoleUserLine> RoleUserLinesCreatedBy { get; set; }
        public virtual ICollection<Session> SessionsCreatedBy { get; set; }
        public virtual ICollection<SessionHistory> SessionHistoriesCreatedBy { get; set; }
        public virtual ICollection<ApplicationSetting> ApplicationSettingsCreatedBy { get; set; }
        public virtual ICollection<User> UsersCreatedBy { get; set; }
       
    }
}
