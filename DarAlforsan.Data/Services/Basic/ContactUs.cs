using AutoMapper;
using Core;
using Core.Data;
using System.Linq.Expressions;

namespace Services
{
    public class ContactUs : Service<UnitOfWork>
    {
        //public ViewModels.Form.ContactUs? InitEdit(long ID)
        //{
        //    var config = new MapperConfiguration(cfg => {
        //        cfg.CreateMap<Models.ContactUs, ViewModels.Form.ContactUs>();
        //    });

        //    IMapper mapper = config.CreateMapper();
        //    return mapper.Map<Models.ContactUs, ViewModels.Form.ContactUs>(unitOfWork.ContactUs.Find(ID));
        //}
        //-----------------------------------------------------------------------------------------
        public PagedList<ViewModels.Form.ContactUs> GetList(string Filter, int PageIndex, int PageSize, string ColumnOrder, string Dir)
        {
            var data = new PagedList<ViewModels.Form.ContactUs>()
            {
                PageIndex = PageIndex,
                PageSize = PageSize
            };
            Expression<Func<Models.ContactUs, bool>> Predicate = a =>
                a.Name.Contains(Filter) ||
                a.Email.Contains(Filter) ||
                a.MobileNo.Contains(Filter) ||
                a.Message.Contains(Filter);

            var OrderFunc = Models.Extensions.ContactUs.GetOrderByFuncion(ColumnOrder, Dir);
            data.Data = unitOfWork.ContactUs.Where(Predicate,
                item => new ViewModels.Form.ContactUs
                {
                    ID = item.ID,
                    Name = item.Name,
                    Email = item.Email,
                    MobileNo = item.MobileNo,
                    Message = item.Message
                }, OrderFunc, PageIndex, PageSize);
            data.FilteredRecords = unitOfWork.ContactUs.Count(Predicate, false);
            data.TotalRecords = unitOfWork.ContactUs.Count();
            return data;
        }
        //-----------------------------------------------------------------------------------------
        public EXResult Add(ViewModels.Form.ContactUs model)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<ViewModels.Form.ContactUs, Models.ContactUs>();
                });

                IMapper mapper = config.CreateMapper();
                unitOfWork.ContactUs.Add(mapper.Map<ViewModels.Form.ContactUs, Models.ContactUs>(model));
                unitOfWork.Commit();
                return ExceptionHandler.GlobalSuccessResult();
            }
            catch (Exception ex)
            {
                return ExceptionHandler.GlobalExceptionResult(ex);
            }
        }
        //-----------------------------------------------------------------------------------------
        public ViewModels.Form.ContactUs? GetByID(long ID)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Models.ContactUs, ViewModels.Form.ContactUs>();
            });

            IMapper mapper = config.CreateMapper();
            return mapper.Map<Models.ContactUs, ViewModels.Form.ContactUs>(unitOfWork.ContactUs.Find(ID));
        }
        //-----------------------------------------------------------------------------------------
        //public EXResult Update(ViewModels.Form.ContactUs vmodel)
        //{
        //    try
        //    {
        //        var config = new MapperConfiguration(cfg => {
        //            cfg.CreateMap<ViewModels.Form.ContactUs, Models.ContactUs>();
        //        });

        //        IMapper mapper = config.CreateMapper();

        //        var model = mapper.Map<ViewModels.Form.ContactUs, Models.ContactUs>(vmodel);
        //        unitOfWork.ContactUs.Update(model);
        //        unitOfWork.Commit();
        //        return ExceptionHandler.GlobalSuccessResult();
        //    }
        //    catch (Exception ex)
        //    {
        //        return Core.ExceptionHandler.GlobalExceptionResult(ex);
        //    }
        //}
        //-----------------------------------------------------------------------------------------
        public EXResult Delete(long ID)
        {
            try
            {
                var model = unitOfWork.ContactUs.Find(ID);
                unitOfWork.ContactUs.Remove(model);
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