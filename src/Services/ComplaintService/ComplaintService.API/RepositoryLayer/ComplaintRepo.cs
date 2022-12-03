using ComplaintService.API.DataLayer.Contexts;
using ComplaintService.API.DataLayer.Models;

namespace ComplaintService.API.RepositoryLayer
{
    public class ComplaintRepo : IComplaintRepo
    {
        private readonly ComplaintDbContext _context;

        public ComplaintRepo(ComplaintDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<ComplaintModel> CreateComplaint(ComplaintModel model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteComplaint(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ComplaintModel>> GetAllComplaints()
        {
            throw new NotImplementedException();
        }

        public Task<ComplaintModel> GetComplaintByUsername(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateComplaint(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
