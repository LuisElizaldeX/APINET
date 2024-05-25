using backendnet.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace backendnet.Data.Seed;

public static class SeedIdentityUserData
{
    public static void SeedUserIdentityData(this ModelBuilder modelBuilder)
    {
        // Agregar el rol "Administrador" a la tabla AspNetRoles
        string AdministradorRoleId = Guid.NewGuid().ToString();
        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = AdministradorRoleId,
            Name = "Administrador",
            NormalizedName = "Administrador".ToUpper()
        });

        // Agregar el rol "Usuario" a la tabla AspNetRoles
        string UsuarioRoleId = Guid.NewGuid().ToString();
        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = UsuarioRoleId,
            Name = "Usuario",
            NormalizedName = "Usuario".ToUpper()
        });

        // Agregamos un usuario a la tabla AspNetUsers
        var UsuarioId = Guid.NewGuid().ToString();
        modelBuilder.Entity<CustomIdentityUser>().HasData(
            new CustomIdentityUser
            {
                Id = UsuarioId,
                UserName = "luis@uv.mx",
                Email = "luis@uv.mx",
                NormalizedEmail = "luis@uv.mx".ToUpper(),
                Nombre = "Luis Angel Elizalde Arroyo",
                NormalizedUserName = "luis@uv.mx".ToUpper(),
                PasswordHash = new PasswordHasher<CustomIdentityUser>().HashPassword(null!, "patito"),
                Protegido = true    // Este no se puede eliminar
            }
        );

        // Aplicamos la relación entre el usuario y el rol en la tabla AspNetUserRoles
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = AdministradorRoleId,
                UserId = UsuarioId
            }
        );

        // Agregamos un usuario a la tabla AspNetUserRoles
        UsuarioId = Guid.NewGuid().ToString();
        modelBuilder.Entity<CustomIdentityUser>().HasData(
            new CustomIdentityUser
            {
                Id = UsuarioId, // Primary key 
                UserName = "patito@uv.mx",
                Email = "patito@uv.mx",
                NormalizedEmail = "patito@uv.mx".ToUpper(),
                Nombre = "Usuario patito",
                NormalizedUserName = "patito@uv.mx".ToUpper(),
                PasswordHash = new PasswordHasher<CustomIdentityUser>().HashPassword(null!, "patito") 
            }
        );

        // Aplicamos la relación entre el usuario y el rol en la tabla AspNetUserRoles
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = UsuarioRoleId,
                UserId = UsuarioId
            }
        );
    }
}