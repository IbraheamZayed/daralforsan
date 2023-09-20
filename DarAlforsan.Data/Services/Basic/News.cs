using AutoMapper;
using Core;
using Core.Data;
using System.Linq.Expressions;

namespace Services
{
    public class News : Service<UnitOfWork>
    {
        public ViewModels.Form.News? InitEdit(long ID)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Models.News, ViewModels.Form.News>();
            });

            IMapper mapper = config.CreateMapper();
            return mapper.Map<Models.News, ViewModels.Form.News>(unitOfWork.News.Find(ID));
        }
        //-----------------------------------------------------------------------------------------
        public PagedList<ViewModels.Form.News> GetList(string Filter, int PageIndex, int PageSize, string ColumnOrder, string Dir)
        {
            var data = new PagedList<ViewModels.Form.News>()
            {
                PageIndex = PageIndex,
                PageSize = PageSize
            };

            Expression<Func<Models.News, bool>> Predicate = a =>
                a.TitleAr.Contains(Filter) || a.TitleEn.Contains(Filter) ||
                a.DetailsAr.Contains(Filter) || a.DetailsEn.Contains(Filter) ||
                a.ViewsCount.ToString().Contains(Filter);

            var OrderFunc = Models.Extensions.News.GetOrderByFuncion(ColumnOrder, Dir);
            data.Data = unitOfWork.News.Where(Predicate,
                item => new ViewModels.Form.News
                {
                    ID = item.ID,
                    TitleAr = item.TitleAr,
                    TitleEn = item.TitleEn,
                    DetailsAr = item.DetailsAr,
                    DetailsEn = item.DetailsEn,
                    AddDate = item.AddDate,
                    ViewsCount = item.ViewsCount
                }, OrderFunc, PageIndex, PageSize);
            data.FilteredRecords = unitOfWork.News.Count(Predicate, false);
            data.TotalRecords = unitOfWork.News.Count();
            return data;
        }
        //-----------------------------------------------------------------------------------------
        public EXResult Add(ViewModels.Form.News model)
        {
            try
            {
                model.AddDate = DateTime.Now;
                model.ViewsCount = 0;

                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<ViewModels.Form.News, Models.News>();
                });

                IMapper mapper = config.CreateMapper();
                unitOfWork.News.Add(mapper.Map<ViewModels.Form.News, Models.News>(model));
                unitOfWork.Commit();
                return ExceptionHandler.GlobalSuccessResult();
            }
            catch (Exception ex)
            {
                return ExceptionHandler.GlobalExceptionResult(ex);
            }
        }
        //-----------------------------------------------------------------------------------------
        public EXResult Update(ViewModels.Form.News vmodel)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<ViewModels.Form.News, Models.News>();
                });

                IMapper mapper = config.CreateMapper();

                var model = mapper.Map<ViewModels.Form.News, Models.News>(vmodel);
                model.AddDate = DateTime.Now;
                unitOfWork.News.Update(model);
                unitOfWork.Commit();
                return ExceptionHandler.GlobalSuccessResult();
            }
            catch (Exception ex)
            {
                return Core.ExceptionHandler.GlobalExceptionResult(ex);
            }
        }
        //-----------------------------------------------------------------------------------------
        public EXResult Delete(long ID)
        {
            try
            {
                var model = unitOfWork.News.Find(ID);
                unitOfWork.News.Remove(model);
                unitOfWork.Commit();
                return ExceptionHandler.GlobalSuccessResult();
            }
            catch (Exception Ex)
            {
                return ExceptionHandler.DeleteExceptionResult(Ex);
            }
        }
        //-----------------------------------------------------------------------------------------
        public ViewModels.Form.News? Get(long ID)
        {
            return unitOfWork.News.Select(a => new ViewModels.Form.News
            {
                ID = a.ID,
                TitleAr = a.TitleAr,
                TitleEn = a.TitleEn,
                DetailsAr = a.DetailsAr,
                DetailsEn = a.DetailsEn,
                AddDate = a.AddDate,
                ViewsCount = a.ViewsCount,
                Imgs = a.Imgs,
                MainImg = a.MainImg,
            }).FirstOrDefault(a => a.ID == ID);
        }
    }
}