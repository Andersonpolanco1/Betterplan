using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using BetterplanAPI.Models;
using BetterplanAPI.DTOs;

namespace BetterplanAPI.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Compositioncategory> Compositioncategories { get; set; } = null!;
        public virtual DbSet<Compositionsubcategory> Compositionsubcategories { get; set; } = null!;
        public virtual DbSet<Currency> Currencies { get; set; } = null!;
        public virtual DbSet<Currencyindicator> Currencyindicators { get; set; } = null!;
        public virtual DbSet<Financialentity> Financialentities { get; set; } = null!;
        public virtual DbSet<Funding> Fundings { get; set; } = null!;
        public virtual DbSet<Fundingcomposition> Fundingcompositions { get; set; } = null!;
        public virtual DbSet<Fundingsharevalue> Fundingsharevalues { get; set; } = null!;
        public virtual DbSet<Goal> Goals { get; set; } = null!;
        public virtual DbSet<Goalcategory> Goalcategories { get; set; } = null!;
        public virtual DbSet<Goaltransaction> Goaltransactions { get; set; } = null!;
        public virtual DbSet<Goaltransactionfunding> Goaltransactionfundings { get; set; } = null!;
        public virtual DbSet<Investmentstrategy> Investmentstrategies { get; set; } = null!;
        public virtual DbSet<Investmentstrategytype> Investmentstrategytypes { get; set; } = null!;
        public virtual DbSet<Portfolio> Portfolios { get; set; } = null!;
        public virtual DbSet<Portfoliocomposition> Portfoliocompositions { get; set; } = null!;
        public virtual DbSet<Portfoliofunding> Portfoliofundings { get; set; } = null!;
        public virtual DbSet<Risklevel> Risklevels { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserBalanceDto> UserBalanceDtos { get; set; } = null!;



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Compositioncategory>(entity =>
            {
                entity.ToTable("compositioncategory");

                entity.HasIndex(e => e.Uuid, "uuidCompositionCategory_")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasPrecision(6)
                    .HasColumnName("created");

                entity.Property(e => e.Modified)
                    .HasPrecision(6)
                    .HasColumnName("modified");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Uuid).HasColumnName("uuid");
            });

            modelBuilder.Entity<Compositionsubcategory>(entity =>
            {
                entity.ToTable("compositionsubcategory");

                entity.HasIndex(e => e.Categoryid, "idx_fk_compositionsubcategory_categoryid");

                entity.HasIndex(e => e.Uuid, "uuidCompositionSubcategory_")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Categoryid).HasColumnName("categoryid");

                entity.Property(e => e.Created)
                    .HasPrecision(6)
                    .HasColumnName("created")
                    .HasDefaultValueSql("'1970-01-01 00:00:00+00'::timestamp with time zone");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Modified)
                    .HasPrecision(6)
                    .HasColumnName("modified")
                    .HasDefaultValueSql("'1970-01-01 00:00:00+00'::timestamp with time zone");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Uuid).HasColumnName("uuid");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Compositionsubcategories)
                    .HasForeignKey(d => d.Categoryid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_compositionsubcategory_categoryid");
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("currency");

                entity.HasIndex(e => e.Uuid, "currency_unique")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasPrecision(6)
                    .HasColumnName("created");

                entity.Property(e => e.Currencycode).HasColumnName("currencycode");

                entity.Property(e => e.Digitsinfo).HasColumnName("digitsinfo");

                entity.Property(e => e.Display).HasColumnName("display");

                entity.Property(e => e.Locale).HasColumnName("locale");

                entity.Property(e => e.Modified)
                    .HasPrecision(6)
                    .HasColumnName("modified");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Serverformatnumber).HasColumnName("serverformatnumber");

                entity.Property(e => e.Uuid)
                    .HasColumnName("uuid")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.Yahoomnemonic).HasColumnName("yahoomnemonic");
            });

            modelBuilder.Entity<Currencyindicator>(entity =>
            {
                entity.ToTable("currencyindicator");

                entity.HasIndex(e => e.Destinationcurrencyid, "IX_currencyindicator_destinationcurrencyid");

                entity.HasIndex(e => new { e.Sourcecurrencyid, e.Destinationcurrencyid, e.Date }, "currency_indicator_unique")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ask).HasColumnName("ask");

                entity.Property(e => e.Bid).HasColumnName("bid");

                entity.Property(e => e.Created)
                    .HasPrecision(6)
                    .HasColumnName("created");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.Destinationcurrencyid).HasColumnName("destinationcurrencyid");

                entity.Property(e => e.Modified)
                    .HasPrecision(6)
                    .HasColumnName("modified");

                entity.Property(e => e.Requestdate)
                    .HasPrecision(6)
                    .HasColumnName("requestdate")
                    .HasDefaultValueSql("'1970-01-01 00:00:00+00'::timestamp with time zone");

                entity.Property(e => e.Sourcecurrencyid).HasColumnName("sourcecurrencyid");

                entity.Property(e => e.Value).HasColumnName("value");

                entity.HasOne(d => d.Destinationcurrency)
                    .WithMany(p => p.CurrencyindicatorDestinationcurrencies)
                    .HasForeignKey(d => d.Destinationcurrencyid)
                    .HasConstraintName("fk_currency_indicator_destinationcurrencyid");

                entity.HasOne(d => d.Sourcecurrency)
                    .WithMany(p => p.CurrencyindicatorSourcecurrencies)
                    .HasForeignKey(d => d.Sourcecurrencyid)
                    .HasConstraintName("fk_currency_indicator_sourcecurrencyid");
            });

            modelBuilder.Entity<Financialentity>(entity =>
            {
                entity.ToTable("financialentity");

                entity.HasIndex(e => e.Defaultcurrencyid, "IX_financialentity_defaultcurrencyid");

                entity.HasIndex(e => e.Uuid, "uuidFinancialEntity_")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasPrecision(6)
                    .HasColumnName("created");

                entity.Property(e => e.Defaultcurrencyid).HasColumnName("defaultcurrencyid");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Logo).HasColumnName("logo");

                entity.Property(e => e.Modified)
                    .HasPrecision(6)
                    .HasColumnName("modified");

                entity.Property(e => e.Title).HasColumnName("title");

                entity.Property(e => e.Uuid).HasColumnName("uuid");

                entity.HasOne(d => d.Defaultcurrency)
                    .WithMany(p => p.Financialentities)
                    .HasForeignKey(d => d.Defaultcurrencyid)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Funding>(entity =>
            {
                entity.ToTable("funding");

                entity.HasIndex(e => e.Currencyid, "IX_funding_currencyid");

                entity.HasIndex(e => e.Financialentityid, "idx_fk_funding_financialentityid");

                entity.HasIndex(e => e.Mnemonic, "mnemonicFunding_");

                entity.HasIndex(e => e.Uuid, "uuidFunding_")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cmfurl).HasColumnName("cmfurl");

                entity.Property(e => e.Created)
                    .HasPrecision(6)
                    .HasColumnName("created");

                entity.Property(e => e.Currencyid).HasColumnName("currencyid");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Financialentityid).HasColumnName("financialentityid");

                entity.Property(e => e.Hassharevalue).HasColumnName("hassharevalue");

                entity.Property(e => e.Isbox).HasColumnName("isbox");

                entity.Property(e => e.Mnemonic).HasColumnName("mnemonic");

                entity.Property(e => e.Modified)
                    .HasPrecision(6)
                    .HasColumnName("modified");

                entity.Property(e => e.Title).HasColumnName("title");

                entity.Property(e => e.Uuid).HasColumnName("uuid");

                entity.Property(e => e.Yahoomnemonic).HasColumnName("yahoomnemonic");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Fundings)
                    .HasForeignKey(d => d.Currencyid)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Financialentity)
                    .WithMany(p => p.Fundings)
                    .HasForeignKey(d => d.Financialentityid)
                    .HasConstraintName("fk_funding_financialentityid");
            });

            modelBuilder.Entity<Fundingcomposition>(entity =>
            {
                entity.ToTable("fundingcomposition");

                entity.HasIndex(e => e.Fundingid, "idx_fk_fundingcomposition_fundingid");

                entity.HasIndex(e => e.Subcategoryid, "idx_fk_fundingcomposition_subcategoryid");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasPrecision(6)
                    .HasColumnName("created");

                entity.Property(e => e.Fundingid).HasColumnName("fundingid");

                entity.Property(e => e.Modified)
                    .HasPrecision(6)
                    .HasColumnName("modified");

                entity.Property(e => e.Percentage).HasColumnName("percentage");

                entity.Property(e => e.Subcategoryid).HasColumnName("subcategoryid");

                entity.HasOne(d => d.Funding)
                    .WithMany(p => p.Fundingcompositions)
                    .HasForeignKey(d => d.Fundingid)
                    .HasConstraintName("fk_fundingcomposition_fundingid");

                entity.HasOne(d => d.Subcategory)
                    .WithMany(p => p.Fundingcompositions)
                    .HasForeignKey(d => d.Subcategoryid)
                    .HasConstraintName("fk_portfoliocomposition_subcategoryid");
            });

            modelBuilder.Entity<Fundingsharevalue>(entity =>
            {
                entity.ToTable("fundingsharevalue");

                entity.HasIndex(e => new { e.Fundingid, e.Date }, "funding_share_value_unique")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasPrecision(6)
                    .HasColumnName("created");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.Fundingid).HasColumnName("fundingid");

                entity.Property(e => e.Modified)
                    .HasPrecision(6)
                    .HasColumnName("modified");

                entity.Property(e => e.Value).HasColumnName("value");

                entity.HasOne(d => d.Funding)
                    .WithMany(p => p.Fundingsharevalues)
                    .HasForeignKey(d => d.Fundingid)
                    .HasConstraintName("fk_fundingsharevalue_fundingid");
            });

            modelBuilder.Entity<Goal>(entity =>
            {
                entity.ToTable("goal");

                entity.HasIndex(e => e.Currencyid, "IX_goal_currencyid");

                entity.HasIndex(e => e.Displaycurrencyid, "IX_goal_displaycurrencyid");

                entity.HasIndex(e => e.Risklevelid, "IX_goal_risklevelid");

                entity.HasIndex(e => e.Financialentityid, "goal_idx2");

                entity.HasIndex(e => e.Goalcategoryid, "idx_fk_goal_goalcategory");

                entity.HasIndex(e => e.Portfolioid, "idx_fk_goal_portfolioid");

                entity.HasIndex(e => e.Userid, "idx_fk_goal_userid");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasPrecision(6)
                    .HasColumnName("created");

                entity.Property(e => e.Currencyid).HasColumnName("currencyid");

                entity.Property(e => e.Displaycurrencyid).HasColumnName("displaycurrencyid");

                entity.Property(e => e.Financialentityid).HasColumnName("financialentityid");

                entity.Property(e => e.Goalcategoryid).HasColumnName("goalcategoryid");

                entity.Property(e => e.Initialinvestment).HasColumnName("initialinvestment");

                entity.Property(e => e.Modified)
                    .HasPrecision(6)
                    .HasColumnName("modified");

                entity.Property(e => e.Monthlycontribution).HasColumnName("monthlycontribution");

                entity.Property(e => e.Portfolioid).HasColumnName("portfolioid");

                entity.Property(e => e.Risklevelid).HasColumnName("risklevelid");

                entity.Property(e => e.Targetamount).HasColumnName("targetamount");

                entity.Property(e => e.Title).HasColumnName("title");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.Property(e => e.Years).HasColumnName("years");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.GoalCurrencies)
                    .HasForeignKey(d => d.Currencyid)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Displaycurrency)
                    .WithMany(p => p.GoalDisplaycurrencies)
                    .HasForeignKey(d => d.Displaycurrencyid)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Financialentity)
                    .WithMany(p => p.Goals)
                    .HasForeignKey(d => d.Financialentityid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_goal_financialentityid");

                entity.HasOne(d => d.Goalcategory)
                    .WithMany(p => p.Goals)
                    .HasForeignKey(d => d.Goalcategoryid)
                    .HasConstraintName("fk_goal_goalcategory");

                entity.HasOne(d => d.Portfolio)
                    .WithMany(p => p.Goals)
                    .HasForeignKey(d => d.Portfolioid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_goal_portfolioid");

                entity.HasOne(d => d.Risklevel)
                    .WithMany(p => p.Goals)
                    .HasForeignKey(d => d.Risklevelid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_goal_risklevelid");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Goals)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("fk_goal_userid");
            });

            modelBuilder.Entity<Goalcategory>(entity =>
            {
                entity.ToTable("goalcategory");

                entity.HasIndex(e => e.Uuid, "uuidGoalCategory_")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.Created)
                    .HasPrecision(6)
                    .HasColumnName("created");

                entity.Property(e => e.Modified)
                    .HasPrecision(6)
                    .HasColumnName("modified");

                entity.Property(e => e.Title).HasColumnName("title");

                entity.Property(e => e.Uuid).HasColumnName("uuid");
            });

            modelBuilder.Entity<Goaltransaction>(entity =>
            {
                entity.ToTable("goaltransaction");

                entity.HasIndex(e => e.Currencyid, "IX_goaltransaction_currencyid");

                entity.HasIndex(e => e.Financialentityid, "IX_goaltransaction_financialentityid");

                entity.HasIndex(e => e.Goalid, "idx_fk_goaltransaction_goalid");

                entity.HasIndex(e => e.Ownerid, "ix_goaltransaction_userid");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.All).HasColumnName("all");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.Created)
                    .HasPrecision(6)
                    .HasColumnName("created");

                entity.Property(e => e.Currencyid).HasColumnName("currencyid");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.Financialentityid).HasColumnName("financialentityid");

                entity.Property(e => e.Goalid).HasColumnName("goalid");

                entity.Property(e => e.Isprocessed).HasColumnName("isprocessed");

                entity.Property(e => e.Modified)
                    .HasPrecision(6)
                    .HasColumnName("modified");

                entity.Property(e => e.Ownerid).HasColumnName("ownerid");

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Goaltransactions)
                    .HasForeignKey(d => d.Currencyid)
                    .HasConstraintName("fk_goaltransaction_currrencyid");

                entity.HasOne(d => d.Financialentity)
                    .WithMany(p => p.Goaltransactions)
                    .HasForeignKey(d => d.Financialentityid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_goaltransaction_financialentityid");

                entity.HasOne(d => d.Goal)
                    .WithMany(p => p.Goaltransactions)
                    .HasForeignKey(d => d.Goalid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_goaltransaction_goalid");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Goaltransactions)
                    .HasForeignKey(d => d.Ownerid)
                    .HasConstraintName("fk_goaltransaction_ownerid");
            });

            modelBuilder.Entity<Goaltransactionfunding>(entity =>
            {
                entity.ToTable("goaltransactionfunding");

                entity.HasIndex(e => new { e.Goalid, e.Fundingid, e.State, e.Date }, "goaltransactionfunding_idx1");

                entity.HasIndex(e => new { e.Goalid, e.Fundingid, e.State }, "goaltransactionfunding_idx2");

                entity.HasIndex(e => new { e.Ownerid, e.State }, "goaltransactionfunding_idx3");

                entity.HasIndex(e => e.Ownerid, "goaltransactionfunding_idx4");

                entity.HasIndex(e => e.Fundingid, "idx_fk_goaltransactionfunding_fundingid");

                entity.HasIndex(e => e.Transactionid, "idx_fk_goaltransactionfunding_goaltransactionid");

                entity.HasIndex(e => e.Goalid, "ix_goaltransactionfunding_goalid");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.Created)
                    .HasPrecision(6)
                    .HasColumnName("created");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.Fundingid).HasColumnName("fundingid");

                entity.Property(e => e.Goalid).HasColumnName("goalid");

                entity.Property(e => e.Modified)
                    .HasPrecision(6)
                    .HasColumnName("modified");

                entity.Property(e => e.Ownerid).HasColumnName("ownerid");

                entity.Property(e => e.Percentage).HasColumnName("percentage");

                entity.Property(e => e.Quotas).HasColumnName("quotas");

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.Transactionid).HasColumnName("transactionid");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.HasOne(d => d.Funding)
                    .WithMany(p => p.Goaltransactionfundings)
                    .HasForeignKey(d => d.Fundingid)
                    .HasConstraintName("fk_goaltransactionfunding_fundingid");

                entity.HasOne(d => d.Goal)
                    .WithMany(p => p.Goaltransactionfundings)
                    .HasForeignKey(d => d.Goalid)
                    .HasConstraintName("fk_goaltransactionfunding_goalid");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Goaltransactionfundings)
                    .HasForeignKey(d => d.Ownerid)
                    .HasConstraintName("fk_goaltransactionfunding_ownerid");

                entity.HasOne(d => d.Transaction)
                    .WithMany(p => p.Goaltransactionfundings)
                    .HasForeignKey(d => d.Transactionid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_goaltransactionfunding_goaltransactionid");
            });

            modelBuilder.Entity<Investmentstrategy>(entity =>
            {
                entity.ToTable("investmentstrategy");

                entity.HasIndex(e => e.Code, "IX_investmentstrategy_code")
                    .IsUnique();

                entity.HasIndex(e => e.Financialentityid, "IX_investmentstrategy_financialentityid");

                entity.HasIndex(e => e.Investmentstrategytypeid, "IX_investmentstrategy_investmentstrategytypeid");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.Created)
                    .HasPrecision(6)
                    .HasColumnName("created");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Displayorder).HasColumnName("displayorder");

                entity.Property(e => e.Features).HasColumnName("features");

                entity.Property(e => e.Financialentityid).HasColumnName("financialentityid");

                entity.Property(e => e.Iconurl).HasColumnName("iconurl");

                entity.Property(e => e.Investmentstrategytypeid).HasColumnName("investmentstrategytypeid");

                entity.Property(e => e.Isdefault).HasColumnName("isdefault");

                entity.Property(e => e.Isrecommended).HasColumnName("isrecommended");

                entity.Property(e => e.Isvisible).HasColumnName("isvisible");

                entity.Property(e => e.Modified)
                    .HasPrecision(6)
                    .HasColumnName("modified");

                entity.Property(e => e.Recommendedfor).HasColumnName("recommendedfor");

                entity.Property(e => e.Shorttitle)
                    .HasColumnName("shorttitle")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.Title).HasColumnName("title");

                entity.HasOne(d => d.Financialentity)
                    .WithMany(p => p.Investmentstrategies)
                    .HasForeignKey(d => d.Financialentityid)
                    .HasConstraintName("fk_investmentstrategy_financialentityid");

                entity.HasOne(d => d.Investmentstrategytype)
                    .WithMany(p => p.Investmentstrategies)
                    .HasForeignKey(d => d.Investmentstrategytypeid)
                    .HasConstraintName("FK_investmentstrategy_investmentstrategytype_investmentstrateg~");
            });

            modelBuilder.Entity<Investmentstrategytype>(entity =>
            {
                entity.ToTable("investmentstrategytype");

                entity.HasIndex(e => e.Financialentityid, "IX_investmentstrategytype_financialentityid");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasPrecision(6)
                    .HasColumnName("created");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Displayorder).HasColumnName("displayorder");

                entity.Property(e => e.Financialentityid).HasColumnName("financialentityid");

                entity.Property(e => e.Iconurl).HasColumnName("iconurl");

                entity.Property(e => e.Isdefault).HasColumnName("isdefault");

                entity.Property(e => e.Isvisible).HasColumnName("isvisible");

                entity.Property(e => e.Modified)
                    .HasPrecision(6)
                    .HasColumnName("modified");

                entity.Property(e => e.Recommendedfor).HasColumnName("recommendedfor");

                entity.Property(e => e.Shorttitle).HasColumnName("shorttitle");

                entity.Property(e => e.Title).HasColumnName("title");

                entity.HasOne(d => d.Financialentity)
                    .WithMany(p => p.Investmentstrategytypes)
                    .HasForeignKey(d => d.Financialentityid);
            });

            modelBuilder.Entity<Portfolio>(entity =>
            {
                entity.ToTable("portfolio");

                entity.HasIndex(e => e.Investmentstrategyid, "IX_portfolio_investmentstrategyid");

                entity.HasIndex(e => e.Risklevelid, "IX_portfolio_risklevelid");

                entity.HasIndex(e => e.Financialentityid, "portfolio_idx2");

                entity.HasIndex(e => e.Uuid, "uuidPortfolio_")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Bpcomission).HasColumnName("bpcomission");

                entity.Property(e => e.Created)
                    .HasPrecision(6)
                    .HasColumnName("created");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Estimatedprofitability).HasColumnName("estimatedprofitability");

                entity.Property(e => e.Extraprofitabilitycurrencycode).HasColumnName("extraprofitabilitycurrencycode");

                entity.Property(e => e.Financialentityid).HasColumnName("financialentityid");

                entity.Property(e => e.Investmentstrategyid).HasColumnName("investmentstrategyid");

                entity.Property(e => e.Isdefault).HasColumnName("isdefault");

                entity.Property(e => e.Maxrangeyear).HasColumnName("maxrangeyear");

                entity.Property(e => e.Minrangeyear).HasColumnName("minrangeyear");

                entity.Property(e => e.Modified)
                    .HasPrecision(6)
                    .HasColumnName("modified");

                entity.Property(e => e.Profitability)
                    .HasColumnType("json")
                    .HasColumnName("profitability");

                entity.Property(e => e.Risklevelid).HasColumnName("risklevelid");

                entity.Property(e => e.Title).HasColumnName("title");

                entity.Property(e => e.Uuid).HasColumnName("uuid");

                entity.Property(e => e.Version).HasColumnName("version");

                entity.HasOne(d => d.Financialentity)
                    .WithMany(p => p.Portfolios)
                    .HasForeignKey(d => d.Financialentityid)
                    .HasConstraintName("fk_portfolio_financialentityid");

                entity.HasOne(d => d.Investmentstrategy)
                    .WithMany(p => p.Portfolios)
                    .HasForeignKey(d => d.Investmentstrategyid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_portfolio_investmentstraegyid");

                entity.HasOne(d => d.Risklevel)
                    .WithMany(p => p.Portfolios)
                    .HasForeignKey(d => d.Risklevelid)
                    .HasConstraintName("fk_portfolio_risklevelid");
            });

            modelBuilder.Entity<Portfoliocomposition>(entity =>
            {
                entity.ToTable("portfoliocomposition");

                entity.HasIndex(e => e.Portfolioid, "idx_fk_portfoliocomposition_portfolioid");

                entity.HasIndex(e => e.Subcategoryid, "idx_fk_portfoliocomposition_subcategoryid");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasPrecision(6)
                    .HasColumnName("created");

                entity.Property(e => e.Modified)
                    .HasPrecision(6)
                    .HasColumnName("modified");

                entity.Property(e => e.Percentage).HasColumnName("percentage");

                entity.Property(e => e.Portfolioid).HasColumnName("portfolioid");

                entity.Property(e => e.Subcategoryid).HasColumnName("subcategoryid");

                entity.HasOne(d => d.Portfolio)
                    .WithMany(p => p.Portfoliocompositions)
                    .HasForeignKey(d => d.Portfolioid)
                    .HasConstraintName("fk_portfoliofunding_portfolioid");

                entity.HasOne(d => d.Subcategory)
                    .WithMany(p => p.Portfoliocompositions)
                    .HasForeignKey(d => d.Subcategoryid)
                    .HasConstraintName("fk_portfoliocomposition_subcategoryid");
            });

            modelBuilder.Entity<Portfoliofunding>(entity =>
            {
                entity.ToTable("portfoliofunding");

                entity.HasIndex(e => e.Fundingid, "idx_fk_portfoliofunding_fundingid");

                entity.HasIndex(e => e.Portfolioid, "portfoliofunding_idx2");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasPrecision(6)
                    .HasColumnName("created");

                entity.Property(e => e.Fundingid).HasColumnName("fundingid");

                entity.Property(e => e.Modified)
                    .HasPrecision(6)
                    .HasColumnName("modified");

                entity.Property(e => e.Percentage).HasColumnName("percentage");

                entity.Property(e => e.Portfolioid).HasColumnName("portfolioid");

                entity.HasOne(d => d.Funding)
                    .WithMany(p => p.Portfoliofundings)
                    .HasForeignKey(d => d.Fundingid)
                    .HasConstraintName("fk_portfoliofunding_fundingid");

                entity.HasOne(d => d.Portfolio)
                    .WithMany(p => p.Portfoliofundings)
                    .HasForeignKey(d => d.Portfolioid)
                    .HasConstraintName("fk_portfoliofunding_portfolioid");
            });

            modelBuilder.Entity<Risklevel>(entity =>
            {
                entity.ToTable("risklevel");

                entity.HasIndex(e => e.Code, "IX_risklevel_code")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.Created)
                    .HasPrecision(6)
                    .HasColumnName("created");

                entity.Property(e => e.Modified)
                    .HasPrecision(6)
                    .HasColumnName("modified");

                entity.Property(e => e.Title).HasColumnName("title");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.Currencyid, "IX_user_currencyid");

                entity.HasIndex(e => e.Advisorid, "idx_fk_user_advisorid");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Advisorid).HasColumnName("advisorid");

                entity.Property(e => e.Created)
                    .HasPrecision(6)
                    .HasColumnName("created");

                entity.Property(e => e.Currencyid).HasColumnName("currencyid");

                entity.Property(e => e.Firstname).HasColumnName("firstname");

                entity.Property(e => e.Modified)
                    .HasPrecision(6)
                    .HasColumnName("modified");

                entity.Property(e => e.Surname).HasColumnName("surname");

                entity.HasOne(d => d.Advisor)
                    .WithMany(p => p.InverseAdvisor)
                    .HasForeignKey(d => d.Advisorid)
                    .HasConstraintName("fk_user_advisorid");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Currencyid)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
