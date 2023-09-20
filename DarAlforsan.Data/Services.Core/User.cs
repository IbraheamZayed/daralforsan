using Core;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;

namespace Services
{
    public class User : Service<UnitOfWork>
    {
        readonly long BranchID;
        readonly long UserID;
        //-----------------------------------------------------------------------------------------
        public User()
        {

        }
        //-----------------------------------------------------------------------------------------
        //public User(long BrachID, long UserID, string Lang, string APIURL)
        //{
        //    this.BranchID = BrachID;
        //    this.UserID = UserID;
        //    this.Lang = Lang;
        //    this.APIURL = APIURL;
        //}
        //-----------------------------------------------------------------------------------------
        public User(long BrachID, long UserID)
        {
            this.BranchID = BrachID;
            this.UserID = UserID;
        }
        //-----------------------------------------------------------------------------------------
        //public User(long BrachID, long UserID, string Lang)
        //{
        //    this.BranchID = BrachID;
        //    this.UserID = UserID;
        //    this.Lang = Lang;
        //}
        //-----------------------------------------------------------------------------------------
        //public ViewModels.UserViewModel LoadData()
        //{
        //    return new ViewModels.UserViewModel();
        //}
        ////-----------------------------------------------------------------------------------------
        //public ViewModels.UserViewModel InitLastLoginDate()
        //{
        //    ViewModels.UserViewModel vm = new ViewModels.UserViewModel();
        //    vm.LastLoginDays = 0;
        //    return vm;
        //}
        ////----------------------------------------------------------------------------------------
        //public ViewModels.UserViewModel ResetPassword()
        //{
        //    return new ViewModels.UserViewModel();
        //}
        //----------------------------------------------------------------------------------------
        //public bool IsCachable()
        //{
        //    return unitOfWork.Users.IsCacheable;
        //}
        //----------------------------------------------------------------------------------------
        public Core.Security.Identity Authenticate(string UserName, string Password)
        {
            Expression<Func<Models.User, bool>> Predicate = (u => u.UserName == UserName && u.Password == Password && u.IsActive == true && u.IsEnabled == true);

            IEnumerable<Core.Security.Identity> Users = unitOfWork.Users.Where(Predicate,
                u => new Core.Security.Identity
                {
                    UserID = u.UserID,
                    UserName = u.UserName,
                    LocalName = u.LocalName,
                    LatinName = u.LatinName,
                    Email = u.Email,
                    MobileNo = u.MobileNo,
                    LastLoginDate = u.LastLoginDate,
                    Theme = u.Theme,
                    PersonalPhotoFileName = !string.IsNullOrEmpty(u.PhotoName) ? u.PhotoName : "",
                    UILanguageID = u.UILanguageID,
                    IsoLanguageName = u.UILanguage == null ? null : u.UILanguage.ISOLanguageName,
                    BranchID = u.BranchID,
                    DeviceID = u.DeviceID
                });

            //if existing
            if (Users.Count() == 1)
            {
                Core.Security.Identity Identity = new Core.Security.Identity();

                Identity = Users.First();

                //Update user last Login Date
                Models.User LoggedinUser = unitOfWork.Users.Where(Predicate).First();
                LoggedinUser.LastLoginDate = DateTime.Now;

                //Roles
                //Identity.Roles = unitOfWork.UserRoles.Where(r => r.UserID == Identity.UserID,
                //    r => new Core.Security.Role { ID = r.RoleID, Name = r.Role.Name, LocalName = r.Role.LocalName, LatinName = r.Role.LatinName }
                //    );
                //Language
                //if UI language does not exist then get default UI language and set it to user
                if (Identity.UILanguageID == null)
                {
                    //get user instance
                    Models.User user = unitOfWork.Users.Find(Identity.UserID);
                    //get default UI Language
                    IEnumerable<Models.UILanguage> Langs = unitOfWork.UILanguages.Where(ui => ui.IsDefault == true);
                    //if there is only and only one default language
                    if (Langs.Count() == 1)
                    {
                        user.UILanguageID = Langs.First().LanguageID;
                        Identity.IsoLanguageName = Langs.First().ISOLanguageName;
                    }
                }
                //save changes
                unitOfWork.Commit();

                return Identity;
            }
            return null;

        }
        //public int CheckRole(List<long> RoleIDs)
        //{
        //    int IsJobRole = 0;
        //    var Roles = unitOfWork.Roles.Where(s => RoleIDs.Contains(s.RoleID)).ToList();
        //    if (Roles.Where(s => s.RoleID == (long)ViewModels.Shared.Enum.Role.Teacher).Count() > 0)
        //    {
        //        IsJobRole = 1;
        //    }
        //    if (Roles.Where(s => s.IsJobRole == true && s.RoleID != (long)ViewModels.Shared.Enum.Role.Teacher).Count() > 0)
        //    {
        //        IsJobRole = 4;
        //    }
        //    if (Roles.Where(s => s.RoleID == (long)ViewModels.Shared.Enum.Role.Parent).Count() > 0)
        //    {
        //        return 2;
        //    }
        //    if (Roles.Where(s => s.RoleID == (long)ViewModels.Shared.Enum.Role.Student).Count() > 0)
        //    {
        //        return 3;
        //    }
        //    return IsJobRole;
        //}
        //----------------------------------------------------------------------------------------
        public EXResult ChangePassword(ViewModels.Security.ChangePassword model)
        {
            EXResult res = new EXResult();

            var users = unitOfWork.Users.Where(u => u.UserID == model.UserID && u.Password == model.CurrentPassword);
            if (users.Count() != 0)
            {
                var user = users.First();
                user.Password = model.NewPassword;
                unitOfWork.Commit();
                res.Status = EXResultStatus.Success;
            }
            else
            {
                res.Status = EXResultStatus.Fail;
                res.Message = "ErrorCurrentPassword";//DBResources.Security.ErrorCurrentPassword;
            }
            return res;
        }
        //----------------------------------------------------------------------------------------
        //public ViewModels.UserViewModel New()
        //{
        //    ViewModels.UserViewModel userViewModel = new ViewModels.UserViewModel
        //    {
        //        User = new Models.User(),
        //        Roles = unitOfWork.Roles.Select().ToList()
        //    };
        //    return userViewModel;
        //}
        //----------------------------------------------------------------------------------------
        //public ViewModels.UserViewModel Edit(long ID)
        //{
        //    ViewModels.UserViewModel userViewModel = new ViewModels.UserViewModel
        //    {
        //        User = unitOfWork.Users.Find(ID),
        //        UserRoles = unitOfWork.UserRoles.Where(userRole => userRole.UserID == ID).Select(x => x.RoleID),
        //        Roles = unitOfWork.Roles.Select().ToList(),
        //    };
        //    return userViewModel;
        //}
        //----------------------------------------------------------------------------------------
        //public ViewModels.UserViewModel ChangePassword(long ID)
        //{
        //    ViewModels.UserViewModel userViewModel = new ViewModels.UserViewModel();
        //    try
        //    {
        //        var user = unitOfWork.Users.Find(ID);
        //        userViewModel.UserID = user.UserID;
        //        userViewModel.Password = user.Password;
        //        InsertLog(new Models.DisplayedPasswordLog() { DisplayedPasswordUserID = user.UserID, LogDate = DateTime.Now, UserID = this.UserID });
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //    return userViewModel;
        //}

