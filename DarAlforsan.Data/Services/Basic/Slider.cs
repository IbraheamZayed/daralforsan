using AutoMapper;
using Core;
using Core.Data;
using System.Linq.Expressions;

namespace Services
{
    public class Slider : Service<UnitOfWork>
    {
        public ViewModels.Form.Slider? InitEdit(long ID)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Models.Slider, ViewModels.Form.Slider>();
            });

            IMapper mapper = config.CreateMapper();
            return mapper.Map<Models.Slider, ViewModels.Form.Slider>(unitOfWork.Slider.Find(ID));
        }
        //-----------------------------------------------------------------------------------------
        public PagedList<ViewModels.Form.Slider> GetList(string Filter, int PageIndex, int PageSize, string ColumnOrder, string Dir)
        {
            var data = new PagedList<ViewModels.Form.Slider>()
            {
                PageIndex = PageIndex,
                PageSize = PageSize
            };
            Expression<Func<Models.Slider, bool>> Predicate = (a => a.TitleAr.Contains(Filter) || a.DetailsAr.Contains(Filter));
            var OrderFunc = Models.Extensions.Slider.GetOrderByFuncion(ColumnOrder, Dir);
            data.Data = unitOfWork.Slider.Where(Predicate,
                item => new ViewModels.Form.Slider
                {
                    ID = item.ID,
                    TitleAr = item.TitleAr,
                    TitleEn = item.TitleEn,
                    DetailsAr = item.DetailsAr,
                    DetailsEn = item.DetailsEn
                }, OrderFunc, PageIndex, PageSize);
            data.FilteredRecords = unitOfWork.Slider.Count(Predicate, false);
            data.TotalRecords = unitOfWork.Slider.Count();
            return data;
        }
        //-----------------------------------------------------------------------------------------
        public EXResult Add(ViewModels.Form.Slider model)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<ViewModels.Form.Slider, Models.Slider>();
                });

                IMapper mapper = config.CreateMapper();
                unitOfWork.Slider.Add(mapper.Map<ViewModels.Form.Slider, Models.Slider>(model));
                unitOfWork.Commit();
                return ExceptionHandler.GlobalSuccessResult();
            }
            catch (Exception ex)
            {
                return ExceptionHandler.GlobalExceptionResult(ex);
            }
        }
        //-----------------------------------------------------------------------------------------
        public EXResult Update(ViewModels.Form.Slider vmodel)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<ViewModels.Form.Slider, Models.Slider>();
                });

                IMapper mapper = config.CreateMapper();

                var model = mapper.Map<ViewModels.Form.Slider, Models.Slider>(vmodel);
                unitOfWork.Slider.Update(model);
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
                var model = unitOfWork.Slider.Find(ID);
                unitOfWork.Slider.Remove(model);
                unitOfWork.Commit();
                return ExceptionHandler.GlobalSuccessResult();
            }
            catch (Exception Ex)
            {
                return ExceptionHandler.DeleteExceptionResult(Ex);
            }
        }
    }
}