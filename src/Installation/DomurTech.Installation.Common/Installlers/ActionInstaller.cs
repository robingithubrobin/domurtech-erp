﻿using System.Collections.Generic;
using System.Linq;
using DomurTech.ERP.Data.Access.Abstract;
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

        public IQueryable<Action> GetAllActions()
        {
            return _repositoryAction.Get();
        }

        public Action AddAction(Action action)
        {
            return _repositoryAction.Add(action);
        }

        public List<Action> GetActionList()
        {
            var list = new ActionDatas().Actions;
            return list.Select(t => new Action
            {
                Id = System.Guid.NewGuid(),
                ActionName = t.ActionName,
                ControllerName = t.ControllerName
            }).ToList();
        }

        
    }
}