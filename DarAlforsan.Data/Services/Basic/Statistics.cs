using AutoMapper;
using Core;
using Core.Data;
using System.Linq.Expressions;

namespace Services
{
    public class Statistics : Service<UnitOfWork>
    {
        public ViewModels.Form.Statistics? InitEdit(long ID)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Models.Statistics, ViewModels.Form.Statistics>();
            });

            IMapper mapper = config.CreateMapper();
            return mapper.Map<Models.Statistics, ViewModels.Form.Statistics>(unitOfWork.Statistics.Find(ID));
        }
        //-----------------------------------------------------------------------------------------
        public PagedList<ViewModels.Form.Statistics> GetList(string Filter, int PageIndex, int PageSize, string ColumnOrder, string Dir)
        {
            var data = new PagedList<ViewModels.Form.Statistics>()
            {
                PageIndex = PageIndex,
                PageSize = PageSize
            };
            Expression<Func<Models.Statistics, bool>> Predicate = a => a.TitleAr.Contains(Filter) || a.Count.ToString() == Filter;
            var OrderFunc = Models.Extensions.Statistics.GetOrderByFuncion(ColumnOrder, Dir);
            data.Data = unitOfWork.Statistics.Where(Predicate,
                item => new ViewModels.Form.Statistics
                {
                    ID = item.ID,
                    TitleAr = item.TitleAr,
                    TitleEn = item.TitleEn,
                    Count = item.Count
                }, OrderFunc, PageIndex, PageSize);
            data.FilteredRecords = unitOfWork.Statistics.Count(Predicate, false);
            data.TotalRecords = unitOfWork.Statistics.Count();
            return data;
        }
        //-----------------------------------------------------------------------------------------
        public EXResult Add(ViewModels.Form.Statistics model)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<ViewModels.Form.Statistics, Models.Statistics>();
                });

                IMapper mapper = config.CreateMapper();
                unitOfWork.Statistics.Add(mapper.Map<ViewModels.Form.Statistics, Models.Statistics>(model));
                unitOfWork.Commit();
                return ExceptionHandler.GlobalSuccessResult();
            }
            catch (Exception ex)
            {
                return ExceptionHandler.GlobalExceptionResult(ex);
            }
        }
        //-----------------------------------------------------------------------------------------
        public EXResult Update(ViewModels.Form.Statistics vmodel)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<ViewModels.Form.Statistics, Models.Statistics>();
                });

                IMapper mapper = config.CreateMapper();

                var model = mapper.Map<ViewModels.Form.Statistics, Models.Statistics>(vmodel);
                unitOfWork.Statistics.Update(model);
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
                var model = unitOfWork.Statistics.Find(ID);
                unitOfWork.Statistics.Remove(model);
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