        //public void InsertLog(Models.DisplayedPasswordLog model)
        //{
        //    unitOfWork.DisplayedPasswordLog.Add(model);
        //    unitOfWork.Commit();
        //}
        //----------------------------------------------------------------------------------------
        //public EXResult Add(ViewModels.UserViewModel userViewModel)
        //{
        //    EXResult result = new EXResult();

        //    userViewModel.User.AddDate = DateTime.Now;
        //    userViewModel.User.IsActive = true;

        //    //check if user name existing 
        //    if (unitOfWork.Users.Count(u => u.UserName == userViewModel.User.UserName) != 0)
        //    {
        //        result.Status = EXResultStatus.Fail;
        //        result.Message = DBResources.Security.ErrorUserNameExists;
        //        return result;
        //    }
        //    //1:Add User
        //    unitOfWork.Users.Add(userViewModel.User);
        //    //2:Add Roles 
        //    if (userViewModel.UserRoles != null)
        //    {
        //        //disable tracking changes
        //        ((DbContext)unitOfWork.Context).ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        //        foreach (long RoleID in userViewModel.UserRoles)
        //        {
        //            Models.UserRole userRole = new Models.UserRole();
        //            userRole.UserID = userViewModel.User.UserID;
        //            userRole.RoleID = RoleID;
        //            unitOfWork.UserRoles.Add(userRole);
        //        }
        //        //Enable Tracking Changes
        //        ((DbContext)unitOfWork.Context).ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        //    }

        //    //save
        //    unitOfWork.Commit();
        //    return result;
        //}
        //public EXResult UpdateUserClassRepeated(long UserID, int RepeatedCount)
        //{
        //    try
        //    {
        //        var User = unitOfWork.Users.Find(UserID);
        //        User.UserClassRepeated = RepeatedCount;
        //        unitOfWork.Users.Update(User);
        //        unitOfWork.Commit();
        //        return ExceptionHandler.GlobalSuccessResult();
        //    }
        //    catch (Exception ex)
        //    {
        //        return ExceptionHandler.GlobalExceptionResult(ex);
        //    }
        //}
        
        //----------------------------------------------------------------------------------------
        //public EXResult UpdateUserMaxClassCountInDay(long UserID, int MaxClassCountInDay)
        //{
        //    try
        //    {
        //        var User = unitOfWork.Users.Find(UserID);
        //        User.MaxPeriodCountInDay = MaxClassCountInDay;
        //        unitOfWork.Users.Update(User);
        //        unitOfWork.Commit();
        //        return ExceptionHandler.GlobalSuccessResult();
        //    }
        //    catch (Exception ex)
        //    {
        //        return ExceptionHandler.GlobalExceptionResult(ex);
        //    }
        //}
        ////----------------------------------------------------------------------------------------
        //public EXResult Update(ViewModels.UserViewModel userViewModel)
        //{
        //    EXResult result = new EXResult();

        //    //check if user name existing 
        //    if (unitOfWork.Users.Count(u => u.UserName == userViewModel.User.UserName && u.UserID != userViewModel.User.UserID) != 0)
        //    {
        //        result.Status = EXResultStatus.Fail;
        //        result.Message = DBResources.Security.ErrorUserNameExists;
        //        return result;
        //    }

        //    //1:Update User
        //    unitOfWork.Users.Update(userViewModel.User);

        //    //2:Update Roles 
        //    if (userViewModel.UserRoles != null)
        //    {
        //        //disable tracking changes
        //        ((DbContext)unitOfWork.Context).ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        //        //get all role IDs 
        //        var AllRoles = unitOfWork.Roles.Select(role => new { role.RoleID });
        //        //get user roles
        //        var UserRoles = unitOfWork.UserRoles.Where(UserRole => UserRole.UserID == userViewModel.User.UserID, role => new { role.RoleID });

        //        Models.UserRole userRole = new Models.UserRole();

        //        foreach (var role in AllRoles)
        //        {
        //            //existing in DB
        //            bool Exists = UserRoles.Any(obj => obj.RoleID == role.RoleID);
        //            bool Selected = userViewModel.UserRoles.Contains(role.RoleID);
        //            //Selected && Not Exisitng => Add
        //            if (Selected && !Exists)
        //            {
        //                userRole.UserID = userViewModel.User.UserID;
        //                userRole.RoleID = role.RoleID;
        //                unitOfWork.UserRoles.Add(userRole);
        //            }
        //            //Not Selected && Exisitng => Delete
        //            if (!Selected && Exists)
        //            {
        //                IEnumerable<Models.UserRole> UserRole = unitOfWork.UserRoles.Where(obj => obj.UserID == userViewModel.User.UserID && obj.RoleID == role.RoleID);
        //                unitOfWork.UserRoles.Remove(UserRole.First());
        //            }
        //        }

        //        //Enable Tracking Changes
        //         ((DbContext)unitOfWork.Context).ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        //    }

        //    //save
        //    unitOfWork.Commit();
        //    return result;
        //}
        ////-----------------------------------------------------------------------------------------
        //public EXResult Remove(long ID)
        //{
        //    EXResult result = new EXResult();
        //    Models.User User = unitOfWork.Users.Find(ID);
        //    unitOfWork.Users.Remove(User);
        //    unitOfWork.Commit();
        //    return result;
        //}

        //-----------------------------------------------------------------------------------------
        public EXResult ChangeTheme(long UserID, string Theme)
        {
            EXResult result = new EXResult();
            Models.User User = unitOfWork.Users.Find(UserID);
            User.Theme = Theme;
            unitOfWork.Commit();
            return result;
        }
        //-----------------------------------------------------------------------------------------
        public EXResult ChangeUILanguage(long UserID, long UILanguageID)
        {
            EXResult result = new EXResult();
            Models.User User = unitOfWork.Users.Find(UserID);
            User.UILanguageID = UILanguageID;
            unitOfWork.Commit();
            return result;
        }
        //-----------------------------------------------------------------------------------------
        //public EXResult SendSMSCirdintionls(long ID)
        //{
        //    EXResult result = new EXResult();
        //    SendSMS(ID);
        //    return result;
        //}
        //-----------------------------------------------------------------------------------------
        //public EXResult SendSMS(long UserID)
        //{
        //    EXResult result = new EXResult();
        //    var User = unitOfWork.Users.Find(UserID);
        //    Message messageservice = new Message(BranchID, Lang, this.UserID, APIURL);
        //    ViewModels.MessageDetails Receiver = new ViewModels.MessageDetails();
        //    Receiver.MobileNo = User.MobileNo;
        //    Receiver.RecevierName = Lang == "ar" ? User.LocalName : User.LatinName;
        //    Services.SysProperty sysProperty = new SysProperty(this.BranchID, Lang);
        //    string BaseUrl = sysProperty.GetValue((long)ViewModels.Shared.Enum.SysProperty.BaseUrl);
        //    Receiver.Text = string.Format(DBResources.Security.SMSCirdintionlDetails, DBResources.Security.DarsAccessData + Environment.NewLine, User.UserName + Environment.NewLine, DBResources.Security.Password, User.Password + Environment.NewLine, DBResources.Security.BaseURL, BaseUrl);
        //    result = messageservice.SendSingleMessage(null, DBResources.Security.SMSCirdintionlTitle, true, DateTime.Now, Core.MessageGateway.Enum.MessageType.Sms, Receiver);
        //    return result;
        //}

