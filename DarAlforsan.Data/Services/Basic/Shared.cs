using Core;

namespace Services
{
    public class Shared : Service<UnitOfWork>
    {
        public ViewModels.Form.HomeVM GetHomeData()
        {
            return new ViewModels.Form.HomeVM
            {
                AboutSchool = unitOfWork.AboutSchool.Select(a => new ViewModels.Form.AboutSchool
                {
                    AddressAr = a.AddressAr,
                    AddressEn = a.AddressEn,
                    DetailsAr = a.DetailsAr,
                    DetailsEn = a.DetailsEn,
                    Logo = a.Logo
                }).ToList(),
                SchoolSystem = unitOfWork.SchoolSystem.Select(a => new ViewModels.Form.SchoolSystem
                {
                    TitleAr = a.TitleAr,
                    TitleEn = a.TitleEn,
                    MessageAr = a.MessageAr,
                    MessageEn = a.MessageEn
                }).ToList(),
                Statistics = unitOfWork.Statistics.Select(a => new ViewModels.Form.Statistics
                {
                    TitleAr = a.TitleAr,
                    TitleEn = a.TitleEn,
                    Count = a.Count,
                    Img = a.Img
                }).ToList(),
                News = unitOfWork.News.Select(a => new ViewModels.Form.News
                {
                    ID = a.ID,
                    TitleAr = a.TitleAr,
                    TitleEn = a.TitleEn,
                    DetailsAr = a.DetailsAr,
                    DetailsEn = a.DetailsEn,
                    AddDate = a.AddDate,
                    ViewsCount = a.ViewsCount,
                    MainImg = a.MainImg
                }).ToList(),
                SaidAboutUs = unitOfWork.SaidAboutUs.Select(a => new ViewModels.Form.SaidAboutUs
                {
                    Name = a.Name,
                    MessageAr = a.MessageAr,
                    MessageEn = a.MessageEn,
                    Img = a.Img
                }).ToList(),
                Sliders = unitOfWork.Slider.Select(a => new ViewModels.Form.Slider
                {
                    TitleAr = a.TitleAr,
                    TitleEn = a.TitleEn,
                    DetailsAr = a.DetailsAr,
                    DetailsEn = a.DetailsEn,
                    Img = a.Img
                }).ToList(),
                SchoolFruits = unitOfWork.SchoolFruits.Select(a => new ViewModels.Form.SchoolFruits
                {
                    TitleAr = a.TitleAr,
                    TitleEn = a.TitleEn,
                    Img = a.Img,
                    FruitsDetails = a.FruitsDetails.Select(d => new ViewModels.Form.SchoolFruitsDetails
                    {
                        ID = d.ID,
                        TitleAr = d.TitleAr,
                        TitleEn = d.TitleEn,
                        FruitsID = d.FruitsID
                    }).ToList(),
                }).ToList()
            };
        }
    }
}