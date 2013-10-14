using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Data.utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using SimpleCrypto;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DataTests
{
    [TestClass]
    public class DataTests
    {
        #region MEMBERSHIP
        [TestMethod]
        public void GetUsers()
        {
            Data.orm.UnitOfWork unitOfWork = new Data.orm.UnitOfWork();
            IEnumerable<User> users = unitOfWork.UserRepository.Get();

            Assert.IsNotNull(users);
        }
        [TestMethod]
        public void InsertPerson()
        {
            Data.orm.UnitOfWork unitOfWork = new Data.orm.UnitOfWork();

            Person person = new Lecturer();
            person.FirstName = "Philippe";
            person.SurName = "De Pauw - Waterschoot";

            unitOfWork.PersonRepository.Insert(person);
            unitOfWork.Save();
        }

        [TestMethod]
        public void SyncStudents()
        {
            string sQuery = @"
                    SELECT *
                    FROM tblCDBStudentImport";

            System.Data.DataSet ds = null;
            SqlDataAdapter da;
            SqlCommand cmd;


            ds = new System.Data.DataSet();
            da = new SqlDataAdapter();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sQuery;
            da.SelectCommand = cmd;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["bamaStudents"].ConnectionString))
            {
                connection.Open();
                cmd.Connection = connection;
                da.Fill(ds);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Data.orm.UnitOfWork unitOfWork = new Data.orm.UnitOfWork();

                    Member user = new Member();
                    user.UserName = (string)row["artv_userid"];
                    user.Email = (string)row["artv_email"];
                    user.PasswordSalt = BCrypt.Net.BCrypt.GenerateSalt(12);
                    user.Password = BCrypt.Net.BCrypt.HashPassword("coala1975", user.PasswordSalt);
                    user.ConfirmPassword = user.Password;

                    Person person = new Lecturer();
                    person.FirstName = (string)row["voornaam"];
                    person.SurName = (string)row["naam"];

                    user.Person = person;
                    user.Roles = unitOfWork.RoleRepository.Get(r => r.Name.Equals("Student")).ToList();

                    unitOfWork.UserRepository.Insert(user);
                    unitOfWork.Save();
                }

            }
        }

        [TestMethod]
        public void InsertMember()
        {
            Data.orm.UnitOfWork unitOfWork = new Data.orm.UnitOfWork();

            Member user = new Member();
            user.UserName = "drdynscript";
            user.Email = "drdynscript@gmail.com";            
            user.PasswordSalt = BCrypt.Net.BCrypt.GenerateSalt(12);
            user.Password = BCrypt.Net.BCrypt.HashPassword("coala1975", user.PasswordSalt);
            user.ConfirmPassword = user.Password;

            Person person = new Lecturer();
            person.FirstName = "Philippe";
            person.SurName = "De Pauw - Waterschoot";

            user.Person = person;
            user.Roles = unitOfWork.RoleRepository.Get().ToList();

            unitOfWork.UserRepository.Insert(user);
            unitOfWork.Save();
        }

        [TestMethod]
        public void InsertOAuthMember()
        {
            Data.orm.UnitOfWork unitOfWork = new Data.orm.UnitOfWork();
            OAuthMember user = new OAuthMember();
            user.UserName = "Administrator";
            user.Email = "philippe.depauw@arteveldehs.be";
            user.Provider = "Twitter";
            user.ProviderUserId = "hdhdikd6d55dkdujnd";

            Person person = new Lecturer();
            person.FirstName = "Philippe";
            person.SurName = "De Pauw - Waterschoot";

            user.Person = person;

            unitOfWork.UserRepository.Insert(user);
            unitOfWork.Save();
        }
        #endregion

        #region message
        [TestMethod]
        public void GetMessages()
        {
            Data.orm.UnitOfWork unitOfWork = new Data.orm.UnitOfWork();

            IEnumerable list = unitOfWork.MessageRepository.Get();
            Assert.IsNotNull(list);
        }
        [TestMethod]
        public void GetMessagesFromSender()
        {
            Data.orm.UnitOfWork unitOfWork = new Data.orm.UnitOfWork();

            IEnumerable list = unitOfWork.MessageRepository.Get(
                message => message.SenderId == 7/*, orderBy: q => q.OrderBy(s => s.CreatedDate)*/
                );
            Assert.IsNotNull(list);
        }
        [TestMethod]
        public void GetMessagesFromReceiver()
        {
            Data.orm.UnitOfWork unitOfWork = new Data.orm.UnitOfWork();

            IEnumerable list = unitOfWork.MessageRepository.Get(
                message => message.ReceiverId == 8/*, orderBy: q => q.OrderBy(s => s.CreatedDate)*/
                );
            Assert.IsNotNull(list);
        }
        [TestMethod]
        public void InsertMessage()
        {
            Data.orm.UnitOfWork unitOfWork = new Data.orm.UnitOfWork();
            User sender = unitOfWork.UserRepository.GetByID(7);
            User receiver = unitOfWork.UserRepository.GetByID(8);
            Message message = new Message();
            message.Sender = sender;
            message.Receiver = receiver;
            message.Body = "Etiam laoreet urna dizzle yo. Fo shizzle quizzle arcu. Maecenas pulvinar, hizzle phat malesuada gizzle, pizzle purus euismod izzle, gizzle luctus metizzle crackalackin izzle fo shizzle. Vivamizzle ullamcorpizzle, tortizzle izzle varizzle da bomb, nibh nunc ultricizzle gizzle, nizzle luctus leo fo shizzle izzle dolizzle. Maurizzle fo shizzle my nizzle, crazy vizzle volutpat consectetuer, sizzle owned shiz own yo', izzle crunk enizzle gangsta own yo' nisl. Nullam gangsta velizzle ac gangsta eleifend viverra. Phasellizzle bow wow wow crackalackin. Curabitur nizzle things mofo pede sodales facilisizzle. Maecenas bow wow wow gangster, iaculizzle go to hizzle, pot sizzle, egestas shit, erizzle. We gonna chung tellivizzle turpis shizzle my nizzle crocodizzle nibh bibendum bizzle. Nizzle funky fresh consectetizzle brizzle. Aliquizzle for sure volutpizzle. Nunc yo leo izzle lectus pretium sizzle. Crizzle lacizzle izzle dui condimentizzle shit. Ut nisl. own yo' urna. Integer bow wow wow ipsizzle dizzle mi. Pimpin' rizzle turpis.";

            unitOfWork.MessageRepository.Insert(message);
            unitOfWork.Save();
        }
        #endregion

        [TestMethod]
        public void GetArticles()
        {
            Data.orm.UnitOfWork unitOfWork = new Data.orm.UnitOfWork();
            IEnumerable<Article> articles = unitOfWork.ArticleRepository.Get();
            Assert.IsNotNull(articles);
        }        

        [TestMethod]
        public void InsertArticle()
        {
            Data.orm.UnitOfWork unitOfWork = new Data.orm.UnitOfWork();
            Article article = new Article();
            article.Title = "Acers Iconia Tab W3: eerste 8\"-Windows 8-tablet";
            article.Description = "Alle Windows 8-tablets die tot nu toe zijn uitgekomen hebben beeldschermen van 10,1\" of groter. Dat was toen Windows 8 nog in ontwikkeling was ook de meest gangbare maat voor tablets. Een paar maanden voor de release van Windows 8 begonnen kleinere tablets echter aan populariteit te winnen en fabrikanten konden daar niet meteen op inspringen.";
            article.Body = "<p>Alle Windows 8-tablets die tot nu toe zijn uitgekomen hebben beeldschermen van 10,1\" of groter. Dat was toen Windows 8 nog in ontwikkeling was ook de meest gangbare maat voor tablets. Een paar maanden voor de release van Windows 8 begonnen kleinere tablets echter aan populariteit te winnen en fabrikanten konden daar niet meteen op inspringen.</p><p>Inmiddels heeft Microsoft zelf naar verluidt al kleinere tablets in ontwikkeling, maar het is Acer dat als eerste met een kleine Windows 8-tablet op de markt komt: de Iconia Tab W3. De 8,1\"-tablet werd onthuld tijdens de persconferentie van de Taiwanese fabrikant, voorafgaand aan de Computex-beurs. Op de beursvloer konden wij ermee aan de slag.</p>";
            article.User = unitOfWork.UserRepository.GetByID(7);
            unitOfWork.ArticleRepository.Insert(article);
            unitOfWork.Save();
        }

        [TestMethod]
        public void InsertArticles()
        {
            Data.orm.UnitOfWork unitOfWork = new Data.orm.UnitOfWork();

            Article article = new Article();
            article.Title = "'Aantal verkochte Android-tablets bijna verdubbeld in jaar tijd'";
            article.Description = "Uit marktcijfers blijkt dat de verkopen van Android-tablets het afgelopen jaar bijna verdubbeld zijn. Het aantal verkochte iPads nam tegelijkertijd af. Android-tablets zijn nu goed voor een martkaandeel van tweederde. Ook het aandeel van Windows-tablets groeit.";
            article.Body = "<p>Dat <a title=\"www.strategyanalytics.com -- Android Dominates the Tablet Market in 2013 Q2\" href=\"http://www.strategyanalytics.com/default.aspx?mod=pressreleaseviewer&amp;a0=5403\" rel=\"external\" target=\"_blank\">becijfert</a> het onderzoeksbureau Strategy Analytics. Ten opzichte van het tweede kwartaal in 2012 steeg het marktaandeel van Android-tablets met dertig procent, bijna volledig ten koste van Apples iPads. De grote verschuiving wordt veroorzaakt door de explosieve stijging van het aantal verkochte Android-tablets. Waar in het tweede kwartaal van 2012 nog 18,5 miljoen Android-tablets verkocht werden, kwam het totaal in het afgelopen kwartaal uit op 34,6 miljoen.</p><p>Strategy Analytics pakt daarbij alle verschillende varianten mee: dus ook Amazons Kindle, die op een aangepaste versie van Android zonder Google-diensten draait. De marktvorser telt overigens ook de zogenoemde whitelabel-tablets - goedkope, naamloze Android-tablets die een bedrijf inkoopt en vervolgens onder eigen merk verkoopt - mee. Deze nemen een flink aandeel van de verkopen voor hun rekening. Het afgelopen kwartaal ging het bij vier op de tien verkochte Android-tables om zo'n whitelabel-model.</p><p>Hoewel het aandeel van Apples iPads slonk, valt dat met de absolute verkopen wel mee. Apple verkocht in het tweede kwartaal van dit jaar 14,6 miljoen iPads, tegenover 17 miljoen een jaar eerder. De analisten merken op dat Apple dit jaar rondom het tweede kwartaal geen nieuw model had, terwijl in 2012 op dat moment de iPad Retina net uit was. Aan de andere kant heeft Apple met de iPad mini een extra model in de line-up, maar deze weet de verkoopdaling niet op te vangen.</p><p>De invloed van Windows 8 op tabletverkopen is ook goed te zien: in het tweede kwartaal van 2012, toen Windows 8 nog niet uit was, hadden Windows-tablets een marktaandeel van een half procent. Nu, een jaar later, ligt het aandeel op 4,5 procent.</p><p><a title=\"Marktaandeel Android en iOS\" href=\"http://ic.tweakimg.net/ext/i/1375166037.jpeg\"><img style=\"margin-left: auto; margin-right: auto; display: block;\" title=\"Marktaandeel Android en iOS\" src=\"http://ic.tweakimg.net/ext/i/imagenormal/1375166037.jpeg\" alt=\"Marktaandeel Android en iOS\" width=\"610\" height=\"264\"></a>Het marktaandeel van Android en iOS in Q2 2012 en Q2 2013. Klik voor vergroting</p>";
            article.User = unitOfWork.UserRepository.GetByID(7);
            unitOfWork.ArticleRepository.Insert(article);
            unitOfWork.Save();

            article = new Article();
            article.Title = "Vierde bèta iOS 7 hint naar vingerafdrukscanner in nieuwe iPhone";
            article.Description = "De deze week uitgebrachte vierde bèta van iOS 7 bevat verwijzingen naar een vingerafdrukscanner die ingebouwd zou zitten in de homeknop. Mogelijk krijgt een aankomende iPhone, waar al maanden geruchten over gaan, zo'n scanner ingebouwd.";
            article.Body = "<p>Dat Blizzard een consolegame wil maken is natuurlijk verrassend voor een studio die volledig gericht lijkt op Windows en OS X. Toch is het uiteraard niet de eerste poging om een consolegame te maken. Tot 2006 werkte de studio aan StarCraft Ghost, al werd de ontwikkeling daarvan in eerste instantie nog uitbesteed aan Nihilistic Software. Daar heeft Blizzard van geleerd; als er een consolegame moet worden gemaakt, doet de studio dat voortaan zelf. Vandaar dat het in 2011 een team heeft samengesteld om Diablo III naar consoles te vertalen. In eerste instantie leek de studio alleen de PlayStation 3 op het oog te hebben, maar de consoleversie verschijnt niet alleen voor de console van Sony. Op 3 september komt de game zowel voor de PlayStation 3 als de Xbox 360 uit. Later zal er ook nog een versie voor de PlayStation 4 verschijnen. Of er ook een versie komt voor de Xbox One is nog niet bekend.</p><h2>Diablo, Deckard en Leah</h2><p>Wat inmiddels wel bekend is van de consoleversie, is het verhaal. Dat zal namelijk precies hetzelfde zijn als in de Windows-versie. Je bent nog steeds de onbekende held die Deckard Cain en zijn nichtje Leah komt helpen in hun strijd tegen de heer van het kwaad die luistert naar de naam Diablo. Niet alleen het verhaal is hetzelfde, ook de omgeving is identiek. Daar heeft Blizzard dus niets aan veranderd. Je speelt dezelfde levels en komt dezelfde dungeons tegen. Je kunt bovendien kiezen uit dezelfde classes, hetzelfde upgrade-pad doorlopen en hetzelfde maximale level halen met je held. Je krijgt bovendien dezelfde moeilijkheidsgradaties en precies dezelfde skills.</p>";
            article.User = unitOfWork.UserRepository.GetByID(7);
            unitOfWork.ArticleRepository.Insert(article);
            unitOfWork.Save();
        }

        [TestMethod]
        public void InsertRole()
        {
            Data.orm.UnitOfWork unitOfWork = new Data.orm.UnitOfWork();
            Role role = new Role();
            role.Name = "Administrator";
            role.Description = "Die kunnen alles";
            unitOfWork.RoleRepository.Insert(role);
            unitOfWork.Save();
        }

        [TestMethod]
        public void AddUserToRole()
        {
            Data.orm.UnitOfWork unitOfWork = new Data.orm.UnitOfWork();
            User user = unitOfWork.UserRepository.GetByID(1);
            Role role = unitOfWork.RoleRepository.GetByID(1);
            user.Roles.Add(role);
            unitOfWork.Save();
        }

        [TestMethod]
        public void InsertCategory()
        {
            Data.orm.UnitOfWork unitOfWork = new Data.orm.UnitOfWork();

            Category category = new Category();
            category.Name = "School";
            category.Description = "Etiam laoreet urna dizzle yo. Fo shizzle quizzle arcu. Maecenas pulvinar, hizzle phat malesuada gizzle, pizzle purus euismod izzle, gizzle luctus metizzle crackalackin izzle fo shizzle. Vivamizzle ullamcorpizzle, tortizzle izzle varizzle da bomb, nibh nunc ultricizzle gizzle, nizzle luctus leo fo shizzle izzle dolizzle. Maurizzle fo shizzle my nizzle, crazy vizzle volutpat consectetuer, sizzle owned shiz own yo', izzle crunk enizzle gangsta own yo' nisl. Nullam gangsta velizzle ac gangsta eleifend viverra. Phasellizzle bow wow wow crackalackin. Curabitur nizzle things mofo pede sodales facilisizzle. Maecenas bow wow wow gangster, iaculizzle go to hizzle, pot sizzle, egestas shit, erizzle. We gonna chung tellivizzle turpis shizzle my nizzle crocodizzle nibh bibendum bizzle. Nizzle funky fresh consectetizzle brizzle. Aliquizzle for sure volutpizzle. Nunc yo leo izzle lectus pretium sizzle. Crizzle lacizzle izzle dui condimentizzle shit. Ut nisl. own yo' urna. Integer bow wow wow ipsizzle dizzle mi. Pimpin' rizzle turpis.";

            unitOfWork.CategoryRepository.Insert(category);
            unitOfWork.Save();
        }

        [TestMethod]
        public void InsertCategories()
        {
            Data.orm.UnitOfWork unitOfWork = new Data.orm.UnitOfWork();

            Category category1 = new Category();
            category1.Name = "Academiejaar";
            category1.Description = "Academiejaar";
            unitOfWork.CategoryRepository.Insert(category1);

            Category category2 = new Category();
            category2.Name = "2012-13";
            category2.Description = "2012-13";
            category2.ParentCategory = category1;
            unitOfWork.CategoryRepository.Insert(category2);

            category2 = new Category();
            category2.Name = "2013-14";
            category2.Description = "2013-14";
            category2.ParentCategory = category1;
            unitOfWork.CategoryRepository.Insert(category2);

            Category category3 = new Category();
            category3.Name = "Afstudeerrichting";
            category3.Description = "Afstudeerrichting";
            unitOfWork.CategoryRepository.Insert(category3);

            Category category4 = new Category();
            category4.Name = "Multimediaproductie (MMP)";
            category4.Description = "Multimediaproductie (MMP)";
            category4.ParentCategory = category3;
            unitOfWork.CategoryRepository.Insert(category4);

            unitOfWork.Save();
        }

        [TestMethod]
        public void SubscribeNewsletterUser()
        {
            Data.orm.UnitOfWork unitOfWork = new Data.orm.UnitOfWork();

            var alreadySubscribed = unitOfWork.NewsletterSubscriptionRepository.Get(n => n.UserId == 1);
        }
    }
}
