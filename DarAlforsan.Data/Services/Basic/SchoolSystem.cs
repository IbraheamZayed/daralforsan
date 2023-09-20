using AutoMapper;
using Core;
using Core.Data;
using System.Linq.Expressions;

namespace Services
{
    public class SchoolSystem : Service<UnitOfWork>
    {
        public ViewModels.Form.SchoolSystem? InitEdit(long ID)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Models.SchoolSystem, ViewModels.Form.SchoolSystem>();
            });

            IMapper mapper = config.CreateMapper();
            return mapper.Map<Models.SchoolSystem, ViewModels.Form.SchoolSystem>(unitOfWork.SchoolSystem.Find(ID));
        }
        //-----------------------------------------------------------------------------------------
        public PagedList<ViewModels.Form.SchoolSystem> GetList(string Filter, int PageIndex, int PageSize, string ColumnOrder, string Dir)
        {
            var data = new PagedList<ViewModels.Form.SchoolSystem>()
            {
                PageIndex = PageIndex,
                PageSize = PageSize
            };
            Expression<Func<Models.SchoolSystem, bool>> Predicate = a => 
                a.TitleAr.Contains(Filter) || 
                a.TitleEn.Contains(Filter) ||
                a.MessageAr.Contains(Filter) ||
                a.MessageEn.Contains(Filter);

            var OrderFunc = Models.Extensions.SchoolSystem.GetOrderByFuncion(ColumnOrder, Dir);
            data.Data = unitOfWork.SchoolSystem.Where(Predicate,
                item => new ViewModels.Form.SchoolSystem
                {
                    ID = item.ID,
                    TitleAr = item.TitleAr,
                    TitleEn = item.TitleEn,
                    MessageAr = item.MessageAr,
                    MessageEn = item.MessageEn
                }, OrderFunc, PageIndex, PageSize);
            data.FilteredRecords = unitOfWork.SchoolSystem.Count(Predicate, false);
            data.TotalRecords = unitOfWork.SchoolSystem.Count();
            return data;
        }
        //-----------------------------------------------------------------------------------------
        public EXResult Add(ViewModels.Form.SchoolSystem model)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<ViewModels.Form.SchoolSystem, Models.SchoolSystem>();
                });

                IMapper mapper = config.CreateMapper();
                unitOfWork.SchoolSystem.Add(mapper.Map<ViewModels.Form.SchoolSystem, Models.SchoolSystem>(model));
                unitOfWork.Commit();
                return ExceptionHandler.GlobalSuccessResult();
            }
            catch (Exception ex)
            {
                return ExceptionHandler.GlobalExceptionResult(ex);
            }
        }
        //-----------------------------------------------------------------------------------------
        public EXResult Update(ViewModels.Form.SchoolSystem vmodel)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<ViewModels.Form.SchoolSystem, Models.SchoolSystem>();
                });

                IMapper mapper = config.CreateMapper();

                var model = mapper.Map<ViewModels.Form.SchoolSystem, Models.SchoolSystem>(vmodel);
                unitOfWork.SchoolSystem.Update(model);
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
                var model = unitOfWork.SchoolSystem.Find(ID);
                unitOfWork.SchoolSystem.Remove(model);
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