﻿using System.Collections.Generic;
using System.Threading;
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
        
       
        public List<string> Set()
        {
            var result = new List<string>();
            var thread = new Thread(() =>
            {
                var list = new ActionDatas().Actions;
                var displayOrder=1;
                var totalCount = list.Count;

                foreach (var t in list)
                {
                    _repositoryAction.Add(new Action
                    {
                        Id = System.Guid.NewGuid(),
                        ActionName = t.ActionName,
                        ControllerName = t.ControllerName
                    });
                    result.Add("İşlem " + displayOrder + " / " + totalCount + " " + t.ActionName+" " + t.ControllerName);
                    displayOrder++;
                }
                _repositoryAction.SaveChanges();

            });
            thread.Start();
            return result;



            
        }
    }
}