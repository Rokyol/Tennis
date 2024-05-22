using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Tennis.Models.Entities;

namespace Tennis.Models
{
    public class Jugador
    {
        public int Id { get; set; }
        public int Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime Nacimiento { get; set; }
        public string Genero { get; set; }
        public int Habilidad { get; set; }
        public int Suerte { get; set; }
        public int Fuerza { get; set; }
        public int Velocidad { get; set; }
        public int Reaccion { get; set; }
        public bool Activo { get; set; }
        public virtual List<TorneoJugador>? TorneoJugador { get; set; }
        public virtual List<Torneo>? Torneo { get; set; }
        public virtual List<Partido>? Partido { get; set; }
        
    }
    public class JugadorConfig : IEntityTypeConfiguration<Jugador>
    {
        public void Configure(EntityTypeBuilder<Jugador> builder)
        {
            builder.ToTable("Jugador");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id").ValueGeneratedOnAdd().IsRequired();
            builder.Property(x => x.Dni).HasColumnName("Dni").IsRequired();
            builder.Property(x => x.Nombre).HasColumnName("Nombre").IsRequired();
            builder.Property(x => x.Apellido).HasColumnName("Apellido").IsRequired();
            builder.Property(x => x.Nacimiento).HasColumnName("Nacimiento").IsRequired();
            builder.Property(x => x.Genero).HasColumnName("Genero").IsRequired();
            builder.Property(x => x.Habilidad).HasColumnName("Habilidad").IsRequired();
            builder.Property(x => x.Suerte).HasColumnName("Suerte");
            builder.Property(x => x.Fuerza).HasColumnName("Fuerza");
            builder.Property(x => x.Velocidad).HasColumnName("Velocidad");
            builder.Property(x => x.Reaccion).HasColumnName("Reaccion");
            builder.Property(x => x.Activo).HasColumnName("Activo").IsRequired();
            builder.HasMany(t => t.TorneoJugador)
                   .WithOne(t => t.Jugador)
                   .HasForeignKey("JugadorId");
        }
    }
}
