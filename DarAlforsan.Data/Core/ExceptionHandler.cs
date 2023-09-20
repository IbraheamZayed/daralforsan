namespace Core
{
    public static class ExceptionHandler
    {
        //-----------------------------------------------------------------------------------------
        public static EXResult GlobalSuccessResult()
        {
            EXResult result = new EXResult();
            result.Status = EXResultStatus.Success;
            result.Message = DBResources.CoreMessage.SaveSuccess;
            return result;
        }
        //-----------------------------------------------------------------------------------------
        public static EXResult GlobalSuccessResult(string Message)
        {
            EXResult result = new EXResult();
            result.Status = EXResultStatus.Success;
            result.Message = Message;
            return result;
        }
        //-----------------------------------------------------------------------------------------
        public static EXResult GlobalFailedResult()
        {
            EXResult result = new EXResult();
            result.Status = EXResultStatus.Fail;
            result.Message = DBResources.CoreMessage.SaveFail;
            return result;
        }
        //-----------------------------------------------------------------------------------------
        public static EXResult DeleteExceptionResult(Exception ex)
        {
            EXResult result = new EXResult();
            result.Status = EXResultStatus.Fail;
            if (ex != null)
                result.Message = DBResources.CoreMessage.DeleteFail + "\n" +
                (!string.IsNullOrEmpty(ex.Message) ? ex.Message : ex.InnerException.Message);
            else
                result.Message = DBResources.CoreMessage.DeleteFail;
            return result;
        }
        //-----------------------------------------------------------------------------------------
        public static EXResult GlobalFailedResult(string message)
        {
            EXResult result = new EXResult();
            result.Status = EXResultStatus.Fail;
            result.Message = message;
            return result;
        }
        //-----------------------------------------------------------------------------------------
        public static EXResult GlobalExceptionResult(Exception ex)
        {
            EXResult result = new EXResult();
            result.Status = EXResultStatus.Fail;
            if (ex != null)
                result.Message = DBResources.CoreMessage.SaveFail + "\n" +
                (!string.IsNullOrEmpty(ex.Message) ? ex.Message : ex.InnerException.Message);
            else
                result.Message = DBResources.CoreMessage.SaveFail;
            return result;
        }
    }
}