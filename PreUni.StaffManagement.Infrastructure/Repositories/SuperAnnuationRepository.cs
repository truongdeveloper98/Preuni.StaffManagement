using PreUni.StaffManagement.Core.Interfaces.Repositories;
using PreUni.StaffManagement.Domain.Entities;
using PreUni.StaffManagement.Infrastructure.EntityFramework;

namespace PreUni.StaffManagement.Infrastructure.Repositories;

public class SuperAnnuationRepository : RepositoryBase<SuperAnnuation>, ISuperAnnuationRepository
{
    public SuperAnnuationRepository(ApplicationDbContext context) : base(context)
    {
        
    }
}