        //-----------------------------------------------------------------------------------------
        //public EXResult SendBulkSMSMessage(ViewModels.User.UserBulkSMS[] model)
        //{
        //    EXResult result = new EXResult();
        //    List<ViewModels.MessageDetails> messageDetails = new List<ViewModels.MessageDetails>();
        //    Message messageservice = new Message(BranchID, Lang, this.UserID, APIURL);
        //    Services.SysProperty sysProperty = new SysProperty(this.BranchID, Lang);
        //    string BaseUrl = sysProperty.GetValue((long)ViewModels.Shared.Enum.SysProperty.BaseUrl);
        //    foreach (var item in model)
        //    {
        //        if (item.LastLoginDate == "null")
        //        {
        //            item.LastLoginDate = "";
        //        }
        //        ViewModels.MessageDetails message = new ViewModels.MessageDetails
        //        {
        //            MobileNo = item.MobileNo,
        //            RecevierName = Lang == "ar" ? item.LocalName : item.LatinName,
        //            Text = string.Format(item.Message, " " + item.UserName.Split(' ')[0] + "  ", item.LastLoginDate.Split(' ')[0] + "  ")
        //        };
        //        messageDetails.Add(message);
        //    }
        //    if (messageDetails.Count == 1)
        //    {
        //        result = messageservice.SendSingleMessage(null, DBResources.UserMangement.ModalSMSTitle, true, DateTime.Now, Core.MessageGateway.Enum.MessageType.Sms, messageDetails.FirstOrDefault());
        //        return result;
        //    }
        //    else
        //        result = messageservice.SendMultiMessage(null, DBResources.UserMangement.ModalSMSTitle, true, DateTime.Now, Core.MessageGateway.Enum.MessageType.Sms, messageDetails);
        //    return result;
        //}
        //-----------------------------------------------------------------------------------------
        //public EXResult SendBulkSMS(ViewModels.User.UserBulkSMS[] model)
        //{
        //    EXResult result = new EXResult();
        //    List<ViewModels.MessageDetails> messageDetails = new List<ViewModels.MessageDetails>();
        //    Message messageservice = new Message(BranchID, Lang, this.UserID, APIURL);
        //    Services.SysProperty sysProperty = new SysProperty(this.BranchID, Lang);
        //    string BaseUrl = sysProperty.GetValue((long)ViewModels.Shared.Enum.SysProperty.BaseUrl);
        //    foreach (var item in model)
        //    {
        //        ViewModels.MessageDetails message = new ViewModels.MessageDetails
        //        {
        //            MobileNo = item.MobileNo,
        //            RecevierName = Lang == "ar" ? item.LocalName : item.LatinName,
        //            Text = string.Format(DBResources.Security.SMSCirdintionlDetails, DBResources.Security.DarsAccessData + Environment.NewLine, item.UserName + Environment.NewLine, DBResources.Security.Password, item.Password + Environment.NewLine, DBResources.Security.BaseURL, BaseUrl)
        //        };
        //        messageDetails.Add(message);
        //    }
        //    result = messageservice.SendMultiMessage(null, DBResources.Security.SMSCirdintionlTitle, true, DateTime.Now, Core.MessageGateway.Enum.MessageType.Sms, messageDetails);
        //    return result;
        //}
        //public List<long> GetUserBranch(long BrnID) => unitOfWork.UserBranch.Where(s => s.BranchID == BrnID, s => s.UserID).ToList();
        //public List<long> GetUserRoleList(long BrnID) => unitOfWork.UserBranch.Where(s => s.BranchID == BrnID, s => s.UserID).ToList();

        //public List<string> GetEmployeeUserGroupToken(List<long> GroupIDs) =>
        //    unitOfWork.PublicEmployeeGroupUser.Where(s => GroupIDs.Contains(s.UserID) && s.User.Token != null, s => s.User.Token).ToList();

        //public List<string> GetUserToken(List<long> UserIDs) => unitOfWork.Users.Where(s => UserIDs.Contains(s.UserID) && s.Token != null, s => s.Token).ToList();
        //public List<string> GetUserStudentToken(List<long> StudentIDs) => unitOfWork.Student.Where(s => StudentIDs.Contains(s.StudentID) && s.User.Token != null, s => s.User.Token).ToList();
        //public List<string> GetUserStageStudentToken(List<long> StageIDs) => unitOfWork.Student.Where(s => StageIDs.Contains(s.CurrentGrade.StageID) && s.User.Token != null, s => s.User.Token).ToList();
        //public List<ViewModels.List.UserToken> GetUserClassToken(List<long> ClassIDS, bool IncluidParent)
        //{
        //    var Data = unitOfWork.Student.Where(s => ClassIDS.Contains(s.ClassID.Value) && s.User.Token != null, s => new
        //    {
        //        Token = s.User.Token,
        //        ParentToken = s.Parent.User.Token,
        //    }).ToList();
        //    List<ViewModels.List.UserToken> TokenList = new List<ViewModels.List.UserToken>();
        //    TokenList.AddRange(Data.Select(s => new ViewModels.List.UserToken { Token = s.Token, IsParent = false }).Distinct().ToList());
        //    if (IncluidParent)
        //        TokenList.AddRange(Data.Select(s => new ViewModels.List.UserToken { Token = s.ParentToken, IsParent = true }).Distinct().ToList());
        //    return TokenList;
        //}
        //public List<ViewModels.List.UserToken> GetUserGroupToken(List<long> GroupIDs, bool IncluidParent)
        //{

        //    var Data = unitOfWork.StudentGroupMember.Where(s => GroupIDs.Contains(s.GroupID) && s.Student.User.Token != null, s => new { Token = s.Student.User.Token, ParentToken = s.Student.Parent.User.Token }).ToList();
        //    List<ViewModels.List.UserToken> TokenList = new List<ViewModels.List.UserToken>();
        //    TokenList.AddRange(Data.Select(s => new ViewModels.List.UserToken { Token = s.Token, IsParent = false }).Distinct().ToList());
        //    if (IncluidParent)
        //        TokenList.AddRange(Data.Select(s => new ViewModels.List.UserToken { Token = s.ParentToken, IsParent = true }).Distinct().ToList());
        //    return TokenList;
        //}
        ////-----------------------------------------------------------------------------------------
        //public EXResult ChangeIsActiveStatus(long ID)
        //{
        //    EXResult result = new EXResult();
        //    Models.User User = unitOfWork.Users.Find(ID);
        //    User.IsEnabled = !User.IsEnabled;
        //    unitOfWork.Users.Update(User);
        //    unitOfWork.Commit();

