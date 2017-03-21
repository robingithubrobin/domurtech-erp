using System;
using System.Collections.Generic;
using System.Linq;
using DomurTech.ERP.Data.Access.Abstract;
using DomurTech.ERP.Data.Entities.Concrete;
using DomurTech.Installation.Common.DefaultDatas;

namespace DomurTech.Installation.Common.Installlers
{
    public class ApplicationSettingInstaller
    {
        private readonly IRepository<ApplicationSetting> _repositoryApplicationSetting;

        public ApplicationSettingInstaller(IRepository<ApplicationSetting> repositoryApplicationSetting)
        {
            _repositoryApplicationSetting = repositoryApplicationSetting;
        }
        public bool Exists()
        {
            return _repositoryApplicationSetting.Get().Any();
        }
        public ApplicationSetting GetSetting(string settingKey)
        {
            var setting = _repositoryApplicationSetting.Get().FirstOrDefault(x => x.SettingKey == settingKey);
            return setting ?? new ApplicationSetting();
        }

        public List<ApplicationSetting> GetList()
        {
            var result = new List<ApplicationSetting>();
            var list = new ApplicationSettingDatas().Settings;
            for (var i = 0; i < list.Count; i++)
            {
                var displayOrder = i + 1;
                result.Add(new ApplicationSetting
                {
                    Id = Guid.NewGuid(),
                    SettingKey = list[i].SettingKey,
                    SettingValue = list[i].SettingValue,
                    Erasable = false,
                    DisplayOrder = displayOrder,
                    IsApproved = true,
                    CreateDate = DateTime.Now,
                });
            }
            return result;
        }

        public List<ApplicationSettingHistory> GetList(List<ApplicationSetting> list)
        {
            return list.Select(setting => new ApplicationSettingHistory
            {
                Id = Guid.NewGuid(), SettingId = setting.Id, SettingKey = setting.SettingKey, SettingValue = setting.SettingValue, DisplayOrder = setting.DisplayOrder, IsApproved = setting.IsApproved, CreateDate = DateTime.Now, VersionNo = 1, RestoreVersionNo = 0, IsDeleted = false
            }).ToList();
        }


        public void Update(ApplicationSetting setting)
        {
            _repositoryApplicationSetting.Update(setting);
            _repositoryApplicationSetting.SaveChanges();
        }
    }
}