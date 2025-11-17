using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TuVung.Models;

namespace TuVung.Data;

public partial class VocabularyDbContext : DbContext
{
    public VocabularyDbContext()
    {
    }

    public VocabularyDbContext(DbContextOptions<VocabularyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Exercise> Exercises { get; set; }

    public virtual DbSet<Grammartopic> Grammartopics { get; set; }

    public virtual DbSet<ListeningLesson> ListeningLessons { get; set; }

    public virtual DbSet<ListeningSentence> ListeningSentences { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<ToeicVocab> ToeicVocabs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = "Host=aws-1-ap-southeast-1.pooler.supabase.com;Port=5432;Database=postgres;Username=postgres.mbllbdmywhyapiooavvs;Password=Congkcs1234@@;SSL Mode=Require;Trust Server Certificate=true";
            optionsBuilder.UseNpgsql(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.HasKey(e => e.ExerciseId).HasName("exercises_pkey");

            entity.ToTable("exercises");

            entity.Property(e => e.ExerciseId).HasColumnName("exercise_id");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("title");
            entity.Property(e => e.TopicId).HasColumnName("topic_id");
            entity.Property(e => e.TotalQuestions).HasColumnName("total_questions");

            entity.HasOne(d => d.Topic).WithMany(p => p.Exercises)
                .HasForeignKey(d => d.TopicId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("exercises_topic_id_fkey");
        });

        modelBuilder.Entity<Grammartopic>(entity =>
        {
            entity.HasKey(e => e.TopicId).HasName("grammartopics_pkey");

            entity.ToTable("grammartopics");

            entity.Property(e => e.TopicId).HasColumnName("topic_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ListeningLesson>(entity =>
        {
            entity.HasKey(e => e.LessonId).HasName("listening_lessons_pkey");

            entity.ToTable("listening_lessons");

            entity.Property(e => e.LessonId).HasColumnName("lesson_id");
            entity.Property(e => e.Level)
                .HasMaxLength(10)
                .HasColumnName("level");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("title");
            entity.Property(e => e.TotalQuestions)
                .HasDefaultValue((short)0)
                .HasColumnName("total_questions");
        });

        modelBuilder.Entity<ListeningSentence>(entity =>
        {
            entity.HasKey(e => e.SentenceId).HasName("listening_sentences_pkey");

            entity.ToTable("listening_sentences");

            entity.Property(e => e.SentenceId).HasColumnName("sentence_id");
            entity.Property(e => e.AudioUrl).HasColumnName("audio_url");
            entity.Property(e => e.LessonId).HasColumnName("lesson_id");
            entity.Property(e => e.OrderNo).HasColumnName("order_no");
            entity.Property(e => e.Transcript).HasColumnName("transcript");
            entity.Property(e => e.Translation).HasColumnName("translation");

            entity.HasOne(d => d.Lesson).WithMany(p => p.ListeningSentences)
                .HasForeignKey(d => d.LessonId)
                .HasConstraintName("listening_sentences_lesson_id_fkey");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("questions_pkey");

            entity.ToTable("questions");

            entity.Property(e => e.QuestionId).HasColumnName("question_id");
            entity.Property(e => e.CorrectOption)
                .HasMaxLength(1)
                .HasColumnName("correct_option");
            entity.Property(e => e.ExerciseId).HasColumnName("exercise_id");
            entity.Property(e => e.OptionA).HasColumnName("option_a");
            entity.Property(e => e.OptionB).HasColumnName("option_b");
            entity.Property(e => e.OptionC).HasColumnName("option_c");
            entity.Property(e => e.OptionD).HasColumnName("option_d");
            entity.Property(e => e.QuestionText).HasColumnName("question_text");

            entity.HasOne(d => d.Exercise).WithMany(p => p.Questions)
                .HasForeignKey(d => d.ExerciseId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("questions_exercise_id_fkey");
        });

        modelBuilder.Entity<ToeicVocab>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("toeic_vocab_pkey");

            entity.ToTable("toeic_vocab");

            entity.HasIndex(e => e.Word, "idx_toeic_vocab_word");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.ExampleEn).HasColumnName("example_en");
            entity.Property(e => e.MeaningVi).HasColumnName("meaning_vi");
            entity.Property(e => e.Phonetic).HasColumnName("phonetic");
            entity.Property(e => e.Pos).HasColumnName("pos");
            entity.Property(e => e.Section).HasColumnName("section");
            entity.Property(e => e.Source)
                .HasDefaultValueSql("'ETS'::text")
                .HasColumnName("source");
            entity.Property(e => e.Stt).HasColumnName("stt");
            entity.Property(e => e.TestNo).HasColumnName("test_no");
            entity.Property(e => e.TestYear).HasColumnName("test_year");
            entity.Property(e => e.Word).HasColumnName("word");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
