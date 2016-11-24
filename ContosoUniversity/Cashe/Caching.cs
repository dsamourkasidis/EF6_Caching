using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContosoUniversity.Models;
using ContosoUniversity.DAL;
using System.Web.Caching;
using System.Data;
using System.Data.Entity;

namespace ContosoUniversity.Cashe
{
    public class Caching
    {
      //  private static EntityConnectionStringBuilder entityConnectionString = new 
         //   EntityConnectionStringBuilder(ConfigurationManager.ConnectionStrings["TestdbEntities"].ConnectionString);

        public static IEnumerable<Student> GetStudentData()
        {
            
            IEnumerable<Student> studentData = HttpContext.Current.Cache.Get("student") as 
                IEnumerable<Student>;
            if (studentData == null)
            {
                using (var context = new SchoolContext())
                {
                    //try
                    //   {
                    SqlCacheDependency SqlDep = new SqlCacheDependency("TEST_SAMO", "Student");
                    //   } 
                    //catch (DatabaseNotEnabledForNotificationException exDBDis)
                    //   {                      
                    //    SqlCacheDependencyAdmin.EnableNotifications(System.Configuration.ConfigurationManager.ConnectionStrings["SchoolContext"].ConnectionString);
                    //   }

                    //catch (TableNotEnabledForNotificationException exTabDis)
                    //   {    
                    //         SqlCacheDependencyAdmin.EnableTableForNotifications(System.Configuration.ConfigurationManager.ConnectionStrings["SchoolContext"].ConnectionString, "Student");

                    IQueryable<Student> studentDataCache = context.Students.Include("Enrollments.Course");
                    studentData = studentDataCache.ToList();
                    HttpContext.Current.Cache.Insert("student", studentData, SqlDep, DateTime.Now.AddMinutes(3), Cache.NoSlidingExpiration);
                    //   }
                }
                
            }
            return studentData;
        }

        public static IEnumerable<Course> GetCourseData()
        {
            IEnumerable<Course> courseData = HttpContext.Current.Cache.Get("course") as
                IEnumerable<Course>;
            if (courseData == null)
            {
                using (var context = new SchoolContext())
                {
                    //try
                    //   {
                    SqlCacheDependency SqlDep = new SqlCacheDependency("TEST_SAMO", "Course");
                    //   }
                    //catch (TableNotEnabledForNotificationException exTabDis)
                    //   {
                    //    SqlCacheDependencyAdmin.EnableTableForNotifications(System.Configuration.ConfigurationManager.ConnectionStrings["SchoolContext"].ConnectionString, "Course");
                    //   }
                    //finally
                    //   {
                    IQueryable<Course> courseDataCache = context.Courses;
                    courseData = courseDataCache.ToList();
                    HttpContext.Current.Cache.Insert("course", courseData, SqlDep, DateTime.Now.AddMinutes(1), Cache.NoSlidingExpiration);
                    //   }
                }

            }
            return courseData;

        }

    }
}