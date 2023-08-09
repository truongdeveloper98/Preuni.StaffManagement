using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection;
using System.Linq.Expressions;
using PreUni.StaffManagement.Domain.Entities;
using Microsoft.Extensions.Hosting;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace PreUni.StaffManagement.Infrastructure.EntityFramework
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<AppUser> AppUsers { get; set; }
        public new DbSet<User> Users { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<BankAccountDetail> BankAccountDetails { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<TFNDeclare> TFNDeclares { get; set; }
        public DbSet<ParentConsent> ParentConsents { get; set; }
        public DbSet<SuperAnnuation> SuperAnnuations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<JobApplication>()
                           .HasOne(x => x.BankAccountDetail)
                           .WithOne(i => i.JobApplication);

            builder.Entity<JobApplication>()
                          .HasOne(x => x.TFNDeclare)
                          .WithOne(i => i.JobApplication);

            builder.Entity<User>()
                         .HasOne(x => x.JobApplication)
                         .WithOne(i => i.User);


            AddSoftDeleteFilters(builder);

            SeedData(builder);

        }
        private static void AddSoftDeleteFilters(ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                //other automated configurations left out
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    entityType.AddSoftDeleteQueryFilter();
                }
            }
        }


        /// <summary>
        /// Seed data for AppUser
        /// </summary>
        /// <returns></returns>

        private static void SeedData(ModelBuilder builder)
        {
            #region seed data for identity role
            // role admin
            builder.Entity<AppRole>().HasData(new AppRole { Id = 1, Name = "Administrator", NormalizedName = "ADMINISTRATOR".ToUpper() });

            //role applicant
            builder.Entity<AppRole>().HasData(new AppRole { Id = 2, Name = "Applicant", NormalizedName = "APPLICANT".ToUpper() });
            #endregion

            #region seed data for identity user
            // create new user admin
            var appUser = new AppUser
            {
                Id = 1,
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                NormalizedUserName = "ADMIN",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            // create password for user admin
            var hasher = new PasswordHasher<AppUser>();
            appUser.PasswordHash = hasher.HashPassword(appUser, "admin");

            // seed data
            builder.Entity<AppUser>().HasData(appUser);


            // create new user applicant
            var userApplicant = new AppUser
            {
                Id = 2,
                UserName = "applicant@gmail.com",
                Email = "applicant@gmail.com",
                NormalizedUserName = "APLLICANT",
                NormalizedEmail = "APLLICANT@GMAIL.COM",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            // create password for user applicant
            var hasherApplicant = new PasswordHasher<AppUser>();
            userApplicant.PasswordHash = hasherApplicant.HashPassword(userApplicant, "applicant");

            builder.Entity<AppUser>().HasData(userApplicant);
            #endregion

            #region seed data for identity userrole
            builder.Entity<IdentityUserRole<int>>().HasData(
            new IdentityUserRole<int>
            {
                RoleId = 1,
                UserId = 1
            });

            builder.Entity<IdentityUserRole<int>>().HasData(
            new IdentityUserRole<int>
            {
                RoleId = 2,
                UserId = 2
            });
            #endregion

            #region seed data for entity user
            builder.Entity<User>().HasData(
            new User
            {
                FirstName = "Admin",
                LastName = "Admin",
                Id = 1,
                IdentityId = 1,
                FullName = "Admin",
                Image = string.Empty,
            });
			#endregion
		}

		public override int SaveChanges()
        {
            UpdateEntityState();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateEntityState();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateEntityState()
        {
            var now = DateTime.UtcNow;

            foreach (var changedEntity in ChangeTracker.Entries())
            {
                if (changedEntity.Entity is BaseEntity entity)
                {
                    switch (changedEntity.State)
                    {
                        case EntityState.Added:
                            entity.CreatedAt = now;
                            entity.UpdatedAt = now;
                            break;
                        case EntityState.Modified:
                            Entry(entity).Property(x => x.CreatedAt).IsModified = false;
                            entity.UpdatedAt = now;
                            break;
                        case EntityState.Deleted:
                            entity.IsDeleted = true;
                            entity.DeletedAt = now;
                            changedEntity.State = EntityState.Modified;
                            break;
                    }
                }
            }
        }
    }
    public static class SoftDeleteQueryExtension
    {
        public static void AddSoftDeleteQueryFilter(
             this IMutableEntityType entityData)
        {
            var methodToCall = typeof(SoftDeleteQueryExtension)
                .GetMethod(nameof(GetSoftDeleteFilter),
                    BindingFlags.NonPublic | BindingFlags.Static)
                ?.MakeGenericMethod(entityData.ClrType);
            var filter = methodToCall?.Invoke(null, new object[] { });
            if (filter != null)
            {
                entityData.SetQueryFilter((LambdaExpression)filter);
            }
        }

        private static LambdaExpression GetSoftDeleteFilter<TEntity>()
            where TEntity : BaseEntity
        {
            Expression<Func<TEntity, bool>> filter = x => !x.IsDeleted;
            return filter;
        }
    }
}