        //    return result;
        //}
        ////-----------------------------------------------------------------------------------------
        //public int Count()
        //{
        //    return unitOfWork.Users.Count();
        //}
        ////-----------------------------------------------------------------------------------------
        //public PagedList<Models.User> Select(string Filter, int PageIndex, int PageSize)
        //{
        //    PagedList<Models.User> PList = new PagedList<Models.User>();
        //    PList.PageIndex = PageIndex;
        //    PList.PageSize = PageSize;
        //    Expression<Func<Models.User, bool>> Predicate = obj =>
        //        obj.LocalName.Contains(Filter) ||
        //        obj.LatinName.Contains(Filter) ||
        //        obj.UserName.Contains(Filter) ||
        //        obj.Email.Contains(Filter) ||
        //         obj.MobileNo.Contains(Filter);
        //    PList.Data = unitOfWork.Users.Where(Predicate, o => o.OrderBy(obj => obj.UserID), PageIndex, PageSize);
        //    PList.TotalRecords = unitOfWork.Users.Count();
        //    PList.FilteredRecords = unitOfWork.Users.Count(Predicate);
        //    return PList;
        //}
        //-----------------------------------------------------------------------------------------
        //public IEnumerable<Models.Role> GetRoleList()
        //{
        //    var Data = unitOfWork.Roles.Select().ToList();
        //    return Data;
        //}
        //-----------------------------------------------------------------------------------------
        //public IEnumerable<Models.LookupItem> GetLookupRoleList()
        //{
        //    var Data = unitOfWork.Roles.Select(s => new Models.LookupItem
        //    {
        //        LocalName = s.LocalName,
        //        LatinName = s.LatinName,
        //        ID = s.RoleID

        //    }).ToList();
        //    return Data;
        //}
        //----------------------------------------------------------------------------------------
        //public IEnumerable<ViewModels.SP.User> GetUserInRole(long BranchID, long RoleID)
        //{
        //    SqlParameter PramBranchID = new SqlParameter("@BranchID", BranchID);
        //    SqlParameter PramRoleID = new SqlParameter("@RoleID", RoleID);
        //    SqlParameter PramUserID = new SqlParameter("@CurrentUserID", UserID);
        //    var Data = unitOfWork.Context.UserSP
        //        .FromSqlRaw("GetUserInRole_Sp @BranchID,@RoleID,@CurrentUserID", PramBranchID, PramRoleID, PramUserID);
        //    return Data.AsEnumerable().OrderBy(a => Lang == "ar" ? a.LocalName : a.LatinName);
        //}
        //----------------------------------------------------------------------------------------
        //public EXResult ChangeUserRolePassword(long RoleID, string Password)
        //{
        //    EXResult res = new EXResult();
        //    try
        //    {
        //        SqlParameter PramRoleID = new SqlParameter("@RoleID", RoleID);
        //        SqlParameter PramPassword = new SqlParameter("@Password", Password);
        //        int commit = unitOfWork.Context.Database.ExecuteSqlRaw("EXEC  [dbo].[User_ResetPassword] @RoleID,@Password", PramRoleID, PramPassword);
        //        res.Status = EXResultStatus.Success;
        //    }
        //    catch
        //    {
        //        res.Status = EXResultStatus.Fail;
        //    }
        //    return res;
        //}
       
        //----------------------------------------------------------------------------------------
        //public IEnumerable<Models.LookupItem> GetUserInRoleLookup(long BranchID, long RoleID)
        //{
        //    var Data = GetUserInRole(BranchID, RoleID).Select(s => new Models.LookupItem
        //    {
        //        ID = s.UserID,
        //        LocalName = s.LocalName,
        //        LatinName = s.LatinName
        //    }).OrderBy(a => Lang == "ar" ? a.LocalName : a.LatinName).ToList();
        //    return Data;
        //}
        //----------------------------------------------------------------------------------------
        //public IEnumerable<Models.LookupItem> GetUserLookupItem(long BranchID)
        //{
        //    var Data = unitOfWork.Users.Where(a => a.BranchID == BranchID && a.IsActive == true).Select(s => new Models.LookupItem
        //    {
        //        ID = s.UserID,
        //        LocalName = s.LocalName,
        //        LatinName = s.LatinName
        //    }).ToList();
        //    return Data;
        //}
        //----------------------------------------------------------------------------------------
        //public IEnumerable<Models.LookupItem> GetUserLookupByIDs(List<long> IDs)
        //{
        //    var Data = unitOfWork.Users.Where(a => a.BranchID == BranchID && a.IsActive == true && IDs.Contains(a.UserID)).Select(s => new Models.LookupItem
        //    {
        //        ID = s.UserID,
        //        LocalName = s.LocalName,
        //        LatinName = s.LatinName,
        //        MobileNo = s.MobileNo
        //    }).ToList();
        //    return Data;
        //}
        //----------------------------------------------------------------------------------------
        //public bool UsernameIsExits(long BranchID, string UserName)
        //{
        //    int count = unitOfWork.Users.Where(u => u.BranchID == BranchID
        //    && u.Id == UserName && u.IsActive == true).Count();
        //    return count > 0;
        //}
        ////-----------------------------------------------------------------------------------------
        //public PagedList<ViewModels.UserViewModel> GetLastLoginDate(string Filter, int PageIndex, int PageSize, int LastLoginDays, long? RoleID, string OrderColumn, string Dir)
        //{
        //    SqlParameter PramBranchID = new SqlParameter("@BranchID", this.BranchID);
        //    SqlParameter PramFilter = new SqlParameter("@Filter", Filter ?? (object)DBNull.Value);
        //    SqlParameter PramRoleID = new SqlParameter("@RoleID", RoleID ?? (object)DBNull.Value);
        //    SqlParameter PramLastLoginDays = new SqlParameter("@LastLoginDays", LastLoginDays);
        //    SqlParameter PramPageSize = new SqlParameter("@PageSize", PageSize);
        //    SqlParameter PramPageIndex = new SqlParameter("@PageIndex", PageIndex);
        //    SqlParameter PramDir = new SqlParameter("@Dir", Dir);
        //    SqlParameter PramOrderColumn = new SqlParameter("@OrderColumn", OrderColumn);
        //    PagedList<ViewModels.UserViewModel> PList = new PagedList<ViewModels.UserViewModel>();
        //    PList.PageIndex = PageIndex;
        //    PList.PageSize = PageSize;
        //    Expression<Func<Models.User, bool>> Predicate = (item => (item.LatinName.Contains(Filter) || item.LocalName.Contains(Filter) || item.MobileNo.Contains(Filter) || item.Id.Contains(Filter) || item.UserName.Contains(Filter)) && item.UserID == UserID && item.BranchID == BranchID);


