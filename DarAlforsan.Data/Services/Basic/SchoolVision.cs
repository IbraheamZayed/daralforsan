using AutoMapper;
using Core;
using Core.Data;
using System.Linq.Expressions;

namespace Services
{
    public class SchoolVision : Service<UnitOfWork>
    {
        public ViewModels.Form.SchoolVision? InitEdit(long ID)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Models.SchoolVision, ViewModels.Form.SchoolVision>();
            });

            IMapper mapper = config.CreateMapper();
            return mapper.Map<Models.SchoolVision, ViewModels.Form.SchoolVision>(unitOfWork.SchoolVision.Find(ID));
        }
        //-----------------------------------------------------------------------------------------
        public PagedList<ViewModels.Form.SchoolVision> GetList(string Filter, int PageIndex, int PageSize, string ColumnOrder, string Dir)
        {
            var data = new PagedList<ViewModels.Form.SchoolVision>()
            {
                PageIndex = PageIndex,
                PageSize = PageSize
            };
            Expression<Func<Models.SchoolVision, bool>> Predicate = a => 
                a.TitleAr.Contains(Filter) || 
                a.TitleEn.Contains(Filter) ||
                a.MessageAr.Contains(Filter) ||
                a.MessageEn.Contains(Filter);

            var OrderFunc = Models.Extensions.SchoolVision.GetOrderByFuncion(ColumnOrder, Dir);
            data.Data = unitOfWork.SchoolVision.Where(Predicate,
                item => new ViewModels.Form.SchoolVision
                {
                    ID = item.ID,
                    TitleAr = item.TitleAr,
                    TitleEn = item.TitleEn,
                    MessageAr = item.MessageAr,
                    MessageEn = item.MessageEn
                }, OrderFunc, PageIndex, PageSize);
            data.FilteredRecords = unitOfWork.SchoolVision.Count(Predicate, false);
            data.TotalRecords = unitOfWork.SchoolVision.Count();
            return data;
        }
        //-----------------------------------------------------------------------------------------
        public EXResult Add(ViewModels.Form.SchoolVision model)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<ViewModels.Form.SchoolVision, Models.SchoolVision>();
                });

                IMapper mapper = config.CreateMapper();
                unitOfWork.SchoolVision.Add(mapper.Map<ViewModels.Form.SchoolVision, Models.SchoolVision>(model));
                unitOfWork.Commit();
                return ExceptionHandler.GlobalSuccessResult();
            }
            catch (Exception ex)
            {
                return ExceptionHandler.GlobalExceptionResult(ex);
            }
        }
        //-----------------------------------------------------------------------------------------
        public EXResult Update(ViewModels.Form.SchoolVision vmodel)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<ViewModels.Form.SchoolVision, Models.SchoolVision>();
                });

                IMapper mapper = config.CreateMapper();

                var model = mapper.Map<ViewModels.Form.SchoolVision, Models.SchoolVision>(vmodel);
                unitOfWork.SchoolVision.Update(model);
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
                var model = unitOfWork.SchoolVision.Find(ID);
                unitOfWork.SchoolVision.Remove(model);
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