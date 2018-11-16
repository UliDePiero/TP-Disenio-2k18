using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class Mongo
    {
        private static MongoClient mongo;
        private Mongo() { }
        public static MongoClient getInstance()
        {
            if(mongo == null)
            {
                mongo = new MongoClient("mongodb://user1:user1@ds062097.mlab.com:62097/dbtp0");
                return mongo;
            }
            else {return mongo;}
        }
    }
}