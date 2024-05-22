using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Emit;

namespace Tennis.Models.Entities
{
    public class TorneoJugador
    {
        public int Id { get; set; }
       
        public int JugadorId { get; set; }
      
        public virtual Jugador? Jugador { get; set; }

     
        public int TorneoId { get; set; }
       

        public virtual Torneo? Torneo { get; set; }
    }
    public class TorneoJugadorConfig : IEntityTypeConfiguration<TorneoJugador>
    {
        public void Configure(EntityTypeBuilder<TorneoJugador> builder)
        {
            builder.ToTable("TorneoJugador");      

            builder.Property(tj => tj.JugadorId)
                .HasColumnName("JugadorId")
                .IsRequired();
            builder.Property(tj => tj.Id)
               .HasColumnName("Id")
               .IsRequired();

            builder.Property(tj => tj.TorneoId)
                .HasColumnName("TorneoId")
                .IsRequired();
        }
    }
}
