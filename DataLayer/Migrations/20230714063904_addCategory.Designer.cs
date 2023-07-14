﻿// <auto-generated />
using System;
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
    [Migration("20230714063904_addCategory")]
    partial class addCategory
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CategoryTagWord", b =>
                {
                    b.Property<int>("CategoryTagsCategoryTagId")
                        .HasColumnType("int");

                    b.Property<int>("WordsWordId")
                        .HasColumnType("int");

                    b.HasKey("CategoryTagsCategoryTagId", "WordsWordId");

                    b.HasIndex("WordsWordId");

                    b.ToTable("CategoryTagWord");
                });

            modelBuilder.Entity("DataLayer.EfClasses.Card", b =>
                {
                    b.Property<int>("CardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CardId"));

                    b.Property<DateTime>("ReviewTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("WordId")
                        .HasColumnType("int");

                    b.HasKey("CardId");

                    b.HasIndex("WordId");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("DataLayer.EfClasses.CategoryTag", b =>
                {
                    b.Property<int>("CategoryTagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryTagId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryTagId");

                    b.ToTable("CategoryTags");
                });

            modelBuilder.Entity("DataLayer.EfClasses.Define", b =>
                {
                    b.Property<int>("DefineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DefineId"));

                    b.Property<string>("Definition")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Example")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VocabularyId")
                        .HasColumnType("int");

                    b.HasKey("DefineId");

                    b.HasIndex("VocabularyId", "Definition")
                        .IsUnique();

                    b.ToTable("Defines");
                });

            modelBuilder.Entity("DataLayer.EfClasses.Vocabulary", b =>
                {
                    b.Property<int>("VocabularyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VocabularyId"));

                    b.Property<string>("IPA")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PartOfSpeech")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Pronounce")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WordId")
                        .HasColumnType("int");

                    b.HasKey("VocabularyId");

                    b.HasIndex("WordId", "PartOfSpeech")
                        .IsUnique();

                    b.ToTable("Vocabularies");
                });

            modelBuilder.Entity("DataLayer.EfClasses.Word", b =>
                {
                    b.Property<int>("WordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WordId"));

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("WordId");

                    b.HasIndex("Text")
                        .IsUnique();

                    b.ToTable("Words");
                });

            modelBuilder.Entity("CategoryTagWord", b =>
                {
                    b.HasOne("DataLayer.EfClasses.CategoryTag", null)
                        .WithMany()
                        .HasForeignKey("CategoryTagsCategoryTagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.EfClasses.Word", null)
                        .WithMany()
                        .HasForeignKey("WordsWordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataLayer.EfClasses.Card", b =>
                {
                    b.HasOne("DataLayer.EfClasses.Word", "Word")
                        .WithMany()
                        .HasForeignKey("WordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Word");
                });

            modelBuilder.Entity("DataLayer.EfClasses.Define", b =>
                {
                    b.HasOne("DataLayer.EfClasses.Vocabulary", "Vocabulary")
                        .WithMany("Definitions")
                        .HasForeignKey("VocabularyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vocabulary");
                });

            modelBuilder.Entity("DataLayer.EfClasses.Vocabulary", b =>
                {
                    b.HasOne("DataLayer.EfClasses.Word", "Word")
                        .WithMany("Vocabularies")
                        .HasForeignKey("WordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Word");
                });

            modelBuilder.Entity("DataLayer.EfClasses.Vocabulary", b =>
                {
                    b.Navigation("Definitions");
                });

            modelBuilder.Entity("DataLayer.EfClasses.Word", b =>
                {
                    b.Navigation("Vocabularies");
                });
#pragma warning restore 612, 618
        }
    }
}
