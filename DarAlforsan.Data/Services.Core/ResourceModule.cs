using AutoMapper;
using Core;
using Core.Data;
using System.Linq.Expressions;

namespace Services
{
    public class ResourceModule : Service<UnitOfWork>
    {
        public ViewModels.Form.ResourceModule? InitEdit(long ID)
        {
            return unitOfWork.ResourceModules.Where(a => a.ModuleID == ID).Select(a => new ViewModels.Form.ResourceModule
            {
                ModuleID = a.ModuleID,
                Name = a.Name,
                LocalName = a.LocalName,
                LatinName = a.LatinName
            }).FirstOrDefault();
        }
        //-----------------------------------------------------------------------------------------
        public PagedList<ViewModels.Form.ResourceModule> GetList(string Filter, int PageIndex, int PageSize, string ColumnOrder, string Dir)
        {
            var data = new PagedList<ViewModels.Form.ResourceModule>()
            {
                PageIndex = PageIndex,
                PageSize = PageSize
            };
            Expression<Func<Models.ResourceModule, bool>> Predicate = a => a.LocalName.Contains(Filter) || a.LatinName.Contains(Filter) || a.Name.Contains(Filter);
            var OrderFunc = Models.Extensions.ResourceModule.GetOrderByFuncion(ColumnOrder, Dir);
            data.Data = unitOfWork.ResourceModules.Where(Predicate,
                item => new ViewModels.Form.ResourceModule
                {
                    ModuleID = item.ModuleID,
                    Name = item.Name,
                    LatinName = item.LatinName,
                    LocalName = item.LocalName
                }, OrderFunc, PageIndex, PageSize);
            data.FilteredRecords = unitOfWork.ResourceModules.Count(Predicate, false);
            data.TotalRecords = unitOfWork.ResourceModules.Count();
            return data;
        }
        //-----------------------------------------------------------------------------------------
        public EXResult Add(ViewModels.Form.ResourceModule model)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<ViewModels.Form.ResourceModule, Models.ResourceModule>();
                });

                IMapper mapper = config.CreateMapper();
                unitOfWork.ResourceModules.Add(mapper.Map<ViewModels.Form.ResourceModule, Models.ResourceModule>(model));
                unitOfWork.Commit();
                return ExceptionHandler.GlobalSuccessResult();
            }
            catch (Exception ex)
            {
                return ExceptionHandler.GlobalExceptionResult(ex);
            }
        }
        //-----------------------------------------------------------------------------------------
        public EXResult Update(ViewModels.Form.ResourceModule vmodel)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<ViewModels.Form.ResourceModule, Models.ResourceModule>();
                });

                IMapper mapper = config.CreateMapper();

                var model = mapper.Map<ViewModels.Form.ResourceModule, Models.ResourceModule>(vmodel);
                unitOfWork.ResourceModules.Update(model);
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
                var model = unitOfWork.ResourceModules.Find(ID);
                unitOfWork.ResourceModules.Remove(model);
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
