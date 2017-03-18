using System.Collections.Generic;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.ERP.UI.Web.Installation.Infrastructure.DefaultDatas
{
    internal class ActionDatas
    {
        private readonly List<string> _actions = new List<string>
        {
            "Add",
            "Update",
            "Detail",
            "Index",
            "Delete"
        };

        private readonly List<string> _controllers = new List<string>
        {
            "Action",
            "City",
            "Country",
            "District",
            "Role",
            "Setting"
        };

        public List<Action> Actions = new List<Action>();

        public ActionDatas()
        {
            foreach (var controller in _controllers)
            {
                foreach (var action in _actions)
                {
                    Actions.Add(new Action
                    {
                        ControllerName = controller,
                        ActionName = action
                    });
                }
            }
        }
    }
}