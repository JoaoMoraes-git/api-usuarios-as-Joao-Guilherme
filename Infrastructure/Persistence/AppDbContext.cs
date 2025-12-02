using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using api_usuarios_as_João_Guilherme.domain;
using Microsoft.EntityFrameworkCore;

namespace api_usuarios_as_João_Guilherme.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
        
            
            //Tabelas presentes no projeto
        public DbSet<Usuario> Usuarios { get; set;}
        
    }
}