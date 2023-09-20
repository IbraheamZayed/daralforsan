using AutoMapper;
using Core;
using Core.Data;
using System.Linq.Expressions;

namespace Services
{
    public class SaidAboutUs : Service<UnitOfWork>
    {
        public ViewModels.Form.SaidAboutUs? InitEdit(long ID)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Models.SaidAboutUs, ViewModels.Form.SaidAboutUs>();
            });

            IMapper mapper = config.CreateMapper();
            return mapper.Map<Models.SaidAboutUs, ViewModels.Form.SaidAboutUs>(unitOfWork.SaidAboutUs.Find(ID));
        }
        //-----------------------------------------------------------------------------------------
        public PagedList<ViewModels.Form.SaidAboutUs> GetList(string Filter, int PageIndex, int PageSize, string ColumnOrder, string Dir)
        {
            var data = new PagedList<ViewModels.Form.SaidAboutUs>()
            {
                PageIndex = PageIndex,
                PageSize = PageSize
            };
            Expression<Func<Models.SaidAboutUs, bool>> Predicate = a => a.Name.Contains(Filter) || a.MessageAr.Contains(Filter) || a.MessageEn.Contains(Filter);
            var OrderFunc = Models.Extensions.SaidAboutUs.GetOrderByFuncion(ColumnOrder, Dir);
            data.Data = unitOfWork.SaidAboutUs.Where(Predicate,
                item => new ViewModels.Form.SaidAboutUs
                {
                    ID = item.ID,
                    Name = item.Name,
                    MessageAr = item.MessageAr,
                    MessageEn = item.MessageEn
                }, OrderFunc, PageIndex, PageSize);
            data.FilteredRecords = unitOfWork.SaidAboutUs.Count(Predicate, false);
            data.TotalRecords = unitOfWork.SaidAboutUs.Count();
            return data;
        }
        //-----------------------------------------------------------------------------------------
        public EXResult Add(ViewModels.Form.SaidAboutUs model)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<ViewModels.Form.SaidAboutUs, Models.SaidAboutUs>();
                });

                IMapper mapper = config.CreateMapper();
                unitOfWork.SaidAboutUs.Add(mapper.Map<ViewModels.Form.SaidAboutUs, Models.SaidAboutUs>(model));
                unitOfWork.Commit();
                return ExceptionHandler.GlobalSuccessResult();
            }
            catch (Exception ex)
            {
                return ExceptionHandler.GlobalExceptionResult(ex);
            }
        }
        //-----------------------------------------------------------------------------------------
        public EXResult Update(ViewModels.Form.SaidAboutUs vmodel)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<ViewModels.Form.SaidAboutUs, Models.SaidAboutUs>();
                });

                IMapper mapper = config.CreateMapper();

                var model = mapper.Map<ViewModels.Form.SaidAboutUs, Models.SaidAboutUs>(vmodel);
                unitOfWork.SaidAboutUs.Update(model);
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
                var model = unitOfWork.SaidAboutUs.Find(ID);
                unitOfWork.SaidAboutUs.Remove(model);
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