        //    PList.Data = unitOfWork.Context.UserSP.FromSqlRaw("GetUserLastLoginDate_WithPagination @BranchID,@LastLoginDays,@RoleID,@Filter,@PageSize,@PageIndex,@Dir,@OrderColumn",
        //     PramBranchID, PramLastLoginDays, PramRoleID, PramFilter, PramPageSize, PramPageIndex, PramDir, PramOrderColumn).AsEnumerable().Select(s => new ViewModels.UserViewModel
        //     {
        //         LastLoginDate = s.LastLoginDate,
        //         IsActive = s.IsActive,
        //         LatinName = s.LatinName,
        //         LocalName = s.LocalName,
        //         MobileNo = s.MobileNo,
        //         UserID = s.UserID,
        //         IsEnabled = s.IsEnabled,
        //         Password = s.Password,
        //         UserName = s.UserName,
        //     });
        //    var filter = unitOfWork.Context.Count.FromSqlRaw("FilterUserLastLoginDate @BranchID,@LastLoginDays,@RoleID,@Filter", PramBranchID, PramLastLoginDays, PramRoleID, PramFilter).AsEnumerable().FirstOrDefault();
        //    PList.FilteredRecords = filter.Count;
        //    PList.TotalRecords = PList.Data.Count();
        //    return PList;
        //}
        ////-----------------------------------------------------------------------------------------
        //public PagedList<ViewModels.UserViewModel> GetList(string Filter, int PageIndex, int PageSize, long? RoleID, long? GradeID, long? ClassID, string OrderColumn, string Dir)
        //{
        //    SqlParameter PramBranchID = new SqlParameter("@BranchID", this.BranchID);
        //    SqlParameter PramUserID = new SqlParameter("@UserID", this.UserID);
        //    SqlParameter PramFilter = new SqlParameter("@Filter", Filter ?? (object)DBNull.Value);
        //    SqlParameter PramRoleID = new SqlParameter("@RoleID", RoleID ?? (object)DBNull.Value);
        //    SqlParameter PramGradeID = new SqlParameter("@GradeID", GradeID ?? (object)DBNull.Value);
        //    SqlParameter PramClassID = new SqlParameter("@ClassID", ClassID ?? (object)DBNull.Value);
        //    SqlParameter PramPageSize = new SqlParameter("@PageSize", PageSize);
        //    SqlParameter PramPageIndex = new SqlParameter("@PageIndex", PageIndex);
        //    SqlParameter PramDir = new SqlParameter("@Dir", Dir);
        //    SqlParameter PramOrderColumn = new SqlParameter("@OrderColumn", OrderColumn);
        //    PagedList<ViewModels.UserViewModel> PList = new PagedList<ViewModels.UserViewModel>();
        //    PList.PageIndex = PageIndex;
        //    PList.PageSize = PageSize;
        //    Expression<Func<Models.User, bool>> Predicate = (item => (item.LatinName.Contains(Filter) || item.LocalName.Contains(Filter) || item.MobileNo.Contains(Filter) || item.Id.Contains(Filter) || item.UserName.Contains(Filter)) && item.UserID == UserID && item.BranchID == BranchID);
        //    PList.Data = unitOfWork.Context.UserSP.FromSqlRaw("GetUserInRole_WithPagination @BranchID,@RoleID,@GradeID,@ClassID,@Filter,@PageSize,@PageIndex,@Dir,@OrderColumn,@UserID",
        //    PramBranchID, PramRoleID, PramGradeID, PramClassID, PramFilter, PramPageSize, PramPageIndex, PramDir, PramOrderColumn, PramUserID).AsEnumerable().Select(s => new ViewModels.UserViewModel
        //    {
        //        LastLoginDate = s.LastLoginDate,
        //        IsActive = s.IsActive,
        //        LatinName = s.LatinName,
        //        LocalName = s.LocalName,
        //        MobileNo = s.MobileNo,
        //        UserID = s.UserID,
        //        IsEnabled = s.IsEnabled,
        //        Password = s.Password,
        //        UserName = s.UserName,
        //        VClassUrl = s.VClassUrl,
        //        Email = s.Email
        //    });
        //    var filter = unitOfWork.Context.Count.FromSqlRaw("FilterUserInRole @BranchID,@RoleID,@GradeID,@ClassID,@Filter", PramBranchID, PramRoleID, PramGradeID, PramClassID, PramFilter).AsEnumerable().FirstOrDefault();
        //    PList.FilteredRecords = filter.Count;
        //    PList.TotalRecords = filter.Count;
        //    return PList;
        //}
        ////-----------------------------------------------------------------------------------------
        //public EXResult ChangePassword(long UserID, string Password)
        //{
        //    EXResult result = new EXResult();
        //    try
        //    {
        //        Models.User User = unitOfWork.Users.Find(UserID);
        //        User.Password = Password;
        //        unitOfWork.Users.Update(User);
        //        unitOfWork.Commit();
        //        return result;
        //    }
        //    catch
        //    {
        //        return result;
        //    }
        //}
        ////-----------------------------------------------------------------------------------------
        //public EXResult UpdateDevice(long UserID, string DeviceID)
        //{
        //    EXResult result = new EXResult();
        //    try
        //    {
        //        Models.User User = unitOfWork.Users.Find(UserID);
        //        if (string.IsNullOrEmpty(User.DeviceID))
        //        {
        //            User.DeviceID = DeviceID;
        //            unitOfWork.Users.Update(User);
        //            unitOfWork.Commit();
        //        }
        //        return result;
        //    }
        //    catch
        //    {
        //        return result;
        //    }
        //}
        //public EXResult UpdateToken(long UserID, string Token)
        //{
        //    EXResult result = new EXResult();
        //    try
        //    {
        //        Models.User User = unitOfWork.Users.Find(UserID);
        //        User.Token = Token;
        //        unitOfWork.Users.Update(User);
        //        unitOfWork.Commit();
        //        return ExceptionHandler.GlobalSuccessResult();
        //    }
        //    catch
        //    {
        //        return result;
        //    }
        //}
        //public EXResult UpdateUserPhoto(long UserID, string FileName)
        //{
        //    try
        //    {
        //        var User = unitOfWork.Users.Find(UserID);
        //        User.PhotoName = FileName;
        //        unitOfWork.Users.Update(User);
        //        unitOfWork.Commit();
        //        return ExceptionHandler.GlobalSuccessResult();
        //    }
        //    catch (Exception ex)
        //    {
        //        return ExceptionHandler.GlobalExceptionResult(ex);
        //    }
        //}
        //public EXResult UpdateUserSignature(long UserID, string FileName)
        //{
        //    try
        //    {
        //        var User = unitOfWork.Users.Find(UserID);
        //        User.SignatureFileName = FileName;
        //        unitOfWork.Users.Update(User);
        //        unitOfWork.Commit();
        //        return ExceptionHandler.GlobalSuccessResult();
        //    }
        //    catch (Exception ex)
        //    {
        //        return ExceptionHandler.GlobalExceptionResult(ex);
        //    }
        //}
        //-----------------------------------------------------------------------------------------
        //public ViewModels.UserInfo GetUserInfo(long ID)
        //{
        //    var UserRole = new Role().GetUserRole(ID);
        //    ViewModels.UserInfo model = new ViewModels.UserInfo();
        //    if (UserRole.Contains((long)ViewModels.Shared.Enum.Role.Parent))
        //    {
        //        model = unitOfWork.Parent.Where(s => s.UserID == ID, item => new ViewModels.UserInfo
        //        {
        //            EmployeeNo = "",
        //            UserID = ID,
        //            IdentityNo = item.IdentityNo,
        //            Img = item.User.PhotoName,
        //            LatinName = item.User.LatinName,
        //            LocalName = item.User.LocalName,
        //            MobileNo = item.MobileNo,
        //            NationName = item.Nation != null ? Core.UI.Locals.IsLocal ? item.Nation.LocalName : item.Nation.LatinName : "",
        //            SignatureFileName = item.User.SignatureFileName

