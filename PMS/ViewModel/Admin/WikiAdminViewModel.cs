using PMS.DAL;
using PMS.Model;
using PMS.ViewModel;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PMS
{
    public class WikiAdminViewModel : BindableBase
    {

        private readonly PMSContext dbContext = new PMSContext();

        //Lista
        private ObservableCollection<Article> _Articles { get; set; }
        public ICommand RemoveArticleButton { get; set; }


        //Dodanie
        private string _Url;
        private string _Description;
        public ICommand AddArticleButton { get; set; }

        //Edycja
        //kolekcaj artykółów z listy
        private Article _mySelectedArticle;
        private bool _IsEnabledEditButton = false;
        private string _UrlEdit;
        private string _DescriptionEdit;
        public ICommand EditArticleButton { get; set; }


        public WikiAdminViewModel()
        {
            RemoveArticleButton = new DelegateCommand<object>(RemoveArticle);
            AddArticleButton = new DelegateCommand(AddArticle);
            EditArticleButton = new DelegateCommand(EditArticle);
        }

        public ObservableCollection<Article> Articles
        {
            get
            {
                if (_Articles == null)
                {
                    _Articles = new ObservableCollection<Article>(dbContext.Article);
                }
                return _Articles;
            }
            set
            {
                _Articles = value;
                RaisePropertyChanged("Articles");
            }
        }

        public string Url
        {
            get
            {
                return _Url;
            }
            set
            {
                _Url = value;
                RaisePropertyChanged("Url");
            }
        }
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
                RaisePropertyChanged("Description");
            }
        }

        public string UrlEdit
        {
            get
            {
                return _UrlEdit;
            }
            set
            {
                _UrlEdit = value;
                RaisePropertyChanged("UrlEdit");
            }
        }
        public string DescriptionEdit
        {
            get
            {
                return _DescriptionEdit;
            }
            set
            {
                _DescriptionEdit = value;
                RaisePropertyChanged("DescriptionEdit");
            }
        }
        public bool IsEnabledEditButton
        {
            get
            {
                return _IsEnabledEditButton;
            }
            set
            {
                _IsEnabledEditButton = value;
                RaisePropertyChanged("IsEnabledEditButton");
            }
        }
        public Article MySelectedArticle
        {
            get { return _mySelectedArticle; }
            set
            {
                _mySelectedArticle = value;
                
                if (_mySelectedArticle != null)
                {
                    IsEnabledEditButton = true;
                    UrlEdit = _mySelectedArticle.Url;
                    DescriptionEdit = _mySelectedArticle.Description;
                } else
                {
                    IsEnabledEditButton = false;
                    UrlEdit = "";
                    DescriptionEdit = "";
                }
                

                RaisePropertyChanged("MySelectedArticle"); //to wczesniej?
            }
        }

        private void AddArticle()
        {

            Article article = new Article()
            {
                Url = _Url,
                Description = _Description,
                DateAdded = DateTime.Now,
                UserID = Global.user.UserID
            };

            dbContext.Article.Add(article);
            dbContext.SaveChanges();

            Articles = new ObservableCollection<Article>(dbContext.Article);

            ErrorMessage er = new ErrorMessage("Add article!");
            er.ShowDialog();

        }


        private void EditArticle()
        {

            MySelectedArticle.Url = UrlEdit;
            MySelectedArticle.Description = DescriptionEdit;

            dbContext.SaveChanges();

            Articles = new ObservableCollection<Article>(dbContext.Article);

            ErrorMessage er = new ErrorMessage("Edit article!");
            er.ShowDialog();

        }

        private void RemoveArticle(object ID)
        {
            int val = Convert.ToInt32(ID);
            Article article = dbContext.Article.Find(val);
            dbContext.Article.Remove(article);
            dbContext.SaveChanges();

            _Articles.Remove(article);

        }
    }
}
