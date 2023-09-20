using AutoMapper;
using Core;
using Core.Data;
using System.Linq.Expressions;

namespace Services
{
    public class AboutSchool : Service<UnitOfWork>
    {
        public ViewModels.Form.AboutSchool? InitEdit(long ID)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Models.AboutSchool, ViewModels.Form.AboutSchool>();
            });

            IMapper mapper = config.CreateMapper();
            return mapper.Map<Models.AboutSchool, ViewModels.Form.AboutSchool>(unitOfWork.AboutSchool.Find(ID));
        }
        //-----------------------------------------------------------------------------------------
        public PagedList<ViewModels.Form.AboutSchool> GetList(string Filter, int PageIndex, int PageSize, string ColumnOrder, string Dir)
        {
            var data = new PagedList<ViewModels.Form.AboutSchool>()
            {
                PageIndex = PageIndex,
                PageSize = PageSize
            };
            Expression<Func<Models.AboutSchool, bool>> Predicate = a => a.AddressAr.Contains(Filter) || a.AddressEn.Contains(Filter) || a.MobileNo.Contains(Filter) || a.WhatsAppNo.Contains(Filter) || a.Email.Contains(Filter) || a.DetailsAr.Contains(Filter)||a.DetailsEn.Contains(Filter);
            var OrderFunc = Models.Extensions.AboutSchool.GetOrderByFuncion(ColumnOrder, Dir);
            data.Data = unitOfWork.AboutSchool.Where(Predicate,
                item => new ViewModels.Form.AboutSchool
                {
                    ID = item.ID,
                    AddressAr = item.AddressAr,
                    AddressEn = item.AddressEn,
                    MobileNo = item.MobileNo,
                    WhatsAppNo = item.WhatsAppNo,
                    Email = item.Email,
                    Lat = item.Lat,
                    Lng = item.Lng,
                    DetailsAr = item.DetailsAr,
                    DetailsEn = item.DetailsEn,
                    Logo = item.Logo,
                    RegisterationUrl = item.RegisterationUrl
                }, OrderFunc, PageIndex, PageSize);
            data.FilteredRecords = unitOfWork.AboutSchool.Count(Predicate, false);
            data.TotalRecords = unitOfWork.AboutSchool.Count();
            return data;
        }
        //-----------------------------------------------------------------------------------------
        public EXResult Add(ViewModels.Form.AboutSchool model)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<ViewModels.Form.AboutSchool, Models.AboutSchool>();
                });

                IMapper mapper = config.CreateMapper();
                unitOfWork.AboutSchool.Add(mapper.Map<ViewModels.Form.AboutSchool, Models.AboutSchool>(model));
                unitOfWork.Commit();
                return ExceptionHandler.GlobalSuccessResult();
            }
            catch (Exception ex)
            {
                return ExceptionHandler.GlobalExceptionResult(ex);
            }
        }
        //-----------------------------------------------------------------------------------------
        public EXResult Update(ViewModels.Form.AboutSchool vmodel)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<ViewModels.Form.AboutSchool, Models.AboutSchool>();
                });
                IMapper mapper = config.CreateMapper();
                var model = mapper.Map<ViewModels.Form.AboutSchool, Models.AboutSchool>(vmodel);
                unitOfWork.AboutSchool.Update(model);
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
                var model = unitOfWork.AboutSchool.Find(ID);
                unitOfWork.AboutSchool.Remove(model);
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