        //        }).FirstOrDefault();
        //    }
        //    if (UserRole.Contains((long)ViewModels.Shared.Enum.Role.Student))
        //    {
        //        model = unitOfWork.Student.Where(s => s.UserID == ID, item => new ViewModels.UserInfo
        //        {
        //            EmployeeNo = item.User.Id,
        //            IdentityNo = item.IdentityNo,
        //            UserID = ID,
        //            StudentID = item.StudentID,
        //            Img = item.User.PhotoName,
        //            LatinName = item.User.LatinName,
        //            LocalName = item.User.LocalName,
        //            MobileNo = item.MobileNo,
        //            StudnetGrade = item.CurrentGrade != null ? Core.UI.Locals.IsLocal ? item.CurrentGrade.LocalName : item.CurrentGrade.LatinName : "",
        //            StudnetStage = item.CurrentGrade != null ? Core.UI.Locals.IsLocal ? item.CurrentGrade.Stage.LocalName : item.CurrentGrade.Stage.LatinName : "",
        //            StudnetClass = item.Class != null ? Core.UI.Locals.IsLocal ? item.Class.LocalName : item.Class.LatinName : "",
        //            NationName = item.Nation != null ? Core.UI.Locals.IsLocal ? item.Nation.LocalName : item.Nation.LatinName : "",
        //            SignatureFileName = item.User.SignatureFileName
        //        }).FirstOrDefault();

        //        model.StudnetSpicialCaseList = new SpecialCase(BranchID).GetStudnetSpcialCaseList(model.StudentID).ToList();
        //    }
        //    else
        //    {
        //        model = unitOfWork.Employee.Where(s => s.UserID == ID, item => new ViewModels.UserInfo
        //        {
        //            EmployeeNo = item.EmployeeNo,
        //            IdentityNo = item.IdentityNo,
        //            Img = item.User.PhotoName,
        //            UserID = ID,
        //            LatinName = item.User.LatinName,
        //            LocalName = item.User.LocalName,
        //            MobileNo = item.MobileNo,
        //            NationName = item.Nation != null ? Core.UI.Locals.IsLocal ? item.Nation.LocalName : item.Nation.LatinName : "",
        //            SignatureFileName = item.User.SignatureFileName

        //        }).FirstOrDefault();
        //        if (model == null)
        //        {
        //            model = unitOfWork.Users.Where(s => s.UserID == ID, item => new ViewModels.UserInfo
        //            {
        //                EmployeeNo = item.Id,
        //                IdentityNo = item.UserName,
        //                Img = item.PhotoName,
        //                UserID = ID,
        //                LatinName = item.LatinName,
        //                LocalName = item.LocalName,
        //                MobileNo = item.MobileNo,
        //                NationName = "",
        //                SignatureFileName = item.SignatureFileName

        //            }).FirstOrDefault();
        //        }
        //    }
        //    model.FunctionName = "Usersetting.editUserSignature()";
        //    return model;
        //}
        //public bool IsAccessToBrn(long UserID, long BranchID)
        //{
        //    var UserBrn = unitOfWork.UserBranch.Where(s => s.UserID == UserID, s => s.BranchID).ToList();
        //    if (UserBrn.Contains(BranchID))
        //        return true;
        //    else
        //        return false;
        //}
        //public ViewModels.UserViewModel GetTestAppUser()
        //{
        //    var Data = unitOfWork.Users.Where(s => s.UserName == "app_test" && s.IsActive, s => new ViewModels.UserViewModel
        //    {
        //        UserName = s.UserName,
        //        LocalName = s.LocalName,
        //        LatinName = s.LatinName,
        //        MobileNo = s.MobileNo,
        //        UserID = s.UserID,
        //        Password = s.Password,
        //        IsActive = s.IsActive,
        //    }).FirstOrDefault();
        //    return Data;
        //}
        ////-----------------------------------------------------------------------------------------
        //public EXResult DisableUser(long UserID)
        //{
        //    try
        //    {
        //        var Data = unitOfWork.Users.Where(s => s.UserID == UserID && s.IsActive).FirstOrDefault();
        //        Data.IsActive = !Data.IsActive;
        //        Data.IsEnabled = false;
        //        unitOfWork.Users.Update(Data);
        //        unitOfWork.Commit();
        //        return ExceptionHandler.GlobalSuccessResult();
        //    }
        //    catch (Exception ex)
        //    {
        //        return ExceptionHandler.GlobalFailedResult();
        //    }
        //}
        ////-----------------------------------------------------------------------------------------
        //public Models.User GetByID(long UserId)
        //{
        //    var Data = unitOfWork.Users.Find(UserId);
        //    return Data;
        //}
        ////-----------------------------------------------------------------------------------------
        //public EXResult ChangePhoto(string Identity, string PhotoName)
        //{
        //    EXResult result = new EXResult();
        //    try
        //    {
        //        Models.User model = unitOfWork.Users.Where(u => u.Id == Identity).FirstOrDefault();
        //        model.PhotoName = PhotoName;
        //        unitOfWork.Users.Update(model);
        //        unitOfWork.Commit();
        //        result.Message = DBResources.CoreMessage.SaveSuccess;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Status = EXResultStatus.Fail;
        //        result.Message = ex.Message;
        //    }
        //    return result;
        //}
        ////-----------------------------------------------------------------------------------------
        //public EXResult Subscribe(long UserID, string MobileToken)
        //{
        //    EXResult result = new EXResult();
        //    try
        //    {
        //        Models.User model = unitOfWork.Users.Find(UserID);
        //        List<ViewModels.API.FirebaseToken> TokenList = new List<ViewModels.API.FirebaseToken>();

        //        if (model.Token.Length > 0)
        //        {
        //            TokenList = JsonSerializer.Deserialize<List<ViewModels.API.FirebaseToken>>(model.Token);

        //        }
        //        TokenList.Add(new ViewModels.API.FirebaseToken
        //        {
        //            MobileToken = MobileToken
        //        });
        //        model.Token = JsonSerializer.Serialize(TokenList);
        //        unitOfWork.Users.Update(model);
        //        unitOfWork.Commit();
        //        result.Message = DBResources.CoreMessage.SaveSuccess;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Status = EXResultStatus.Fail;
        //        result.Message = ex.Message;
        //    }
        //    return result;
        //}
        ////-----------------------------------------------------------------------------------------
        //public EXResult UnSubscribe(long UserID, string MobileToken)
        //{
        //    EXResult result = new EXResult();
        //    try
        //    {
        //        Models.User model = unitOfWork.Users.Find(UserID);
        //        List<ViewModels.API.FirebaseToken> TokenList = new List<ViewModels.API.FirebaseToken>();

        //        if (model.Token.Length > 0)
        //        {
        //            TokenList = JsonSerializer.Deserialize<List<ViewModels.API.FirebaseToken>>(model.Token);
        //        }

        //        ViewModels.API.FirebaseToken token = TokenList.FirstOrDefault(t => t.MobileToken == MobileToken);
        //        if (token != null)
        //        {
        //            TokenList.Remove(token);
        //        }

