using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using sweapi.Models;

namespace sweapi.Data.Seed
{
    public static class SeedIdentityUderData
    {
        public static void SeedUserIdentityData(this ModelBuilder modelBuilder)
        {
            string AdministradorGeneralRoleId = Guid.NewGuid().ToString();
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = AdministradorGeneralRoleId,
                Name = "Administrador",
                NormalizedName = "Administrador".ToUpper()
            });

            string UsuarioGeneralRoleId = Guid.NewGuid().ToString();
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = UsuarioGeneralRoleId,
                Name = "Usuario general",
                NormalizedName = "Usuario general".ToUpper()
            });

            var UsuarioId = Guid.NewGuid().ToString();
            modelBuilder.Entity<CustomIdentityUser>().HasData(
                new CustomIdentityUser
                {
                    Id = UsuarioId, 
                    UserName = "jona@uv.mx",
                    Email = "jona@uv.mx",
                    NormalizedEmail = "jona@uv.mx".ToUpper(),
                    Nombre = "Jona Hernandez",
                    NormalizedUserName = "jona@uv.mx".ToUpper(),
                    PasswordHash = new PasswordHasher<CustomIdentityUser>().HashPassword(null, "jonapass")
                }
            );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = AdministradorGeneralRoleId,
                    UserId = UsuarioId
                }
            );

            UsuarioId = Guid.NewGuid().ToString();
            modelBuilder.Entity<CustomIdentityUser>().HasData(
                new CustomIdentityUser
                {
                    Id = UsuarioId,
                    UserName = "jose@uv.mx",
                    Email = "jose@uv.mx",
                    NormalizedEmail = "jose@uv.mx".ToUpper(),
                    Nombre = "jose jose",
                    NormalizedUserName = "jose@uv.mx".ToUpper(),
                    PasswordHash = new PasswordHasher<CustomIdentityUser>().HashPassword(null, "josepass")
                }
            );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = AdministradorGeneralRoleId,
                    UserId = UsuarioId
                }
            );

            UsuarioId = Guid.NewGuid().ToString();
            modelBuilder.Entity<CustomIdentityUser>().HasData(
                new CustomIdentityUser
                {
                    Id = UsuarioId, 
                    UserName = "denisse@gmail.com",
                    Email = "denisse@gmail.com",
                    NormalizedEmail = "denisse@gmail.com".ToUpper(),
                    Nombre = "denisse hernandez",
                    NormalizedUserName = "denisse@gmail.com".ToUpper(),
                    PasswordHash = new PasswordHasher<CustomIdentityUser>().HashPassword(null, "denissepass")
                }
            );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = UsuarioGeneralRoleId,
                    UserId = UsuarioId
                }
            );
        }
    }
}
