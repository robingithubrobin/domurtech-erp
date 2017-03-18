using System.Collections.Generic;

namespace DomurTech.Messaging.Entities
{
    public class EmailRow
    {
        private List<EmailKey> _emailKeys;
        public List<EmailKey> EmailKeys
        {
            get
            {
                if (_emailKeys != null) return _emailKeys;
                _emailKeys = new List<EmailKey>();
                return _emailKeys;
            }
            set { _emailKeys = value; }
        }
    }
}
