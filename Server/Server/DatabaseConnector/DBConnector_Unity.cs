using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCouch.Contexts;

namespace DatabaseConnector
{
    public class DBConnector_Unity
    {
        //static string databaseName = "CommunityCraft";
        //private Database db;
        //private Document doc;

        //public DBConnector(string playerName)
        //{
        //    db = Manager.SharedInstance.GetDatabase(databaseName);
        //    doc = db.GetDocument("player_" + playerName);
        //}

        //public object GetObject(string keyName)
        //{
        //    if (doc != null && doc.UserProperties.ContainsKey("high_score"))
        //    {
        //        return doc.UserProperties["high_score"];
        //    }
        //    return null;
        //}

        //public void SetObject(string key, object value)
        //{
        //    doc.Update(rev => {
        //        var props = rev.UserProperties;
        //        props[key] = value;
        //        rev.SetUserProperties(props);
        //        return true;
        //    });
        //}
    }
}
