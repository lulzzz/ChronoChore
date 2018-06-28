using System.Collections.Generic;
using OpenSourceAPIData.Persistence.Logic;

namespace OpenSourceAPIData.WorldBankData.Model
{
    public class BaseTable<T>
    {
        public virtual string TableName
        {
            get
            {
                return GetType().Name.Substring(0, GetType().Name.Length - 5);
            }
        }

        public virtual void Create(PersistenceManager persistenceManager)
        {

        }

        public virtual void Save(IEnumerable<T> resultSet, PersistenceManager persistenceManager) { }
        public virtual void Save(T result, PersistenceManager persistenceManager) { }

        public virtual string NormalizeText(string text)
        {
            return (text == null)? null:text.Replace("'", "''");
        }


    }
}
