//// IUserService.cs
//using Meditrack.Repository.IRepository;

//public interface IUserService
//{
//    string GetUserIdByPRId(int prId);
//}

//// UserService.cs
//public class UserService : IUserService
//{
//    private readonly IUnitOfWork _unitOfWork;

//    public UserService(IUnitOfWork unitOfWork)
//    {
//        _unitOfWork = unitOfWork;
//    }

//    public string GetUserIdByPRId(int prId)
//    {
//        var prHeader = _unitOfWork.PurchaseRequisitionHeader.GetFirstOrDefault(prh => prh.PRHdrID == prId);
//        return prHeader?.CreatedByUserId;
//    }
//}
