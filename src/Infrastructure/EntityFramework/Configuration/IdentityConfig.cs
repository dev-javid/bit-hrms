namespace Infrastructure.EntityFramework.Configuration
{
    internal class IdentityConfig
    {
        internal class UserConfiguration : IEntityTypeConfiguration<User>
        {
            public void Configure(EntityTypeBuilder<User> builder)
            {
                builder.ToTable("users");

                builder
                    .Property(x => x.Id)
                    .HasColumnName("id");

                builder
                    .Property(x => x.AccessFailedCount)
                    .HasColumnName("access_failed_count");

                builder
                    .Property(x => x.NormalizedEmail)
                    .HasColumnName("normalized_email");

                builder
                    .Property(x => x.UserName)
                    .HasColumnName("user_name");

                builder
                    .Property(x => x.NormalizedUserName)
                    .HasColumnName("normalized_user_name");

                builder
                    .Property(x => x.Email)
                    .HasColumnName("email");

                builder
                    .Property(x => x.EmailConfirmed)
                    .HasColumnName("email_confirmed");

                builder
                    .Property(x => x.PhoneNumber)
                    .HasColumnName("phone_number");

                builder
                    .Property(x => x.PasswordHash)
                    .HasColumnName("password_hash");

                builder
                    .Property(x => x.SecurityStamp)
                    .HasColumnName("security_stamp");
                builder
                    .Property(x => x.ConcurrencyStamp)
                    .HasColumnName("concurrency_stamp");

                builder
                    .Property(x => x.PhoneNumberConfirmed)
                    .HasColumnName("phone_number_confirmed");
                builder
                    .Property(x => x.TwoFactorEnabled)
                    .HasColumnName("two_factor_enabled");

                builder
                    .Property(x => x.LockoutEnabled)
                    .HasColumnName("lockout_enabled");

                builder
                    .Property(x => x.LockoutEnd)
                    .HasColumnName("lockout_end");

                builder.HasMany<IdentityUserRole<int>>()
                     .WithOne()
                     .HasForeignKey(uc => uc.UserId)
                     .IsRequired();
            }
        }

        internal class IdentityRoleClaimConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<int>>
        {
            public void Configure(EntityTypeBuilder<IdentityRoleClaim<int>> builder)
            {
                builder.ToTable("role_claims");

                builder
                    .Property(x => x.Id)
                    .HasColumnName("id");

                builder
                    .Property(x => x.RoleId)
                    .HasColumnName("role_id");

                builder
                    .Property(x => x.ClaimType)
                    .HasColumnName("claim_type");

                builder
                    .Property(x => x.ClaimValue)
                    .HasColumnName("claim_value");
            }
        }

        internal class IdentityUserClaimConfiguration : IEntityTypeConfiguration<IdentityUserClaim<int>>
        {
            public void Configure(EntityTypeBuilder<IdentityUserClaim<int>> builder)
            {
                builder.ToTable("user_claims");

                builder
                    .Property(x => x.Id)
                    .HasColumnName("id");

                builder
                    .Property(x => x.UserId)
                    .HasColumnName("user_id");

                builder
                    .Property(x => x.ClaimType)
                    .HasColumnName("claim_type");

                builder
                    .Property(x => x.ClaimValue)
                    .HasColumnName("claim_value");
            }
        }

        internal class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole<int>>
        {
            public void Configure(EntityTypeBuilder<IdentityRole<int>> builder)
            {
                builder.ToTable("roles");

                builder
                    .Property(x => x.Id)
                    .HasColumnName("id");

                builder
                    .Property(x => x.Name)
                    .HasColumnName("name");

                builder
                    .Property(x => x.NormalizedName)
                    .HasColumnName("normalized_name");

                builder
                    .Property(x => x.ConcurrencyStamp)
                    .HasColumnName("concurrency_stamp");

                builder.HasMany<IdentityUserRole<int>>()
                     .WithOne()
                     .HasForeignKey(uc => uc.UserId)
                     .IsRequired();
            }
        }

        internal class IdentityUserLoginConfiguration : IEntityTypeConfiguration<IdentityUserLogin<int>>
        {
            public void Configure(EntityTypeBuilder<IdentityUserLogin<int>> builder)
            {
                builder.HasNoKey();

                builder.ToTable("user_logins");

                builder
                    .Property(x => x.UserId)
                    .HasColumnName("user_id");

                builder
                    .Property(x => x.LoginProvider)
                    .HasColumnName("login_provider");

                builder
                    .Property(x => x.ProviderKey)
                    .HasColumnName("provider_key");

                builder
                    .Property(x => x.ProviderDisplayName)
                    .HasColumnName("provider_display_name");
            }
        }

        internal class IdentityUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<int>>
        {
            public void Configure(EntityTypeBuilder<IdentityUserRole<int>> builder)
            {
                builder.HasKey(x => new
                {
                    x.UserId,
                    x.RoleId
                });

                builder.ToTable("user_roles");

                builder
                    .Property(x => x.RoleId)
                    .HasColumnName("role_id");

                builder
                    .Property(x => x.UserId)
                    .HasColumnName("user_id");
            }
        }

        internal class IdentityUserTokenConfiguration : IEntityTypeConfiguration<IdentityUserToken<int>>
        {
            public void Configure(EntityTypeBuilder<IdentityUserToken<int>> builder)
            {
                builder.HasKey(x => new
                {
                    x.UserId,
                    x.LoginProvider,
                    x.Name
                });

                builder.ToTable("user_tokens");

                builder
                    .Property(x => x.UserId)
                    .HasColumnName("user_id");

                builder
                    .Property(x => x.LoginProvider)
                    .HasColumnName("login_provider");

                builder
                    .Property(x => x.Name)
                    .HasColumnName("name");

                builder
                    .Property(x => x.Value)
                    .HasColumnName("value");
            }
        }

        internal class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
        {
            public void Configure(EntityTypeBuilder<RefreshToken> builder)
            {
                builder.HasKey(x => x.Id);

                builder.ToTable("refresh_tokens");

                builder
                    .Property(x => x.Id)
                    .HasColumnName("id");

                builder
                    .Property(x => x.UserId)
                    .HasColumnName("user_id");

                builder
                    .Property(x => x.Value)
                    .HasColumnName("value");

                builder
                    .Property(x => x.DeviceIdentifier)
                    .HasColumnName("device_identifier");

                builder
                    .Property(x => x.CreatedOn)
                    .HasColumnName("created_on");

                builder
                    .Property(x => x.ModifiedOn)
                    .HasColumnName("modified_on");
            }
        }
    }
}
