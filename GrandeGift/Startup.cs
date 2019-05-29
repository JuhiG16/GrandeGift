using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using GrandeGift.Services;
using GrandeGift.Models;
using Microsoft.AspNetCore.Identity;

namespace GrandeGift
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //test
            services.AddDbContext<GrandeHamperDbContext>();
            services.AddScoped<IDataServices<Customer>, DataServices<Customer>>();
            services.AddScoped<IDataServices<Hamper>, DataServices<Hamper>>();
            services.AddScoped<IDataServices<Category>, DataServices<Category>>();
            IdentityBuilder iBuilder = services.AddIdentity<User, Role>(
                config =>
                {
                    config.User.RequireUniqueEmail = false;
                    config.Password.RequireDigit = true;
                    config.Password.RequiredLength = 8;
                    config.Password.RequireLowercase = true;
                    config.Password.RequireUppercase = true;
                    config.Password.RequireNonAlphanumeric = true;
                }
                );
            iBuilder = new IdentityBuilder(iBuilder.UserType, typeof(Role), services);
            iBuilder.AddEntityFrameworkStores<GrandeHamperDbContext>().AddDefaultTokenProviders();
            iBuilder.AddRoleValidator<RoleValidator<Role>>();
            iBuilder.AddRoleManager<RoleManager<Role>>();
            iBuilder.AddSignInManager<SignInManager<User>>();
            services.AddMvc();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();
            CreateRoles(serviceProvider).Wait();
            app.UseMvc(route =>
            {
            route.MapRoute("default",
                         "{Controller=Account}/{Action=Index}/{id?}");
            }   
            );
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //initializing custom roles   
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<User>>();
            string[] roleNames = { "Admin", "Manager", "Customer" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles and seed them to the database: Question 1  
                    Role role = new Role()
                    {
                        Name = roleName
                    };
                    roleResult = await RoleManager.CreateAsync(role);
                }
            }

            User user = await UserManager.FindByEmailAsync("admin@gmail.com");
            //  user.Role = "Admin";
            if (user == null)
            {
                user = new User()
                {
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    Role = "Admin"
                };
                await UserManager.CreateAsync(user, "Abcd@123");
            }
            await UserManager.AddToRoleAsync(user, "Admin");


            User user1 = await UserManager.FindByEmailAsync("manager@gmail.com");
            // user1.Role = "Travel Provider";
            if (user1 == null)
            {
                user1 = new User()
                {
                    UserName = "manager",
                    Email = "manager@gmail.com",
                    Role = "Manager"
                };
                await UserManager.CreateAsync(user1, "Abcd@123");
            }
            await UserManager.AddToRoleAsync(user1, "Manager");

            User user2 = await UserManager.FindByEmailAsync("customer@gmail.com");
            // user2.Role = "Customer";
            if (user2 == null)
            {
                user2 = new User()
                {
                    UserName = "customer",
                    Email = "customer@gmail.com",
                    Role = "Customer"
                };
                await UserManager.CreateAsync(user2, "Abcd@123");
            }
            await UserManager.AddToRoleAsync(user2, "Customer");

        }
    }
}
