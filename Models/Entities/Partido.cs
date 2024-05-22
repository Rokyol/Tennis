using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tennis.Models
{
    public class Partido
    {
        [Key]
        public int Id { get; set; }
        public int IdTorneo { get; set; }
        public virtual Torneo Torneo { get; set; }
        public int IdJugadorL { get; set; }
        public Jugador JugadorL { get; set; }
        public int IdJugadorW { get; set; }
        public Jugador JugadorW { get; set; }

    }
    public class PartidoConfig : IEntityTypeConfiguration<Partido>
    {
        public void Configure(EntityTypeBuilder<Partido> builder)
        {
            builder.ToTable("Partido");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id").ValueGeneratedOnAdd().IsRequired();
            builder.Property(x => x.IdTorneo).HasColumnName("IdTorneo").IsRequired();
            builder.Property(x => x.IdJugadorL).HasColumnName("IdJugadorL").IsRequired();
            builder.Property(x => x.IdJugadorW).HasColumnName("IdJugadorW").IsRequired();

            builder.HasOne(p => p.Torneo)
                   .WithMany(t => t.Partido)
                   .HasForeignKey(p => p.IdTorneo);

            builder.HasOne(p => p.JugadorL)
                   .WithMany(j => j.Partido)
                   .HasForeignKey(p => p.IdJugadorL)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.JugadorW)
                   .WithMany()
                   .HasForeignKey(p => p.IdJugadorW)
                   .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
