using DarAlforsan.Data.Core;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Core
{
    public class UnitOfWork : IUnitOfWork
    {
        internal DBContext Context;
        //-----------------------------------------------------------------------------------------
        internal Repository.IAboutSchool AboutSchool { set; get; }
        internal Repository.IContactUs ContactUs { set; get; }
        internal Repository.INews News { set; get; }
        internal Repository.ISaidAboutUs SaidAboutUs { set; get; }
        internal Repository.ISchoolFruits SchoolFruits { set; get; }
        internal Repository.ISchoolFruitsDetails SchoolFruitsDetails { set; get; }
        internal Repository.ISchoolSystem SchoolSystem { set; get; }
        internal Repository.ISchoolVision SchoolVision { set; get; }
        internal Repository.ISlider Slider { set; get; }
        internal Repository.ISocialMedia SocialMedia { set; get; }
        internal Repository.IStatistics Statistics { set; get; }
        //-----------------------------------------------------------------------------------------
        internal Repository.IResource Resources { set; get; }
        internal Repository.IResourceModule ResourceModules { set; get; }
        internal Repository.IUILanguage UILanguages { set; get; }
        internal Repository.IUser Users { set; get; }
        //-----------------------------------------------------------------------------------------
        public UnitOfWork()
        {
            Context = new DBContext();
            //-----------------------------------------------------------------------------------------
            AboutSchool = new Repository.AboutSchool(Context);
            ContactUs = new Repository.ContactUs(Context);
            News = new Repository.News(Context);
            SaidAboutUs = new Repository.SaidAboutUs(Context);
            SchoolFruits = new Repository.SchoolFruits(Context);
            SchoolFruitsDetails = new Repository.SchoolFruitsDetails(Context);
            SchoolSystem = new Repository.SchoolSystem(Context);
            SchoolVision = new Repository.SchoolVision(Context);
            Slider = new Repository.Slider(Context);
            SocialMedia = new Repository.SocialMedia(Context);
            Statistics = new Repository.Statistics(Context);
            //-----------------------------------------------------------------------------------------
            Resources = new Repository.Resource(Context);
            ResourceModules = new Repository.ResourceModule(Context);
            UILanguages = new Repository.UILanguage(Context);
            Users = new Repository.User(Context);
        }
        //-----------------------------------------------------------------------------------------
        internal int Commit()
        {
            int rows = Context.SaveChanges();
            return rows;
        }
        //-----------------------------------------------------------------------------------------
        public void Dispose()
        {
            if (Context != null)
            {
                Context.Database.CloseConnection();
                Context.Dispose();
            }
        }
        //-----------------------------------------------------------------------------------------
    }
}