﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Data.orm.mappings
{
    internal class ArticleMapping : EntityTypeConfiguration<Article>
    {
        public ArticleMapping():base()
        {
            this.ToTable("articles", "gdm");

            this.Property(t => t.Body).HasColumnName("article_body").IsRequired();
        }        
    }
}
