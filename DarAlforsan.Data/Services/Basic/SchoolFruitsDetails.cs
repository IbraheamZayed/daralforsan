using Core;
using Core.Data;
using System.Linq.Expressions;

namespace Services
{
    public class SchoolFruitsDetails : Service<UnitOfWork>
    {
        public ViewModels.Form.SchoolFruitsDetails? InitAdd()
        {
            return new ViewModels.Form.SchoolFruitsDetails
            {
                SchoolFruits = unitOfWork.SchoolFruits.Select(a => new ViewModels.Form.SchoolFruits
                {
                    ID = a.ID,
                    TitleAr = a.TitleAr,
                    TitleEn = a.TitleEn
                }).ToList()
            };
        }
        //-----------------------------------------------------------------------------------------
        public ViewModels.Form.SchoolFruitsDetails? InitEdit(long ID)
        {
            var vmodel =  unitOfWork.SchoolFruitsDetails.Find(ID);
            var model = new ViewModels.Form.SchoolFruitsDetails
            {
                TitleEn = vmodel.TitleEn,
                TitleAr = vmodel.TitleAr,
                ID = vmodel.ID,
                FruitsID = vmodel.FruitsID
            };
            model.SchoolFruits = unitOfWork.SchoolFruits.Select(a => new ViewModels.Form.SchoolFruits
            {
                ID = a.ID,
                TitleAr = a.TitleAr,
                TitleEn = a.TitleEn
            }).ToList();
            return model;
        }
        //-----------------------------------------------------------------------------------------
        public PagedList<ViewModels.List.SchoolFruitsDetails> GetList(string Filter, int PageIndex, int PageSize, string ColumnOrder, string Dir)
        {
            var data = new PagedList<ViewModels.List.SchoolFruitsDetails>()
            {
                PageIndex = PageIndex,
                PageSize = PageSize
            };
            Expression<Func<Models.SchoolFruitsDetails, bool>> Predicate = a => 
            a.TitleAr.Contains(Filter) ||
            a.TitleEn.Contains(Filter) ||
            a.Fruits.TitleAr.Contains(Filter) ||
            a.Fruits.TitleEn.Contains(Filter);

            var OrderFunc = Models.Extensions.SchoolFruitsDetails.GetOrderByFuncion(ColumnOrder, Dir);
            data.Data = unitOfWork.SchoolFruitsDetails.Where(Predicate,
                item => new ViewModels.List.SchoolFruitsDetails
                {
                    ID = item.ID,
                    TitleAr = item.TitleAr,
                    TitleEn = item.TitleEn,
                    FruitsTitleAr = item.Fruits.TitleAr,
                    FruitsTitleEn = item.Fruits.TitleEn,
                    FruitsID = item.FruitsID
                }, OrderFunc, PageIndex, PageSize);
            data.FilteredRecords = unitOfWork.SchoolFruitsDetails.Count(Predicate, false);
            data.TotalRecords = unitOfWork.SchoolFruitsDetails.Count();
            return data;
        }
        //-----------------------------------------------------------------------------------------
        public EXResult Add(ViewModels.Form.SchoolFruitsDetails vmodel)
        {
            try
            {
                var model = new Models.SchoolFruitsDetails();
                model.TitleAr = vmodel.TitleAr;
                model.TitleEn = vmodel.TitleEn;
                model.FruitsID = vmodel.FruitsID;
                model.Fruits = null;
                unitOfWork.SchoolFruitsDetails.Add(model);
                unitOfWork.Commit();
                return ExceptionHandler.GlobalSuccessResult();
            }
            catch (Exception ex)
            {
                return ExceptionHandler.GlobalExceptionResult(ex);
            }
        }
        //-----------------------------------------------------------------------------------------
        public EXResult Update(ViewModels.Form.SchoolFruitsDetails vmodel)
        {
            try
            {
                var model = unitOfWork.SchoolFruitsDetails.Find(vmodel.ID);
                model.TitleAr = vmodel.TitleAr;
                model.TitleEn = vmodel.TitleEn;
                model.FruitsID = vmodel.FruitsID;
                unitOfWork.SchoolFruitsDetails.Update(model);
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
                var model = unitOfWork.SchoolFruitsDetails.Find(ID);
                unitOfWork.SchoolFruitsDetails.Remove(model);
                unitOfWork.Commit();
                return ExceptionHandler.GlobalSuccessResult();
            }
            catch (Exception Ex)
            {
                return ExceptionHandler.DeleteExceptionResult(Ex);
            }
        }
        //-----------------------------------------------------------------------------------------
        public List<ViewModels.Form.SchoolFruitsDetails> GetBySchoolFruitsID(long FruitsID)
        {
            return unitOfWork.SchoolFruitsDetails.Where(a => a.FruitsID == FruitsID).Select(a => new ViewModels.Form.SchoolFruitsDetails
            {
                TitleAr = a.TitleAr,
                TitleEn = a.TitleEn
            }).ToList();
        }
    }
}