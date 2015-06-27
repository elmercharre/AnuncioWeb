namespace Ads.Repository
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Ads.Dominio;
    using System.Data.Entity.ModelConfiguration;

    public class DbAdsContext : DbContext
    {
        public DbAdsContext()
            : base("name=Adstmp")
        {
        }

        public virtual DbSet<articles> articles { get; set; }
        public virtual DbSet<articleTypes> articleTypes { get; set; }
        public virtual DbSet<categories> categories { get; set; }
        public virtual DbSet<conditions> conditions { get; set; }
        public virtual DbSet<customers> customers { get; set; }
        public virtual DbSet<marcas> marcas { get; set; }
        public virtual DbSet<modelos> modelos { get; set; }
        public virtual DbSet<relationship_condition> relationship_condition { get; set; }
        public virtual DbSet<relationship_marca> relationship_marca { get; set; }
        public virtual DbSet<resources> resources { get; set; }
        public virtual DbSet<tipos> tipos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<articles>()
                .Property(e => e.detail)
                .IsUnicode(false);

            modelBuilder.Entity<articles>()
                .HasMany(e => e.resources)
                .WithRequired(e => e.articles)
                .HasForeignKey(e => e.article_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<articles>()
                .Map<moto>(x => x.Requires("Type").HasValue("moto").HasColumnType("varchar").HasMaxLength(20))
                .Map<auto>(x => x.Requires("Type").HasValue("auto"))
                .Map<camion>(x => x.Requires("Type").HasValue("camion"))
                .Map<departamento_venta>(x => x.Requires("Type").HasValue("depa_venta"))
                .Map<departamento_alquiler>(x => x.Requires("Type").HasValue("depa_alquiler"))
                ;

            modelBuilder.Entity<articleTypes>()
                .HasMany(e => e.relationship_condition)
                .WithRequired(e => e.articleTypes)
                .HasForeignKey(e => e.articleType_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<articleTypes>()
                .HasMany(e => e.relationship_marca)
                .WithRequired(e => e.articleTypes)
                .HasForeignKey(e => e.articleType_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<articleTypes>()
                .HasMany(e => e.tipos)
                .WithRequired(e => e.articleTypes)
                .HasForeignKey(e => e.articleType_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<categories>()
                .HasMany(e => e.articles)
                .WithOptional(e => e.categories)
                .HasForeignKey(e => e.category_Id);

            modelBuilder.Entity<categories>()
                .HasMany(e => e.articleTypes)
                .WithRequired(e => e.categories)
                .HasForeignKey(e => e.category_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<conditions>()
                .HasMany(e => e.relationship_condition)
                .WithRequired(e => e.conditions)
                .HasForeignKey(e => e.condition_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<customers>()
                .HasMany(e => e.articles)
                .WithRequired(e => e.customers)
                .HasForeignKey(e => e.customer_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<marcas>()
                .HasMany(e => e.modelos)
                .WithRequired(e => e.marcas)
                .HasForeignKey(e => e.marca_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<marcas>()
                .HasMany(e => e.relationship_marca)
                .WithRequired(e => e.marcas)
                .HasForeignKey(e => e.marca_id)
                .WillCascadeOnDelete(false);
        }
    }

}