        //        model.Token = TokenList.Count > 0 ? JsonSerializer.Serialize(TokenList) : "";
        //        unitOfWork.Users.Update(model);
        //        unitOfWork.Commit();
        //        result.Message = DBResources.CoreMessage.SaveSuccess;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Status = EXResultStatus.Fail;
        //        result.Message = ex.Message;
        //    }
        //    return result;
        //}
        ////-----------------------------------------------------------------------------------------
        //public ViewModels.UserViewModel InitUserlogin()
        //{
        //    ViewModels.UserViewModel model = new ViewModels.UserViewModel();
        //    model.RoleList = GetRoleList();
        //    return model;
        //}
        ////-----------------------------------------------------------------------------------------
        //public IEnumerable<ViewModels.UserLogin> GetUserLogins(long RoleID)
        //{
        //    var Data = unitOfWork.Users.Where(a => a.UserRoles.Select(z => z.RoleID).Contains(RoleID) && a.IsActive == true).
        //        Select(s => new ViewModels.UserLogin
        //        {
        //            Name = Lang == "ar" ? s.LocalName : s.LatinName,
        //            LoginDate = s.LastLoginDate,
        //            Password = s.Password,
        //            UserName = s.UserName
        //        }).ToList();
        //    return Data.OrderBy(a => a.Name);
        //}
        //public List<long> GetUserStageList()
        //{
        //    List<long> StageIDs = unitOfWork.UserStage.Where(s => s.BranchID == BranchID && s.UserID == UserID,
        //        s => s.StageID).ToList();
        //    List<long> UserStage = unitOfWork.UserStage.Where(s => StageIDs.Contains(s.StageID), s => s.UserID).ToList();
        //    return UserStage;
        //}
        //-----------------------------------------------------------------------------------------
        //public IEnumerable<Models.LookupItem> GetUsersByStage(long StageID, bool IsLocal)
        //{
        //    return unitOfWork.UserStage.Where(s => s.StageID == StageID && s.User.IsActive == true,
        //        item => new Models.LookupItem
        //        {
        //            ID = item.UserID,
        //            LocalName = item.User.LocalName,
        //            LatinName = item.User.LatinName,
        //        }, x => x.OrderBy(obj => IsLocal ? obj.User.LocalName : obj.User.LatinName));
        //}
        //public IEnumerable<Models.LookupItem> GetTeacherStage(long? StageID)
        //{
        //    var UserRole = unitOfWork.UserRoles.Where(s => s.RoleID == (long)ViewModels.Shared.Enum.Role.Teacher, s => s.UserID).ToList();
        //    if (StageID == null)
        //        return unitOfWork.Users.Where(s => UserRole.Contains(s.UserID)
        //    && s.IsActive,
        //        item => new Models.LookupItem
        //        {
        //            ID = item.UserID,
        //            LocalName = item.LocalName,
        //            LatinName = item.LocalName,
        //        }, x => x.OrderBy(obj => Core.UI.Locals.IsLocal ? obj.LocalName : obj.LatinName)).GroupBy(s => s.ID).Select(s => s.FirstOrDefault()).ToList();
        //    else
        //        return unitOfWork.UserStage.Where(s => (StageID == null || s.StageID == StageID.Value) && UserRole.Contains(s.UserID)
        //        && s.User.IsActive,
        //            item => new Models.LookupItem
        //            {
        //                ID = item.UserID,
        //                LocalName = item.User.LocalName,
        //                LatinName = item.User.LatinName,
        //            }, x => x.OrderBy(obj => Core.UI.Locals.IsLocal ? obj.User.LocalName : obj.User.LatinName)).GroupBy(s => s.ID).Select(s => s.FirstOrDefault()).ToList();
        //}
        ////-----------------------------------------------------------------------------------------
        //public EXResult ChangeUserActive(string FilePath, bool Case)
        //{
        //    EXResult result = new EXResult();
        //    var Data = FilePath.FromExcelToDataTable(3, 1, 2, 1);
        //    System.Data.DataTable dataTable = new System.Data.DataTable();
        //    dataTable.TableName = "dbo.ListOfUserIDS";
        //    dataTable.Columns.Add("IdentityNo", typeof(string));
        //    try
        //    {
        //        for (int i = 0; i < Data.Rows.Count; i++)
        //        {
        //            DataRow DR = dataTable.NewRow();
        //            DR["IdentityNo"] = Data.Rows[i]["IdentityNo"].ToString();
        //            dataTable.Rows.Add(DR);
        //        }
        //        SqlParameter PramUser = new SqlParameter("@ListOfUserIDS", SqlDbType.Structured);
        //        SqlParameter PramCase = new SqlParameter("@Case", Case);
        //        PramUser.TypeName = "dbo.ListOfUserIDS";
        //        PramUser.Value = dataTable;
        //        unitOfWork.Context.Database.ExecuteSqlRaw("EXEC [dbo].[UserManagement_EnableDisableUser] @ListOfUserIDS,@Case", PramUser, PramCase);
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Status = EXResultStatus.Fail;
        //        result.Message = ex.Message;
        //        return result;
        //    }
        //    return result;
        //}
        ////-----------------------------------------------------------------------------------------
        //public List<ViewModels.List.UserLog> GetUserLog(long UserID)
        //{
        //    var Data = unitOfWork.DisplayedPasswordLog.Where(s => s.DisplayedPasswordUserID == UserID, s => new ViewModels.List.UserLog
        //    {
        //        LogDate = s.LogDate,
        //        UserName = Core.UI.Locals.IsLocal ? s.User.LocalName : s.User.LatinName,
        //        ID = s.ID

        //    }).ToList();
        //    return Data;
        //}
        ////-----------------------------------------------------------------------------------------
        //public IEnumerable<ViewModels.List.UserList> LoadUserFromExcelFile(string FilePath)
        //{
        //    try
        //    {
        //        var Data = FilePath.FromExcelToDataTable(3, 1, 2, 1);
        //        System.Data.DataTable dataTable = new System.Data.DataTable();
        //        dataTable.Columns.Add("IdentityNo", typeof(string));
        //        dataTable.Columns.Add("StudentName", typeof(string));
        //        for (int i = 0; i < Data.Rows.Count; i++)
        //        {
        //            DataRow DR = dataTable.NewRow();
        //            DR["IdentityNo"] = Data.Rows[i]["IdentityNo"].ToString();
        //            DR["StudentName"] = Data.Rows[i]["StudentName"].ToString();
        //            dataTable.Rows.Add(DR);
        //        }
        //        var UserData = ConvertDataTable<ViewModels.List.UserList>(dataTable);
        //        return UserData;
        //    }
        //    catch
        //    {
        //        return new List<ViewModels.List.UserList>();
        //    }
        //}
        //-----------------------------------------------------------------------------------------
        //private static List<ViewModels.List.UserList> ConvertDataTable<T>(DataTable dt)
        //{
        //    List<ViewModels.List.UserList> data = new List<ViewModels.List.UserList>();
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        ViewModels.List.UserList item = GetStringItem<ViewModels.List.UserList>(row);
        //        data.Add(item);
        //    }
        //    return data;
        //}
        //-----------------------------------------------------------------------------------------
        //private static T GetStringItem<T>(DataRow dr)
        //{
        //    Type temp = typeof(T);
        //    T obj = Activator.CreateInstance<T>();

