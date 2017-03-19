﻿using DomurTech.ERP.Data.Access.Abstract;
using DomurTech.ERP.Data.Entities.Concrete;
using DomurTech.Installation.Common.DefaultDatas;

namespace DomurTech.Installation.Common.Installlers
{
    public class ActionInstaller
    {
        private readonly IRepository<Action> _repositoryAction;

        public ActionInstaller(IRepository<Action> repositoryAction)
        {
            _repositoryAction = repositoryAction;
        }
        
       
        public void Set()
        {
            var list = new ActionDatas().Actions;

            foreach (var t in list)
            {
                _repositoryAction.Add(new Action
                {
                    Id = System.Guid.NewGuid(),
                    ActionName = t.ActionName,
                    ControllerName = t.ControllerName
                });
            }
            _repositoryAction.SaveChanges();
        }
    }
}