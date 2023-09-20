using Core;
using Core.Data;
using System.Linq.Expressions;

namespace Services
{
    public class SchoolFruits : Service<UnitOfWork>
    {
        public ViewModels.Form.SchoolFruits? InitEdit(long ID)
        {
            var model = unitOfWork.SchoolFruits.Find(ID);
            return new ViewModels.Form.SchoolFruits
            {
                ID = model.ID,
                TitleAr = model.TitleAr,
                TitleEn = model.TitleEn,
                Img = model.Img
            };
        }
        //-----------------------------------------------------------------------------------------
        public PagedList<ViewModels.Form.SchoolFruits> GetList(string Filter, int PageIndex, int PageSize, string ColumnOrder, string Dir)
        {
            var data = new PagedList<ViewModels.Form.SchoolFruits>()
            {
                PageIndex = PageIndex,
                PageSize = PageSize
            };
            Expression<Func<Models.SchoolFruits, bool>> Predicate = a => a.TitleAr.Contains(Filter);
            var OrderFunc = Models.Extensions.SchoolFruits.GetOrderByFuncion(ColumnOrder, Dir);
            data.Data = unitOfWork.SchoolFruits.Where(Predicate,
                item => new ViewModels.Form.SchoolFruits
                {
                    ID = item.ID,
                    TitleAr = item.TitleAr,
                    TitleEn = item.TitleEn,
                }, OrderFunc, PageIndex, PageSize);
            data.FilteredRecords = unitOfWork.SchoolFruits.Count(Predicate, false);
            data.TotalRecords = unitOfWork.SchoolFruits.Count();
            return data;
        }
        //-----------------------------------------------------------------------------------------
        public EXResult Add(ViewModels.Form.SchoolFruits model)
        {
            try
            {
                unitOfWork.SchoolFruits.Add(new Models.SchoolFruits
                {
                    TitleAr = model.TitleAr,
                    TitleEn = model.TitleEn,
                    Img = model.Img
                });
                unitOfWork.Commit();
                return ExceptionHandler.GlobalSuccessResult();
            }
            catch (Exception ex)
            {
                return ExceptionHandler.GlobalExceptionResult(ex);
            }
        }
        //-----------------------------------------------------------------------------------------
        public EXResult Update(ViewModels.Form.SchoolFruits vmodel)
        {
            try
            {
                var model = unitOfWork.SchoolFruits.Find(vmodel.ID);
                model.TitleAr = vmodel.TitleAr;
                model.TitleEn = vmodel.TitleEn;
                model.Img = vmodel.Img;
                unitOfWork.SchoolFruits.Update(model);
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
                var model = unitOfWork.SchoolFruits.Find(ID);
                unitOfWork.SchoolFruits.Remove(model);
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