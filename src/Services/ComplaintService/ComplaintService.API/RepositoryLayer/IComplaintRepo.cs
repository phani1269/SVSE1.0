using ComplaintService.API.DataLayer.Models;

namespace ComplaintService.API.RepositoryLayer
{
    public interface IComplaintRepo
    {
        Task<ComplaintModel> CreateComplaint(ComplaintModel model);
        Task<IEnumerable<ComplaintModel>> GetAllComplaints();
        Task<ComplaintModel> GetComplaintByUsername(string userName);
        //Task<bool> UpdateComplaint(string userName);
        Task<bool> DeleteComplaint(string userName);
    }
}
