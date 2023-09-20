using AutoMapper;
using Core;
using Core.Data;
using System.Linq.Expressions;

namespace Services
{
    public class SocialMedia : Service<UnitOfWork>
    {
        public ViewModels.Form.SocialMedia? InitEdit(long ID)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Models.SocialMedia, ViewModels.Form.SocialMedia>();
            });

            IMapper mapper = config.CreateMapper();
            return mapper.Map<Models.SocialMedia, ViewModels.Form.SocialMedia>(unitOfWork.SocialMedia.Find(ID));
        }
        //-----------------------------------------------------------------------------------------
        public PagedList<ViewModels.Form.SocialMedia> GetList(string Filter, int PageIndex, int PageSize, string ColumnOrder, string Dir)
        {
            var data = new PagedList<ViewModels.Form.SocialMedia>()
            {
                PageIndex = PageIndex,
                PageSize = PageSize
            };
            Expression<Func<Models.SocialMedia, bool>> Predicate = a => a.Name.Contains(Filter) || a.Url.Contains(Filter);
            var OrderFunc = Models.Extensions.SocialMedia.GetOrderByFuncion(ColumnOrder, Dir);
            data.Data = unitOfWork.SocialMedia.Where(Predicate,
                item => new ViewModels.Form.SocialMedia
                {
                    ID = item.ID,
                    Name = item.Name,
                    Url = item.Url,
                    Img = item.Img
                }, OrderFunc, PageIndex, PageSize);
            data.FilteredRecords = unitOfWork.SocialMedia.Count(Predicate, false);
            data.TotalRecords = unitOfWork.SocialMedia.Count();
            return data;
        }
        //-----------------------------------------------------------------------------------------
        public EXResult Add(ViewModels.Form.SocialMedia model)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<ViewModels.Form.SocialMedia, Models.SocialMedia>();
                });

                IMapper mapper = config.CreateMapper();
                unitOfWork.SocialMedia.Add(mapper.Map<ViewModels.Form.SocialMedia, Models.SocialMedia>(model));
                unitOfWork.Commit();
                return ExceptionHandler.GlobalSuccessResult();
            }
            catch (Exception ex)
            {
                return ExceptionHandler.GlobalExceptionResult(ex);
            }
        }
        //-----------------------------------------------------------------------------------------
        public EXResult Update(ViewModels.Form.SocialMedia vmodel)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<ViewModels.Form.SocialMedia, Models.SocialMedia>();
                });

                IMapper mapper = config.CreateMapper();

                var model = mapper.Map<ViewModels.Form.SocialMedia, Models.SocialMedia>(vmodel);
                unitOfWork.SocialMedia.Update(model);
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
                var model = unitOfWork.SocialMedia.Find(ID);
                unitOfWork.SocialMedia.Remove(model);
                unitOfWork.Commit();
                return ExceptionHandler.GlobalSuccessResult();
            }
            catch (Exception Ex)
            {
                return ExceptionHandler.DeleteExceptionResult(Ex);
            }
        }
        //-----------------------------------------------------------------------------------------
        public List<Models.SocialMedia> GetAll()
        {
            return unitOfWork.Context.SocialMedia.ToList();
        }
    }
}