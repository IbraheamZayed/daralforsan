using AutoMapper;
using Core;
using Core.Data;
using System.Linq.Expressions;

namespace Services
{
    public class Resource : Service<UnitOfWork>
    {
        public ViewModels.Form.Resource? InitAdd()
        {
            return new ViewModels.Form.Resource
            {
                ResourceModules = unitOfWork.Context.ResourceModule.Select(a => new ViewModels.Form.ResourceModule
                {
                    ModuleID = a.ModuleID,
                    Name = a.Name
                }).ToList()
            };
        }
        //-----------------------------------------------------------------------------------------
        public ViewModels.Form.Resource? InitEdit(long ID)
        {
            var resource = unitOfWork.Resources.Where(a => a.ModuleID == ID).Select(a => new ViewModels.Form.Resource
            {
                ModuleID = a.ModuleID,
                Name = a.Name,
                LocalValue = a.LocalValue,
                LatinValue = a.LatinValue
            }).FirstOrDefault();

            resource.ResourceModules = unitOfWork.Context.ResourceModule.Select(a => new ViewModels.Form.ResourceModule
            {
                ModuleID = a.ModuleID,
                Name = a.Name
            }).ToList();

            return resource;
        }
        //-----------------------------------------------------------------------------------------
        public PagedList<ViewModels.List.Resource> GetList(string Filter, int PageIndex, int PageSize, string ColumnOrder, string Dir)
        {
            var data = new PagedList<ViewModels.List.Resource>()
            {
                PageIndex = PageIndex,
                PageSize = PageSize
            };
            Expression<Func<Models.Resource, bool>> Predicate = a => a.LocalValue.Contains(Filter) || a.LatinValue.Contains(Filter) || a.Name.Contains(Filter);
            var OrderFunc = Models.Extensions.Resource.GetOrderByFuncion(ColumnOrder, Dir);

            data.Data = unitOfWork.Resources.Where(Predicate,
                item => new ViewModels.List.Resource
                {
                    ResourceID = item.ResourceID,
                    ModuleID = item.ModuleID,
                    Name = item.Name,
                    LatinValue = item.LatinValue,
                    LocalValue = item.LocalValue,
                    Module = unitOfWork.ResourceModules.Find(item.ModuleID).Name
                }, OrderFunc, PageIndex, PageSize);
            data.FilteredRecords = unitOfWork.Resources.Count(Predicate, false);
            data.TotalRecords = unitOfWork.Resources.Count();
            return data;
        }
        //-----------------------------------------------------------------------------------------
        public EXResult Add(ViewModels.Form.Resource model)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<ViewModels.Form.Resource, Models.Resource>();
                });

                IMapper mapper = config.CreateMapper();
                unitOfWork.Resources.Add(mapper.Map<ViewModels.Form.Resource, Models.Resource>(model));
                unitOfWork.Commit();
                return ExceptionHandler.GlobalSuccessResult();
            }
            catch (Exception ex)
            {
                return ExceptionHandler.GlobalExceptionResult(ex);
            }
        }
        //-----------------------------------------------------------------------------------------
        public EXResult Update(ViewModels.Form.Resource vmodel)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<ViewModels.Form.Resource, Models.Resource>();
                });

                IMapper mapper = config.CreateMapper();

                var model = mapper.Map<ViewModels.Form.Resource, Models.Resource>(vmodel);
                unitOfWork.Resources.Update(model);
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
                var model = unitOfWork.Resources.Find(ID);
                unitOfWork.Resources.Remove(model);
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