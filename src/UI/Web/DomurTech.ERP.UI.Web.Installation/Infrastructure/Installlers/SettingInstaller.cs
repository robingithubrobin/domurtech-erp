using System;
using System.Linq;
using DomurTech.ERP.Data.Access.Abstract;
using DomurTech.ERP.Data.Entities.Concrete;
using DomurTech.ERP.UI.Web.Installation.Infrastructure.DefaultDatas;

namespace DomurTech.ERP.UI.Web.Installation.Infrastructure.Installlers
{
    internal class SettingInstaller
    {
        private readonly IRepository<Setting> _repositorySetting;
        private readonly IRepository<SettingHistory> _repositorySettingHistory;
        private readonly IRepository<User> _repositoryUser;

        public SettingInstaller(IRepository<Setting> repositorySetting, IRepository<SettingHistory> repositorySettingHistory, IRepository<User> repositoryUser)
        {
            _repositorySetting = repositorySetting;
            _repositorySettingHistory = repositorySettingHistory;
            _repositoryUser = repositoryUser;

        }

        public Setting GetSetting(string settingKey)
        {
            var setting = _repositorySetting.Get().FirstOrDefault(x => x.SettingKey == settingKey);
            return setting ?? new Setting();
        }

        public void Set()
        {
            var list = new SettingDatas().Settings;
            for (var i = 0; i < list.Count; i++)
            {
                _repositorySetting.Add(new Setting
                {
                    Id = Guid.NewGuid(),
                    SettingKey = list[i].SettingKey,
                    SettingValue = list[i].SettingValue,
                    Erasable = false,
                    DisplayOrder = i + 1,
                    IsApproved = true,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    CreatedBy = _repositoryUser.Get().FirstOrDefault(x => x.DisplayOrder == 1),
                    UpdatedBy = _repositoryUser.Get().FirstOrDefault(x => x.DisplayOrder == 1)
                });
            }
            _repositorySetting.SaveChanges();


        }

        public void SetHistory()
        {
            foreach (var setting in _repositorySetting.Get().ToList())
            {
                _repositorySettingHistory.Add(new SettingHistory
                {
                    Id = Guid.NewGuid(),
                    SettingId = setting.Id,
                    SettingKey = setting.SettingKey,
                    SettingValue = setting.SettingValue,
                    DisplayOrder = setting.DisplayOrder,
                    IsApproved = setting.IsApproved,
                    CreateDate = DateTime.Now,
                    CreatedBy = setting.CreatedBy.Id,
                    VersionNo = 1,
                    RestoreVersionNo = 0,
                    IsDeleted = false
                });
            }
            _repositorySettingHistory.SaveChanges();
        }

        public void Update(Setting setting)
        {
            _repositorySetting.Update(setting);
            _repositorySetting.SaveChanges();
        }
    }
}