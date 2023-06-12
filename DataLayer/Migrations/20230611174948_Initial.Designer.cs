﻿// <auto-generated />
using DataLayer.EFCode;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataLayer.Migrations
{
    [DbContext(typeof(VocabularyAppContext))]
    [Migration("20230611174948_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DataLayer.EfClasses.Vocabulary", b =>
                {
                    b.Property<int>("VocabularyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VocabularyId"));

                    b.Property<int>("WordId")
                        .HasColumnType("int");

                    b.HasKey("VocabularyId");

                    b.HasIndex("WordId");

                    b.ToTable("Vocabularies");
                });

            modelBuilder.Entity("DataLayer.EfClasses.Word", b =>
                {
                    b.Property<int>("WordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WordId"));

                    b.HasKey("WordId");

                    b.ToTable("Words");
                });

            modelBuilder.Entity("DataLayer.EfClasses.Vocabulary", b =>
                {
                    b.HasOne("DataLayer.EfClasses.Word", null)
                        .WithMany("Vocabularies")
                        .HasForeignKey("WordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataLayer.EfClasses.Word", b =>
                {
                    b.Navigation("Vocabularies");
                });
#pragma warning restore 612, 618
        }
    }
}