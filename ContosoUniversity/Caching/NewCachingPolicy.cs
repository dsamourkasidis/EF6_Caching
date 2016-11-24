using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using EFCache;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace ContosoUniversity.Caching
{
    public class NewCachingPolicy : CachingPolicy
    {
        protected override bool CanBeCached(ReadOnlyCollection<EntitySetBase> affectedEntitySets, string sql,
            IEnumerable<KeyValuePair<string, object>> parameters)     
        {
            string can = null;
            XDocument xdoc = XDocument.Load("C:/Users/dimitris/Desktop/ContosoUniversity/ContosoUniversity/App_Data/CachingSettings.xml");
            foreach (var stop in xdoc.Root.Elements("set"))
            {
                string Cached = (string)stop.Element("CanBeCached");               
                string id = (string)stop.Attribute("id");
                foreach (EntitySetBase set in affectedEntitySets)
                {
                    if (id == (set.Name))
                    {
                        if (Cached == "True")
                        {
                            can = "true";
                            
                        }
                        else
                        {
                            can = "false";
                        }

                    }
                }
            }
            if (can == null || can == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void GetExpirationTimeout(ReadOnlyCollection<EntitySetBase> affectedEntitySets,
            out TimeSpan slidingExpiration, out DateTimeOffset absoluteExpiration)
        {
            
            double time = 0;
            string period = null;
            XDocument xdoc = XDocument.Load("C:/Users/dimitris/Desktop/ContosoUniversity/ContosoUniversity/App_Data/CachingObjects.xml");
            foreach (var obj in xdoc.Root.Elements("set"))
            {               
                string ChangeFreq = (string)obj.Element("DataChangeFreq");
                string id = (string)obj.Attribute("id");
                foreach (EntitySetBase set in affectedEntitySets)
                {
                    if (id == (set.Name))
                    {
                         XDocument xdoc2 = XDocument.Load("C:/Users/dimitris/Desktop/ContosoUniversity/ContosoUniversity/App_Data/CachingSettings.xml");
                         foreach (var sett in xdoc2.Root.Elements("Frequency"))
                         {
                             string Name = (string)sett.Element("name");
                             double Time = (double)sett.Element("time");
                             string Period = (string)sett.Element("period");
                             if (ChangeFreq == Name)
                             {
                                 time = Time;
                                 period = Period;
                             }
                         }
                    }
                }
            }
            if (period == "seconds")
            {
                absoluteExpiration = DateTimeOffset.Now.AddSeconds(time);
            }
            else
            {
                absoluteExpiration = DateTimeOffset.Now.AddMinutes(time);
            }
            
            slidingExpiration = TimeSpan.MaxValue;
        }

    }
}