        //    foreach (DataColumn column in dr.Table.Columns)
        //    {
        //        foreach (PropertyInfo pro in temp.GetProperties())
        //        {
        //            if (pro.Name == column.ColumnName)
        //                pro.SetValue(obj, dr[column.ColumnName], null);
        //            else
        //                continue;
        //        }
        //    }
        //    return obj;
        //}
        //-----------------------------------------------------------------------------------------
        //public List<ViewModels.ChatUsersVM> GetChatUsers()
        //{
        //    var branchUsersIds = GetUserBranch(BranchID);

        //    return unitOfWork.Employee.Where(a => a.UserID != UserID && a.User.IsActive && a.User.IsEnabled && branchUsersIds.Contains(a.UserID), a =>
        //    new ViewModels.ChatUsersVM
        //    {
        //        UserID = a.UserID,
        //        LocalName = a.LocalName,
        //        LatinName = a.LatinName,
        //        UserName = a.User.UserName,
        //        Email = a.Email,
        //        PhotoName = a.User.PhotoName,
        //        IDCardTitle = a.IDCardTitle
        //    }).GroupBy(s => s.UserID).Select(s => s.FirstOrDefault()).ToList();
        //}
        //-----------------------------------------------------------------------------------------
        //public List<ViewModels.ChatUsersVM> GetTeacherChatUsers(List<long> exceptionRoleIDs)
        //{
        //    List<ViewModels.ChatUsersVM> UserList = new List<ViewModels.ChatUsersVM>();
        //    List<long> UserClass = new List<long>();
        //    var branchUsersIds = GetUserBranch(BranchID);
        //    UserClass.AddRange(new ClassTimeTable(BranchID, UserID).GetUserClassList(UserID).ToList());
        //    UserClass.AddRange(new VClassTimeTable(BranchID, UserID).GetUserClassList(UserID).ToList());
        //    var StudentList = unitOfWork.Student.Where(s => s.IsActive && s.AdmissionStatusID == (long)ViewModels.Shared.Enum.AdmissionStatus.Accepted && UserClass.Distinct().ToList().Contains(s.ClassID.Value), s => new ViewModels.ChatUsersVM
        //    {
        //        UserID = s.UserID.Value,
        //        Email = s.User.Email,
        //        LocalName = s.LocalName,
        //        LatinName = s.LatinName,
        //        UserName = s.User.UserName,
        //        PhotoName = s.User.PhotoName,
        //        ClassLocalName = s.Class.LocalName,
        //        ClassLatinName = s.Class.LatinName
        //    }).ToList();
        //    var ParentList = unitOfWork.Student.Where(s => s.IsActive && s.AdmissionStatusID == (long)ViewModels.Shared.Enum.AdmissionStatus.Accepted && UserClass.Distinct().ToList().Contains(s.ClassID.Value), s => new ViewModels.ChatUsersVM
        //    {
        //        UserID = s.Parent.UserID,
        //        Email = s.Parent.User.Email,
        //        LocalName = s.Parent.LocalName,
        //        LatinName = s.Parent.LatinName,
        //        UserName = s.Parent.User.UserName,
        //        PhotoName = s.Parent.User.PhotoName,
        //        ClassLocalName = s.Class.LocalName,
        //        ClassLatinName = s.Class.LatinName
        //    }).ToList();
        //    var AdminUserList = GetChatUsers();
        //    UserList.AddRange(StudentList);
        //    UserList.AddRange(ParentList);
        //    UserList.AddRange(AdminUserList);
        //    return UserList.Where(s => s.UserID != UserID).GroupBy(s => s.UserID).Select(s => s.FirstOrDefault()).ToList();
        //}
        ////-----------------------------------------------------------------------------------------
        //public List<ViewModels.ChatUsersVM> GetStudnetChatUsers()
        //{
        //    List<long> UserClass = new List<long>();
        //    List<ViewModels.ChatUsersVM> UserList = new List<ViewModels.ChatUsersVM>();
        //    var StudentInfo = new Student(BranchID).GetStudentByUser(UserID);
        //    UserClass.Add(StudentInfo.ClassID.Value);
        //    UserClass.AddRange(unitOfWork.GroupStudentClass.Where(s => s.Class.IsActive && s.StudentID == StudentInfo.StudentID, s => s.ClassID));

        //    var TeacherListClassTime = unitOfWork.ClassTimeTable.Where(s => s.Class.IsActive && UserClass.Distinct().ToList().Contains(s.ClassID), s => new ViewModels.ChatUsersVM
        //    {
        //        UserID = s.UserID,
        //        Email = s.User.Email,
        //        LocalName = s.User.LocalName,
        //        LatinName = s.User.LatinName,
        //        UserName = s.User.UserName,
        //        PhotoName = s.User.PhotoName,
        //    }).ToList();
        //    var TeacherListVClassTime = unitOfWork.VClassTimeTable.Where(s => s.Class.IsActive && UserClass.Distinct().ToList().Contains(s.ClassID), s => new ViewModels.ChatUsersVM
        //    {
        //        UserID = s.UserID,
        //        Email = s.User.Email,
        //        LocalName = s.User.LocalName,
        //        LatinName = s.User.LatinName,
        //        UserName = s.User.UserName,
        //        PhotoName = s.User.PhotoName,
        //    }).ToList();

        //    UserList.AddRange(TeacherListClassTime);
        //    UserList.AddRange(TeacherListVClassTime);
        //    return UserList.Where(s => s.UserID != UserID).GroupBy(s => s.UserID).Select(s => s.FirstOrDefault()).ToList();
        //}
        ////-----------------------------------------------------------------------------------------
        //public List<ViewModels.ChatUsersVM> GetParentChatUsers()
        //{
        //    List<long> UserClass = new List<long>();
        //    List<ViewModels.ChatUsersVM> UserList = new List<ViewModels.ChatUsersVM>();
        //    UserClass = new Parent(BranchID, UserID).GetChildrenClassByParent(UserID);
        //    var TeacherListClassTime = unitOfWork.ClassTimeTable.Where(s => UserClass.Distinct().ToList().Contains(s.ClassID), s => new ViewModels.ChatUsersVM
        //    {
        //        UserID = s.UserID,
        //        Email = s.User.Email,
        //        LocalName = s.User.LocalName,
        //        LatinName = s.User.LatinName,
        //        UserName = s.User.UserName,
        //        PhotoName = s.User.PhotoName,

        //    }).ToList();
        //    var TeacherListVClassTime = unitOfWork.VClassTimeTable.Where(s => UserClass.Distinct().ToList().Contains(s.ClassID), s => new ViewModels.ChatUsersVM
        //    {
        //        UserID = s.UserID,
        //        Email = s.User.Email,
        //        LocalName = s.User.LocalName,
        //        LatinName = s.User.LatinName,
        //        UserName = s.User.UserName,
        //        PhotoName = s.User.PhotoName,
        //    }).ToList();

        //    UserList.AddRange(TeacherListClassTime);
        //    UserList.AddRange(TeacherListVClassTime);
        //    return UserList.Where(s => s.UserID != UserID).GroupBy(s => s.UserID).Select(s => s.FirstOrDefault()).ToList();
        //}
        //-----------------------------------------------------------------------------------------
    }
    //\////////////////////////////////////////////////////////////////////////////////////////////
}
//\////////////////////////////////////////////////////////////////////////////////////////////////
