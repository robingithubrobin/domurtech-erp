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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual User UpdatedBy { get; set; }
        public virtual Language Language { get; set; }
        public string FullName => FirstName + " " + LastName;
        public string DisplayName { get; set; }
        public string ConfirmPassword { get; set; }
        public string OldPassword { get; set; }

        public virtual IList<City> CitiesCreatedBy { get; set; }
        public virtual IList<City> CitiesUpdatedBy { get; set; }

        public virtual IList<CityLanguageLine> CityLanguageLinesCreatedBy { get; set; }
        public virtual IList<CityLanguageLine> CityLanguageLinesUpdatedBy { get; set; }

        public virtual IList<Country> CountriesCreatedBy { get; set; }
        public virtual IList<Country> CountriesUpdatedBy { get; set; }

        public virtual IList<CountryLanguageLine> CountryLanguageLinesCreatedBy { get; set; }
        public virtual IList<CountryLanguageLine> CountryLanguageLinesUpdatedBy { get; set; }
        public virtual IList<Department> DepartmentsCreatedBy { get; set; }
        public virtual IList<Department> DepartmentsUpdatedBy { get; set; }
        public virtual IList<District> DistrictsCreatedBy { get; set; }
        public virtual IList<District> DistrictsUpdatedBy { get; set; }

        public virtual IList<DistrictLanguageLine> DistrictLanguageLinesCreatedBy { get; set; }
        public virtual IList<DistrictLanguageLine> DistrictLanguageLinesUpdatedBy { get; set; }
        public virtual IList<Organization> OrganizationsCreatedBy { get; set; }
        public virtual IList<Organization> OrganizationsUpdatedBy { get; set; }
        public virtual IList<Person> PersonsCreatedBy { get; set; }
        public virtual IList<Person> PersonsUpdatedBy { get; set; }
        public virtual IList<Role> RolesCreatedBy { get; set; }
        public virtual IList<Role> RolesUpdatedBy { get; set; }

        public virtual IList<RoleLanguageLine> RoleLanguageLinesCreatedBy { get; set; }
        public virtual IList<RoleLanguageLine> RoleLanguageLinesUpdatedBy { get; set; }
        public virtual IList<RoleUserLine> RoleUserLines { get; set; }
        public virtual IList<RoleUserLine> RoleUserLinesCreatedBy { get; set; }
        public virtual IList<RoleUserLine> RoleUserLinesUpdatedBy { get; set; }
        public virtual IList<Session> SessionsCreatedBy { get; set; }
        public virtual IList<SessionHistory> SessionHistoriesCreatedBy { get; set; }
        public virtual IList<Setting> SettingsCreatedBy { get; set; }
        public virtual IList<Setting> SettingsUpdatedBy { get; set; }
        public virtual IList<User> UsersCreatedBy { get; set; }
        public virtual IList<User> UsersUpdatedBy { get; set; }
       
    }
}
