using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.orm.repositories;
using Models;

namespace Data.orm
{
    public class UnitOfWork:IDisposable
    {
        private GDMContext _context = new GDMContext();
        private GDMRepository<User> _userRepository;
        private GDMRepository<OAuthMember> _oAuthRepository;
        private GDMRepository<Member> _memberRepository;
        private GDMRepository<Person> _personRepository;
        private GDMRepository<Role> _roleRepository;
        private GDMRepository<Article> _articleRepository;
        private GDMRepository<Message> _messageRepository;
        private GDMRepository<EntityVisit> _visitRepository;
        private GDMRepository<Category> _categoryRepository;
        private GDMRepository<InfoContact> _infoContactRepository;
        private GDMRepository<NewsletterSubscription> _newsletterSubscriptionRepository;
        private GDMRepository<RouteVisit> _routeRepository;
        private GDMRepository<Entity> _entityRepository;
        private GDMRepository<SchoolProject> _schoolProjectRepository;
        private GDMRepository<LevelOfDifficulty> _levelOfDifficultyRepository;
        private GDMRepository<SchoolProjectEntry> _schoolProjectEntryRepository;
        private GDMRepository<Medium> _mediumRepository;

        public GDMRepository<User> UserRepository
        {
            get
            {
                if (this._userRepository == null)
                    this._userRepository = new GDMRepository<User>(_context);
                return _userRepository;
            }
        }

        public GDMRepository<Member> MemberRepository
        {
            get
            {
                if (this._memberRepository == null)
                    this._memberRepository = new GDMRepository<Member>(_context);
                return _memberRepository;
            }
        }

        public GDMRepository<OAuthMember> OAuthRepository
        {
            get
            {
                if (this._oAuthRepository == null)
                    this._oAuthRepository = new GDMRepository<OAuthMember>(_context);
                return _oAuthRepository;
            }
        }

        public GDMRepository<Person> PersonRepository
        {
            get
            {
                if (this._personRepository == null)
                    this._personRepository = new GDMRepository<Person>(_context);
                return _personRepository;
            }
        }

        public GDMRepository<Role> RoleRepository
        {
            get
            {
                if (this._roleRepository == null)
                    this._roleRepository = new GDMRepository<Role>(_context);
                return _roleRepository;
            }
        }

        public GDMRepository<Article> ArticleRepository
        {
            get
            {
                if (this._articleRepository == null)
                    this._articleRepository = new GDMRepository<Article>(_context);
                return _articleRepository;
            }
        }

        public GDMRepository<Message> MessageRepository
        {
            get
            {
                if (this._messageRepository == null)
                    this._messageRepository = new GDMRepository<Message>(_context);
                return _messageRepository;
            }
        }

        public GDMRepository<EntityVisit> VisitRepository
        {
            get
            {
                if (this._visitRepository == null)
                    this._visitRepository = new GDMRepository<EntityVisit>(_context);
                return _visitRepository;
            }
        }

        public GDMRepository<Category> CategoryRepository
        {
            get
            {
                if (this._categoryRepository == null)
                    this._categoryRepository = new GDMRepository<Category>(_context);
                return _categoryRepository;
            }
        }

        public GDMRepository<InfoContact> InfoContactRepository
        {
            get
            {
                if (this._infoContactRepository == null)
                    this._infoContactRepository = new GDMRepository<InfoContact>(_context);
                return _infoContactRepository;
            }
        }

        public GDMRepository<NewsletterSubscription> NewsletterSubscriptionRepository
        {
            get
            {
                if (this._newsletterSubscriptionRepository == null)
                    this._newsletterSubscriptionRepository = new GDMRepository<NewsletterSubscription>(_context);
                return _newsletterSubscriptionRepository;
            }
        }

        public GDMRepository<RouteVisit> RouteRepository
        {
            get
            {
                if (this._routeRepository == null)
                    this._routeRepository = new GDMRepository<RouteVisit>(_context);
                return _routeRepository;
            }
        }

        public GDMRepository<Entity> EntityRepository
        {
            get
            {
                if (this._entityRepository == null)
                    this._entityRepository = new GDMRepository<Entity>(_context);
                return _entityRepository;
            }
        }

        public GDMRepository<SchoolProject> SchoolProjectRepository
        {
            get
            {
                if (this._schoolProjectRepository == null)
                    this._schoolProjectRepository = new GDMRepository<SchoolProject>(_context);
                return _schoolProjectRepository;
            }
        }

        public GDMRepository<LevelOfDifficulty> LevelOfDifficultyRepository
        {
            get
            {
                if (this._levelOfDifficultyRepository == null)
                    this._levelOfDifficultyRepository = new GDMRepository<LevelOfDifficulty>(_context);
                return _levelOfDifficultyRepository;
            }
        }

        public GDMRepository<SchoolProjectEntry> SchoolProjectEntryRepository
        {
            get
            {
                if (this._schoolProjectEntryRepository == null)
                    this._schoolProjectEntryRepository = new GDMRepository<SchoolProjectEntry>(_context);
                return _schoolProjectEntryRepository;
            }
        }

        public GDMRepository<Medium> MediumRepository
        {
            get
            {
                if (this._mediumRepository == null)
                    this._mediumRepository = new GDMRepository<Medium>(_context);
                return _mediumRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
